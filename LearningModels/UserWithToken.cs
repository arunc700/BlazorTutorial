using System;
using System.Collections.Generic;
using System.Text;

namespace LearningModels
{
    public class UserWithToken : User
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            this.UserId = user.UserId;
            this.Email = user.Email;
            this.RoleId = user.RoleId;
            this.Role = user.Role;
            this.CreatedOn = user.CreatedOn;
        }
    }
}
