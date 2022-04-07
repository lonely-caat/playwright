using Refit;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm
{
    public interface ICrmServiceProxy
    {
        [Get("/health")]
        Task<string> HealthCheck();

        [Post("/api/comment")]
        Task<CommentDto> CreateComment(CreateCommentRequest request);

        [Get("/api/comment/customer/{customerId}?pageIndex={pageIndex}&pageSize={pageSize}")]
        Task<Pagination<CommentDto>> GetCustomerComment(long customerId, long pageIndex = 1, long pageSize = 100);
    }
}
