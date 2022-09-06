using System.Net;
using Microsoft.AspNetCore.Http;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApiErrors
{
    public class UnprocessableEntityError : ApiError
    {
        public UnprocessableEntityError()
            : base(StatusCodes.Status422UnprocessableEntity, HttpStatusCode.UnprocessableEntity.ToString())
        {
        }

        public UnprocessableEntityError(string message)
            : base(StatusCodes.Status422UnprocessableEntity, HttpStatusCode.UnprocessableEntity.ToString(), message)
        {
        }
    }
}
