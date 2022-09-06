using System;
using System.Threading;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerProfile.Interfaces;
using Zip.Api.CustomerProfile.Middleware;

namespace Zip.Api.CustomerProfile.Extensions
{
    // Extension methods used to add the middleware to the HTTP request pipeline.
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseWarmerService(this IApplicationBuilder builder)
        {
            new Thread(() =>
            {
                using (var scope = builder.ApplicationServices.CreateScope())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<IServiceWarmupState>>();
                    logger.LogInformation("Starting Warmer Service");

                    var state = scope.ServiceProvider.GetRequiredService<IServiceWarmupState>();
                    state.StartTask();

                    try
                    {
                        var warmupService = scope.ServiceProvider.GetRequiredService<IWarmingService>();
                        warmupService.WarmUp();
                        state.MarkTaskAsComplete();
                        logger.LogInformation("Warmer Service Complete");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Failed to warm up service");
                    }
                }
            }).Start();
            return builder;
        }

        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }

        public static IApplicationBuilder UseGraphQL(this IApplicationBuilder builder)
        {
            // add http for Schema at default url /graphql
            builder.UseGraphQL<ISchema>();

            // use graphql-playground at default url /ui/playground
            builder.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });

            return builder;
        }
    }
}