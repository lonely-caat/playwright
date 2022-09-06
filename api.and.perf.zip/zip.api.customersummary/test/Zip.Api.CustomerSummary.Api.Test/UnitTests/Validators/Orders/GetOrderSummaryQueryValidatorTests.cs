using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderSummary;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Orders
{
    public class GetOrderSummaryQueryValidatorTests
    {
        private readonly GetOrderSummaryQueryValidator _validator;

        public GetOrderSummaryQueryValidatorTests()
        {
            _validator = new GetOrderSummaryQueryValidator();
        }
        
        [Fact]
        public void Should_Be_Invalid()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.OrderId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.OrderId, -1);
        }

        [Fact]
        public void Should_Be_Ok()
        {
            var command = new GetOrderSummaryQuery
            {
                OrderId = 123456
            };

            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}
