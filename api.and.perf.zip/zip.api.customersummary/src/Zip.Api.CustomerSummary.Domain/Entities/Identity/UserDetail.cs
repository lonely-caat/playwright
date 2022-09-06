using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Domain.Entities.Identity
{
    public class UserDetail
    {
        public string Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public List<RoleDetail> Roles { get; set; } = new List<RoleDetail>();
    }
}
