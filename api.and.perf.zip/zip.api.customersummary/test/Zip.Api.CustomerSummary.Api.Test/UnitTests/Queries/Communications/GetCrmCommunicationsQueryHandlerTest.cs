using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Communications.Query.GetCrmCommunications;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Communications
{
    public class GetCrmCommunicationsQueryHandlerTest
    {
        private readonly GetCrmCommunicationsQueryHandler _target;

        private readonly Mock<IMessageLogContext> _messageLogContext;

        private readonly Fixture _fixture;

        public GetCrmCommunicationsQueryHandlerTest()
        {
            _fixture = new Fixture();
            _messageLogContext = new Mock<IMessageLogContext>();

            _target = new GetCrmCommunicationsQueryHandler(_messageLogContext.Object);
        }

        [Fact]
        public void Given_DependencyInjection_Null_ShouldThrow_ArgumentNullException()
        {
            Action action = () => { new GetCrmCommunicationsQueryHandler(null); };

            action.Should()
                  .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Given_Valid_ConsumerId_Should_Return_Result_Correctly()
        {
            // Arrange
            var expect = _fixture.CreateMany<MessageLogDto>(1);
            var request = _fixture.Build<GetCrmCommunicationsQuery>()
                                  .With(x => x.MessageLogCategory, expect.Single().MessageLog.Category)
                                  .With(x => x.ReferenceId, expect.Single().MessageLog.ReferenceId)
                                  .Create();

            _messageLogContext.Setup(x => x.GetAsync(request.MessageLogCategory, request.ReferenceId))
                              .Returns(Task.FromResult(expect));

            // Action
            var actual = (await _target.Handle(request, CancellationToken.None)).ToList();
            
            // Assert
            actual.Count.Should().Be(1);
            actual.Single()
                  .Should()
                  .BeEquivalentTo(expect.Single());
        }
    }
}
