using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;
using Zip.Api.CustomerSummary.Application.Comments.Command.CreateCrmComment;
using Zip.Api.CustomerSummary.Application.Comments.Query.GetCrmComments;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class CrmCommentsControllerTests
    {
        private readonly Mock<IMediator> _mediator;

        public CrmCommentsControllerTests()
        {
            _mediator = new Mock<IMediator>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CrmCommentsController(null);
            });
        }

        [Fact]
        public async Task Given_InvalidConsumerId_WhenCall_Get_ShouldReturn_BadRequest()
        {
            var controller = new CrmCommentsController(_mediator.Object);
            var result = await controller.GetAsync(-1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Given_NoCommentsFound_ShouldReturn_NotFound()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetCrmCommentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as Pagination<CommentDto>);

            var controller = new CrmCommentsController(_mediator.Object);
            var result = await controller.GetAsync(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Given_CommentsFound_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetCrmCommentsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Pagination<CommentDto>()
                {
                    PageSize = It.IsAny<long>(),
                    Current = It.IsAny<long>(),
                    TotalCount = It.IsAny<long>(),
                    Items = new List<CommentDto>()
                    {
                        new CommentDto(), new CommentDto()
                    }
                });

            var controller = new CrmCommentsController(_mediator.Object);
            var result = await controller.GetAsync(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_Get_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetCrmCommentsQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());


            var controller = new CrmCommentsController(_mediator.Object);
            var result = await controller.GetAsync(1);

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Given_Created_ShouldReturn_Ok()
        {
            _mediator.Setup(x => x.Send(It.IsAny<CreateCrmCommentCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CommentDto() { ReferenceId = 291 });

            _mediator.Setup(x => x.Send(It.IsAny<GetCrmCommentsQuery>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new Pagination<CommentDto>()
                 {
                     PageSize = It.IsAny<long>(),
                     Current = It.IsAny<long>(),
                     TotalCount = It.IsAny<long>(),
                     Items = new List<CommentDto>()
                    {
                        new CommentDto()
                    }
                 });

            var controller = new CrmCommentsController(_mediator.Object);
            var result = await controller.CreateAsync(new CreateCrmCommentCommand() { ReferenceId = 922 });

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Given_Error_WhenCall_Create_ShouldReturn_500()
        {
            _mediator.Setup(x => x.Send(It.IsAny<CreateCrmCommentCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            var controller = new CrmCommentsController(_mediator.Object);
            var result = await controller.CreateAsync(new CreateCrmCommentCommand());

            Assert.Equal(500, (result as ObjectResult).StatusCode);
        }

    }
}
