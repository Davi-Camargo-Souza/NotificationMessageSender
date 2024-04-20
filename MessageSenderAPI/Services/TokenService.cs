using Microsoft.IdentityModel.Tokens;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotificationMessageSender.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(UserEntity user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(UserEntity user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(
                new Claim(ClaimTypes.Name, user.Id.ToString())
            );

            foreach (var role in user.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));

            return ci;
        }
    }
}
