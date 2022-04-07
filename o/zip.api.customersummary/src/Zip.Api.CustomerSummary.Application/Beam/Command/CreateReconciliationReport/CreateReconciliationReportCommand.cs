using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;

namespace Zip.Api.CustomerSummary.Application.Beam.Command.CreateReconciliationReport
{
    public class CreateReconciliationReportCommand : IRequest<CreateReconciliationReportResponse>
    {
        [Required]
        public DateTime SelectedDate { get; set; }

        public string RequestedBy { get; set; }

        public string Region { get; set; } = Regions.Australia;

        public CreateReconciliationReportCommand(DateTime selectedDate, string requestedBy, string region = Regions.Australia)
        {
            SelectedDate = selectedDate;
            RequestedBy = requestedBy;
            Region = region;
        }

        public CreateReconciliationReportCommand()
        {
        }
    }
}
