using System.Net;
using Microsoft.AspNetCore.Http;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApiErrors
{
    public class UnauthorizedError : ApiError
    {
        public UnauthorizedError()
            : base(StatusCodes.Status401Unauthorized, HttpStatusCode.Unauthorized.ToString())
        {
        }
        
        public UnauthorizedError(string message)
            : base(StatusCodes.Status401Unauthorized, HttpStatusCode.Unauthorized.ToString(), message)
        {
        }
    }
}
