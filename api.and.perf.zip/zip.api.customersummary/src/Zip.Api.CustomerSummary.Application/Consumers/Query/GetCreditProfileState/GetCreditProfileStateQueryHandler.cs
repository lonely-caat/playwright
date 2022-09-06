using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Consumers.Query.GetCreditProfileState
{
    public class GetCreditProfileStateQueryHandler : IRequestHandler<GetCreditProfileStateQuery, CreditProfileState>
    {
        private readonly ICreditProfileContext _creditProfileContext;

        public GetCreditProfileStateQueryHandler(ICreditProfileContext creditProfileContext)
        {
            _creditProfileContext = creditProfileContext ?? throw new ArgumentNullException(nameof(creditProfileContext));
        }

        public async Task<CreditProfileState> Handle(GetCreditProfileStateQuery request, CancellationToken cancellationToken)
        {
            return await _creditProfileContext.GetStateTypeByConsumerIdAsync(request.ConsumerId);
        }
    }
}
