using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskService.Domain.Interfaces.BaseRepository;
using TaskService.Infrastructure.Configurations;

namespace TaskService.Infrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IOptions<JwtConfiguration> _options;
        public IdentityRepository(IOptions<JwtConfiguration> options)
        {
            _options = options;
        }
        public string GenerateMockToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_options.Value.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "TestUser"),
                    new Claim(ClaimTypes.Name, "Admin")
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                Audience = _options.Value.ValidAudience,
                Issuer = _options.Value.ValidIssuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var generatedToken = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(generatedToken);

            return stringToken;
        }
    }
}
