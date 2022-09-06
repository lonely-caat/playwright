using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Tango;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Services.Accounts.Contract.Invalidation;
using Frequency = Zip.Api.CustomerSummary.Domain.Entities.Tango.Frequency;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.CreateRepayment
{
    public class CreateRepaymentCommandHandler : IRequestHandler<CreateRepaymentCommand, Repayment>
    {
        private readonly IAccountsService _accountsService;
        private readonly ITangoProxy _tangoProxy;
        private readonly IMapper _mapper;
        private readonly IAccountContext _accountContext;

        public CreateRepaymentCommandHandler(IAccountsService accountsService, ITangoProxy tangoProxy, IMapper mapper, IAccountContext accountContext)
        {
            _accountsService = accountsService ;
            _tangoProxy = tangoProxy ?? throw new ArgumentNullException(nameof(tangoProxy));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _accountContext = accountContext ?? throw new ArgumentNullException(nameof(accountContext));
        }

        public async Task<Repayment> Handle(CreateRepaymentCommand request, CancellationToken cancellationToken)
        {
            var variations = await _tangoProxy.ListAllRepaymentSchedulesAsync($"{request.AccountId}");

            if (variations == null || !variations.Any())
            {
                var newVariation = new LoanMgtRepaymentScheduleVariation()
                {
                    AccountHash = request.AccountId.ToString(),
                    OverrideRepaymentAmount = request.Amount,
                    RepaymentVariationStart = request.StartDate
                };
                newVariation.SetFrequency(_mapper.Map<Frequency>(request.Frequency));

                var responseMessage = await _tangoProxy.AddRepaymentScheduleAsync(newVariation);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    Log.Error($"{nameof(CreateRepaymentCommandHandler)} :: TangoProxy AddRepaymentScheduleAsync : AccountId {request.AccountId} : {responseContent}");
                    throw new Exception("Failed to create new repayment schedule");
                }

                await InvalidateCacheAsync(request.AccountId);

                var newRepayment = _mapper.Map<Repayment>(newVariation);
                newRepayment.AccountId = request.AccountId;
                newRepayment.ChangedBy = request.ChangedBy;

                return await _accountContext.AddRepaymentAsync(newRepayment);
            }

            var variation = variations.OrderByDescending(x => x.DirectDebitId).FirstOrDefault();

            variation.OverrideRepaymentAmount = request.Amount;
            variation.RepaymentVariationStart = request.StartDate;
            variation.VariationHasPrecedence = true;
            variation.SetFrequency(_mapper.Map<Frequency>(request.Frequency));

            var response = await _tangoProxy.UpdateRepaymentScheduleAsync(variation.DirectDebitId, variation);
            response.EnsureSuccessStatusCode();

            await InvalidateCacheAsync(request.AccountId);

            var updatedVariations = await _tangoProxy.GetRepaymentScheduleByIdAsync(variation.DirectDebitId);

            var updatedRepayment = _mapper.Map<Repayment>(updatedVariations.FirstOrDefault());
            updatedRepayment.AccountId = request.AccountId;
            updatedRepayment.ChangedBy = request.ChangedBy;

            return await _accountContext.AddRepaymentAsync(updatedRepayment);
        }

        private async Task InvalidateCacheAsync(long accountId)
        {
            await _accountsService.Invalidate(new InvalidationRequest
            {
                AccountIds = new List<string>
                {
                    accountId.ToString()
                },
                SkipQueueOnRedisError = true
            });
        }
    }
}
