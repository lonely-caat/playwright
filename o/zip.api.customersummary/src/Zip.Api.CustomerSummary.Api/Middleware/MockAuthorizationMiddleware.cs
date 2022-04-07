using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Api.Middleware
{
    public class MockAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public MockAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var identity = await Task.Run(
                () => {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Email, "abc@zip.co") };
                    claims.AddRange(Enum.GetNames(typeof(AppRole)).Select(x => new Claim(ClaimTypes.Role, x)));

                    return new ClaimsIdentity(claims);
                });

            context.User.AddIdentity(identity);

            await _next(context);
        }
    }
}
