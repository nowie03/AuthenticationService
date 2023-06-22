using AuthenticationService.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthenticationService.Utils
{
    public class JwtClient
    {
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWbWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public static string GenerateToken(int userId,String role)
        {
            try
            {
                var symmetricKey = Convert.FromBase64String(Secret);
                var tokenHandler = new JwtSecurityTokenHandler();

                var now = DateTime.UtcNow;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                 {
                  new Claim(ClaimTypes.Sid,userId.ToString()),
                  new Claim(ClaimTypes.Role,role)
                 }),
                    Expires = now.AddHours(24),
                    SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var stoken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(stoken);
                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static (bool, int, String) ValidateToken(string token)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var payload = jwtToken.Payload;
                var UserId = int.Parse( payload.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value);
                var Name = payload.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                Console.WriteLine(UserId);


                return (true, UserId, Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (false, -1, null);
            }
        }

    }
}
