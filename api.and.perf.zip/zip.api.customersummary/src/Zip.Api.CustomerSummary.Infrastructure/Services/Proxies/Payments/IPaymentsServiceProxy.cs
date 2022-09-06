using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Refit;
using ZipMoney.Services.Payments.Contract.Hash;
using ZipMoney.Services.Payments.Contract.PaymentBatches;
using ZipMoney.Services.Payments.Contract.PaymentMethods;
using ZipMoney.Services.Payments.Contract.Payments;
using PaymentsContractTypes = ZipMoney.Services.Payments.Contract.Types;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments
{
    public interface IPaymentsServiceProxy
    {
        [Get("/payments/{id}")]
        Task<PaymentResponse> GetPayment(string id);

        [Get("/payments")]
        Task<ReadOnlyCollection<PaymentResponse>> GetAllPayments(string accountId, DateTime? fromDate = null, DateTime? toDate = null, Guid? PaymentBatchId = null);

        [Post("/payments")]
        Task<PaymentResponse> MakePayment([Body] MakePaymentRequest request);

        [Get("/paymentmethods")]
        Task<ReadOnlyCollection<PaymentMethodResponse>> GetAllPaymentMethods(string customerId, bool includeFailedAttempted = false);

        [Get("/paymentmethods/{id}")]
        Task<PaymentMethodResponse> GetPaymentMethod(Guid id);

        [Post("/paymentmethods")]
        Task<PaymentMethodResponse> CreatePaymentMethod([Body] CreatePaymentMethodRequest request);

        [Delete("/paymentmethods/{id}")]
        Task DeletePaymentMethod(Guid id);

        [Patch("/paymentmethods/{id}")]
        Task UpdatePaymentMethodState(Guid id, PaymentsContractTypes.PaymentMethodState paymentMethodState);

        [Post("/paymentmethods/{id}/default")]
        Task SetDefaultPaymentMethod(Guid id, string customerId);

        [Post("/paymentmethods/removeAllDefaults")]
        Task RemoveAllDefaultPaymentMethod(string customerId);

        [Get("/diagnostics/ping")]
        Task<string> Ping();

        [Post("/payments/{id}/refund")]
        Task<PaymentRefundResponse> RefundPayment(Guid id);

        [Get("/paymentBatches")]
        Task<IReadOnlyCollection<PaymentBatchItem>> GetAllPaymentBatches(DateTime startDate, DateTime endDate, int? skip = null, int? take = null);

        [Get("/paynow/hash")]
        Task<GetHashMethodResponse> GetHash(PaymentsContractTypes.ProductClassification product, PaymentsContractTypes.CountryCode country, string reference, decimal amount, string paymentType);

        [Get("/provider/hash")]
        Task<GetHashMethodResponse> GetProviderHash(PaymentsContractTypes.ProductClassification product, PaymentsContractTypes.CountryCode country, string reference, decimal amount, string paymentType);
    }
}
