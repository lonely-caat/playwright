using MediatR;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Common.Exceptions;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendPayNowLink;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.PayNowUrlGenerator;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.OutgoingMessages;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.PayNowUrlGenerator.Models;
using Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Communications
{
    public class SendPayNowLinkCommandHandlerTest
    {
        private readonly Mock<IOptions<CommunicationsServiceProxyOptions>> _commServiceProxyOptions;
        private readonly Mock<ICommunicationsServiceProxy> _commServiceProxy;

        public SendPayNowLinkCommandHandlerTest()
        {
            _commServiceProxyOptions = new Mock<IOptions<CommunicationsServiceProxyOptions>>();
            _commServiceProxy = new Mock<ICommunicationsServiceProxy>();

            _commServiceProxy.Setup(x => x.GetSmsContentAsync(It.IsAny<string>()))
                .ReturnsAsync(new SmsContent
                {
                    Active = true,
                    Content = "Some test template"
                });

            _commServiceProxy.Setup(x => x.SendPayNowLinkAsync(It.IsAny<SendPayNowLink>()))
                .ReturnsAsync(new SmsResponse
                {
                    Success = true
                });
        }

        [Fact]
        public void Given_NullPayNowUrlGenerator_ShouldThrow()
        {
            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            var mediator = new Mock<IMediator>();
            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            Assert.Throws<ArgumentNullException>(() => {
                new SendPayNowLinkCommandHandler(null,
                                                 zipUrlShorteningService.Object,
                                                 mediator.Object,
                                                 payNowAccountContext.Object,
                                                 messageLogContext.Object,
                                                 _commServiceProxy.Object,
                                                 outgoingMessageConfig.Object);
            });
        }

        [Fact]
        public void Given_NullZipUrlShorteningService_ShouldThrow()
        {
            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            var mediator = new Mock<IMediator>();
            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            Assert.Throws<ArgumentNullException>(() => {
                new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                 null,
                                                 mediator.Object,
                                                 payNowAccountContext.Object,
                                                 messageLogContext.Object,
                                                 _commServiceProxy.Object,
                                                 outgoingMessageConfig.Object);
            });
        }

        [Fact]
        public void Given_NullMediator_ShouldThrow()
        {
            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            Assert.Throws<ArgumentNullException>(() => {
                new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                 zipUrlShorteningService.Object,
                                                 null,
                                                 payNowAccountContext.Object,
                                                 messageLogContext.Object,
                                                 _commServiceProxy.Object,
                                                 outgoingMessageConfig.Object);
            });
        }

        [Fact]
        public void Given_NullPayNowAccountContext_ShouldThrow()
        {
            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            var mediator = new Mock<IMediator>();
            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            Assert.Throws<ArgumentNullException>(() => {
                new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                 zipUrlShorteningService.Object,
                                                 mediator.Object,
                                                 null,
                                                 messageLogContext.Object,
                                                 _commServiceProxy.Object,
                                                 outgoingMessageConfig.Object);
            });
        }

        [Fact]
        public async Task Given_ConsumerNotFound_ShouldThrow_ConsumerNotFoundException()
        {
            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null);
            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            await Assert.ThrowsAsync<ConsumerNotFoundException>(async () => {
                var handler = new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                               zipUrlShorteningService.Object,
                                                               mediator.Object,
                                                               payNowAccountContext.Object,
                                                               messageLogContext.Object,
                                                               _commServiceProxy.Object,
                                                               outgoingMessageConfig.Object);
                
                await handler.Handle(new SendPayNowLinkCommand(100, 200), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_AccountNotFound_ShouldThrow_AccountNotFoundException()
        {
            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Consumer());
            mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as GetAccountInfoQueryResult);

            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            await Assert.ThrowsAsync<AccountNotFoundException>(async () => {
                var handler = new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                               zipUrlShorteningService.Object,
                                                               mediator.Object,
                                                               payNowAccountContext.Object,
                                                               messageLogContext.Object,
                                                               _commServiceProxy.Object,
                                                               outgoingMessageConfig.Object);

                await handler.Handle(new SendPayNowLinkCommand(100, 200), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_PaymentMethodNotFound_ShouldThrow_PaymentMethodNotFoundException()
        {
            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Consumer());
            mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetAccountInfoQueryResult());
            mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as ContactDto);

            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            await Assert.ThrowsAsync<PaymentMethodNotFoundException>(async () => {
                var handler = new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                               zipUrlShorteningService.Object,
                                                               mediator.Object,
                                                               payNowAccountContext.Object,
                                                               messageLogContext.Object,
                                                               _commServiceProxy.Object,
                                                               outgoingMessageConfig.Object);

                await handler.Handle(new SendPayNowLinkCommand(100, 200), CancellationToken.None);
            });
        }


        [Fact]
        public async Task Given_AccountTypeNotFound_ShouldThrow_AccountTypeNotFoundException()
        {
            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Consumer());
            mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetAccountInfoQueryResult());
            mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContactDto());

            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            payNowAccountContext.Setup(x => x.GetPayNowLinkAccountAsync(It.IsAny<long>()))
                .ReturnsAsync(null as PayNowLinkAccount);
            
            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            await Assert.ThrowsAsync<AccountTypeNotFoundException>(async () => {
                var handler = new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                               zipUrlShorteningService.Object,
                                                               mediator.Object,
                                                               payNowAccountContext.Object,
                                                               messageLogContext.Object,
                                                               _commServiceProxy.Object,
                                                               outgoingMessageConfig.Object);

                await handler.Handle(new SendPayNowLinkCommand(100, 200), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_PayNowUrlGenerationFailed_ShouldThrow_PayNowUrlGenerationFailedException()
        {
            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            payNowUrlGenerator.Setup(x => 
                x.GeneratePayNowUrlAsync(
                    It.IsAny<ProductClassification>(), 
                    It.IsAny<CountryCode>(), 
                    It.IsAny<decimal>(), 
                    It.IsAny<string>()))
                .ReturnsAsync(null as GeneratePayNowUrlResponse);

            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Consumer());
            mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetAccountInfoQueryResult());
            mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContactDto());

            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            payNowAccountContext.Setup(x => x.GetPayNowLinkAccountAsync(It.IsAny<long>()))
                .ReturnsAsync(new PayNowLinkAccount());

            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            await Assert.ThrowsAsync<PayNowUrlGenerationFailedException>(async () => {
                var handler = new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                               zipUrlShorteningService.Object,
                                                               mediator.Object,
                                                               payNowAccountContext.Object,
                                                               messageLogContext.Object,
                                                               _commServiceProxy.Object,
                                                               outgoingMessageConfig.Object);

                await handler.Handle(new SendPayNowLinkCommand(100, 200), CancellationToken.None);
            });
        }


        [Fact]
        public async Task Given_ShorteningUrlFailed_ShouldThrow_ShorteningUrlFailedException()
        {
            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            payNowUrlGenerator.Setup(x =>
                x.GeneratePayNowUrlAsync(
                    It.IsAny<ProductClassification>(),
                    It.IsAny<CountryCode>(),
                    It.IsAny<decimal>(),
                    It.IsAny<string>()))
                .ReturnsAsync(new GeneratePayNowUrlResponse());

            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            zipUrlShorteningService.Setup(x => x.GetZipShortenedUrlAsync(It.IsAny<string>()))
                .ReturnsAsync(null as string);

            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Consumer());
            mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetAccountInfoQueryResult());
            mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContactDto());

            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            payNowAccountContext.Setup(x => x.GetPayNowLinkAccountAsync(It.IsAny<long>()))
                .ReturnsAsync(new PayNowLinkAccount());

            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            await Assert.ThrowsAsync<ShorteningUrlFailedException>(async () => {
                var handler = new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                               zipUrlShorteningService.Object,
                                                               mediator.Object,
                                                               payNowAccountContext.Object,
                                                               messageLogContext.Object,
                                                               _commServiceProxy.Object,
                                                               outgoingMessageConfig.Object);

                await handler.Handle(new SendPayNowLinkCommand(100, 200), CancellationToken.None);
            });
        }

        [Fact]
        public async Task Given_SmsContentNotFound_ShouldThrow_SmsContentNotFoundException()
        {
            _commServiceProxy.Setup(x => x.GetSmsContentAsync(It.IsAny<string>()))
                .ReturnsAsync(null as SmsContent);

            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            payNowUrlGenerator.Setup(x =>
                x.GeneratePayNowUrlAsync(
                    It.IsAny<ProductClassification>(),
                    It.IsAny<CountryCode>(),
                    It.IsAny<decimal>(),
                    It.IsAny<string>()))
                .ReturnsAsync(new GeneratePayNowUrlResponse());

            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            zipUrlShorteningService.Setup(x => x.GetZipShortenedUrlAsync(It.IsAny<string>()))
                .ReturnsAsync("https://zip.co/xxx");

            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Consumer());
            mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetAccountInfoQueryResult());
            mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContactDto());

            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            payNowAccountContext.Setup(x => x.GetPayNowLinkAccountAsync(It.IsAny<long>()))
                .ReturnsAsync(new PayNowLinkAccount());
            
            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            await Assert.ThrowsAsync<SmsContentNotFoundException>(async () => {
                var handler = new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                               zipUrlShorteningService.Object,
                                                               mediator.Object,
                                                               payNowAccountContext.Object,
                                                               messageLogContext.Object,
                                                               _commServiceProxy.Object,
                                                               outgoingMessageConfig.Object);

                await handler.Handle(new SendPayNowLinkCommand(100, 200), CancellationToken.None);
            });
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Given_AllGood_ShouldReturn_UnitValue(bool communicationResponse)
        {
            _commServiceProxy.Setup(x => x.SendPayNowLinkAsync(It.IsAny<SendPayNowLink>()))
                .ReturnsAsync(new SmsResponse
                {
                    Success = communicationResponse
                });

            var payNowUrlGenerator = new Mock<IPayNowUrlGenerator>();
            payNowUrlGenerator.Setup(x =>
                x.GeneratePayNowUrlAsync(
                    It.IsAny<ProductClassification>(),
                    It.IsAny<CountryCode>(),
                    It.IsAny<decimal>(),
                    It.IsAny<string>()))
                .ReturnsAsync(new GeneratePayNowUrlResponse());

            var zipUrlShorteningService = new Mock<IZipUrlShorteningService>();
            zipUrlShorteningService.Setup(x => x.GetZipShortenedUrlAsync(It.IsAny<string>()))
                .ReturnsAsync("https://zip.co/xxx");

            var mediator = new Mock<IMediator>();
            mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Consumer { FirstName = Guid.NewGuid().ToString() });
            mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetAccountInfoQueryResult());
            mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ContactDto());

            var payNowAccountContext = new Mock<IPayNowAccountContext>();
            payNowAccountContext.Setup(x => x.GetPayNowLinkAccountAsync(It.IsAny<long>()))
                .ReturnsAsync(new PayNowLinkAccount());

            var messageLogContext = new Mock<IMessageLogContext>();
            var outgoingMessageConfig = new Mock<IOutgoingMessagesConfig>();

            var handler = new SendPayNowLinkCommandHandler(payNowUrlGenerator.Object,
                                                           zipUrlShorteningService.Object,
                                                           mediator.Object,
                                                           payNowAccountContext.Object,
                                                           messageLogContext.Object,
                                                           _commServiceProxy.Object, 
                                                           outgoingMessageConfig.Object);

            var result = await handler.Handle(new SendPayNowLinkCommand(100, 200), CancellationToken.None);

            Assert.Equal(Unit.Value, result);
        }

    }
}
