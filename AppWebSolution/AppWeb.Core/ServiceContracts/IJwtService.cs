using AppWeb.Core.Domain.Entities.Identity;
using AppWeb.Core.DTO;
using System.Security.Claims;

namespace AppWeb.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);

        ClaimsPrincipal? GetPrincipalFromJwtToken(string?  token);
    }
}
