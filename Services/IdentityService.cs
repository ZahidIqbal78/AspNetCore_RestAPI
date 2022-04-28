using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCore_RestAPI.DTOs.V1;
using AspNetCore_RestAPI.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCore_RestAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtOptions jwtOptions;

        public IdentityService(UserManager<IdentityUser> userManager,
            JwtOptions jwtOptions)
        {
            this.userManager = userManager;
            this.jwtOptions = jwtOptions;
        }
        public async Task<AuthenticationResponse> RegisterUserAsync(string email, string password)
        {
            var existingUser = await this.userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return new AuthenticationResponse
                {
                    Success = false,
                    ErrorMessages = new[] { $"User already exists with {email} email" }
                };
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email
            };
            var result = await this.userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                return new AuthenticationResponse
                {
                    ErrorMessages = result.Errors.Select(x => x.Description),
                    Success = false
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

            return new AuthenticationResponse{
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}