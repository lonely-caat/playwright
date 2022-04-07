using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm.Models;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Comments.Command.CreateCrmComment
{
    public class CreateCrmCommentCommandHandler : IRequestHandler<CreateCrmCommentCommand, CommentDto>
    {
        private readonly ICrmCommentContext _crmCommentContext;
        private readonly ICrmServiceProxy _crmServiceProxy;
        private readonly CrmServiceProxyOptions _crmServiceProxyOptions;

        public CreateCrmCommentCommandHandler(ICrmCommentContext crmCommentContext,
            ICrmServiceProxy crmServiceProxy,
            IOptions<CrmServiceProxyOptions> crmServiceProxyOptions)
        {
            _crmCommentContext = crmCommentContext ?? throw new ArgumentNullException(nameof(crmCommentContext));
            _crmServiceProxy = crmServiceProxy;
            _crmServiceProxyOptions = crmServiceProxyOptions?.Value;
        }

        public async Task<CommentDto> Handle(CreateCrmCommentCommand request, CancellationToken cancellationToken)
        {
            if (!_crmServiceProxyOptions.Enabled)
            {
                return await _crmCommentContext.CreateAsync(request.ReferenceId,
                                                            request.Category.Value,
                                                            request.Type.Value,
                                                            request.Detail,
                                                            request.CommentBy);
            }
            
            var createCommentRequest = new CreateCommentRequest
            {
                ReferenceId = request.ReferenceId,
                Category = request.Category.Value,
                Type = request.Type.Value,
                CommentBy = request.CommentBy,
                Detail = request.Detail
            };

            return await _crmServiceProxy.CreateComment(createCommentRequest);
        }
    }
}
