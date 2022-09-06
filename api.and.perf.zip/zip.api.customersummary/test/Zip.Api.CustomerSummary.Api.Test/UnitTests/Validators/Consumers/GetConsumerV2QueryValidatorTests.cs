using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V2;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Consumers
{
    public class GetConsumerV2QueryValidatorTests
    {
        private readonly GetConsumerQueryV2Validator _queryV2Validator;
        
        public GetConsumerV2QueryValidatorTests()
        {
            _queryV2Validator = new GetConsumerQueryV2Validator();
        }

        [Fact]
        public void Given_ConsumerIdInvalid_Should_have_error()
        {
            _queryV2Validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, 0);
            _queryV2Validator.ShouldHaveValidationErrorFor(x => x.ConsumerId, -1);
        }

        [Fact]
        public void Should_pass()
        {
            var result = _queryV2Validator.Validate(new GetConsumerQueryV2()
            {
                ConsumerId = 29382,
            });

            Assert.True(result.IsValid);
        }
    }
}
