using ToDOWebApp.Core.Domain.Entities.Identity;
using ToDOWebApp.Core.DTO;

namespace ToDOWebApp.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}
