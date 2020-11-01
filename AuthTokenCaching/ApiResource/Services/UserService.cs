using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiResource.Models;

namespace ApiResource
{
    public class UserService : IUserService
    {
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
        
        public Task<User> Authenticate(string username, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}