using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateAddress;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.KinesisProducer.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Consumers
{
    public class UpdateAddressCommandHandlerTest
    {
        private readonly IFixture _fixture;

        private readonly Mock<IOptions<KinesisSettings>> _settings;

        private readonly Mock<IKinesisProducer> _kinesisProducer;

        private readonly Mock<IMapper> _mapper;

        private readonly Mock<IAddressValidator> _addressValidator;

        private readonly Mock<IAddressContext> _addressContext;

        public UpdateAddressCommandHandlerTest()
        {
            _fixture = new Fixture();
            _settings = new Mock<IOptions<KinesisSettings>>();
            _kinesisProducer = new Mock<IKinesisProducer>();
            _mapper = new Mock<IMapper>();
            _addressValidator = new Mock<IAddressValidator>();
            _addressContext = new Mock<IAddressContext>();
        }

        [Fact]
        public void Given_NullAddressContext_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                    () => {
                        new UpdateAddressCommandHandler(
                                null,
                                _settings.Object,
                                _kinesisProducer.Object,
                                _mapper.Object,
                                _addressValidator.Object);
                    });
        }

        [Fact]
        public void Given_NullOptions_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                    () => {
                        new UpdateAddressCommandHandler(
                                _addressContext.Object,
                                null,
                                _kinesisProducer.Object,
                                _mapper.Object,
                                _addressValidator.Object);
                    });
        }

        [Fact]
        public void Given_NullKinesisProducer_ShouldThrow_ArgumentNullException()
        {
            _settings.Setup(x => x.Value).Returns(new KinesisSettings());

            Assert.Throws<ArgumentNullException>(
                    () => {
                        new UpdateAddressCommandHandler(
                                _addressContext.Object,
                                _settings.Object,
                                null,
                                _mapper.Object,
                                _addressValidator.Object);
                    });
        }

        [Fact]
        public void Given_NullMapper_ShouldThrow_ArgumentNullException()
        {
            _settings.Setup(x => x.Value).Returns(new KinesisSettings());

            Assert.Throws<ArgumentNullException>(
                    () => {
                        new UpdateAddressCommandHandler(
                                _addressContext.Object,
                                _settings.Object,
                                _kinesisProducer.Object,
                                null,
                                _addressValidator.Object);
                    });
        }

        [Fact]
        public void Given_NullAddressValidator_ShouldThrow_ArgumentNullException()
        {
            _settings.Setup(x => x.Value).Returns(new KinesisSettings());

            Assert.Throws<ArgumentNullException>(
                    () => {
                        new UpdateAddressCommandHandler(
                                _addressContext.Object,
                                _settings.Object,
                                _kinesisProducer.Object,
                                _mapper.Object,
                                null);
                    });
        }

        [Fact]
        public async Task Given_NullCountryCode_ShouldAssign_CountryId()
        {
            // Arrange
            _settings.Setup(x => x.Value)
                   .Returns(new KinesisSettings { Enabled = false });

            var handler = new UpdateAddressCommandHandler(
                    _addressContext.Object,
                    _settings.Object,
                    _kinesisProducer.Object,
                    _mapper.Object,
                    _addressValidator.Object);

            var request = new UpdateAddressCommand
            {
                Address = new Address
                {
                    CountryCode = null,
                    Country = new Country
                    {
                        Id = _fixture.Create<string>(),
                        Name = _fixture.Create<string>()
                    }
                }
            };

            // Action
            await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(request.Address.CountryCode, request.Address.Country.Id);
        }

        [Fact]
        public async Task Given_AddressValidatorEnabled_But_CountryIdIsAU_Should_CorrectAddress()
        {
            // Arrange
            _settings.Setup(x => x.Value)
                   .Returns(new KinesisSettings { Enabled = true });

            var unitType = _fixture.Create<string>();
            var unitNumber = _fixture.Create<string>();

            _addressValidator.Setup(x => x.IsEnabled).Returns(true);

            _addressValidator
                   .Setup(x =>
                                  x.ValidateByKelberAsync(
                                          It.IsAny<string>(),
                                          It.IsAny<string>(),
                                          It.IsAny<string>(),
                                          It.IsAny<string>(),
                                          It.IsAny<string>(),
                                          It.IsAny<string>()))
                   .ReturnsAsync(
                            new VerifyAddressResponse
                            {
                                DtResponse = new VerifyAddressInnerResponse
                                {
                                    Result = new List<VerifyAddressResponseResult>
                                    {
                                        new VerifyAddressResponseResult
                                        {
                                            UnitType = unitType,
                                            UnitNumber = unitNumber
                                        }
                                    }
                                }
                            });

            var handler = new UpdateAddressCommandHandler(
                    _addressContext.Object,
                    _settings.Object,
                    _kinesisProducer.Object,
                    _mapper.Object,
                    _addressValidator.Object);

            var request = new UpdateAddressCommand
            {
                Address = new Address
                {
                    CountryCode = null,
                    Country = new Country
                    {
                        Id = "AU",
                        Name = "Australia"
                    }
                }
            };

            // Action
            await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal($"{unitType} {unitNumber}", request.Address.UnitNumber);
        }
    }
}
