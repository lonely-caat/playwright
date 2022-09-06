using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Zip.Api.CustomerSummary.Api.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Api.Test.Middleware
{
    public class AuthenticatedTestRequestMiddleware
    {
        public const string TestingCookieAuthentication = "TestCookieAuthentication";
        public const string TestingHeader = "X-Integration-Testing";
        public const string TestingHeaderValue = "zip-abcde-12345";

        private readonly RequestDelegate _next;

        public AuthenticatedTestRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (GetHeaderValue(context, TestingHeader).Equals(TestingHeaderValue, StringComparison.OrdinalIgnoreCase) &&
                context.Request.Headers.Keys.Contains(CustomClaimTypes.Email))
            {
                var claims = new List<Claim>();

                var email = GetHeaderValue(context, CustomClaimTypes.Email);
                claims.Add(new Claim(ClaimTypes.Email, email));

                claims.AddRange(Enum.GetNames(typeof(AppRole)).Select(x => new Claim(ClaimTypes.Role, x)));
                
                var claimsIdentity = new ClaimsIdentity(claims, TestingCookieAuthentication);
                context.User = new ClaimsPrincipal(claimsIdentity);
            }

            await _next(context);
        }

        private static string GetHeaderValue(HttpContext context, string key)
        {
            return context.Request.Headers.ContainsKey(key) ? context.Request.Headers[key].First() : "";
        }
    }
}