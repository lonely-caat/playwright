using System;
using MediatR;
using Zip.Api.CustomerSummary.Application.Statements.Models;

namespace Zip.Api.CustomerSummary.Application.Statements.Command.GenerateStatement
{
    public class GenerateStatementCommand : IRequest<GenerateStatementResponse>
    {
        public long ConsumerId { get; set; }
        
        public long AccountId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public GenerateStatementCommand(
            long consumerId,
            long accountId,
            DateTime startDate,
            DateTime endDate)
        {
            ConsumerId = consumerId;
            AccountId = accountId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public GenerateStatementCommand()
        {

        }
    }
}
