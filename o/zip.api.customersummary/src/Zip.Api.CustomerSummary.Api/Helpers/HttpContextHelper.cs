using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Zip.Core.Extensions;

namespace Zip.Api.CustomerSummary.Api.Helpers
{
    public static class HttpContextHelper
    {
        public static string GetUserEmail(this HttpContext context)
        {
            var email = context?.User?.Claims != null ? context.User.FindFirst(ClaimTypes.Email).Value : string.Empty;
            if (string.IsNullOrEmpty(email) && context != null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    State = "Unauthorized",
                    Message = "User data does not contains email address."
                };
                context.Response.WriteAsync(response.ToJsonString());
            }
            return email;
        }
    }
}