using System;
using System.Collections.Generic;
using System.Text;

namespace LearningModels
{
    public partial class Role
    {
        public Role()
        {
            //Users = new HashSet<User>();
        }

        public short RoleId { get; set; }
        public string RoleDesc { get; set; }

        //public virtual ICollection<User> Users { get; set; }
    }
}
