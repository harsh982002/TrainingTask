using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TrainingAssignment.Entities.Models;
using TrainingAssignment.Entities.ViewModels;
using TrainingAssignment.Models;

namespace TrainingAssignment.Helpers
{
    public class JwtHelper
    {
      private readonly IConfiguration _configuration;
        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private enum UserRole
        {
            User,
            Admin,
        }
        //generates the token
        public static string GenerateToken(JwtViewModel jwtSetting, User model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSetting.Key);
            var claims = new ClaimsIdentity(new Claim[]
              {
                new Claim(ClaimTypes.Name,model.Userid.ToString()),
                new Claim(ClaimTypes.UserData,model.Userid.ToString()),
                new Claim(ClaimTypes.NameIdentifier, model.Username + " "+ model.Surname),
                new Claim(ClaimTypes.Role, model.Role == "Admin"? nameof(UserRole.User):nameof(UserRole.Admin)),
            });
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSetting.Issuer,
                Audience = jwtSetting.Audience,
                IssuedAt = DateTime.UtcNow,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static ClaimsPrincipal? ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("261616fvbbgurbubrtuvbtuibuitbubuibuvuihubuiuituib");

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                //var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                //return claimsPrincipal;

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
