using System;
using System.Collections.Generic;
using System.Text;

namespace LearningModels
{
    public class UserRefreshRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
