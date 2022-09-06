using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Communications.Query.GetEmailsSent;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Communications
{
    public class GetEmailsSentQueryHandlerTest
    {
        private readonly Mock<IMessageLogContext> _messageLogContext;

        private readonly Fixture _fixture;

        private readonly GetEmailsSentQueryHandler _target;

        public GetEmailsSentQueryHandlerTest()
        {
            _fixture = new Fixture();
            _messageLogContext = new Mock<IMessageLogContext>();
            _target = new GetEmailsSentQueryHandler(_messageLogContext.Object);
        }

        [Fact]
        public void Test_NullInject_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => { new GetEmailsSentQueryHandler(null); });
        }

        [Fact]
        public async Task Given_Invalid_EmailTypes_Handle_Should_Return_null()
        {
            // Arrange
            var request = _fixture.Build<GetEmailsSentQuery>()
                                  .With(x => x.EmailTypes, new List<string> { Guid.NewGuid().ToString() })
                                  .Create();
            // Act
            var response = await _target.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task Given_Valid_EmailTypes_When_GetEmailsSentAsync_Returns_Null_Then_Handle_Should_Return_null()
        {
            // Arrange
            var request = _fixture.Build<GetEmailsSentQuery>()
                                  .With(x => x.EmailTypes, new List<string> { nameof(NotificationType.ArrearsNoticeSection21D) })
                                  .Create();

            _messageLogContext.Setup(x => x.GetEmailsSentAsync(request.ConsumerId, It.IsAny<List<int>>()))
                              .ReturnsAsync((IEnumerable<MessageLog>)null);
            
            // Act
            var response = await _target.Handle(request, CancellationToken.None);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task Test_Handle_With_TestData()
        {
            // Arrange
            var request = _fixture.Build<GetEmailsSentQuery>()
                                  .With(x => x.EmailTypes, new List<string> { nameof(NotificationType.ArrearsNoticeSection21D) })
                                  .Create();

            var messageLog = _fixture.Build<MessageLog>()
                                     .With(x => x.Type, (MessageLogType)NotificationType.ArrearsNoticeSection21D)
                                     .Create();

            var expect = _fixture.Build<EmailSent>()
                                 .With(x => x.ConsumerId, request.ConsumerId)
                                 .With(x => x.EmailType, nameof(NotificationType.ArrearsNoticeSection21D))
                                 .With(x => x.Email, messageLog.Subject)
                                 .With(x => x.CreatedDateTime, messageLog.TimeStamp)
                                 .With(x => x.Success, true)
                                 .Create();
            
            _messageLogContext.Setup(x => x.GetEmailsSentAsync(request.ConsumerId, It.IsAny<List<int>>()))
                              .ReturnsAsync(new List<MessageLog> { messageLog });

            // Act
            var actual = await _target.Handle(request, CancellationToken.None);

            // Assert
            actual.Single().Should().BeEquivalentTo(expect);
        }
    }
}