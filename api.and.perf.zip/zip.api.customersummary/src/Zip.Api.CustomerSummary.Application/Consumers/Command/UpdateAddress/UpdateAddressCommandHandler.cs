using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Kinesis;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using KinesisCustomerRecord = Zip.Api.CustomerSummary.Domain.Entities.Kinesis.KinesisCustomerRecord;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand>
    {
        private readonly IAddressContext _addressContext;
        private readonly KinesisSettings _settings;
        private readonly IKinesisProducer _kinesisProducer;
        private readonly IMapper _mapper;
        private readonly IAddressValidator _addressValidator;

        public UpdateAddressCommandHandler(
            IAddressContext addressContext,
            IOptions<KinesisSettings> settings,
            IKinesisProducer kinesisProducer,
            IMapper mapper,
            IAddressValidator addressValidator)
        {
            _addressContext = addressContext ?? throw new ArgumentNullException(nameof(addressContext));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _kinesisProducer = kinesisProducer ?? throw new ArgumentNullException(nameof(kinesisProducer));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _addressValidator = addressValidator ?? throw new ArgumentNullException(nameof(addressValidator));
        }

        public async Task<Unit> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Address.CountryCode))
            {
                request.Address.CountryCode = request.Address.Country.Id;
            }

            // The address verification only applies to Australian addresses as Kleber only provides the service to Australia.
            if (_addressValidator.IsEnabled && request.Address.Country.Id.Equals("AU", StringComparison.OrdinalIgnoreCase))
            {
                var addressVerifyResponse =
                     await _addressValidator.ValidateByKelberAsync(
                         request.Address.UnitNumber,
                         request.Address.StreetNumber,
                         request.Address.StreetName,
                         request.Address.Suburb,
                         request.Address.PostCode,
                         request.Address.State);

                var addressCorrected = addressVerifyResponse.DtResponse.Result.FirstOrDefault();

                if (addressCorrected != null)
                {
                    if (!string.IsNullOrEmpty(addressCorrected.UnitType) && !(string.IsNullOrEmpty(addressCorrected.UnitNumber)))
                    {
                        request.Address.UnitNumber = $"{addressCorrected.UnitType} {addressCorrected.UnitNumber}";
                    }
                    else if (!string.IsNullOrEmpty(addressCorrected.UnitNumber))
                    {
                        request.Address.UnitNumber = addressCorrected.UnitNumber;
                    }

                    request.Address.StreetName = $"{addressCorrected.StreetName} {addressCorrected.StreetType}";
                    if (!string.IsNullOrEmpty(addressCorrected.StreetNumber1) && !string.IsNullOrEmpty(addressCorrected.StreetNumber2))
                    {
                        request.Address.StreetNumber = $"{addressCorrected.StreetNumber1}-{addressCorrected.StreetNumber2}";
                    }
                    else
                    {
                        request.Address.StreetNumber = addressCorrected.StreetNumber1;
                    }

                    request.Address.Suburb = addressCorrected.Locality;
                    request.Address.PostCode = addressCorrected.Postcode;
                    request.Address.State = addressCorrected.State;
                }
            }

            await _addressContext.UpdateAsync(request.ConsumerId, request.Address);

            await PutKinesisRecordAsync(request.ConsumerId, request.Address);

            return Unit.Value;
        }

        private async Task PutKinesisRecordAsync(long consumerId, Address address)
        {
            if (_settings.Enabled)
            {
                var kinesisAddress = _mapper.Map<AddressDetail>(address);
                var kinesisRecord = new KinesisCustomerRecord
                {
                    ConsumerId = consumerId,
                    BillingAddress = kinesisAddress
                };
                var data = JsonConvert.SerializeObject(kinesisRecord, SerializerSettings);

                await _kinesisProducer.PutRecord(_settings.KinesisStreamName, data, consumerId.ToString());
            }
        }
        
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }
}
