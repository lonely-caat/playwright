using Microsoft.AspNetCore.Authorization;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Api.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeRoleAttribute(params AppRole[] roleTypes)
        {
            Roles = string.Join(",", roleTypes);
        }
    }
}
