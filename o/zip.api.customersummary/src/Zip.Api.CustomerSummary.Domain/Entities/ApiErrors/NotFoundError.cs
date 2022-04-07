using System.Net;
using Microsoft.AspNetCore.Http;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApiErrors
{
    public class NotFoundError : ApiError
    {
        public NotFoundError()
            : base(StatusCodes.Status404NotFound, HttpStatusCode.NotFound.ToString())
        {
        }

        public NotFoundError(string message)
            : base(StatusCodes.Status404NotFound, HttpStatusCode.NotFound.ToString(), message)
        {
        }
    }
}
