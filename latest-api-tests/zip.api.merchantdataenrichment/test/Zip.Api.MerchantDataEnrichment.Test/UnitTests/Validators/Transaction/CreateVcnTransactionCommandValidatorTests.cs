using System;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Transaction.Command.CreateVcnTransaction;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Validators.Transaction
{
    public class CreateVcnTransactionCommandValidatorTests : CommonTestsFixture
    {
        [Theory]
        [InlineData(12345, "8f4d9890-bd75-46a9-82b2-9cf6a8b68696", "c0cbe719-4a12-42da-8061-055085acc143", "CardAcceptorName", "CardAcceptorCity", true)]
        [InlineData(default(long), "8f4d9890-bd75-46a9-82b2-9cf6a8b68696", "c0cbe719-4a12-42da-8061-055085acc143", "CardAcceptorName", "CardAcceptorCity", false)]
        [InlineData(12345, "00000000-0000-0000-0000-000000000000", "c0cbe719-4a12-42da-8061-055085acc143", "CardAcceptorName", "CardAcceptorCity", false)]
        [InlineData(12345, "8f4d9890-bd75-46a9-82b2-9cf6a8b68696", "", "CardAcceptorName", "CardAcceptorCity", false)]
        [InlineData(12345, "8f4d9890-bd75-46a9-82b2-9cf6a8b68696", "c0cbe719-4a12-42da-8061-055085acc143", "", "CardAcceptorCity", false)]
        [InlineData(12345, "8f4d9890-bd75-46a9-82b2-9cf6a8b68696", "c0cbe719-4a12-42da-8061-055085acc143", "CardAcceptorName", "", false)]
        public void CreateVcnTransactionCommandValidatorTest(
            long accountId,
            Guid customerId,
            string vcnCardId,
            string cardAcceptorName,
            string cardAcceptorCity,
            bool isValid)
        {
            // Arrange
            var validator = new CreateVcnTransactionCommandValidator();
            var command = Fixture.Build<CreateVcnTransactionCommand>()
                                 .With(x => x.AccountId, accountId)
                                 .With(x => x.CustomerId, customerId)
                                 .With(x => x.VcnCardId, vcnCardId)
                                 .With(x => x.CardAcceptorName, cardAcceptorName)
                                 .With(x => x.CardAcceptorCity, cardAcceptorCity)
                                 .Create();

            // Act & Assert
            validator.Validate(command).IsValid.Should().Be(isValid);
        }
    }
}
