using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService.Models;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class VcnCardServiceTests : CommonTestsFixture
    {
        private readonly Mock<IVcnCardsApiProxy> _vcnCardsApiProxy;

        private readonly VcnCardService _target;
        
        public VcnCardServiceTests()
        {
            _vcnCardsApiProxy = new Mock<IVcnCardsApiProxy>();
            _target = new VcnCardService(_vcnCardsApiProxy.Object, Mapper);
        }
        
        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Is_Ok_When_GetCardAsync_Then_Response_Should_Be_Correct()
        {
            // Arrange
            var cardId = Guid.NewGuid();
            var cardResponse = Fixture.Build<CardResponse>().With(x => x.Id, cardId.ToString).Create();
            var data = cardResponse.ToJsonString();
            var expected = Mapper.Map<Card>(cardResponse);
            
            _vcnCardsApiProxy.Setup(x => x.GetCardAsync(cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Content = new StringContent(data)
                              });

            // Act
            var actual = await _target.GetCardAsync(cardId, CancellationToken);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Has_Error_When_GetCardAsync_Should_Throw_Exception()
        {
            // Arrange
            var cardId = Guid.NewGuid();

            _vcnCardsApiProxy.Setup(x => x.GetCardAsync(cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.InternalServerError
                              });

            // Act
            Func<Task> func = async () => { await _target.GetCardAsync(cardId, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<VcnCardsApiException>();
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Is_Ok_When_GetCardsAsync_Then_Response_Should_Be_Correct()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var accountId = Fixture.Create<long?>();
            var cardResponses = Fixture.Build<CardResponse>()
                                       .With(x => x.CustomerId, customerId.ToString)
                                       .CreateMany();
            var rootCardsResponse = new RootCardsResponse { Cards = cardResponses.ToList() };
            var data = rootCardsResponse.ToJsonString();
            var expected = Mapper.Map<RootCards>(rootCardsResponse);

            _vcnCardsApiProxy.Setup(x => x.GetCardsAsync(customerId, accountId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Content = new StringContent(data)
                              });

            // Act
            var actual = await _target.GetCardsAsync(customerId, accountId, CancellationToken);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Has_Error_When_GetCardsAsync_Should_Throw_Exception()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            
            _vcnCardsApiProxy.Setup(x => x.GetCardsAsync(customerId, It.IsAny<long?>(), It.IsAny<CancellationToken>()))
                             .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.InternalServerError
                              });

            // Act
            Func<Task> func = async () => { await _target.GetCardsAsync(customerId); };

            // Assert
            await func.Should().ThrowAsync<VcnCardsApiException>();
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Is_Ok_When_BlockCardAsync_Then_Proxy_Should_Be_Invoked_Correctly()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();
            var cardId = Guid.NewGuid();
            var cardResponse = Fixture.Build<CardResponse>()
                                      .With(x => x.Id, cardId.ToString)
                                      .With(x => x.CustomerId, customerId)
                                      .Create();
            var data = cardResponse.ToJsonString();

            _vcnCardsApiProxy.Setup(x => x.GetCardAsync(cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Content = new StringContent(data)
                              });

            _vcnCardsApiProxy.Setup(x => x.BlockCardAsync(customerId, cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Content = new StringContent(data)
                              });

            // Act
            await _target.BlockCardAsync(cardId, CancellationToken);

            // Assert
            _vcnCardsApiProxy.Verify(x => x.GetCardAsync(cardId, CancellationToken), Times.Once);
            _vcnCardsApiProxy.Verify(x => x.BlockCardAsync(customerId, cardId, CancellationToken), Times.Once);
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Has_Error_When_BlockCardAsync_Should_Throw_Exception()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();
            var cardId = Guid.NewGuid();
            var cardResponse = Fixture.Build<CardResponse>()
                                      .With(x => x.Id, cardId.ToString)
                                      .With(x => x.CustomerId, customerId)
                                      .Create();
            var data = cardResponse.ToJsonString();

            _vcnCardsApiProxy.Setup(x => x.GetCardAsync(cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.OK,
                                  Content = new StringContent(data)
                              });

            _vcnCardsApiProxy.Setup(x => x.BlockCardAsync(customerId, cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.InternalServerError
                              });

            // Act
            Func<Task> func = async () => { await _target.BlockCardAsync(cardId, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<VcnCardsApiException>();
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Is_Ok_When_UnblockCardAsync_Then_Proxy_Should_Be_Invoked_Correctly()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();
            var cardId = Guid.NewGuid();
            var cardResponse = Fixture.Build<CardResponse>()
                                      .With(x => x.Id, cardId.ToString)
                                      .With(x => x.CustomerId, customerId)
                                      .Create();
            var data = cardResponse.ToJsonString();

            _vcnCardsApiProxy.Setup(x => x.GetCardAsync(cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                             {
                                 StatusCode = HttpStatusCode.OK,
                                 Content = new StringContent(data)
                             });

            _vcnCardsApiProxy.Setup(x => x.UnblockCardAsync(customerId, cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                             {
                                 StatusCode = HttpStatusCode.OK,
                                 Content = new StringContent(data)
                             });

            // Act
            await _target.UnblockCardAsync(cardId, CancellationToken);

            // Assert
            _vcnCardsApiProxy.Verify(x => x.GetCardAsync(cardId, CancellationToken), Times.Once);
            _vcnCardsApiProxy.Verify(x => x.UnblockCardAsync(customerId, cardId, CancellationToken), Times.Once);
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Has_Error_When_UnblockCardAsync_Should_Throw_Exception()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();
            var cardId = Guid.NewGuid();
            var cardResponse = Fixture.Build<CardResponse>()
                                      .With(x => x.Id, cardId.ToString)
                                      .With(x => x.CustomerId, customerId)
                                      .Create();
            var data = cardResponse.ToJsonString();

            _vcnCardsApiProxy.Setup(x => x.GetCardAsync(cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                             {
                                 StatusCode = HttpStatusCode.OK,
                                 Content = new StringContent(data)
                             });

            _vcnCardsApiProxy.Setup(x => x.UnblockCardAsync(customerId, cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                             {
                                 StatusCode = HttpStatusCode.InternalServerError
                             });

            // Act
            Func<Task> func = async () => { await _target.UnblockCardAsync(cardId, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<VcnCardsApiException>();
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Is_Ok_When_CloseCardAsync_Then_Proxy_Should_Be_Invoked_Correctly()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();
            var cardId = Guid.NewGuid();
            var cardResponse = Fixture.Build<CardResponse>()
                                      .With(x => x.Id, cardId.ToString)
                                      .With(x => x.CustomerId, customerId)
                                      .Create();
            var data = cardResponse.ToJsonString();

            _vcnCardsApiProxy.Setup(x => x.GetCardAsync(cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                             {
                                 StatusCode = HttpStatusCode.OK,
                                 Content = new StringContent(data)
                             });

            _vcnCardsApiProxy.Setup(x => x.CloseCardAsync(customerId, cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                             {
                                 StatusCode = HttpStatusCode.OK,
                                 Content = new StringContent(data)
                             });

            // Act
            await _target.CloseCardAsync(cardId, CancellationToken);

            // Assert
            _vcnCardsApiProxy.Verify(x => x.GetCardAsync(cardId, CancellationToken), Times.Once);
            _vcnCardsApiProxy.Verify(x => x.CloseCardAsync(customerId, cardId, CancellationToken), Times.Once);
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Has_Error_When_CloseCardAsync_Should_Throw_Exception()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();
            var cardId = Guid.NewGuid();
            var cardResponse = Fixture.Build<CardResponse>()
                                      .With(x => x.Id, cardId.ToString)
                                      .With(x => x.CustomerId, customerId)
                                      .Create();
            var data = cardResponse.ToJsonString();

            _vcnCardsApiProxy.Setup(x => x.GetCardAsync(cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                             {
                                 StatusCode = HttpStatusCode.OK,
                                 Content = new StringContent(data)
                             });

            _vcnCardsApiProxy.Setup(x => x.CloseCardAsync(customerId, cardId, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                             {
                                 StatusCode = HttpStatusCode.InternalServerError
                             });

            // Act
            Func<Task> func = async () => { await _target.CloseCardAsync(cardId, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<VcnCardsApiException>();
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Is_Ok_When_SendTokenTransitionRequestAsync_Then_Proxy_Should_Be_Invoked_Correctly()
        {
            // Arrange
            var request = Fixture.Create<TokenTransitionRequest>();

            _vcnCardsApiProxy.Setup(x => x.SendTokenTransitionRequestAsync(request, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                             {
                                 StatusCode = HttpStatusCode.OK
                             });

            // Act
            await _target.SendTokenTransitionRequestAsync(request, CancellationToken);

            // Assert
            _vcnCardsApiProxy.Verify(x => x.SendTokenTransitionRequestAsync(request, CancellationToken), Times.Once);
        }

        [Fact]
        public async Task Given_VcnCardsApiProxy_Response_Has_Error_When_SendTokenTransitionRequestAsync_Should_Throw_Exception()
        {
            // Arrange
            var request = Fixture.Create<TokenTransitionRequest>();

            _vcnCardsApiProxy.Setup(x => x.SendTokenTransitionRequestAsync(request, CancellationToken))
                             .ReturnsAsync(new HttpResponseMessage
                              {
                                  StatusCode = HttpStatusCode.InternalServerError
                             });

            // Act
            Func<Task> func = async () => { await _target.SendTokenTransitionRequestAsync(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<VcnCardsApiException>();
        }
    }
}