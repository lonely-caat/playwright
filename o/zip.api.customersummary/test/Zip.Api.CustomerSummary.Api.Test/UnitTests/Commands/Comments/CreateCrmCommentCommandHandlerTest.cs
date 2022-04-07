using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Comments.Command.CreateCrmComment;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm.Models;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Comments
{
    public class CreateCrmCommentCommandHandlerTest
    {

        private readonly Fixture _fixture;

        private readonly Mock<ICrmCommentContext> _crmCommentContext;

        private readonly Mock<IOptions<CrmServiceProxyOptions>> _options;

        private readonly Mock<ICrmServiceProxy> _crmServiceProxy;

        public CreateCrmCommentCommandHandlerTest()
        {
            _fixture = new Fixture();
            _crmCommentContext = new Mock<ICrmCommentContext>();
            _options = new Mock<IOptions<CrmServiceProxyOptions>>();
            _crmServiceProxy = new Mock<ICrmServiceProxy>();

        }

        [Fact]
        public void Given_DependencyInjection_Null_Should_Throw_ArgumentNullException()
        {
            Action action = () => { new CreateCrmCommentCommandHandler(null, null, null); };

            action.Should()
                  .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Given_ValidConsumerId_ShouldReturn_Result()
        {
            // Arrange
            var expect = _fixture.Create<CommentDto>();

            var command = new CreateCrmCommentCommand
            {
                ReferenceId = expect.ReferenceId,
                Category = CommentCategory.General,
                Type = CommentType.Consumer,
                Detail = Guid.NewGuid().ToString(),
                CommentBy = Guid.NewGuid().ToString()
            };

            _crmCommentContext.Setup(x => x.CreateAsync(command.ReferenceId,
                                                        command.Category.Value,
                                                        command.Type.Value,
                                                        command.Detail,
                                                        command.CommentBy))
                              .Returns(Task.FromResult(expect));

            _options.Setup(x => x.Value).Returns(new CrmServiceProxyOptions
            {
                Enabled = false
            });

            var _target = new CreateCrmCommentCommandHandler(_crmCommentContext.Object, _crmServiceProxy.Object, _options.Object);

            // Action 
            var actual = await _target.Handle(command, CancellationToken.None);

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expect);
        }

        [Fact]
        public async Task Given_ValidConsumerId_Api_ShouldReturn_Result()
        {
            var expect = _fixture.Create<CommentDto>();

            var command = new CreateCrmCommentCommand
            {
                ReferenceId = expect.ReferenceId,
                Category = CommentCategory.General,
                Type = CommentType.Consumer,
                Detail = Guid.NewGuid().ToString(),
                CommentBy = Guid.NewGuid().ToString()
            };

            _options.Setup(x => x.Value).Returns(new CrmServiceProxyOptions
            {
                Enabled = true
            });

            _crmServiceProxy.Setup(x => x.CreateComment(It.IsAny<CreateCommentRequest>())).Returns(Task.FromResult(expect));

            var _target = new CreateCrmCommentCommandHandler(_crmCommentContext.Object, _crmServiceProxy.Object, _options.Object);

            // Action 
            var actual = await _target.Handle(command, CancellationToken.None);

            // Assert
            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expect);
        }
    }
}