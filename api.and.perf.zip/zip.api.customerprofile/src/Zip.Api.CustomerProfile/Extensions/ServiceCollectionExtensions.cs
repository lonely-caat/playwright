using GraphQL;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Zip.Api.CustomerProfile.GraphQL;
using Zip.Api.CustomerProfile.GraphQL.Types;

namespace Zip.Api.CustomerProfile.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddExternalGraphQl(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<CustomerType>();
            services.AddScoped<CustomerQuery>();
            services.AddScoped<ISchema, CustomerSchema>();
            services.AddGraphQL(x =>
                {
                    x.EnableMetrics = bool.Parse(configuration["GraphQlSettings:EnableMetrics"]);
                    x.ExposeExceptions =
                        bool.Parse(configuration["GraphQlSettings:ExposeExceptions"]); // set true only in dev mode.
                }).AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(httpContext => httpContext.User)
                .AddDataLoader();
        }

        public static IServiceCollection AddSerilog(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .CreateLogger();

            return services.AddSingleton(Log.Logger);
        }
    }
}