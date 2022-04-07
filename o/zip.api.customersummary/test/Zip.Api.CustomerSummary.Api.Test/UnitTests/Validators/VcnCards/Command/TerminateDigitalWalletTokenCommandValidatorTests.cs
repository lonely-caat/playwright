﻿using System;
using AutoFixture;
using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.TerminateDigitalWalletToken;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.VcnCards.Command
{
    public class TerminateDigitalWalletTokenCommandValidatorTests : CommonTestsFixture
    {
        private readonly TerminateDigitalWalletTokenCommandValidator _validator;

        public TerminateDigitalWalletTokenCommandValidatorTests()
        {
            _validator = new TerminateDigitalWalletTokenCommandValidator();
        }

        [Fact]
        public void Given_Valid_DigitalWalletToken_Should_Not_Have_Error()
        {
            var result = _validator.Validate(new TerminateDigitalWalletTokenCommand
            {
                DigitalWalletToken = Fixture.Create<Guid>()
            });

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Given_Empty_DigitalWalletToken_Should_Have_Error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.DigitalWalletToken, Guid.Empty);
        }
    }
}
