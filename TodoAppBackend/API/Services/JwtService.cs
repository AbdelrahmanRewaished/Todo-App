using DataAccessLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IList<string> userRoles);
        string? GetUserEmail(HttpContext httpContext);
    }

    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(ApplicationUser user, IList<string> userRoles)
        {
            // Create a list of claims for the user
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!)
            };

            foreach(var userRole in userRoles) {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // Get the signing key from appsettings.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            // Create a credentials object with the key and algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Set the expiration time for the token
            var expiration = DateTime.UtcNow.AddMinutes(30);

            // Create a JwtSecurityToken object with the claims, credentials and expiration
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            // Write the token as a string and return it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? GetUserEmail(HttpContext httpContext)
        { 
            return httpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value; 
        }
    }
}
