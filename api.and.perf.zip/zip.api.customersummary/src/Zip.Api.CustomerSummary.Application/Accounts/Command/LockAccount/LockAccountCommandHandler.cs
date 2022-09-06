using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm.Models;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Account;
using Zip.Services.Accounts.Contract.Account.Status;

namespace Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount
{
    public class LockAccountCommandHandler : IRequestHandler<LockAccountCommand>
    {
        private readonly IAccountsService _accountsService;
        private readonly IAttributeContext _attributeContext;
        private readonly IConsumerContext _consumerContext;
        private readonly ICrmServiceProxy _crmServiceProxy;

        public LockAccountCommandHandler(
            IAccountsService accountsService,
            IAttributeContext attributeContext,
            IConsumerContext consumerContext,
            ICrmServiceProxy crmProxy)
        {
            _accountsService = accountsService;
            _attributeContext = attributeContext ?? throw new ArgumentNullException(nameof(attributeContext));
            _consumerContext = consumerContext ?? throw new ArgumentNullException(nameof(consumerContext));
            _crmServiceProxy = crmProxy ?? throw new ArgumentNullException(nameof(crmProxy));
        }

        public async Task<Unit> Handle(LockAccountCommand request, CancellationToken cancellationToken)
        {
            AccountResponse account;

            try
            {
                account = await _accountsService.GetAccount(request.AccountId.ToString());
            }
            catch(Exception ex)
            {
                throw new AccountNotFoundException($"Account not found with id {request.AccountId}", ex);
            }

            if (account == null)
            {
                throw new AccountNotFoundException(request.ConsumerId, request.AccountId);
            }

            try
            {
                var response = await _accountsService.LockAccount(request.AccountId, new LockAccountRequest { SubState = AccountSubState.Other });

                if (response == null || response.State != AccountState.Locked)
                {
                    throw new LockAccountException($"Unable to lock account with id {request.AccountId}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"LockAccountCommandHandler :: Handle : {request.ConsumerId}");
                throw new LockAccountException(ex.Message);
            }

            try
            {
                await AddNfdAttributeIfNotExist(request.ConsumerId);
                await AddNfdLockCrmComment(request);
                await _consumerContext.SetTrustScoreAsync(request.ConsumerId, TrustScore.MaximumNegative);

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"LockAccountCommandHandler :: Handle : {request.ConsumerId}");

                throw;
            }

            return Unit.Value;
        }

        private async Task AddNfdAttributeIfNotExist(long consumerId)
        {
            var consumerAttributes = await _attributeContext.GetConsumerAttributesAsync(consumerId);
            var newAttributeIds = consumerAttributes.Select(attribute => attribute.Id).ToList();

            if (!newAttributeIds.Contains((long)ConsumerAttributeEnum.NoFurtherDrawdown))
            {
                newAttributeIds.Add((long)ConsumerAttributeEnum.NoFurtherDrawdown);

                await _attributeContext.SetConsumerAttributesAsync(consumerId, newAttributeIds);
            }
        }

        private async Task AddNfdLockCrmComment(LockAccountCommand request)
        {
            var formattedReasonString = $"{CommentConstant.NfdLockTag} {request.Reason}";
            var createCommentRequest = new CreateCommentRequest
            {
                ReferenceId = request.ConsumerId,
                Category = CommentCategory.Risk,
                Type = CommentType.Consumer,
                Detail = formattedReasonString,
                CommentBy = request.ChangedBy
            };

            await _crmServiceProxy.CreateComment(createCommentRequest);
        }
    }
}
