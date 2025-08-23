using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Unohana.Api.Helpers
{
    public class JwtCreater
    {
        public string GetToken(double id)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,id.ToString()),
                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()
                    )
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
                ));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
