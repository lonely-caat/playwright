using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;

namespace Zip.Api.CustomerSummary.Application.Beam.Query.PollReconciliationReport
{
    public class PollReconciliationReportQuery : IRequest<PollReconciliationReportResponse>
    {
        [Required]
        public Guid Uuid { get; set; }

        public string RequestedBy { get; set; }

        public PollReconciliationReportQuery(Guid uuid, string requestedBy)
        {
            Uuid = uuid;
            RequestedBy = requestedBy;
        }

        public PollReconciliationReportQuery()
        {
        }
    }
}
