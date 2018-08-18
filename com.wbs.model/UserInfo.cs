using System;
using System.Collections.Generic;
using System.Text;

namespace com.wbs.model
{
    public class UserInfo
    {
        //用户
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string UserEmail { get; set; }
        public DateTime RegTime { get; set; }
    }
}
