using System;
using System.Collections.Generic;
using System.Text;

namespace LearningModels
{
    public partial class User
    {
        public User()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }         
        public short RoleId { get; set; }       
        public DateTime? CreatedOn { get; set; }       
        public virtual Role Role { get; set; }
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
