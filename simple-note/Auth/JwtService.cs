using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace simple_note.Auth
{
    public class JwtService
    {
        private readonly string secureKey = "this is private secure string. hidden!!!!";
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            secureKey = _configuration["Jwt:Key"];
        }

        public string Generate(int id)
        {
            var symmetricSecurityKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var crediential = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(crediential);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Now.AddHours(2));

            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }

}
