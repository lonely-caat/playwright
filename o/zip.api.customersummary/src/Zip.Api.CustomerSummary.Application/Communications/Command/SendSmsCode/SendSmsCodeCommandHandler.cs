using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;
using Zip.Api.CustomerSummary.Infrastructure.Services.SmsService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Communications.Command.SendSmsCode
{
    public class SendSmsCodeCommandHandler : IRequestHandler<SendSmsCodeCommand, string>
    {
        private readonly IConsumerStatContext _consumerStatContext;
        private readonly IContactContext _contactContext;
        private readonly IMfaContext _mfaContext;
        private readonly ISmsService _smsService;
        private readonly CommunicationsServiceProxyOptions _communicationsServiceProxyOptions;
        private readonly ICommunicationsServiceProxy _communicationsServiceProxy;

        public SendSmsCodeCommandHandler(
            IContactContext contactContext,
            IConsumerStatContext consumerStatContext,
            IMfaContext mfaContext,
            ISmsService smsService,
            IOptions<CommunicationsServiceProxyOptions> communicationsServiceProxyOptions,
            ICommunicationsServiceProxy communicationsServiceProxy)
        {
            _contactContext = contactContext ?? throw new ArgumentNullException(nameof(contactContext));
            _consumerStatContext = consumerStatContext ?? throw new ArgumentNullException(nameof(consumerStatContext));
            _mfaContext = mfaContext ?? throw new ArgumentNullException(nameof(mfaContext));
            _smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
            _communicationsServiceProxyOptions = communicationsServiceProxyOptions?.Value;
            _communicationsServiceProxy = communicationsServiceProxy;
        }

        public async Task<string> Handle(SendSmsCodeCommand request, CancellationToken cancellationToken)
        {
            await _mfaContext.ExpireTokensAsync(MfaEntityType.Customer, request.ConsumerId);

            var mobile = await _contactContext.GetMobileAsync(request.ConsumerId);

            if (string.IsNullOrEmpty(mobile))
            {
                throw new MobileNotFoundException(request.ConsumerId);
            }

            var stat = await _consumerStatContext.GetByConsumerIdAsync(request.ConsumerId);

            if (stat == null)
            {
                stat = new ConsumerStat
                {
                    ConsumerId = request.ConsumerId,
                    SmsSendRetryCount = 0,
                    SmsVerificationRetryCount = 0,
                    VerificationTimeStamp = DateTime.Now,
                    SendTimeStamp = DateTime.Now,
                };
            }

            stat.SmsSendRetryCount++;
            stat.SendTimeStamp = DateTime.Now;

            await _consumerStatContext.SaveAsync(stat);

            byte attempt = 1;
            var previousCode = await _mfaContext.GetTokenAsync(MfaEntityType.Customer, request.ConsumerId);
            if (previousCode != null)
            {
                attempt += previousCode.Attempt;
            }

            var secret = $"{MfaEntityType.Customer}{request.ConsumerId}{MfaSmsSources.Admin}";
            var code = _mfaContext.CreateToken(secret, attempt, mobile, MfaType.PhoneNumber);

            var smsCodeCreated = await _mfaContext.SaveAsync(
                new MfaCode
                {
                    Attempt = attempt,
                    ConsumerId = request.ConsumerId,
                    Destination = mobile,
                    PhoneNumber = mobile,
                    EntityId = request.ConsumerId,
                    Code = code,
                    MfaEntityType = MfaEntityType.Customer,
                    MfaType = MfaType.PhoneNumber
                });

            SmsResponse response;

            if (_communicationsServiceProxyOptions.Enabled)
            {
                response = await _communicationsServiceProxy.SendSmsAsync(new SendSms
                {
                    Message = $"Your SMS Code is {code}",
                    PhoneNumber = mobile,
                    ConsumerId = request.ConsumerId
                });
            }
            else
            {
                response = _smsService.SendSmsToConsumer(mobile, $"Your SMS Code is {code}");
            }

            if (!response.Success)
            {
                throw new SmsDeliveryException(response.ErrorMessage);
            }

            return smsCodeCreated.Code;
        }
    }
}
