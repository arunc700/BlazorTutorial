﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LearningModels
{
    public class UserRefreshToken
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }       
        public DateTime ExpiryDate { get; set; }

        public virtual User User { get; set; }
    }
}
