using System;
using ApiResource.Entities;

namespace ApiResource.Models
{
    public class AuthenticationResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Username { get; }
        public string Token { get; }

        public AuthenticationResponse(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Username = user.Username;
            Token = token;
        }
    }
}