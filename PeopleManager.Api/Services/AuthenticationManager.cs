using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PeopleManager.Api.Settings;
using PeopleManager.Dto.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PeopleManager.Api.Services
{
    public class AuthenticationManager(JwtSettings jwtSettings)
    {
        public string GenerateJwtToken(IdentityUser user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.Add(jwtSettings.ExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = handler.CreateToken(descriptor);

            return handler.WriteToken(token);
        }
    }
}
