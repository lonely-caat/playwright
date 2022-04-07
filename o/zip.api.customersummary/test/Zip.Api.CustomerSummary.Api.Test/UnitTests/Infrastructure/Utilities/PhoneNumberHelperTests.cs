using System;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Infrastructure.Common.Utilities;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Utilities
{
    public class PhoneNumberHelperTests
    {
        [Theory]
        [InlineData("+61428245234", "0428245234")]
        [InlineData("428245234", "0428245234")]
        [InlineData("0428245234", "0428245234")]
        public void ToMobileLocalFormat_Should_Be_Expected(string input, string expect)
        {
            var actual = PhoneNumberHelper.ToMobileLocalFormat(input);

            actual.Should().Be(expect);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("+61428245234", "+61428245234")]
        [InlineData("0428245234", "+61428245234")]
        [InlineData("0528245234", "+61528245234")]
        [InlineData("0228245234", "+64228245234")]
        public void ToMobileIntFormat_Should_Be_Expected(string input, string expect)
        {
            var actual = PhoneNumberHelper.ToMobileIntFormat(input);

            actual.Should().Be(expect);
        }

        [Theory]
        [InlineData("0028245234")]
        [InlineData("0128245234")]
        [InlineData("0328245234")]
        [InlineData("0628245234")]
        public void Given_Invalid_Number_ToMobileIntFormat_Should_Throws_FormatException(string input)
        {
            Action action = () => { PhoneNumberHelper.ToMobileIntFormat(input); };

            action.Should()
                   .Throw<FormatException>();
        }
    }
}
