using System.Net;
using Microsoft.AspNetCore.Http;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApiErrors
{
    public class InternalServerError : ApiError
    {
        public InternalServerError()
            : base(StatusCodes.Status500InternalServerError, HttpStatusCode.InternalServerError.ToString())
        {
        }

        public InternalServerError(string message)
            : base(StatusCodes.Status500InternalServerError, HttpStatusCode.InternalServerError.ToString(), message)
        {
        }
    }
}
