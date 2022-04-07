using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Domain.Entities.GoogleAddress;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class AddressSearchServiceTests : CommonTestsFixture
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory;

        private readonly Mock<IOptions<GoogleSettings>> _options;

        private readonly AddressSearchService _target;

        public AddressSearchServiceTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _options = new Mock<IOptions<GoogleSettings>>();
            _options.Setup(x => x.Value)
                    .Returns(new GoogleSettings
                     {
                         ApiKey = Guid.NewGuid()
                                      .ToString()
                     });

            _target = new AddressSearchService(_httpClientFactory.Object, _options.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Given_Invalid_GoogleApiKey_When_Instantiation_Should_Throw_ApiKeyNotFoundException(string googleApiKey)
        {
            // Arrange
            _options.Setup(x => x.Value)
                    .Returns(new GoogleSettings { ApiKey = googleApiKey });

            // Action
            Action action = () => { new AddressSearchService(_httpClientFactory.Object, _options.Object); };

            // Assert
            action.Should()
                  .Throw<ApiKeyNotFoundException>();
        }

        [Fact]
        public async Task Given_Empty_PlaceId_When_FindAndVerifyAsync_Should_Return_Null()
        {
            // Arrange
            var expect = Fixture.Build<GoogleAddressResponse>()
                                .Without(x => x.Predictions)
                                .Create();

            var mockHttpMessageHandler = new MockHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expect),
                                                Encoding.UTF8,
                                                "application/json")
                });

            var mockHttpClient = new HttpClient(mockHttpMessageHandler);

            _httpClientFactory
               .Setup(x => x.CreateClient(It.IsAny<string>()))
               .Returns(mockHttpClient);

            // Action
            var actual = Task.FromResult(await _target.FindAndVerifyAsync(Fixture.Create<string>()))
                             .Result;

            actual.Should()
                  .BeNull();
        }

        [Fact]
        public async Task Given_Valid_PlaceId_When_FindAndVerifyAsync_Should_Return_Null()
        {
            // Arrange - Mock Address Lookup
            var expectPrediction = Fixture.Create<Prediction>();
            var expectPredictions = new List<Prediction> { expectPrediction };

            var expectAddressResponse = Fixture
                                       .Build<GoogleAddressResponse>()
                                       .With(x => x.Predictions, expectPredictions)
                                       .Create();

            var searchAddressAsyncMockHttpMessageHandler = new MockHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectAddressResponse),
                                                Encoding.UTF8,
                                                "application/json")
                });

            var searchAddressAsyncMockHttpClient = new HttpClient(searchAddressAsyncMockHttpMessageHandler);

            // Arrange - Mock Place Lookup
            var expectPlaceDetailsResponse = Fixture.Create<GooglePlaceDetailsResponse>();
            expectPlaceDetailsResponse.Result.Address_Components[0]
                                      .Types[0] = GoogleAddressComponentType.Subpremise.ToString();

            var findPlaceAsyncMockHttpMessageHandler = new MockHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectPlaceDetailsResponse),
                                                Encoding.UTF8,
                                                "application/json")
                });

            var findPlaceAsyncMockHttpClient = new HttpClient(findPlaceAsyncMockHttpMessageHandler);

            _httpClientFactory
               .SetupSequence(x => x.CreateClient(It.IsAny<string>()))
               .Returns(searchAddressAsyncMockHttpClient)
               .Returns(findPlaceAsyncMockHttpClient);

            // Action
            var actual = Task.FromResult(await _target.FindAndVerifyAsync(Fixture.Create<string>()))
                             .Result;

            // Assert
            actual.UnitNumber.Should()
                  .Be(expectPlaceDetailsResponse.Result.Address_Components[0]
                                                .Long_Name);
        }

        [Fact]
        public async Task Given_Input_Value_When_SearchAddressAsync_Should_Work_Correctly()
        {
            // Arrange
            var expect = Fixture.Create<GoogleAddressResponse>();

            var mockHttpMessageHandler = new MockHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expect),
                                                Encoding.UTF8,
                                                "application/json")
                });

            var mockHttpClient = new HttpClient(mockHttpMessageHandler);

            _httpClientFactory
               .Setup(x => x.CreateClient(It.IsAny<string>()))
               .Returns(mockHttpClient);

            // Action
            var actual = Task.FromResult(await _target.SearchAddressAsync(Fixture.Create<string>()))
                             .Result;

            // Assert
            actual.Should()
                  .BeEquivalentTo(expect);
        }

        [Fact]
        public async Task Given_CountryCode_And_Keyword_When_SearchAsync_Should_Work_Correctly()
        {
            // Arrange - Mock Country Lookup
            var expectPrediction = Fixture.Create<Prediction>();
            var expectPredictions = new List<Prediction> { expectPrediction };

            var expectAddressResponse = Fixture
                                       .Build<GoogleAddressResponse>()
                                       .With(x => x.Predictions, expectPredictions)
                                       .Create();

            var searchAddressAsyncMockHttpMessageHandler = new MockHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectAddressResponse),
                                                Encoding.UTF8,
                                                "application/json")
                });

            var searchAddressAsyncMockHttpClient = new HttpClient(searchAddressAsyncMockHttpMessageHandler);

            _httpClientFactory
               .SetupSequence(x => x.CreateClient(It.IsAny<string>()))
               .Returns(searchAddressAsyncMockHttpClient);

            // Action
            var actual = Task.FromResult(await _target.SearchAsync(null, null))
                             .Result.ToList();

            // Assert
            actual.Should()
                  .HaveCount(1);
            actual.Single()
                  .Should()
                  .BeEquivalentTo(expectPrediction);
        }

        [Fact]
        public async Task Given_CountryCode_And_Keyword_When_SearchAddressWithCountryAsync_Should_Work_Correctly()
        {
            var expectAddressResponse = Fixture.Create<GoogleAddressResponse>();

            var searchAddressAsyncMockHttpMessageHandler = new MockHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectAddressResponse),
                                                Encoding.UTF8,
                                                "application/json")
                });

            var searchAddressAsyncMockHttpClient = new HttpClient(searchAddressAsyncMockHttpMessageHandler);

            _httpClientFactory
               .SetupSequence(x => x.CreateClient(It.IsAny<string>()))
               .Returns(searchAddressAsyncMockHttpClient);

            // Action
            var actual = Task.FromResult(await _target.SearchAddressWithCountryAsync(null, null))
                             .Result;

            // Assert
            actual.Should()
                  .BeEquivalentTo(expectAddressResponse);
        }

        [Fact]
        public async Task Given_PlaceId_When_FindPlaceAsync_Should_Work_Correctly()
        {
            // Arrange - Mock Place Lookup
            var expectPlaceDetailsResponse = Fixture.Create<GooglePlaceDetailsResponse>();
            expectPlaceDetailsResponse.Result.Address_Components[0]
                                      .Types[0] = GoogleAddressComponentType.Subpremise.ToString();

            var findPlaceAsyncMockHttpMessageHandler = new MockHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(expectPlaceDetailsResponse),
                                                Encoding.UTF8,
                                                "application/json")
                });

            var findPlaceAsyncMockHttpClient = new HttpClient(findPlaceAsyncMockHttpMessageHandler);

            _httpClientFactory
               .SetupSequence(x => x.CreateClient(It.IsAny<string>()))
               .Returns(findPlaceAsyncMockHttpClient);

            // Action
            var actual = Task.FromResult(await _target.FindPlaceAsync(Fixture.Create<string>()))
                             .Result;

            // Assert
            actual.UnitNumber.Should()
                  .Be(expectPlaceDetailsResponse.Result.Address_Components[0]
                                                .Long_Name);
        }

        [Fact]
        public void Given_Predictions_Null_In_GoogleAddressResponse_When_ParsePlaceId_Should_Return_Empty()
        {
            // Arrange
            var googleAddressResponse = Fixture.Create<GoogleAddressResponse>();
            googleAddressResponse.Predictions = null;

            // Action
            var actual = _target.ParsePlaceId(googleAddressResponse);

            // Assert
            actual.Should()
                  .BeEmpty();
        }

        [Fact]
        public void Given_Predictions_Is_Empty_In_GoogleAddressResponse_When_ParsePlaceId_Should_Return_Empty()
        {
            // Arrange
            var googleAddressResponse = Fixture.Create<GoogleAddressResponse>();
            googleAddressResponse.Predictions.Clear();

            // Action
            var actual = _target.ParsePlaceId(googleAddressResponse);

            // Assert
            actual.Should()
                  .BeEmpty();
        }

        [Fact]
        public void Given_Predictions_Place_Id_Is_Null_In_GoogleAddressResponse_When_ParsePlaceId_Should_Return_Empty()
        {
            // Arrange
            var expectPrediction = Fixture.Build<Prediction>()
                                          .Without(x => x.Place_Id)
                                          .Create();
            var expectPredictions = new List<Prediction> { expectPrediction };

            var googleAddressResponse = Fixture
                                       .Build<GoogleAddressResponse>()
                                       .With(x => x.Predictions, expectPredictions)
                                       .Create();

            // Action
            var actual = _target.ParsePlaceId(googleAddressResponse);

            // Assert
            actual.Should()
                  .BeEmpty();
        }

        [Fact]
        public void Given_Valid_Predictions_In_GoogleAddressResponse_When_ParsePlaceId_Should_Return_Correctly()
        {
            // Arrange
            var expectPlaceId = Fixture.Create<string>();
            var expectPrediction = Fixture.Build<Prediction>()
                                          .With(x => x.Place_Id, expectPlaceId)
                                          .Create();
            var expectPredictions = new List<Prediction> { expectPrediction };

            var googleAddressResponse = Fixture
                                       .Build<GoogleAddressResponse>()
                                       .With(x => x.Predictions, expectPredictions)
                                       .Create();

            // Action
            var actual = _target.ParsePlaceId(googleAddressResponse);

            // Assert
            actual.Should()
                  .Be(expectPlaceId);
        }

        [Fact]
        public void Given_GooglePlaceDetailsResponse_Is_Null_When_ParseAddress_Should_Return_Null()
        {
            var actual = _target.ParseAddress(null);

            actual.Should()
                  .BeNull();
        }

        [Fact]
        public void Given_GooglePlaceDetailsResponse_Result_Is_Null_When_ParseAddress_Should_Return_Null()
        {
            var googlePlaceDetailsResponse = Fixture.Build<GooglePlaceDetailsResponse>()
                                                    .Without(x => x.Result)
                                                    .Create();

            var actual = _target.ParseAddress(googlePlaceDetailsResponse);

            actual.Should()
                  .BeNull();
        }

        [Fact]
        public void
            Given_GooglePlaceDetailsResponse_Result_Address_Components_Is_Empty_When_ParseAddress_Should_Return_Null()
        {
            var googlePlaceDetailsResponse = Fixture.Create<GooglePlaceDetailsResponse>();
            googlePlaceDetailsResponse.Result.Address_Components.Clear();

            var actual = _target.ParseAddress(googlePlaceDetailsResponse);

            actual.Should()
                  .BeNull();
        }

        [Fact]
        public void Given_No_AddressComponent_Types_Or_Geometry_When_ParseAddress_All_Details_Should_Be_Empty()
        {
            // Arrange
            var googlePlaceDetailsResponse = Fixture.Create<GooglePlaceDetailsResponse>();
            googlePlaceDetailsResponse.Result.Address_Components.ForEach(x => x.Types.Clear());
            googlePlaceDetailsResponse.Result.Geometry = null;

            // Action
            var actual = _target.ParseAddress(googlePlaceDetailsResponse);

            // Assert
            actual.UnitNumber.Should()
                  .BeNullOrEmpty();
            actual.StreetNumber.Should()
                  .BeNullOrEmpty();
            actual.StreetName.Should()
                  .BeNullOrEmpty();
            actual.Suburb.Should()
                  .BeNullOrEmpty();
            actual.State.Should()
                  .BeNullOrEmpty();
            actual.PostCode.Should()
                  .BeNullOrEmpty();
            actual.Country.Should()
                  .BeNullOrEmpty();
            actual.CountryCode.Should()
                  .BeNullOrEmpty();
            actual.FormattedAddress.Should()
                  .BeNullOrEmpty();
            actual.Latitude.Should()
                  .Be(0);
            actual.Longitude.Should()
                  .Be(0);
        }

        [Fact]
        public void Given_Valid_AddressComponent_Type_When_ParseAddress_All_Details_Should_Be_Correct()
        {
            // Arrange
            var expectLongName = Fixture.Create<string>();
            var expectShortName = Fixture.Create<string>();

            var googlePlaceDetailsResponse = Fixture.Create<GooglePlaceDetailsResponse>();
            foreach (var type in Enum.GetNames(typeof(GoogleAddressComponentType))
                                     .ToList())
            {
                googlePlaceDetailsResponse.Result.Address_Components.Add(
                    new AddressComponent
                    {
                        Types = new List<string> { type },
                        Long_Name = expectLongName,
                        Short_Name = expectShortName
                    });
            }

            // Action
            var actual = _target.ParseAddress(googlePlaceDetailsResponse);

            // Assert
            actual.UnitNumber.Should()
                  .Be(expectLongName);
            actual.StreetNumber.Should()
                  .Be(expectLongName);
            actual.StreetName.Should()
                  .Be(expectLongName);
            actual.Suburb.Should()
                  .Be(expectLongName);
            actual.State.Should()
                  .Be(expectShortName);
            actual.PostCode.Should()
                  .Be(expectLongName);
            actual.Country.Should()
                  .Be(expectLongName);
            actual.CountryCode.Should()
                  .Be(expectShortName);
            actual.FormattedAddress.Should()
                  .BeNullOrEmpty();
            actual.Latitude.Should()
                  .Be(googlePlaceDetailsResponse.Result.Geometry.Location.Lat);
            actual.Longitude.Should()
                  .Be(googlePlaceDetailsResponse.Result.Geometry.Location.Lng);
        }
    }
}
