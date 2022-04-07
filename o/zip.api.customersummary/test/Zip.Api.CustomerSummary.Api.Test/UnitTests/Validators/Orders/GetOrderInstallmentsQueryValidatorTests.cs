using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderInstallments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Orders
{
    public class GetOrderInstallmentsQueryValidatorTests
    {
        private readonly GetOrderInstallmentsQueryValidator _validator;

        public GetOrderInstallmentsQueryValidatorTests()
        {
            _validator = new GetOrderInstallmentsQueryValidator();
        }

        [Fact]
        public void Should_Be_Invalid()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.AccountId, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.OrderId, 0);
        }

        [Fact]
        public void Should_Be_Ok()
        {
            var command = new GetOrderInstallmentsQuery
            {
                AccountId = 123,
                OrderId = 123
            };

            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}
