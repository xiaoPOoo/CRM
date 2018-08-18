using com.wbs.dao;
using com.wbs.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wbs.bll
{
    public class UserInfoService
    {
        UserInfoDao userInfoDao = new UserInfoDao();
        public UserInfo GetUserInfo(string userNme,string userPwd)
        {
            return userInfoDao.GetUserInfo(userNme, userPwd);
        }
    }
}
