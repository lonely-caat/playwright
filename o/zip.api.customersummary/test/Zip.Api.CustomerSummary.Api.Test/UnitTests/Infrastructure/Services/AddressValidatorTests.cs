using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class AddressValidatorTests
    {
        private readonly IAddressValidator _target;
        
        private readonly IFixture _fixture;

        private readonly IOptions<AddressVerificationProxyOptions> _options;

        private readonly Mock<IAddressVerificationProxy> _proxy;
        
        public AddressValidatorTests()
        {
            _fixture = new Fixture();
            
            var proxyOptions = _fixture.Create<AddressVerificationProxyOptions>();
            _options = Options.Create(proxyOptions);
            
            _proxy = new Mock<IAddressVerificationProxy>();

            _target = new AddressValidator(_proxy.Object, _options);
        }

        [Fact]
        public void Given_AddressVerificationProxy_Is_Null_Should_Throw_ArgumentNullException()
        {
            Action action = () => { new AddressValidator(null, _options); };

            action.Should().Throw<ArgumentException>();
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Given_Option_When_IsEnabled_Is_Retrieved_Should_Return_Correctly(bool optionIsEnabled)
        {
            // Arrange
            var proxyOptions = _fixture.Build<AddressVerificationProxyOptions>()
                   .With(x => x.Enabled, optionIsEnabled)
                   .Create();
            
            var option = Options.Create(proxyOptions);

            // Action
            var target = new AddressValidator(_proxy.Object, option);
            
            // Assert
            target.IsEnabled.Should().Be(optionIsEnabled);
        }

        [Theory]
        [InlineData("9", "Unit number or level is incorrect or unspecified.")]
        [InlineData("18", "Unit number or level is incorrect or unspecified.")]
        [InlineData("20", "Address incorrect. Please input more address details.")]
        [InlineData("5", "Address incorrect. Please input more address details.")]
        [InlineData("default", "MatchTypeDescription")]
        public void Given_VerifyAddressInnerResponse_Has_No_ErrorMessage_And_Verification_Failed_When_ValidateByKelberAsync_Should_Throw_AddressValidationException(
            string matchType,
            string validationMessage)
        {
            // Arrange
            var expectedValidationMessage = string.Join(Environment.NewLine, validationMessage);

            var verifyAddressInnerResponse = _fixture
                   .Build<VerifyAddressInnerResponse>()
                   .With(x => x.ErrorMessage, string.Empty)
                   .With(x => x.Result,
                         new List<VerifyAddressResponseResult>
                         {
                             new VerifyAddressResponseResult
                             {
                                 MatchType = matchType,
                                 MatchTypeDescription = "MatchTypeDescription"
                             }
                         })
                   .Create();

            _proxy.Setup(x => x.VerifyAddress(It.IsAny<string>(), It.IsAny<VerifyAddressRequest>()))
                   .Returns(Task.FromResult(new VerifyAddressResponse { DtResponse = verifyAddressInnerResponse }));

            // Action
            Func<Task> func = async () =>
            {
                await _target.ValidateByKelberAsync(
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty);
            };

            // Assert
            func.Should()
                   .Throw<AddressValidationException>()
                   .WithMessage(expectedValidationMessage);
        }

        [Fact]
        public async Task Given_VerifyAddressInnerResponse_Has_No_ErrorMessage_And_Verification_Succeed_When_ValidateByKelberAsync_Should_Return_Correctly()
        {
            // Arrange
            var expectedVerifyAddressResponseResult = _fixture.Build<VerifyAddressResponseResult>()
                   .With(x => x.MatchType, "0")
                   .Create();
            
            var expectedVerifyAddressResponse = _fixture
                   .Build<VerifyAddressInnerResponse>()
                   .With(x => x.RequestId, _fixture.Create<string>())
                   .With(x => x.ResultCount, _fixture.Create<string>())
                   .With(x => x.ErrorMessage, string.Empty)
                   .With(x => x.Result,
                         new List<VerifyAddressResponseResult>
                         {
                             expectedVerifyAddressResponseResult
                         })
                   .Create();

            _proxy.Setup(x => x.VerifyAddress(It.IsAny<string>(), It.IsAny<VerifyAddressRequest>()))
                   .Returns(Task.FromResult(new VerifyAddressResponse { DtResponse = expectedVerifyAddressResponse }));
            
            // Action
            var actual = await _target.ValidateByKelberAsync(
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty);

            // Assert
            actual.DtResponse.Should().BeEquivalentTo(expectedVerifyAddressResponse);
            actual.DtResponse.RequestId.Should().BeEquivalentTo(expectedVerifyAddressResponse.RequestId);
            actual.DtResponse.ResultCount.Should().BeEquivalentTo(expectedVerifyAddressResponse.ResultCount);

            var actualVerifyAddressResponseResult = actual.DtResponse.Result.FirstOrDefault();
            actualVerifyAddressResponseResult.Should().NotBeNull();
            actualVerifyAddressResponseResult?.DPID.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.DPID);
            actualVerifyAddressResponseResult?.BuildingName.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.BuildingName);
            actualVerifyAddressResponseResult?.AddressLine.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AddressLine);
            actualVerifyAddressResponseResult?.AddressBlockLine1.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AddressBlockLine1);
            actualVerifyAddressResponseResult?.AddressBlockLine2.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AddressBlockLine2);
            actualVerifyAddressResponseResult?.AddressBlockLine3.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AddressBlockLine3);
            actualVerifyAddressResponseResult?.AddressBlockLine4.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AddressBlockLine4);
            actualVerifyAddressResponseResult?.AddressBlockLine5.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AddressBlockLine5);
            actualVerifyAddressResponseResult?.AddressBlockLine6.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AddressBlockLine6);
            actualVerifyAddressResponseResult?.AddressBlockLine7.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AddressBlockLine7);
            actualVerifyAddressResponseResult?.FieldChanges.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.FieldChanges);
            actualVerifyAddressResponseResult?.AustraliaPostBarcode.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AustraliaPostBarcode);
            actualVerifyAddressResponseResult?.AustraliaPostBarcodeSortPlan.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AustraliaPostBarcodeSortPlan);
            actualVerifyAddressResponseResult?.LevelType.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.LevelType);
            actualVerifyAddressResponseResult?.LevelNumber.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.LevelNumber);
            actualVerifyAddressResponseResult?.LotNumber.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.LotNumber);
            actualVerifyAddressResponseResult?.StreetNumberSuffix1.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.StreetNumberSuffix1);
            actualVerifyAddressResponseResult?.StreetNumber2.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.StreetNumber2);
            actualVerifyAddressResponseResult?.StreetNumberSuffix2.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.StreetNumberSuffix2);
            actualVerifyAddressResponseResult?.PostBoxNumber.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.PostBoxNumber);
            actualVerifyAddressResponseResult?.PostBoxNumberPrefix.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.PostBoxNumberPrefix);
            actualVerifyAddressResponseResult?.PostBoxNumberSuffix.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.PostBoxNumberSuffix);
            actualVerifyAddressResponseResult?.StreetSuffix.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.StreetSuffix);
            actualVerifyAddressResponseResult?.PostBoxType.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.PostBoxType);
            actualVerifyAddressResponseResult?.AltStreetName.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AltStreetName);
            actualVerifyAddressResponseResult?.AltStreetType.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AltStreetType);
            actualVerifyAddressResponseResult?.AltStreetSuffix.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AltStreetSuffix);
            actualVerifyAddressResponseResult?.AltLocality.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AltLocality);
            actualVerifyAddressResponseResult?.AltPostcode.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.AltPostcode);
            actualVerifyAddressResponseResult?.UnknownData.Should().BeEquivalentTo(expectedVerifyAddressResponseResult.UnknownData);
        }

        [Fact]
        public void Given_VerifyAddressInnerResponse_Has_ErrorMessage_When_ValidateByKelberAsync_Should_Throw_AddressValidationException()
        {
            // Arrange
            const string ErrorMessage = "ErrorMessage";
            var expectedExceptionMessage = $"{ErrorMessage} - Kleber";
            
            var expectedVerifyAddressResponse = _fixture
                   .Build<VerifyAddressInnerResponse>()
                   .With(x => x.ErrorMessage, ErrorMessage)
                   .Create();

            _proxy.Setup(x => x.VerifyAddress(It.IsAny<string>(), It.IsAny<VerifyAddressRequest>()))
                   .Returns(Task.FromResult(new VerifyAddressResponse { DtResponse = expectedVerifyAddressResponse }));
            
            // Action
            Func<Task> func = async () =>
            {
                await _target.ValidateByKelberAsync(
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty);
            };

            //Assert
            func.Should()
                   .Throw<AddressValidationException>()
                   .WithMessage(expectedExceptionMessage);
        }
    }
}
