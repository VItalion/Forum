using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.Interfaces;
using Microsoft.AspNet.Identity;

namespace Forum.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IUser Get(string username)
        {
            return context.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}