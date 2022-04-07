using System;
using AutoFixture;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Dto
{
    public class CommentVMTests
    {
        private readonly IFixture _fixture;

        public CommentVMTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Should_equal()
        {
            var id = _fixture.Create<long>();
            var type = _fixture.Create<CommentType>();
            var category = _fixture.Create<CommentCategory>();
            var refId = _fixture.Create<long>();
            var detail = _fixture.Create<string>();
            var timestamp = _fixture.Create<DateTime>();
            var cb = _fixture.Create<string>();

            var result = new CommentDto()
            {
                Id = id,
                Type = type,
                Category = category,
                ReferenceId  = refId,
                Detail = detail,
                TimeStamp = timestamp,
                CommentBy = cb
            };

            Assert.Equal(id, result.Id);
            Assert.Equal(type, result.Type);
            Assert.Equal(category, result.Category);
            Assert.Equal(detail, result.Detail);
            Assert.Equal(timestamp, result.TimeStamp);
            Assert.Equal(cb, result.CommentBy);
        }
    }
}
