using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services.PaymentWebhook
{
    public class PaymentWebhookServiceTests : CommonTestsFixture
    {
        private readonly Mock<IPaymentWebhookApiProxy> _paymentWebhookApiProxy;
        private readonly PaymentWebhookService _target;


        public PaymentWebhookServiceTests()
        {
            _paymentWebhookApiProxy = new Mock<IPaymentWebhookApiProxy>();
            _target = new PaymentWebhookService(_paymentWebhookApiProxy.Object);
        }

        [Fact]
        public async Task Given_PaymentWebhookApiProxy_Valid_Response_Should_Return_CardTransaction_List()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var dynamicTransaction =
                JsonConvert.DeserializeObject<dynamic>(PaymentWebhookServiceTestConstants.ValidFullTransaction);
            var cardTransaction = new CardTransaction
            {
                Id = dynamicTransaction.id,
                TimeStamp = dynamicTransaction.timestamp,
                Merchant = dynamicTransaction.marqetaData?.cardAcceptor?.name,
                Type = dynamicTransaction.eventType,
                State = dynamicTransaction.state,
                Token = dynamicTransaction.digitalWalletToken,
                DeviceName = dynamicTransaction.marqetaData.digitalWalletToken.device.name,
                Amount = dynamicTransaction.requestAmount,
                Memo =
                    $"{dynamicTransaction.marqetaData.response.code} - {dynamicTransaction.marqetaData.response.memo}",
                Metadata = PaymentWebhookServiceTestConstants.ValidFullTransaction
            };

            var mockProxyresponse = new List<dynamic> { dynamicTransaction };
            var serializedResponse = JsonConvert.SerializeObject(mockProxyresponse);
            _paymentWebhookApiProxy.Setup(x => x.GetCardTransactionsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(serializedResponse)
            });

            var expected = new List<CardTransaction> { cardTransaction };

            // Act
            var actual = await _target.GetCardTransactionsAsync(cardId, default);

            // Assert
            Assert.True(HasSameValue(expected, actual));
        }

        [Fact]
        public async Task
            Given_PaymentWebhookApiProxy_Valid_Response_With_Missing_Transaction_Details_Should_Not_Throw_Exception()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var dynamicTransaction =
                JsonConvert.DeserializeObject<dynamic>(PaymentWebhookServiceTestConstants.TransactionWithOnlyId);
            var cardTransaction = new CardTransaction { Id = dynamicTransaction.id, Metadata = PaymentWebhookServiceTestConstants.TransactionWithOnlyId };
            var expected = new List<CardTransaction> { cardTransaction };

            var mockProxyresponse = new List<dynamic> { dynamicTransaction };
            var serializedResponse = JsonConvert.SerializeObject(mockProxyresponse);
            _paymentWebhookApiProxy.Setup(x => x.GetCardTransactionsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(serializedResponse)
                });

            // Act
            var actual = await _target.GetCardTransactionsAsync(cardId, default);

            // Assert
            Assert.True(HasSameValue(expected, actual));
        }

        [Fact]
        public async Task Given_PaymentWebhookApiProxy_Valid_Response_Should_Order_By_Timestamp()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var dynamicOlderTransaction =
                JsonConvert.DeserializeObject<dynamic>(PaymentWebhookServiceTestConstants.TransactionWithOlderTimestamp);
            var dynamicNewerTransaction =
                JsonConvert.DeserializeObject<dynamic>(PaymentWebhookServiceTestConstants.TransactionWithNewerTimestamp);

            var olderCardTransaction = new CardTransaction
            {
                TimeStamp = dynamicOlderTransaction.timestamp,
                Metadata = PaymentWebhookServiceTestConstants.TransactionWithOlderTimestamp
            };
            var newerCardTransaction = new CardTransaction
            {
                TimeStamp = dynamicNewerTransaction.timestamp,
                Metadata = PaymentWebhookServiceTestConstants.TransactionWithNewerTimestamp
            };

            var mockProxyresponse = new List<dynamic> { dynamicOlderTransaction, dynamicNewerTransaction };
            var serializedResponse = JsonConvert.SerializeObject(mockProxyresponse);
            _paymentWebhookApiProxy.Setup(x => x.GetCardTransactionsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(serializedResponse)
                });

            var expectedOrder = new List<CardTransaction> { newerCardTransaction, olderCardTransaction };

            // Act
            var actualOrder = await _target.GetCardTransactionsAsync(cardId, default);

            // Assert
            Assert.True(HasSameValue(expectedOrder, actualOrder));
        }

        [Fact]
        public async Task Given_PaymentWebhookApiProxy_Empty_Response_Should_Return_Empty_CardTransaction_List()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var serializedResponse = JsonConvert.SerializeObject(new List<dynamic>());
            _paymentWebhookApiProxy.Setup(x => x.GetCardTransactionsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(serializedResponse)
                });

            // Act
            var actual = await _target.GetCardTransactionsAsync(cardId, default);

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public async Task Given_PaymentWebhookApiProxy_Has_Error_Should_Throw_Exception()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            _paymentWebhookApiProxy.Setup(x => x.GetCardTransactionsAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception());

            // Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                // Act
                await _target.GetCardTransactionsAsync(cardId, default);
            });
        }

        [Fact]
        public async Task Given_PaymentWebhookApiProxy_Non_Successful_Response_Should_Throw_Exception()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            _paymentWebhookApiProxy.Setup(x => x.GetCardTransactionsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent("test failure status code")
                });

            // Assert
            await Assert.ThrowsAsync<PaymentWebhookApiException>(async () =>
            {
                // Act
                await _target.GetCardTransactionsAsync(cardId, default);
            });
        }

        [Fact]
        public async Task Given_PaymentWebhookApiProxy_Valid_GetTransactionsByNetworkReferenceIdAsync_Should_Return_CardTransaction_List()
        {
            // Arrange
            var dynamicTransaction =
                JsonConvert.DeserializeObject<dynamic>(PaymentWebhookServiceTestConstants.ValidFullTransaction);
            var cardTransaction = new CardTransaction
            {
                Id = dynamicTransaction.id,
                TimeStamp = dynamicTransaction.timestamp,
                Merchant = dynamicTransaction.marqetaData?.cardAcceptor?.name,
                Type = dynamicTransaction.eventType,
                State = dynamicTransaction.state,
                Token = dynamicTransaction.digitalWalletToken,
                DeviceName = dynamicTransaction.marqetaData.digitalWalletToken.device.name,
                Amount = dynamicTransaction.requestAmount,
                Memo =
                    $"{dynamicTransaction.marqetaData.response.code} - {dynamicTransaction.marqetaData.response.memo}",
                Metadata = PaymentWebhookServiceTestConstants.ValidFullTransaction
            };

            var mockProxyresponse = new List<dynamic> { dynamicTransaction };
            var serializedResponse = JsonConvert.SerializeObject(mockProxyresponse);
            _paymentWebhookApiProxy.Setup(x => x.GetTransactionsByNetworkReferenceIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(serializedResponse)
                });

            var expected = new List<CardTransaction> { cardTransaction };

            // Act
            var actual = await _target.GetTransactionsByNetworkReferenceIdAsync("12345678", default);

            // Assert
            Assert.True(HasSameValue(expected, actual));
        }

        private bool HasSameValue(object expected, object actual)
        {
            var expectedStr = JsonConvert.SerializeObject(expected);
            var actualStr = JsonConvert.SerializeObject(actual);

            return expectedStr.Equals(actualStr);
        }
    }
}