using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.DTO;

namespace Forum.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        public UserDto ToDto()
        {
            return new UserDto { UserName = UserName };
        }
    }
}