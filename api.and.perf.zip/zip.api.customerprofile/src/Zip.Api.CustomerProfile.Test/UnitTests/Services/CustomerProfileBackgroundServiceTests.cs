using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
using Zip.MessageTypes.Sync;

namespace Zip.Api.CustomerProfile.Test.UnitTests.Services
{
    public class CustomerProfileBackgroundServiceTests
    {
        private readonly CustomerProfileBackgroundService _customerProfileService;
        private readonly Mock<ILogger<CustomerProfileBackgroundService>> _logger;
        private readonly Mock<IBus> _bus;

        public CustomerProfileBackgroundServiceTests()
        {
            _bus = new Mock<IBus>();
            _logger = new Mock<ILogger<CustomerProfileBackgroundService>>();

            _customerProfileService = new CustomerProfileBackgroundService(_bus.Object,
                                                                 _logger.Object);
        }

        [Fact]
        public async Task DeleteCustomerById_Should_Publish_Message_EventHub()
        {
            // Arrange
            var id = Guid.NewGuid();
            var (esRecords, dbRecords) = GetTestRecords(id);

            _bus.Setup(x => x.Publish(
                It.IsAny<CustomerProfileSyncDelete>(),
                It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _customerProfileService.DeleteCustomerById(id,
                    Guid.NewGuid().ToString(), CancellationToken.None);

            // Assert
            _bus.Verify(
                x => x.Publish(
                It.IsAny<CustomerProfileSyncDelete>(),
                It.IsAny<CancellationToken>()), Times.Once());
        }
        
        [Fact]
        public async Task UpdateCustomerById_Should_Publish_Message_EventHub()
        {
            // Arrange
            var id = Guid.NewGuid();
            var (esRecords, dbRecords) = GetTestRecords(id);

            _bus.Setup(x => x.Publish(
                    It.IsAny<CustomerProfileSyncUpdate>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _customerProfileService.UpdateCustomerById(id,
                Guid.NewGuid().ToString(), CancellationToken.None);

            // Assert
            _bus.Verify(
                x => x.Publish(
                    It.IsAny<CustomerProfileSyncUpdate>(),
                    It.IsAny<CancellationToken>()), Times.Once());
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