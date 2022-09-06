using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Statement;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Statements.Query.GetStatementDates
{
    public class GetStatementDatesQueryHandler : IRequestHandler<GetStatementDatesQuery, IEnumerable<StatementDate>>
    {
        private readonly IStatementContext _statementContext;

        public GetStatementDatesQueryHandler(IStatementContext statementContext)
        {
            _statementContext = statementContext ?? throw new ArgumentNullException(nameof(statementContext));
        }

        public async Task<IEnumerable<StatementDate>> Handle(GetStatementDatesQuery request, CancellationToken cancellationToken)
        {
            return await _statementContext.GetStatementDatesAsync(request.AccountId);
        }
    }
}
