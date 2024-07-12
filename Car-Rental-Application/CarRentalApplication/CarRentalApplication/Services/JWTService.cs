using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedDetails.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalApplication.Services
{
    public class JWTService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _jwtKey;
        private readonly UserManager<User> _userManager;

        public JWTService(IConfiguration config, UserManager<User> userManager)
        {
            _userManager = userManager;
            _config = config;
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        }

        public string CreateJWT(User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),

            };

            var roles = _userManager.GetRolesAsync(user).Result;
            if (roles.Any())
            {
                var rolesClaim = new Claim(ClaimTypes.Role, string.Join(",", roles));
                userClaims.Add(rolesClaim);
            }

            var credentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha256Signature);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_config["JWT:ExpiresInDays"])),
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"],

            };
            var TokenHandler = new JwtSecurityTokenHandler();
            var jwt = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(jwt);
            
        }
    }
}
