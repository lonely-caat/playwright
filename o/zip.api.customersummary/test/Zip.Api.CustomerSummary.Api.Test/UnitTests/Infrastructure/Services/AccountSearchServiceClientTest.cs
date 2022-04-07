using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class AccountSearchServiceClientTest
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClient _errorClient;
        private readonly Mock<IOptions<AccountSearchSettings>> _options;
        private readonly Mock<HttpMessageHandler> _mockHandler;
        private readonly Mock<HttpMessageHandler> _mockErrorHandler;

        public AccountSearchServiceClientTest()
        {
            _mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new List<AccountListItem>
                    {
                        new AccountListItem()
                        {
                            ActivationDate = DateTime.Now,
                            ConsumerId = 123,
                            CountryId = "AU"
                        },
                        new AccountListItem
                        {
                            ActivationDate = DateTime.Now.AddMonths(-1),
                            ConsumerId = 345,
                            CountryId = "NZ"
                        }
                    }))
                }).Verifiable();

            _mockErrorHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _mockErrorHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Content = new StringContent(JsonConvert.SerializeObject(new List<AccountListItem>
                    {
                        new AccountListItem()
                        {
                            ActivationDate = DateTime.Now,
                            ConsumerId = 123,
                            CountryId = "AU"
                        },
                        new AccountListItem
                        {
                            ActivationDate = DateTime.Now.AddMonths(-1),
                            ConsumerId = 345,
                            CountryId = "NZ"
                        }
                    }))
                }).Verifiable();

            _httpClient = new HttpClient(_mockHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/")
            };

            _errorClient = new HttpClient(_mockErrorHandler.Object)
            {
                BaseAddress = new Uri("http://test.com/")
            };

            _options = new Mock<IOptions<AccountSearchSettings>>();

            _options.Setup(x => x.Value)
                .Returns(new AccountSearchSettings());
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AccountSearchServiceClient(null, _options.Object));
            Assert.Throws<ArgumentNullException>(() => new AccountSearchServiceClient(_httpClient, null));
        }

        [Fact]
        public async Task Should_get_results()
        {
            var client = new AccountSearchServiceClient(_httpClient, _options.Object);
            var result = await client.SearchAccountsAsync(CustomerSearchType.AccountNumber, "abc", 1, 1);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task When_Error_ShouldReturn_Empty()
        {
            var client = new AccountSearchServiceClient(_errorClient, _options.Object);
            var result = await client.SearchAccountsAsync(CustomerSearchType.AccountNumber, "abc", 1, 1);

            Assert.Empty(result);
        }

        [Fact]
        public async Task Given_StatusValid()
        {
            var client = new AccountSearchServiceClient(_httpClient, _options.Object);
            await client.GetStatusAsync();


            Assert.True(true);
        }

        [Fact]
        public async Task Given_StatusInvalid()
        {
            var ex = await Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                var client = new AccountSearchServiceClient(_errorClient, _options.Object);
                await client.GetStatusAsync();
            });


            Assert.NotNull(ex);
        }
    }
}
