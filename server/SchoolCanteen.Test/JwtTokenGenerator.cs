using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolCanteen.Test
{
    public class JwtTokenGenerator
    {
        private IConfiguration Configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string GenerateJwtTokenWithAdminRole()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var validIssuer = config["Authentication:ValidIssuer"];
            var validAudience = config["Authentication:ValidAudience"];
            var claimNameSub = config["Authentication:ClaimNameSub"];
            var issuerSigningKey = Configuration["Authentication:IssuerSigningKey"];

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, claimNameSub),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                new Claim(ClaimTypes.Role, "Admin"),
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = validIssuer,
                Audience = validAudience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
                    SecurityAlgorithms.HmacSha256
                ),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateJwtTokenWithUserRole()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var validIssuer = config["Authentication:ValidIssuer"];
            var validAudience = config["Authentication:ValidAudience"];
            var claimNameSub = config["Authentication:ClaimNameSub"];
            var issuerSigningKey = Configuration["Authentication:IssuerSigningKey"];

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, claimNameSub),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")),
                new Claim(ClaimTypes.Role, "User"),
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = validIssuer,
                Audience = validAudience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
                    SecurityAlgorithms.HmacSha256
                ),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
