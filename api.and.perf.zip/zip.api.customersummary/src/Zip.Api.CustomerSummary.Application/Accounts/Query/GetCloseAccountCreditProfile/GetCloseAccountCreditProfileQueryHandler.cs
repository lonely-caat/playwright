using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile.Models;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile
{
    public class GetCloseAccountCreditProfileQueryHandler : IRequestHandler<GetCloseAccountCreditProfileQuery, GetCloseAccountCreditProfileQueryResult>
    {
        private readonly ICreditProfileContext _creditProfileContext;

        public GetCloseAccountCreditProfileQueryHandler(ICreditProfileContext creditProfileContext)
        {
            _creditProfileContext = creditProfileContext ?? throw new ArgumentNullException(nameof(creditProfileContext));
        }

        public async Task<GetCloseAccountCreditProfileQueryResult> Handle(GetCloseAccountCreditProfileQuery request, CancellationToken cancellationToken)
        {
            var creditProfileState = await _creditProfileContext.GetStateTypeByConsumerIdAsync(request.ConsumerId);

            if (creditProfileState == null)
            {
                throw new CreditProfileNotFoundException(request.ConsumerId);
            }

            var result = new GetCloseAccountCreditProfileQueryResult()
            {
                CreditProfileId = creditProfileState.CreditProfileId,
                CurrentStateType = creditProfileState.CreditStateType,
            };

            var newStates = GetValidStates(creditProfileState.CreditStateType);

            foreach (var @state in newStates)
            {
                result.NewStateTypes.Add(
                    new StateTypeDetail()
                    {
                        StateType = @state,
                        Attributes = await _creditProfileContext.GetProfileAttributesAsync(@state),
                        Classifications = await _creditProfileContext.GetProfileClassificationsAsync(@state)
                    });
            }

            return result;
        }

        private List<CreditProfileStateType> GetValidStates(CreditProfileStateType currentStateType)
        {
            var stateList = new List<CreditProfileStateType>();

            switch (currentStateType)
            {
                case CreditProfileStateType.ApplicationInProgress:
                    stateList.Add(CreditProfileStateType.Expired);
                    break;
                case CreditProfileStateType.ApplicationCompleted:
                    stateList.Add(CreditProfileStateType.Declined);
                    stateList.Add(CreditProfileStateType.Cancelled);
                    stateList.Add(CreditProfileStateType.Expired);
                    break;
                case CreditProfileStateType.Refer1:
                case CreditProfileStateType.Verify:
                    stateList.Add(CreditProfileStateType.Approved);
                    stateList.Add(CreditProfileStateType.Declined);
                    stateList.Add(CreditProfileStateType.Cancelled);
                    stateList.Add(CreditProfileStateType.Expired);
                    break;

                case CreditProfileStateType.Approved:
                    stateList.Add(CreditProfileStateType.Cancelled);
                    break;

                case CreditProfileStateType.Active:
                    stateList.Add(CreditProfileStateType.InActive);
                    break;

                case CreditProfileStateType.Registered:
                    stateList.Add(CreditProfileStateType.InActive);
                    break;

                case CreditProfileStateType.Declined:
                    stateList.Add(CreditProfileStateType.Approved);
                    stateList.Add(CreditProfileStateType.Expired);
                    stateList.Add(CreditProfileStateType.Cancelled);
                    break;

                case CreditProfileStateType.Cancelled:
                    stateList.Add(CreditProfileStateType.Approved);
                    stateList.Add(CreditProfileStateType.Expired);
                    stateList.Add(CreditProfileStateType.Declined);
                    break;
            }

            return stateList;
        }
    }
}
