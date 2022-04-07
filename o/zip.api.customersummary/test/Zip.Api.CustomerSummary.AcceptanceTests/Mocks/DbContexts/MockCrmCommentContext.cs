using AutoFixture;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
{
    public class MockCrmCommentContext : ICrmCommentContext
    {
        private readonly IFixture _fixture = new Fixture();

        public Task<CommentDto> CreateAsync(long consumerId, CommentCategory category, CommentType type, string detail, string commentBy)
        {
            return Task.FromResult(Create());
        }

        public Task<CommentDto> GetAsync(long id)
        {
            return Task.FromResult(Create());
        }

        private CommentDto Create()
        {
            return new CommentDto
            {
                Id = _fixture.Create<long>(),
                Detail = _fixture.Create<string>(),
                Category = _fixture.Create<CommentCategory>(),
                CommentBy = _fixture.Create<string>(),
                ReferenceId = _fixture.Create<long>(),
                TimeStamp = _fixture.Create<DateTime>(),
                Type = _fixture.Create<CommentType>()
            };
        }

        public Task<Pagination<CommentDto>> GetCommentsAsync(long consumerId, CommentCategory? category, CommentType? type, long pageIndex = 1, long pageSize = 100)
        {
            return Task.FromResult(new Pagination<CommentDto> {
                Current = _fixture.Create<long>(),
                PageSize = _fixture.Create<long>(),
                TotalCount = _fixture.Create<long>(),
                Items = new List<CommentDto>
                {
                    Create(),
                    Create(),
                    Create(),
                    Create(),
                    Create(),
                    Create(),
                }
            });
        }
    }
}
