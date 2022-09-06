using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Statements.Query.GetStatementDates;
using Zip.Api.CustomerSummary.Domain.Entities.Statement;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Statements
{
    public class GetStatementDatesQueryHandlerTest
    {
        private readonly Mock<IStatementContext> _statementContext;

        public GetStatementDatesQueryHandlerTest()
        {
            _statementContext = new Mock<IStatementContext>();
        }

        [Fact]
        public void Given_NullInjection_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new GetStatementDatesQueryHandler(null));
        }

        [Fact]
        public async Task Should_return()
        {
            _statementContext.Setup(x => x.GetStatementDatesAsync(It.IsAny<long>()))
                .ReturnsAsync(new List<StatementDate>()
                {
                    new StatementDate(new DateTime(2019,1,1),new DateTime(2019,2,1)),
                    new StatementDate(new DateTime(2019,2,1),new DateTime(2019,3,1)),
                });

            var handler = new GetStatementDatesQueryHandler(_statementContext.Object);
            var result = await handler.Handle(new GetStatementDatesQuery(), CancellationToken.None);

            Assert.Equal(2, result.Count());
        }
    }
}
