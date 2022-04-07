using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Statements;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Contract
{
    public class StatementTests
    {
        [Fact]
        public void Dummy_Properties_Test()
        {
            var target = new Fixture().Create<Statement>();

            target.Should()
                  .Be(target);
        }
    }
}

