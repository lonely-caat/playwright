using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerProfile.Services;
using Zip.CustomerAcquisition.Core.Kafka.MessageBus;
using Zip.CustomerProfile.Contracts;
using Zip.CustomerProfile.Data.ElasticSearch.Entities;
using Zip.CustomerProfile.Data.ElasticSearch.Models;
using Zip.CustomerProfile.Data.Interfaces;
using Zip.CustomerProfile.Data.Models;

namespace Zip.Api.CustomerProfile.Test.UnitTests.Services
{
    public class CustomerProfileServiceTests
    {
        private readonly CustomerProfileService _customerProfileService;
        private readonly Mock<IDataProvider> _dataAccessProvider;
        private readonly Mock<IElasticSearchDataProvider> _elasticSearchService;
        private readonly Mock<ILogger<CustomerProfileService>> _logger;
        private readonly Mock<IMapper> _mapper;

        public CustomerProfileServiceTests()
        {
            _elasticSearchService = new Mock<IElasticSearchDataProvider>();
            _dataAccessProvider = new Mock<IDataProvider>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<CustomerProfileService>>();

            _customerProfileService = new CustomerProfileService(_elasticSearchService.Object,
                                                                 _dataAccessProvider.Object,
                                                                 _mapper.Object,
                                                                 _logger.Object);
        }

        [Fact]
        public async Task GetCustomersById_should_return_empty_result_when_Ids_are_empty()
        {
            // Act
            var actualResult = await _customerProfileService.GetCustomersById(Array.Empty<Guid>(), "", null);

            // Assert
            Assert.Empty(actualResult);
        }

        [Fact]
        public async Task GetCustomersById_should_return_results_for_customer_ids()
        {
            // Arrange
            var id = Guid.NewGuid();
            var (_, dbRecords) = GetTestRecords(id);
            _dataAccessProvider
                .Setup(x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>())).Returns(Task.FromResult(dbRecords));

            // Act
            var actualResult = await _customerProfileService.GetCustomersById(new[] {id}, "", null);

            // Assert
            Assert.NotEmpty(actualResult);
            Assert.Equal(id, actualResult.First().Id);
        }

        [Fact]
        public async Task GetCustomersByMobile_should_return_empty_result_when_mobile_number_is_empty()
        {
            // Act
            var actualResult = await _customerProfileService.GetCustomersByMobile(string.Empty, "", null);

            // Assert
            Assert.Empty(actualResult);
        }

        [Fact]
        public async Task GetCustomersByMobile_should_return_results_from_es_when_requested_fields_are_available()
        {
            // Arrange
            var id = Guid.NewGuid();
            var (esRecords, _) = GetTestRecords(id);
            _elasticSearchService.Setup(x =>
                    x.SearchDocumentsAsync(It.IsAny<ElasticSearchQueryModel>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(esRecords));

            // Act
            var actualResult = await _customerProfileService.GetCustomersByMobile("01234567", "email");

            // Assert
            Assert.NotEmpty(actualResult);
            _dataAccessProvider.Verify(
                x => x.GetCustomersByIdsAsync(It.IsAny<IList<Guid>>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>()), Times.Never);
        }

        [Fact]
        public async Task GetCustomersByMobile_should_return_results_from_db_when_requested_fields_are_not_available()
        {
            // Arrange
            var id = Guid.NewGuid();
            var (esRecords, dbRecords) = GetTestRecords(id);
            _dataAccessProvider
                .Setup(x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>())).Returns(Task.FromResult(dbRecords));
            _elasticSearchService.Setup(x =>
                    x.SearchDocumentsAsync(It.IsAny<ElasticSearchQueryModel>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(esRecords));

            // Act
            var actualResult =
                await _customerProfileService.GetCustomersByMobile("01234567", It.IsAny<string>(), "accounts");

            // Assert
            actualResult.Should().NotBeEmpty();
            _dataAccessProvider.Verify(
                x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>()), Times.Once);
        }

        [Fact]
        public async Task GetCustomersByKeyword_should_return_results_from_es_when_requested_fields_are_available()
        {
            // Arrange
            var id = Guid.NewGuid();
            var (esRecords, dbRecords) = GetTestRecords(id);

            _dataAccessProvider
                .Setup(x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>())).Returns(Task.FromResult(dbRecords));
            _elasticSearchService.Setup(x =>
                    x.SearchDocumentsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(esRecords));
            // Act
            var actualResult =
                await _customerProfileService.GetCustomersByKeyword(It.IsAny<string>(), It.IsAny<string>(), "email");

