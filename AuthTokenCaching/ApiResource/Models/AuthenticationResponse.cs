using System;
using ApiResource.Entities;

namespace ApiResource.Models
{
    public class AuthenticationResponse
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Username { get; }
        public string AccessToken { get; }
        
        public int ExpiresIn { get; }

        public AuthenticationResponse(User user, string token, int expiry)
        {
            Id = user.Id;
            Name = user.Name;
            Username = user.Username;
            AccessToken = token;
            ExpiresIn = expiry;
        }
    }
}