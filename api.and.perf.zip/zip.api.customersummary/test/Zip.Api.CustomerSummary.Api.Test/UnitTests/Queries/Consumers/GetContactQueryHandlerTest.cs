using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Consumers
{
    public class GetContactQueryHandlerTest
    {
        private readonly Mock<IContactContext> _contactContext;

        public GetContactQueryHandlerTest()
        {
            _contactContext = new Mock<IContactContext>();
        }

        [Fact]
        public void Given_NullInject_Should_throw()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetContactQueryHandler(null);
            });
        }

        [Fact]
        public async Task Should_return()
        {
            _contactContext.Setup(x => x.GetContactAsync(It.IsAny<long>()))
                .ReturnsAsync(new ContactDto()
                {
                    Email = "shan.ke@zip.co"
                });

            var handler = new GetContactQueryHandler(_contactContext.Object);
            var result = await handler.Handle(new GetContactQuery(), CancellationToken.None);

            Assert.Equal("shan.ke@zip.co", result.Email);
        }
    }
}
