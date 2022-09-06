using System;
using System.Threading;
using AutoFixture;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Payments.Command.CreateBankPaymentMethod;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Payments;
using ZipMoney.Services.Payments.Contract.PaymentMethods;
using Task = System.Threading.Tasks.Task;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Payments
{
    public class CreateBankPaymentMethodCommandHandlerTests
    {
        private readonly CreateBankPaymentMethodCommandHandler _target;

        private readonly Fixture _fixture;

        private readonly Mock<IPaymentsServiceProxy> _paymentsServiceProxy;
        
        private readonly Mock<IMediator> _mediator;
        
        public CreateBankPaymentMethodCommandHandlerTests()
        {
            _fixture = new Fixture();
            _paymentsServiceProxy = new Mock<IPaymentsServiceProxy>();
            _mediator = new Mock<IMediator>();
            
            _target = new CreateBankPaymentMethodCommandHandler(_paymentsServiceProxy.Object, _mediator.Object);
        }
        
        [Fact]
        public async Task Given_DependencyInjection_Null_ShouldThrow_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () =>
                {
                    var handler = new CreateBankPaymentMethodCommandHandler(null, null);
                    await handler.Handle(null, CancellationToken.None);
                });
        }

        [Fact]
        public async Task Given_Consumer_Null_ShouldThrow_Exception()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetConsumerQuery>(), default))
                     .Returns(Task.FromResult(new Consumer()));

            await Assert.ThrowsAsync<ConsumerNotFoundException>(
                async () =>
                {
                    await _target.Handle(new CreateBankPaymentMethodCommand(), default);
                });
        }

        [Fact]
        public async Task Given_Consumer_CountryId_Incorrect_ShouldThrow_Exception()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetConsumerQuery>(), default))
                     .Returns(Task.FromResult(new Consumer()
                      {
                          ConsumerId = 12345,
                          CountryId = "CN"
                      }));

            await Assert.ThrowsAsync<InvalidCountryCodeException>(
                async () =>
                { 
                    await _target.Handle(new CreateBankPaymentMethodCommand(), default);
                });
        }

        [Fact]
        public async Task Given_Account_Null_ShouldThrow_Exception()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetConsumerQuery>(), default))
                     .Returns(Task.FromResult(new Consumer()
                      {
                          ConsumerId = 12345,
                          CountryId = "AU"
                      }));
            _mediator.Setup(m => m.Send(It.IsAny<GetAccountInfoQuery>(), default))
                     .Returns(Task.FromResult(new GetAccountInfoQueryResult()
                      {
                          AccountInfo = null
                      }));
            
            await Assert.ThrowsAsync<AccountNotFoundException>(
                async () =>
                {
                    await _target.Handle(new CreateBankPaymentMethodCommand(), default);
                });
        }

        [Fact]
        public async Task Given_Product_Incorrect_ShouldThrow_Exception()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetConsumerQuery>(), default))
                     .Returns(Task.FromResult(new Consumer()
                      {
                          ConsumerId = 12345,
                          CountryId = "AU"
                      }));
            _mediator.Setup(m => m.Send(It.IsAny<GetAccountInfoQuery>(), default))
                     .Returns(Task.FromResult(new GetAccountInfoQueryResult()
                      {
                          AccountInfo = new AccountInfo()
                          {
                              Product = null
                          }
                      }));

            var ex = await Assert.ThrowsAsync<InvalidProductCodeException>(
                async () =>
                {
                    await _target.Handle(new CreateBankPaymentMethodCommand(), default);
                });

            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Given_ErrorInProxy_ShouldThrow_Exception()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetConsumerQuery>(), default))
                     .Returns(Task.FromResult(new Consumer()
                      {
                          ConsumerId = 12345,
                          CountryId = "AU"
                      }));
            _mediator.Setup(m => m.Send(It.IsAny<GetAccountInfoQuery>(), default))
                     .Returns(Task.FromResult(new GetAccountInfoQueryResult()
                      {
                          AccountInfo = new AccountInfo()
                          {
                              Product = ProductClassification.zipMoney
                          }
                      }));

            _paymentsServiceProxy.Setup(x => x.CreatePaymentMethod(It.IsAny<CreatePaymentMethodRequest>()))
                                 .Returns(Task.FromResult(null as PaymentMethodResponse));

            var ex = await Assert.ThrowsAsync<CreatePaymentFailedException>(
                async () =>
                {
                    await _target.Handle(new CreateBankPaymentMethodCommand(), default);
                });

            Assert.NotNull(ex);
        }

        [Fact]
        public async Task Given_EverythingIsFine_Should_ReturnResponse()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetConsumerQuery>(), default))
                     .Returns(Task.FromResult(new Consumer()
                      {
                          ConsumerId = 12345,
                          CountryId = "AU"
                      }));
            _mediator.Setup(m => m.Send(It.IsAny<GetAccountInfoQuery>(), default))
                     .Returns(Task.FromResult(new GetAccountInfoQueryResult()
                      {
                          AccountInfo = new AccountInfo()
                          {
                              Product = ProductClassification.zipMoney
                          }
                      }));


            var expectedResponseId = Guid.NewGuid();

            _paymentsServiceProxy.Setup(x => x.CreatePaymentMethod(It.IsAny<CreatePaymentMethodRequest>()))
                .Returns(Task.FromResult(new PaymentMethodResponse {  Id = expectedResponseId }));

            var response = await _target.Handle(new CreateBankPaymentMethodCommand(), default);

            Assert.NotNull(response);
            Assert.Equal(expectedResponseId, response.Id);
        }
        
        [Fact]
        public async Task Given_Payment_Method_Is_Default_Then_Should_Invoke_SetDefaultPaymentMethod()
        {
            // Arrange
            var consumer = _fixture.Build<Consumer>()
                                   .With(x=> x.CountryId, "AU")
                                   .Without(x => x.LinkedConsumer)
                                   .Create();
            var account = _fixture.Create<GetAccountInfoQueryResult>();
            var paymentMethodResponse = _fixture.Create<PaymentMethodResponse>();
            var request = new CreateBankPaymentMethodCommand { ConsumerId = consumer.ConsumerId, IsDefault = true };

            _mediator.Setup(m => m.Send(It.IsAny<GetConsumerQuery>(), default))
                     .Returns(Task.FromResult(consumer));

            _mediator.Setup(m => m.Send(It.IsAny<GetAccountInfoQuery>(), default))
                        .Returns(Task.FromResult(account));

            _paymentsServiceProxy.Setup(x => x.CreatePaymentMethod(It.IsAny<CreatePaymentMethodRequest>()))
                                 .ReturnsAsync(paymentMethodResponse);
            
            // Action
            await _target.Handle(request, new CancellationToken());

            // Assert
            _paymentsServiceProxy.Verify(
                x => x.SetDefaultPaymentMethod(paymentMethodResponse.Id, consumer.ConsumerId.ToString()),
                Times.Once());
        }
        
        [Fact]
        public async Task Given_Payment_Method_Is_Not_Default_Then_Should_Not_Invoke_SetDefaultPaymentMethod()
        {
            // Arrange
            var consumer = _fixture.Build<Consumer>()
                                   .With(x => x.CountryId, "AU")
                                   .Without(x => x.LinkedConsumer)
                                   .Create();
            var account = _fixture.Create<GetAccountInfoQueryResult>();
            var paymentMethodResponse = _fixture.Create<PaymentMethodResponse>();
            var request = new CreateBankPaymentMethodCommand { ConsumerId = consumer.ConsumerId, IsDefault = false };

            _mediator.Setup(m => m.Send(It.IsAny<GetConsumerQuery>(), default))
                     .Returns(Task.FromResult(consumer));

            _mediator.Setup(m => m.Send(It.IsAny<GetAccountInfoQuery>(), default))
                     .Returns(Task.FromResult(account));

            _paymentsServiceProxy.Setup(x => x.CreatePaymentMethod(It.IsAny<CreatePaymentMethodRequest>()))
                                 .ReturnsAsync(paymentMethodResponse);

            // Action
            await _target.Handle(request, new CancellationToken());

            // Assert
            _paymentsServiceProxy.Verify(
                x => x.SetDefaultPaymentMethod(paymentMethodResponse.Id, consumer.ConsumerId.ToString()),
                Times.Never);
        }
    }
}