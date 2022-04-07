using System.Net;
using Microsoft.AspNetCore.Http;

namespace Zip.Api.CustomerSummary.Domain.Entities.ApiErrors
{
    public class FailedDependencyError : ApiError
    {
        public FailedDependencyError()
            : base(StatusCodes.Status424FailedDependency, HttpStatusCode.FailedDependency.ToString())
        {
        }

        public FailedDependencyError(string message)
            : base(StatusCodes.Status424FailedDependency, HttpStatusCode.FailedDependency.ToString(), message)
        {
        }
    }
}
