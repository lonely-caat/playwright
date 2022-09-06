using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Sms;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Extensions
{
    public class StringExtensionsTests
    {
        private readonly Fixture _fixture;

        public StringExtensionsTests()
        {
            _fixture = new Fixture();
        }
        
        [Theory]
        [InlineData("{first-name}", "value-FirstName")]
        [InlineData("{classification}", "value-Classification")]
        [InlineData("{payNowUrl}", "value-PayNowUrl")]
        public void String_Extension_ReplaceTag_Should_Be_Correct(string input, string expect)
        {
            var tagDictionary = new Dictionary<string, string>
            {
                { input, expect }
            };

            var actual = input.ReplaceTags(tagDictionary);

            actual.Should()
                   .Be(expect);
        }
        
        [Fact]
        public void String_Extension_ReplaceTags_Should_Be_Correct()
        {
            const string input = "{first-name}{classification}{payNowUrl}";
            var expectFirstName = _fixture.Create<string>();
            var expectClassificationName = _fixture.Create<string>();
            var expectPayNowUrl = _fixture.Create<string>();

            var tagDictionary = new Dictionary<string, string>
            {
                { SmsSubstitutionKeys.FirstName, expectFirstName},
                { SmsSubstitutionKeys.Classification, expectClassificationName},
                { SmsSubstitutionKeys.PayNowUrl, expectPayNowUrl }
            };

            var actual = input.ReplaceTags(tagDictionary);

            actual.Should()
                   .Be($"{expectFirstName}{expectClassificationName}{expectPayNowUrl}");
        }
    }
}
