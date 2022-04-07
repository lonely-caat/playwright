using System;
using Xunit;
using Zip.Api.CustomerSummary.Api.Exceptions;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Exceptions
{
    public class HeaderMissingExceptionTest
    {
        [Fact]
        public void Shall_instanced()
        {
            var ex1 = new HeaderMissingException();
            Assert.NotNull(ex1);

            var ex2 = new HeaderMissingException("some message");
            Assert.NotNull(ex2);

            var ex3 = new HeaderMissingException("some test", new Exception());
            Assert.NotNull(ex3);
        }
    }
}
