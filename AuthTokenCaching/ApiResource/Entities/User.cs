using System;
using System.Text.Json.Serialization;

namespace ApiResource.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }

    public static class UserExtensions
    {
        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
    }
}