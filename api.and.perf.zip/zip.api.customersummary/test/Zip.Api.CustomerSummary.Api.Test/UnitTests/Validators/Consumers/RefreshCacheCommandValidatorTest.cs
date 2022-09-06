using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Command.RefreshCache;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Consumers
{
    public class RefreshCacheCommandValidatorTest
    {
        private readonly RefreshCacheCommandValidator _validator;

        public RefreshCacheCommandValidatorTest()
        {
            _validator = new RefreshCacheCommandValidator();
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, -1);
        }

        [Fact]
        public void Given_ConsumerId_Should_Be_Valid()
        {
            var r1 = _validator.Validate(new RefreshCacheCommand()
            {
                ConsumerId = 291
            });

            Assert.True(r1.IsValid);
        }
    }
}