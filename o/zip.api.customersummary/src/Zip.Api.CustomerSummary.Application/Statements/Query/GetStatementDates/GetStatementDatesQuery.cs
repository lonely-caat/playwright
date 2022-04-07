using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Statement;

namespace Zip.Api.CustomerSummary.Application.Statements.Query.GetStatementDates
{
    public class GetStatementDatesQuery : IRequest<IEnumerable<StatementDate>>
    {
        public long AccountId { get; set; }

        public GetStatementDatesQuery(long accountId)
        {
            AccountId = accountId;
        }

        public GetStatementDatesQuery()
        {

        }
    }
}
