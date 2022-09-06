using System.Net;
using Microsoft.AspNetCore.Http;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApiErrors
{
    public class NoContentError : ApiError
    {
        public NoContentError()
            : base(StatusCodes.Status204NoContent, HttpStatusCode.NoContent.ToString())
        {
        }

        public NoContentError(string message)
            : base(StatusCodes.Status204NoContent, HttpStatusCode.NoContent.ToString(), message)
        {
        }
    }
}
