using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Comments.Query.GetCrmComments
{
    public class GetCrmCommentsQueryHandler : IRequestHandler<GetCrmCommentsQuery, Pagination<CommentDto>>
    {
        private readonly ICrmCommentContext crmCommentContext;
        private readonly ICrmServiceProxy _crmServiceProxy;
        private readonly CrmServiceProxyOptions _crmServiceProxyOptions;

        public GetCrmCommentsQueryHandler(ICrmCommentContext commentService,
            ICrmServiceProxy crmServiceProxy,
            IOptions<CrmServiceProxyOptions> crmServiceProxyOptions)
        {
            crmCommentContext = commentService ?? throw new ArgumentNullException(nameof(commentService));
            _crmServiceProxy = crmServiceProxy;
            _crmServiceProxyOptions = crmServiceProxyOptions?.Value;
        }

        public async Task<Pagination<CommentDto>> Handle(GetCrmCommentsQuery request, CancellationToken cancellationToken)
        {
            if (_crmServiceProxyOptions.Enabled)
            {
                return  await  _crmServiceProxy.GetCustomerComment(request.ConsumerId, request.PageIndex, request.PageSize);
            }
            else
            {
                return await crmCommentContext.GetCommentsAsync(
                            request.ConsumerId,
                            null,
                            CommentType.Consumer,
                            request.PageIndex,
                            request.PageSize);
            }
        }
    }
}
