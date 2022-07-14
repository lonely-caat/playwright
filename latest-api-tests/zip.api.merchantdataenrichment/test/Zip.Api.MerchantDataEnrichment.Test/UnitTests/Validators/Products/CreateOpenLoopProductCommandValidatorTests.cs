using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Products.Command.CreateOpenLoopProduct;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.Products
{
    public class CreateOpenLoopProductCommandValidatorTests : CommonTestsFixture
    {
        [Theory]
        [MemberData(nameof(OpenLoopProductSampleData))]
        public void ShouldHaveErrorWhenRequiredFieldIsEmpty(string description, bool isActive,
            decimal serviceFeePercentage, decimal transactionAmountLowerLimit,
            decimal? transactionAmountUpperLimit, decimal transactionFee, bool isValid)
        {
            var validator = new CreateOpenLoopProductCommandValidator();
            var command = Fixture.Build<CreateOpenLoopProductCommand>()
                                  .With(x => x.Description, description)
                                  .With(x => x.IsActive, isActive)
                                  .With(x => x.ServiceFeePercentage, serviceFeePercentage)
                                  .With(x => x.TransactionAmountLowerLimit, transactionAmountLowerLimit)
                                  .With(x => x.TransactionAmountUpperLimit, transactionAmountUpperLimit ?? default)
                                  .With(x => x.TransactionFee, transactionFee)
                                  .Create();

            validator.TestValidate(command).IsValid.Should().Be(isValid);
        }

        public static IEnumerable<object[]> OpenLoopProductSampleData =>
        new List<object[]>
        {
            new object[] { " ", true, 1.5, 0M, 5000M, 1.5M, false },
            new object[] { "", true, 1.5, 0M, 5000M, 1.5M, false },
            new object[] { null, true, 1.5, 0M, 5000M, 1.5M, false },
            new object[] { null, true, 1.5, 0M, null, 1.5M, false },
            new object[] { "test ol product", true, 1.5M, 0M, 100M, 1.5M, true },
            new object[] { "test ol product", true, -1.5M, 0M, null, 1.5M, false },
            new object[] { "test ol product", true, 1.5M, 0M, null, -0.16M, false },
            new object[] { "test ol product", true, 1.5M, 100M, 50M, -0.16M, false }
        };
    }
}
