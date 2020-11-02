using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiResource.Entities;
using ApiResource.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiResource.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> Authenticate(string username, string password);
        Task<User> GetById(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private List<User> _users = new List<User>
        {
            new User
            {
                Id = Guid.Parse("3e130be5-4d3d-49a4-aaaf-a33fe57e53b9"),
                Name = "sample user",
                Username = "username",
                Password = "password"
            }
        };

        public async Task<AuthenticationResponse> Authenticate(string username, string password)
        {
            var user = await Task.Run(() =>
                _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            var (token, expiry) = GenerateJwtToken(user);
            return new AuthenticationResponse(user, token, expiry);
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await Task.Run(() =>
                _users.SingleOrDefault(x => x.Id == id));
            return user?.WithoutPassword();
        }

        private (string Token, int Expiry) GenerateJwtToken(User user)
        {
            const int EXPIRES_SECONDS = 3600;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddSeconds(EXPIRES_SECONDS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), EXPIRES_SECONDS);
        }
    }
}