            // Assert
            actualResult.Should().NotBeEmpty();
            _dataAccessProvider.Verify(
                x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>()), Times.Never);
        }

        [Fact]
        public async Task GetCustomersByKeyword_should_return_results_from_db_when_requested_fields_are_not_available()
        {
            // Arrange
            var id = Guid.NewGuid();
            var (esRecords, dbRecords) = GetTestRecords(id);

            _dataAccessProvider
                .Setup(x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>())).Returns(Task.FromResult(dbRecords));
            _elasticSearchService.Setup(x =>
                    x.SearchDocumentsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(esRecords));

            // Act
            var actualResult =
                await _customerProfileService.GetCustomersByKeyword("test", It.IsAny<string>(), "accounts");

            // Assert
            actualResult.Should().NotBeEmpty();
            _dataAccessProvider.Verify(
                x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetCustomersByFacebookId_should_return_empty_results_when_facebook_id_is_null_or_empty(
            string facebookId)
        {
            var results = await _customerProfileService.GetCustomersByFacebookId(facebookId, "", null);
            results.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetCustomersByPaypalId_should_return_empty_results_when_paypal_id_is_null_or_empty(
            string paypalId)
        {
            var results = await _customerProfileService.GetCustomersByPaypalId(paypalId, "", null);
            results.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCustomersByComparableAddress_should_return_empty_result_when_comparable_address_is_empty()
        {
            // Act
            var actualResult = await _customerProfileService.GetCustomersByComparableAddress(string.Empty, "", null);

            // Assert
            actualResult.Should().BeEmpty();
        }

        [Fact]
        public async Task
            GetCustomersByVedaReferenceNumber_should_return_empty_result_when_veda_reference_number_is_empty()
        {
            // Act
            var actualResult = await _customerProfileService.GetCustomersByVedaReferenceNumber(string.Empty, "", null);

            // Assert
            actualResult.Should().BeEmpty();
        }

        [Fact]
        public async Task
            GetCustomersByVedaReferenceNumber_should_return_result_from_db_when_requested_field_is_available()
        {
            IEnumerable<Customer> result = new List<Customer> {new Customer {VedaReferenceNumber = "1234567"}};
            var (esRecords, _) = GetTestRecords(It.IsAny<Guid>());

            _dataAccessProvider
                .Setup(x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>())).Returns(Task.FromResult(result));

            _elasticSearchService.Setup(x =>
                    x.SearchDocumentsAsync(It.IsAny<ElasticSearchQueryModel>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(esRecords));

            // Act
            var actualResult =
                await _customerProfileService.GetCustomersByVedaReferenceNumber("1234567", "", "vedaReferenceNumber");

            // Assert
            Assert.NotEmpty(actualResult);
            _dataAccessProvider.Verify(
                x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>()), Times.Never());
            _elasticSearchService.Verify(x =>
                    x.SearchDocumentsAsync(It.IsAny<ElasticSearchQueryModel>(), It.IsAny<string>(), It.IsAny<int>()),
                Times.Once());
        }

        [Fact]
        public async Task
            GetCustomersByComparableAddress_should_return_results_from_es_when_requested_fields_are_available()
        {
            // Arrange
            var id = Guid.NewGuid();
            var (esRecords, dbRecords) = GetTestRecords(id);

            _dataAccessProvider
                .Setup(x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>())).Returns(Task.FromResult(dbRecords));
            _elasticSearchService.Setup(x =>
                    x.SearchDocumentsAsync(It.IsAny<ElasticSearchQueryModel>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(esRecords));

            // Act
            var actualResult =
                await _customerProfileService.GetCustomersByComparableAddress("1/2 John 2020",
                    "residentialComparableAddress");

            // Assert
            actualResult.Should().NotBeEmpty();
            _dataAccessProvider.Verify(
                x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>()), Times.Never);
        }

        [Fact]
        public async Task
            GetCustomersByComparableAddress_should_return_results_from_db_when_requested_fields_are_not_available()
        {
            // Arrange
            var id = Guid.NewGuid();
            var (esRecords, dbRecords) = GetTestRecords(id);

            _dataAccessProvider
                .Setup(x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>())).Returns(Task.FromResult(dbRecords));
            _elasticSearchService.Setup(x =>
                    x.SearchDocumentsAsync(It.IsAny<ElasticSearchQueryModel>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(esRecords));

            // Act
            var actualResult =
                await _customerProfileService.GetCustomersByComparableAddress("1/2 John 2020",
                    Guid.NewGuid().ToString(), "accounts");

            // Assert
            actualResult.Should().NotBeEmpty();
            _dataAccessProvider.Verify(
                x => x.GetCustomersAsync(It.IsAny<CustomerQuery>(), It.IsAny<string>(),
                    It.IsAny<CustomerResponseOptions>()), Times.Once());
        }

        private (IEnumerable<CustomerEntity>, IEnumerable<Customer>) GetTestRecords(Guid guid)
        {
            var esRecords = new List<CustomerEntity>
            {
                new CustomerEntity
                {
                    Id = guid.ToString(),
                    GivenName = "test data",
                    Email = "some@thing.com",
                    ResidentialComparableAddress = "1/2 John 2020"
                }
            };

            var dbRecords = new List<Customer>
            {
                new Customer
                {
                    Id = guid,
                    GivenName = "test data",
                    Email = "some@thing.com"
                }
            };

            return (esRecords, dbRecords);
        }
    }
}