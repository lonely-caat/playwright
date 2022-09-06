using System.Net;
using Microsoft.AspNetCore.Http;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApiErrors
{
    public class BadRequestError : ApiError
    {
        public BadRequestError()
            : base(StatusCodes.Status400BadRequest, HttpStatusCode.BadRequest.ToString())
        {
        }

        public BadRequestError(string message)
            : base(StatusCodes.Status400BadRequest, HttpStatusCode.BadRequest.ToString(), message)
        {
        }
    }
}
