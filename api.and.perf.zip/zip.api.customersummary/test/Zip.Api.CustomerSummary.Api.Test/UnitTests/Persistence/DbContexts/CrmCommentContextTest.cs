using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class CrmCommentContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public CrmCommentContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNulLException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CrmCommentContext(null);
            });
        }

        [Fact]
        public async Task Should_Create()
        {
            _dbContext.Setup(x => x.ExecuteScalarAsync<long>(
               It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(2291);

            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<CommentDto>(It.IsAny<string>(), It.IsAny<object>()))
              .ReturnsAsync(new CommentDto());

            var ctx = new CrmCommentContext(_dbContext.Object);
            var result = await ctx.CreateAsync(392, CommentCategory.Collection, CommentType.Application, "Shan", "Ke");


            _dbContext.Verify(x => x.ExecuteScalarAsync<long>(
               It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Get()
        {
            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<CommentDto>(It.IsAny<string>(), It.IsAny<object>()))
              .ReturnsAsync(new CommentDto());

            var ctx = new CrmCommentContext(_dbContext.Object);
            var result = await ctx.CreateAsync(392, CommentCategory.Collection, CommentType.Application, "Shan", "Ke");

            Assert.NotNull(result);

        }

        [Fact]
        public async Task Should_GetComments()
        {
            _dbContext.Setup(x => x.QueryAsync<CommentDto>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new List<CommentDto>
                {
                    new CommentDto()
                });

            var ctx = new CrmCommentContext(_dbContext.Object);
            var result = await ctx.GetCommentsAsync(123, CommentCategory.Collection, CommentType.Application);

            Assert.NotNull(result);
            Assert.NotNull(result.Items);

            Assert.Single(result.Items);
        }
    }
}
