using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                Id = Guid.NewGuid(),
                Name = "sample user",
                Username = "username",
                Password = "password"
            }
        };

        public async Task<AuthenticationResponse> Authenticate(string username, string password)
        {
            var user = await Task.Run(() =>
                _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            var token = GenerateJwtToken(user);
            return new AuthenticationResponse(user, token);
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await Task.Run(() =>
                _users.SingleOrDefault(x => x.Id == id));
            return user?.WithoutPassword();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString())}),
                Expires = DateTime.UtcNow.AddSeconds(3600),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}