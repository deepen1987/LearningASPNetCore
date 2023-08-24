using AppWeb.Core.Domain.Entities.Identity;
using AppWeb.Core.DTO;
using AppWeb.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), // Issued at (date and time of token generation)
                new Claim(ClaimTypes.NameIdentifier, user.Email),
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
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpirationDateTime = DateTime
                .Now
                .AddMinutes(
                    Convert.ToDouble(_config["RefreshToekn:EXPIRATION_MINUTES"])
                    ),
                ExpirationTime = expiration
            };
        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
        {
            //1. We add all the validation parameters we need to validate if a token is valid.
            var tokenValidationParameters = new TokenValidationParameters() 
            {
                ValidateAudience = true,
                ValidAudience= _config["Jwt:Audience"],
                ValidateIssuer = true,
                ValidIssuer = _config["JWT:Issuer"],
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]))
            };

            //2. We use below jwtTokenHandler to fetch user details from the token 
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //3. we use ValidateToken method which alidates a toekn based on token validate parameters
            // and spits out a security Token if its valid or not.
            ClaimsPrincipal principal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        
            //4. securityToken is not of JwtSecurityToken class its invalid 
            if(securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }

        private string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }
    }
}
