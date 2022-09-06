using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetOrderActivities;
using Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Payments
{
    public class GetOrderActivitiesQueryHandlerTest
    {
        private readonly Mock<IConsumerOperationRequestContext> _consumerOperationContextMock = new Mock<IConsumerOperationRequestContext>();
    
        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GetOrderActivitiesQueryHandler(null));
        }

        [Fact]
        public async Task ShouldReturn()
        {
            _consumerOperationContextMock.Setup(x => x.GetOrderActivitiesAsync(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<OrderActivityDto>
                {
                    new OrderActivityDto()
                    {
                        TimeStamp = DateTime.Now,
                        Id = 30675,
                        ParentOperationRequestId = null,
                        Reference = null,
                        MerchantName = "Fantastic Furniture",
                        Type = OperationRequestType.Authorise,
                        Status = OperationRequestStatus.Processed,
                        Metadata = "{\"shipping_address\": {\"OneLineAddress\": \"61 York St Sydney 2000\"}," +
                                   "\"order\": {\"total\": \"251\"}}"
                    },
                    new OrderActivityDto()
                    {
                        TimeStamp = DateTime.Now,
                        Id = 30676,
                        ParentOperationRequestId = null,
                        Reference = null,
                        MerchantName = "Fantastic Furniture",
                        Type = OperationRequestType.Authorise,
                        Status = OperationRequestStatus.Processed,
                        Metadata = "{\"shipping_address\": {\"OneLineAddress\": \"61 York St Sydney 2000\"}," +
                                   "\"order\": {\"total\": \"151\"}}"
                    }
                });

            var handler = new GetOrderActivitiesQueryHandler(_consumerOperationContextMock.Object);
            var result = await handler.Handle(new GetOrderActivitiesQuery(It.IsAny<long>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool>()), CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal("61 York St Sydney 2000", result.First().ShippingAddress);
            Assert.Equal(251, result.First().Amount);
        }

        [Fact]
        public async Task Given_Metadata_With_MissingProperties_Should_Catch_And_Continue()
        {
            _consumerOperationContextMock.Setup(x => x.GetOrderActivitiesAsync(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<OrderActivityDto>
                {
                    new OrderActivityDto()
                    {
                        TimeStamp = DateTime.Now,
                        Id = 30675,
                        ParentOperationRequestId = null,
                        Reference = null,
                        MerchantName = "Fantastic Furniture",
                        Type = OperationRequestType.Authorise,
                        Status = OperationRequestStatus.Processed,
                        Metadata = "{\"shipping_address\": null," +
                                   "\"order\": {\"total\": \"251\"}}"
                    },
                    new OrderActivityDto()
                    {
                        TimeStamp = DateTime.Now,
                        Id = 30676,
                        ParentOperationRequestId = null,
                        Reference = null,
                        MerchantName = "Fantastic Furniture",
                        Type = OperationRequestType.Authorise,
                        Status = OperationRequestStatus.Processed,
                        Metadata = "{\"shipping_address\": {\"OneLineAddress\": \"61 York St Sydney 2000\"}," +
                                   "\"order\": null}"
                    }
                });

            var handler = new GetOrderActivitiesQueryHandler(_consumerOperationContextMock.Object);
            var result = await handler.Handle(new GetOrderActivitiesQuery(It.IsAny<long>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool>()), CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Equal("", result.First().ShippingAddress);
            Assert.Equal(251, result.First().Amount);

            Assert.Equal("61 York St Sydney 2000", result.Last().ShippingAddress);
            Assert.Equal(0, result.Last().Amount);
        }

        [Fact]
        public async Task Given_Null_Metadata_Should_Not_Throw_Error()
        {
            _consumerOperationContextMock.Setup(x => x.GetOrderActivitiesAsync(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<OrderActivityDto>
                {
                    new OrderActivityDto()
                    {
                        Id = 30675,
                        Metadata = null

                    },
                    new OrderActivityDto()
                    {
                        Id = 30676,
                        Metadata = null
                    }
                });

            var handler = new GetOrderActivitiesQueryHandler(_consumerOperationContextMock.Object);
            var result = await handler.Handle(new GetOrderActivitiesQuery(It.IsAny<long>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool>()), CancellationToken.None);

            Assert.Equal(2, result.Count());
            Assert.Null(result.First().ShippingAddress);
            Assert.Equal(0, result.First().Amount);
        }

        [Fact]
        public async Task Given_ShowAll_False_Should_Not_Return_Parent_OperationRequest()
        {
            _consumerOperationContextMock.Setup(x => x.GetOrderActivitiesAsync(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<OrderActivityDto>
                {
                    new OrderActivityDto()
                    {
                        Id = 30675,
                        Metadata = null

                    },
                    new OrderActivityDto()
                    {
                        Id = 30676,
                        Metadata = null,
                        ParentOperationRequestId = 30675
                    }
                });

            var handler = new GetOrderActivitiesQueryHandler(_consumerOperationContextMock.Object);
            var result = await handler.Handle(new GetOrderActivitiesQuery(It.IsAny<long>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), false), CancellationToken.None);

            Assert.Single(result);
            Assert.Equal(30676, result.First().Id);
        }

        [Fact]
        public async Task Given_ShowAll_True_Should_Return_All()
        {
            _consumerOperationContextMock.Setup(x => x.GetOrderActivitiesAsync(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<OrderActivityDto>
                {
                    new OrderActivityDto()
                    {
                        Id = 30675,
                        Metadata = null

                    },
                    new OrderActivityDto()
                    {
                        Id = 30676,
                        Metadata = null,
                        ParentOperationRequestId = 30675
                    }
                });

            var handler = new GetOrderActivitiesQueryHandler(_consumerOperationContextMock.Object);
            var result = await handler.Handle(new GetOrderActivitiesQuery(It.IsAny<long>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), true), CancellationToken.None);

            Assert.Equal(2, result.Count());
        }
    }
}
