using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDOWebApp.Core.Domain.Entities.Identity;
using ToDOWebApp.Core.DTO;
using ToDOWebApp.Core.ServiceContracts;

namespace ToDOWebApp.Core.Services
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
            // Step 1
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JWT:Expiration_Time"]));

            // Step 2
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Subject User
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // New Guid everytime token is generated
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()), // Issued at (date and time of token generation)
                new Claim(ClaimTypes.NameIdentifier, user.PersonName) //Name of the user
            };

            // Step 3
            SymmetricSecurityKey secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));

            // Step 4 - Setting up which algorithm to use.
            SigningCredentials signingCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGen = new JwtSecurityToken(
                _config["JWT:Issuer"],
                _config["JWT:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGen);

            var respose = new AuthenticationResponse
            {
                Token = token,
                Email = user.Email,
                UserName = user.PersonName,
                ExpirationTime = expiration,
            };

            return respose;
        }
    }
}
