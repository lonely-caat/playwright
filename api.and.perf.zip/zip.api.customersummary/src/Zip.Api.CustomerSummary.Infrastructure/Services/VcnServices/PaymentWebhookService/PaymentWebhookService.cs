using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Helper;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService
{
    public class PaymentWebhookService : IPaymentWebhookService
    {
        private readonly IPaymentWebhookApiProxy _paymentWebhookApiProxy;

        public PaymentWebhookService(IPaymentWebhookApiProxy paymentWebhookApiProxy)
        {
            _paymentWebhookApiProxy = paymentWebhookApiProxy;
        }

        public async Task<IEnumerable<CardTransaction>> GetCardTransactionsAsync(Guid cardId,
            CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("{class} :: {action} : {message}",
                    nameof(PaymentWebhookService),
                    nameof(GetCardTransactionsAsync),
                    $"Start sending request to PaymentWebhookApi to get transactions for card of cardId::{cardId}");

                using (var httpResponseMessage = await _paymentWebhookApiProxy.GetCardTransactionsAsync(cardId))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new PaymentWebhookApiException(
                            $"Failed to get card transactions for cardId::{cardId}, StatusCode::{httpResponseMessage.StatusCode}");
                    }

                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    var cardTransactions = ConvertToCardTransactions(responseContent);

                    return cardTransactions;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "{class} :: {action} : {message}",
                    nameof(PaymentWebhookService),
                    nameof(GetCardTransactionsAsync),
                    ex.Message);

                throw;
            }
        }

        public async Task<IEnumerable<CardTransaction>> GetTransactionsByNetworkReferenceIdAsync(string networkReferenceId, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("{class} :: {action} : {message}",
                    nameof(PaymentWebhookService),
                    nameof(GetTransactionsByNetworkReferenceIdAsync),
                    $"Start sending request to PaymentWebhookApi to get transactions for networkReferenceId::{networkReferenceId}");

                using (var httpResponseMessage = await _paymentWebhookApiProxy.GetTransactionsByNetworkReferenceIdAsync(networkReferenceId))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        throw new PaymentWebhookApiException(
                            $"Failed to get transactions for networkReferenceId::{networkReferenceId}, StatusCode::{httpResponseMessage.StatusCode}");
                    }

                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    var cardTransactions = ConvertToCardTransactions(responseContent);

                    return cardTransactions;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "{class} :: {action} : {message}",
                    nameof(PaymentWebhookService),
                    nameof(GetTransactionsByNetworkReferenceIdAsync),
                    ex.Message);

                throw;
            }
        }

        private IEnumerable<CardTransaction> ConvertToCardTransactions(string response)
        {
            Log.Information("{class} :: {action} : {message}",
                nameof(PaymentWebhookService),
                nameof(GetCardTransactionsAsync),
                "Start converting PaymentWebhookApi response to List of CardTransaction");

            try
            {
                var dynamicTransactions =
                    JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response);
                var cardTransactions = new List<CardTransaction>();
                var expandoObjectConverter = new ExpandoObjectConverter();

                foreach (dynamic transaction in dynamicTransactions)
                {
                    string serializedTransaction = JsonConvert.SerializeObject(transaction);
                    dynamic transactionWithNullableFields = JsonConvert.DeserializeObject<NullableExpandoObject>(serializedTransaction, expandoObjectConverter);

                    var cardTransaction = MapToCardTransaction(transactionWithNullableFields, serializedTransaction);
                    if (cardTransaction != null)
                    {
                        cardTransactions.Add(cardTransaction);
                    }
                }

                return cardTransactions.OrderByDescending(x => x.TimeStamp).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "{class} :: {action} : {message}",
                    nameof(PaymentWebhookService),
                    nameof(ConvertToCardTransactions),
                    $"Failed converting PaymentWebhookApi response to List of CardTransaction with message: {ex.Message}");

                throw;
            }
        }

        private CardTransaction MapToCardTransaction(dynamic transaction, string metadata)
        {
            try
            {
                var memoCode = transaction.marqetaData?.response?.code;
                var memoContent = transaction.marqetaData?.response?.memo;
                var transactionMemo = memoCode != null && memoContent != null
                    ? $"{memoCode} - {memoContent}"
                    : null;

                var cardTransaction = new CardTransaction
                {
                    Id = transaction.id,
                    TimeStamp = transaction.timestamp != null ? Convert.ToDateTime(transaction.timestamp) : null,
                    Merchant = transaction.marqetaData?.cardAcceptor?.name,
                    Type = transaction.eventType,
                    State = transaction.state,
                    Token = transaction.marqetaData?.digitalWalletToken != null ? transaction.marqetaData?.digitalWalletToken.token : null,
                    DeviceName = transaction.marqetaData?.digitalWalletToken != null ? transaction.marqetaData?.digitalWalletToken?.device?.name : null,
                    Amount = transaction.requestAmount != null ? Convert.ToDecimal(transaction.requestAmount) : null,
                    Memo = transactionMemo,
                    Metadata = metadata
                };

                return cardTransaction;
            }
            catch (Exception ex)
            {
                Log.Error("{Controller} :: {Method} : {Message}",
                    nameof(PaymentWebhookService),
                    nameof(MapToCardTransaction),
                    ex.Message.ToString());
                return null;
            }
        }
    }
}