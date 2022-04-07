using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Comments.Query.GetCrmComments;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Crm;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Comments
{
    public class GetCrmCommentsQueryHandlerTest
    {

        private readonly Mock<ICrmCommentContext> _crmCommentContext;
        private readonly Mock<ICrmServiceProxy> _crmServiceProxy;
        private readonly Mock<IOptions<CrmServiceProxyOptions>> _options;

        public GetCrmCommentsQueryHandlerTest()
        {
            _crmCommentContext = new Mock<ICrmCommentContext>();
            _crmServiceProxy = new Mock<ICrmServiceProxy>();
            _options = new Mock<IOptions<CrmServiceProxyOptions>>();
        }

        [Fact]
        public async Task Given_Valid_ShouldReturn_Result()
        {
            SetupMockObjects();

            _options.Setup(x => x.Value).Returns(new CrmServiceProxyOptions
            {
                Enabled = false
            });

            var handler = new GetCrmCommentsQueryHandler(_crmCommentContext.Object, _crmServiceProxy.Object, _options.Object);

            var query = new GetCrmCommentsQuery(1);

            var results = await handler.Handle(query, System.Threading.CancellationToken.None);

            Assert.NotNull(results);

            Assert.True(results.Items.Any());

            Assert.Equal(2, results.TotalCount);

            foreach (var item in results.Items)
            {
                Assert.IsType<CommentDto>(item);
            }
        }

        [Fact]
        public async Task Given_Valid_Api_Input_ShouldReturn_Result()
        {
            SetupMockObjects();

            _options.Setup(x => x.Value).Returns(new CrmServiceProxyOptions
            {
                Enabled = true
            });

            var handler = new GetCrmCommentsQueryHandler(_crmCommentContext.Object, _crmServiceProxy.Object, _options.Object);

            var query = new GetCrmCommentsQuery(1);

            var results = await handler.Handle(query, System.Threading.CancellationToken.None);

            Assert.NotNull(results);

            Assert.True(results.Items.Any());

            Assert.Equal(2, results.TotalCount);

            foreach (var item in results.Items)
            {
                Assert.IsType<CommentDto>(item);
            }
        }

        private void SetupMockObjects()
        {
            var comments = new List<CommentDto>()
            {
                new CommentDto()
                {
                    Id = 12345,
                    Detail = "Testing",
                    Category = CommentCategory.Complaint,
                    Type = CommentType.Application,
                    ReferenceId = 555,
                    CommentBy = "zipMoney",
                    TimeStamp = DateTime.Now
                },
                new CommentDto()
                {
                    Id = 54321,
                    Detail = "Testing",
                    Category = CommentCategory.Complaint,
                    Type = CommentType.Application,
                    ReferenceId = 555,
                    CommentBy = "zipMoney",
                    TimeStamp = DateTime.Now
                },
            };

            _crmCommentContext.Setup(x => x.GetCommentsAsync(It.IsAny<long>(), null, It.IsAny<CommentType>(),It.IsAny<long>(), It.IsAny<long>()))
                    .Returns(Task.FromResult(new Pagination<CommentDto> { 
                    Current = It.IsAny<long>(),
                    PageSize = It.IsAny<long>(),
                    TotalCount = comments.Count(),
                    Items = comments
                }));

            _crmServiceProxy.Setup(x => x.GetCustomerComment(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>()))
                .Returns(Task.FromResult(new Pagination<CommentDto>
                {
                    Current = It.IsAny<long>(),
                    PageSize = It.IsAny<long>(),
                    TotalCount = comments.Count(),
                    Items = comments
                }));
        }
    }
}
