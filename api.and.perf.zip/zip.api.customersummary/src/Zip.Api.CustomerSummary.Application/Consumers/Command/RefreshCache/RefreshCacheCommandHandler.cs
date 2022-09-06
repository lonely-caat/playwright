using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Invalidation;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.RefreshCache
{
    public class RefreshCacheCommandHandler : IRequestHandler<RefreshCacheCommand>
    {
        private readonly IAccountsService _accountsService;
        private readonly IConsumerContext _consumerContext;

        public RefreshCacheCommandHandler(IAccountsService accountsService, IConsumerContext consumerContext)
        {
            _accountsService = accountsService;
            _consumerContext = consumerContext ?? throw new ArgumentNullException(nameof(consumerContext));
        }
        
        public async Task<Unit> Handle(RefreshCacheCommand request, CancellationToken cancellationToken)
        {
            var accountInfo = await _consumerContext.GetAccountInfoAsync(request.ConsumerId);

            if (accountInfo == null)
            {
                throw new AccountNotFoundException() { ConsumerId = request.ConsumerId };
            }
            
            await _accountsService.Invalidate(new InvalidationRequest
            {
                AccountIds = new List<string>
                {
                    accountInfo.AccountId.ToString()
                },
                SkipQueueOnRedisError = true
            });

            return Unit.Value;
        }
    }
}
