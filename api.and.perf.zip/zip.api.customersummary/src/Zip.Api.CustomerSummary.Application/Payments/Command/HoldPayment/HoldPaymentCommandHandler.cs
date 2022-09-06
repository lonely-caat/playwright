using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.HoldPayment
{
    public class HoldPaymentCommandHandler : IRequestHandler<HoldPaymentCommand>
    {
        private readonly IAccountContext _accountContext;
        private readonly IAccountsService _accountsService;

        public HoldPaymentCommandHandler(IAccountContext accountContext, IAccountsService accountsService)
        {
            _accountContext = accountContext ?? throw new ArgumentNullException(nameof(accountContext));
            _accountsService = accountsService;
        }

        public async Task<Unit> Handle(HoldPaymentCommand request, CancellationToken cancellationToken)
        {
            await _accountContext.HoldPaymentDateAsync(request.AccountId, request.HoldDate);
            await _accountsService.Invalidate(new Services.Accounts.Contract.Invalidation.InvalidationRequest
            {
                AccountIds = new System.Collections.Generic.List<string>
                {
                    request.AccountId.ToString()
                },
                SkipQueueOnRedisError = true
            });
            return Unit.Value;
        }
    }
}
