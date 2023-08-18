using AppWeb.Core.Domain.Entities.Identity;
using AppWeb.Core.DTO;

namespace AppWeb.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}
