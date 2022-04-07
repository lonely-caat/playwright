using System;

namespace Zip.Api.CustomerSummary.Domain.Entities.Dto
{
    public class ContactDto
    {
        public long ConsumerId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AuthorityFullName { get; set; }
        public string AuthorityMobile { get; set; }
        public DateTime AuthorityDateOfBirth { get; set; }
    }
}
