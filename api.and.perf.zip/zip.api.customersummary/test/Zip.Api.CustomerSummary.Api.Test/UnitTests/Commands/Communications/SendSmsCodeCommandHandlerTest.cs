using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendSmsCode;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Communications
{
    public class SendSmsCodeCommandHandlerTest
    {
        private readonly Mock<IContactContext> _contactContext;
        private readonly Mock<IConsumerStatContext> _consumerStateContext;
        private readonly Mock<IMfaContext> _mfaContext;
        private readonly Mock<ISmsService> _smsService;
        private readonly Mock<IOptions<CommunicationsServiceProxyOptions>> _options;
        private readonly Mock<ICommunicationsServiceProxy> _communicationsServiceProxy;

        public SendSmsCodeCommandHandlerTest()
        {
            _contactContext = new Mock<IContactContext>();
            _consumerStateContext = new Mock<IConsumerStatContext>();
            _mfaContext = new Mock<IMfaContext>();
            _smsService = new Mock<ISmsService>();
            _options = new Mock<IOptions<CommunicationsServiceProxyOptions>>();
            _communicationsServiceProxy = new Mock<ICommunicationsServiceProxy>();
        }

        [Fact]
        public void Check_NullInjection()
        {
            Assert.Throws<ArgumentNullException>(
                () => new SendSmsCodeCommandHandler(
                    null, _consumerStateContext.Object, _mfaContext.Object, _smsService.Object, _options.Object, _communicationsServiceProxy.Object));

            Assert.Throws<ArgumentNullException>(
                () => new SendSmsCodeCommandHandler(_contactContext.Object, null, _mfaContext.Object, _smsService.Object, _options.Object, _communicationsServiceProxy.Object));

            Assert.Throws<ArgumentNullException>(
                () => new SendSmsCodeCommandHandler(_contactContext.Object, _consumerStateContext.Object, null, _smsService.Object, _options.Object, _communicationsServiceProxy.Object));

            Assert.Throws<ArgumentNullException>(
              () => new SendSmsCodeCommandHandler(_contactContext.Object, _consumerStateContext.Object, _mfaContext.Object, null, _options.Object, _communicationsServiceProxy.Object));
        }

        [Fact]
        public async Task Given_NoMobileFound_ShouldThrow_MobileNotFoundException()
        {
            _contactContext.Setup(x => x.GetMobileAsync(It.IsAny<long>()))
                .ReturnsAsync(string.Empty);

            var handler = new SendSmsCodeCommandHandler(
                _contactContext.Object, _consumerStateContext.Object, _mfaContext.Object, _smsService.Object, _options.Object, _communicationsServiceProxy.Object);

            await Assert.ThrowsAsync<MobileNotFoundException>(async () =>
            {
                await handler.Handle(new SendSmsCodeCommand { ConsumerId = 2910 }, default);
            });
        }

        [Fact]
        public async Task Given_ErrorInSendingSms_ShouldThrow_SmsDeliveryException()
        {
            var phone = "0415838292";

            _contactContext.Setup(x => x.GetMobileAsync(It.IsAny<long>()))
                .ReturnsAsync(phone);

            _consumerStateContext.Setup(x => x.GetByConsumerIdAsync(It.IsAny<long>()))
                .ReturnsAsync(null as ConsumerStat);

            _consumerStateContext.Setup(x => x.SaveAsync(It.IsAny<ConsumerStat>()));

            _mfaContext.Setup(x => x.GetTokenAsync(It.IsAny<MfaEntityType>(), It.IsAny<long>()))
                .ReturnsAsync(null as MfaCode);

            _mfaContext.Setup(x => x.SaveAsync(It.IsAny<MfaCode>()))
                .ReturnsAsync(new MfaCode { Code = "392817" });
            _options.Setup(x => x.Value).Returns(new CommunicationsServiceProxyOptions
            {
                Enabled = true
            });

            var msg = $"{Guid.NewGuid()}";

            _communicationsServiceProxy.Setup(x => x.SendSmsAsync(It.IsAny<SendSms>()))
              .ReturnsAsync(new SmsResponse
              {
                  Success = false,
                  ErrorMessage = msg
              });

            
            _smsService.Setup(x => x.SendSmsToConsumer(It.Is<string>(b => b == phone), It.IsAny<string>()))
                .Returns(new SmsResponse
                {
                    Success = false,
                    ErrorMessage = msg
                });

            var handler = new SendSmsCodeCommandHandler(
                _contactContext.Object, _consumerStateContext.Object, _mfaContext.Object, _smsService.Object, _options.Object, _communicationsServiceProxy.Object);

            var ex = await Assert.ThrowsAsync<SmsDeliveryException>(async () =>
            {
                await handler.Handle(new SendSmsCodeCommand { ConsumerId = 2910 }, default);
            });

            Assert.Equal(msg, ex.Message);
        }

        [Fact]
        public async Task Sould_return_sms_code()
        {
            var phone = "0415838292";

            _contactContext.Setup(x => x.GetMobileAsync(It.IsAny<long>()))
                .ReturnsAsync(phone);

            _consumerStateContext.Setup(x => x.GetByConsumerIdAsync(It.IsAny<long>()))
                .ReturnsAsync(null as ConsumerStat);

            _consumerStateContext.Setup(x => x.SaveAsync(It.IsAny<ConsumerStat>()));

            _mfaContext.Setup(x => x.GetTokenAsync(It.IsAny<MfaEntityType>(), It.IsAny<long>()))
                .ReturnsAsync(null as MfaCode);

            _options.Setup(x => x.Value).Returns(new CommunicationsServiceProxyOptions
            {
                Enabled = true
            });

            _communicationsServiceProxy.Setup(x => x.SendSmsAsync(It.IsAny<SendSms>()))
                .ReturnsAsync(new SmsResponse
                {
                    Success = true
                });


            var code = "392817";
            _mfaContext.Setup(x => x.SaveAsync(It.IsAny<MfaCode>()))
                .ReturnsAsync(new MfaCode { Code = code });

            var msg = $"{Guid.NewGuid()}";
            _smsService.Setup(x => x.SendSmsToConsumer(It.Is<string>(b => b == phone), It.IsAny<string>()))
                .Returns(new SmsResponse
                {
                    Success = true,
                    ErrorMessage = msg
                });

            var handler = new SendSmsCodeCommandHandler(
                _contactContext.Object, _consumerStateContext.Object, _mfaContext.Object, _smsService.Object, _options.Object, _communicationsServiceProxy.Object);

            var result = await handler.Handle(new SendSmsCodeCommand { ConsumerId = 2910 }, default);

            Assert.Equal(code, result);
        }
    }
}
