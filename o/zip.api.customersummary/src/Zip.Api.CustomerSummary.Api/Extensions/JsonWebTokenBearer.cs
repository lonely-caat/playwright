using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService.Interfaces;
using Zip.Core.Extensions;

namespace Zip.Api.CustomerSummary.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class JsonWebTokenBearer
    {
        public static IServiceCollection AddJsonWebTokenBearer(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
                {
                    options.Audience = configuration["OneLogin:ClientId"];
                    options.Authority = configuration["OneLogin:AuthorityEndpoint"];
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        RequireSignedTokens = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["OneLogin:ClientSecret"])),
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["OneLogin:ClientId"]
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var email = context.Principal.FindFirstValue(ClaimTypes.Email);

                            if (string.IsNullOrEmpty(email))
                            {
                                await UnauthorizedRequestStatus(context, "Token does not contains user email address.");
                                return;
                            }
                            
                            var identityService = context.HttpContext.RequestServices.GetRequiredService<IIdentityService>();
                            var userDetail = await identityService.GetUserByEmailAsync(email);

                            if (userDetail == null || !userDetail.IsActive || !userDetail.Roles.Any())
                            {
                                await UnauthorizedRequestStatus(context, $"User [{email}] is not active in User Management.");
                                return;
                            }

                            var claimsList = new List<Claim> { new Claim(ClaimTypes.Email, email) };
                            var rolesClaims = userDetail.Roles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
                            claimsList.AddRange(rolesClaims);
                            var identity = new ClaimsIdentity(claimsList.AsEnumerable());

                            context.Principal.AddIdentity(identity);
                        }
                    };
                });

            return services;
        }

        private static async Task UnauthorizedRequestStatus(TokenValidatedContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var response = new
            {
                State = "Unauthorized",
                Message = message
            };
            await context.Response.WriteAsync(response.ToJsonString());
        }
    }
}
