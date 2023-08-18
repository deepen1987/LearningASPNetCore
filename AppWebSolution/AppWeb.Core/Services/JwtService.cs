using AppWeb.Core.Domain.Entities.Identity;
using AppWeb.Core.DTO;
using AppWeb.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppWeb.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public AuthenticationResponse CreateJwtToken(ApplicationUser user)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JWT:EXPIRATION_MINUTES"]));

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Subject User
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // New Guid everytime token is generated
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()), // Issued at (date and time of token generation)
                new Claim(ClaimTypes.NameIdentifier, user.PersonName) //Name of the user
            };

            SymmetricSecurityKey secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            SigningCredentials signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationResponse()
            {
                Token = token,
                Email = user.Email,
                PersonName = user.PersonName,
                ExpirationTime = expiration
            };
        }
    }
}
