using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence.DbContexts
{
    public class MfaContextTest
    {
        private readonly Mock<IDbContext> _dbContext;

        public MfaContextTest()
        {
            _dbContext = new Mock<IDbContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullEx()
        {
            Assert.Throws<ArgumentNullException>(() => new MfaContext(null));
        }

        [Fact]
        public void Should_createtoken()
        {
            var ctx = new MfaContext(_dbContext.Object);
            var code = ctx.CreateToken(Guid.NewGuid().ToString(), 1, "0439201928", MfaType.PhoneNumber);

            Assert.NotEmpty(code);
            Assert.Equal(6, code.Length);
        }

        [Fact]
        public void Should_createtoken_with_expectedlength()
        {
            var len = 29;

            var ctx = new MfaContext(_dbContext.Object);
            var code = ctx.CreateToken(Guid.NewGuid().ToString(), 1, "0439201928", MfaType.PhoneNumber, len);

            Assert.NotEmpty(code);
            Assert.Equal(len, code.Length);
        }

        [Fact]
        public async Task Should_get()
        {
            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<MfaCode>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new MfaCode());

            var ctx = new MfaContext(_dbContext.Object);
            var result = await ctx.GetAsync(3928);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_gettoken()
        {
            _dbContext.Setup(x => x.QueryFirstOrDefaultAsync<MfaCode>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new MfaCode());

            var ctx = new MfaContext(_dbContext.Object);
            var result = await ctx.GetTokenAsync(MfaEntityType.Customer, 3920);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_save()
        {
            var id = 93;

            _dbContext.Setup(x => x.QuerySingleOrDefaultAsync<MfaCode>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(new MfaCode
                {
                    Id = id
                });

            _dbContext.Setup(x => x.ExecuteScalarAsync<long>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(2221);

            var ctx = new MfaContext(_dbContext.Object);
            var result = await ctx.SaveAsync(new MfaCode());

            Assert.Equal(id, result.Id);
        }
    }
}
