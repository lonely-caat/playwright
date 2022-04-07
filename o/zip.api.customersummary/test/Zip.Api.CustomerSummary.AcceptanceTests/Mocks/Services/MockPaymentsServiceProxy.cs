using AutoFixture;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using ZipMoney.Services.Payments.Contract.Hash;
using ZipMoney.Services.Payments.Contract.PaymentBatches;
using ZipMoney.Services.Payments.Contract.PaymentMethods;
using ZipMoney.Services.Payments.Contract.Payments;
using ZipMoney.Services.Payments.Contract.Types;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.Services
{
    public class MockPaymentsServiceProxy : IPaymentsServiceProxy
    {
        private readonly IFixture _fixture;

        public MockPaymentsServiceProxy()
        {
            _fixture = new Fixture();
        }

        public Task<PaymentMethodResponse> CreatePaymentMethod([Body] CreatePaymentMethodRequest request)
        {
            return Task.FromResult(new PaymentMethodResponse());
        }

        public Task DeletePaymentMethod(Guid id)
        {
            throw new NotImplementedException();
        }

        private PaymentBatchItem CreatePBI()
        {
            return new PaymentBatchItem
            {
                BatchId = Guid.NewGuid(),
                PaymentDate = _fixture.Create<DateTime>(),
                BatchFileName = _fixture.Create<string>(),
                Method = _fixture.Create<PaymentMethodType>(),
                Reason = _fixture.Create<string>()
            };
        }

        public Task<IReadOnlyCollection<PaymentBatchItem>> GetAllPaymentBatches(DateTime startDate, DateTime endDate, int? skip = null, int? take = null)
        {
            return Task.FromResult<IReadOnlyCollection<PaymentBatchItem>>(new List<PaymentBatchItem> {
                CreatePBI(),
                CreatePBI(),
                CreatePBI(),
                CreatePBI(),
                CreatePBI()
            });
        }

        private PaymentMethodResponse CreatePMR()
        {
            return new PaymentMethodResponse
            {
                State = _fixture.Create<PaymentMethodState>(),
                IsDefault = _fixture.Create<bool>(),
                CreatedTimeStamp = DateTime.Now.AddDays(-10)
            };
        }

        public Task<ReadOnlyCollection<PaymentMethodResponse>> GetAllPaymentMethods(string customerId, bool includeFailedAttempted = false)
        {
            return Task.FromResult<ReadOnlyCollection<PaymentMethodResponse>>(new List<PaymentMethodResponse>
            {
                new PaymentMethodResponse
                {
                    State = PaymentMethodState.Approved,
                    IsDefault = _fixture.Create<bool>(),
                    CreatedTimeStamp = DateTime.Now.AddDays(-10)
                },
                new PaymentMethodResponse
                {
                    State = PaymentMethodState.Approved,
                    IsDefault = true,
                    CreatedTimeStamp = DateTime.Now,
                },
                new PaymentMethodResponse
                {
                    State = PaymentMethodState.Declined,
                    IsDefault = _fixture.Create<bool>(),
                    CreatedTimeStamp = DateTime.Now.AddDays(-10)
                },
                new PaymentMethodResponse
                {
                    State = PaymentMethodState.Failed,
                    IsDefault = _fixture.Create<bool>(),
                    CreatedTimeStamp = DateTime.Now.AddDays(-10)
                },
                new PaymentMethodResponse
                {
                    State = PaymentMethodState.Invalid,
                    IsDefault = _fixture.Create<bool>(),
                    CreatedTimeStamp = DateTime.Now.AddDays(-10)
                },
                new PaymentMethodResponse
                {
                    State = PaymentMethodState.Removed,
                    IsDefault = true,
                    CreatedTimeStamp = DateTime.Now
                }
            }.AsReadOnly());
        }

        public Task<ReadOnlyCollection<PaymentResponse>> GetAllPayments(string accountId, DateTime? fromDate = null, DateTime? toDate = null, Guid? PaymentBatchId = null)
        {
            return Task.FromResult(new List<PaymentResponse>
            {
                new PaymentResponse
                {
                    AccountId = accountId,
                    Amount =100,
                    CountryCode = CountryCode.AU,
                    CurrencyCode = "AUD",
                    Gateway = CardGateway.FatZebra,
                    MethodType = PaymentMethodType.BankAccount,
                }

            }.AsReadOnly());
        }

        public Task<GetHashMethodResponse> GetHash(ProductClassification product, CountryCode country, string reference, decimal amount, string paymentType)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> GetPayment(string id)
        {
            return Task.FromResult(new PaymentResponse
            {
                Id = Guid.NewGuid(),
                CountryCode = CountryCode.AU,
                CreatedDateTime = _fixture.Create<DateTime>(),
                Status = PaymentState.Captured
            });
        }

        public Task<PaymentMethodResponse> GetPaymentMethod(Guid id)
        {
            return Task.FromResult(new PaymentMethodResponse());
        }

        public Task<GetHashMethodResponse> GetProviderHash(ProductClassification product, CountryCode country, string reference, decimal amount, string paymentType)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResponse> MakePayment([Body] MakePaymentRequest request)
        {
            return Task.FromResult(new PaymentResponse
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CreatedDateTime = DateTime.Now,
                Status = PaymentState.Authorised,
                MethodType = PaymentMethodType.BankAccount,
                Type = PaymentType.OneOff,
                CountryCode = CountryCode.AU,
                Gateway = CardGateway.FatZebra
            });
        }

        public Task<string> Ping()
        {
            throw new NotImplementedException();
        }

        public Task<PaymentRefundResponse> RefundPayment(Guid id)
        {
            return Task.FromResult(new PaymentRefundResponse
            {
                Amount = 100m,
                CurrencyCode = "AUD",
                PaymentId = id,
                State = PaymentState.Refunded
            });
        }

        public Task RemoveAllDefaultPaymentMethod(string customerId)
        {
            throw new NotImplementedException();
        }

        public Task SetDefaultPaymentMethod(Guid id, string customerId)
        {
            return Task.FromResult(0);
        }

        public Task UpdatePaymentMethodState(Guid id, PaymentMethodState paymentMethodState)
        {
            throw new NotImplementedException();
        }
    }
}
