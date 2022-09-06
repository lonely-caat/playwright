using System;
using System.Threading.Tasks;
using Dapper;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts
{
    public class CrmCommentContext : ICrmCommentContext
    {
        private readonly IDbContext _dbContext;

        public CrmCommentContext(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<CommentDto> CreateAsync(
            long consumerId,
            CommentCategory category,
            CommentType type,
            string detail,
            string commentBy)
        {
            const string sql = @"
INSERT INTO [Comment] 
    (ReferenceId,[Type],[Category],[Detail],[TimeStamp],CommentBy) 
VALUES 
    (@consumerId, @type, @category, @detail, GETDATE(), @commentBy);
SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await _dbContext.ExecuteScalarAsync<long>(
                sql,
                new
                {
                    consumerId,
                    type = (int)type,
                    category = (int)category,
                    detail,
                    commentBy
                });

            return await GetAsync(id);
        }

        public async Task<CommentDto> GetAsync(long id)
        {
            const string sql = @"
SELECT * 
FROM   [Comment]
WHERE  Id = @id
";
            
            return await _dbContext.QuerySingleOrDefaultAsync<CommentDto>(sql, new { id });
        }

        public async Task<Pagination<CommentDto>> GetCommentsAsync(long consumerId, CommentCategory? category, CommentType? type, long pageIndex = 1, long pageSize = 100)
        {
            var offset = (pageIndex - 1) * pageSize;

            var builder = new SqlBuilder();
            var totalCountBuilder = new SqlBuilder();
            var selector = builder.AddTemplate("SELECT * FROM [Comment] /**where**/ /**orderby**/ OFFSET " + offset + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY");
            var totalCountSql = builder.AddTemplate("SELECT COUNT(*) FROM [Comment] /**where**/ /**orderby**/");

            builder.Where("[ReferenceId] = @rid", new { rid = consumerId });
            totalCountBuilder.Where("[ReferenceId] = @rid", new { rid = consumerId });

            if (category.HasValue)
            {
                builder.Where("[Category] = @category", new { category = (int)category });
                totalCountBuilder.Where("[Category] = @category", new { category = (int)category });
            }

            if (type.HasValue)
            {
                builder.Where("[Type] = @type", new { type = (int)type });
                totalCountBuilder.Where("[Type] = @type", new { type = (int)type });
            }

            var totalCount = await _dbContext.ExecuteScalarAsync<long>(totalCountSql.RawSql, totalCountSql.Parameters);
            builder.OrderBy("[TimeStamp] DESC");

            var pagination = new Pagination<CommentDto>()
            {
                Current = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = await _dbContext.QueryAsync<CommentDto>(selector.RawSql, selector.Parameters)
            };

            return pagination;
        }
    }
}
