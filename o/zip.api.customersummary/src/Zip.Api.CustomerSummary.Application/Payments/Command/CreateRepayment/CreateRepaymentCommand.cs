using System;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Payments;

namespace Zip.Api.CustomerSummary.Application.Payments.Command.CreateRepayment
{
    public class CreateRepaymentCommand : IRequest<Repayment>
    {
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
        public Frequency? Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public string ChangedBy { get; set; }

        public CreateRepaymentCommand()
        {

        }

        public CreateRepaymentCommand(long accountId, decimal amount, Frequency frequency, DateTime startDate, string changedBy)
        {
            AccountId = accountId;
            Amount = amount;
            Frequency = frequency;
            StartDate = startDate;
            ChangedBy = changedBy;
        }
    }
}
