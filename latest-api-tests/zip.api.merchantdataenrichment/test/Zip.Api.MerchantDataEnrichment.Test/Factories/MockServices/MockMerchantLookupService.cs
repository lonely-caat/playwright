using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Domain.Entities;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Interfaces;
using Zip.MerchantDataEnrichment.Infrastructure.Services.MerchantLookupService.Models;

namespace Zip.Api.MerchantDataEnrichment.Test.Factories.MockServices
{
    public class MockMerchantLookupService : CommonTestsFixture, IMerchantLookupService
    {
        public async Task<MerchantDetail> LookupMerchantDetailAsync(MerchantLookupDetail merchantLookupDetail, CancellationToken cancellationToken)
        {
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .With(x => x.Id, new Guid(Constants.MerchantDetailId2))
                                        .With(x => x.CardAcceptorLocator, $"{Constants.CardAcceptorName2} {Constants.CardAcceptorCity2}")
                                        .Create();

            return await Task.FromResult(merchantDetail);
        }

        public async Task<IList<MerchantLookupResult>> LookupMerchantDetailsAsync(IList<MerchantLookupDetail> merchantLookupDetails, CancellationToken cancellationToken)
        {
            var merchantLookupResult = new List<MerchantLookupResult>
            {
                new MerchantLookupResult
                {
                    TransactionCorrelationGuid = Constants.TransactionCorrelationGuid1,
                    CardAcceptorLocator = Constants.CardAcceptorLocator1,
                    Message = $"Successfully retrieved MerchantDetail for TransactionCorrelationGuid:{Constants.TransactionCorrelationGuid1}",
                    MerchantDetail = Fixture.Build<MerchantDetail>().Without(x => x.VcnTransactions).Create()
                },
                new MerchantLookupResult
                {
                    TransactionCorrelationGuid = Constants.TransactionCorrelationGuid2,
                    CardAcceptorLocator = Constants.CardAcceptorLocator2,
                    Message = $"Successfully retrieved MerchantDetail for TransactionCorrelationGuid:{Constants.TransactionCorrelationGuid2}",
                    MerchantDetail = Fixture.Build<MerchantDetail>().Without(x => x.VcnTransactions).Create()
                },
                new MerchantLookupResult
                {
                    TransactionCorrelationGuid = Constants.FailedTransactionCorrelationGuid,
                    CardAcceptorLocator = Constants.FailedCardAcceptorLocator,
                    Message = $"MerchantLookupResponse is missing SearchResult for TransactionCorrelationGuid:{Constants.FailedTransactionCorrelationGuid}.",
                    MerchantDetail = null
                }
            };

            return await Task.FromResult(merchantLookupResult);
        }
    }
}
