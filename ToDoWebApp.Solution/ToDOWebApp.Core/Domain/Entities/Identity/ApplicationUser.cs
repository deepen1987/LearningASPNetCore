using Microsoft.AspNetCore.Identity;

namespace ToDOWebApp.Core.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
