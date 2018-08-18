using com.wbs.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace com.wbs.dao
{
    public class UserInfoDao
    {
        public UserInfo GetUserInfo(string username,string userpwd)
        {
            string sql = "select * from T_Userinfo where UserName=@UserName and UserPwd=@UserPwd";
            SqlParameter[] paras = {
                new SqlParameter("@UserName",SqlDbType.NVarChar, 32),
                new SqlParameter("@UserPwd", System.Data.SqlDbType.NVarChar, 32)
            };
            paras[0].Value = username;//将接收到的参数付给刚刚new的参数
            paras[1].Value = userpwd;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, paras);
            UserInfo userInfo = null;
            if (da.Rows.Count > 0)
            {
                userInfo = new UserInfo();
                LoadEntity(userInfo, da.Rows[0]);//将查找到的数据的第一条（按理只能有一条）放到userinfo中
            }
            return userInfo;//返回一个UserInfo
        }
        public void LoadEntity(UserInfo userInfo,DataRow row)
        {
            userInfo.Id = Convert.ToInt32(row["Id"]);//将用户信息写入到userinfo
            userInfo.UserName = row["UserName"] != null ? row["UserName"].ToString() : string.Empty;//判断下用户名是否为空
            userInfo.UserPwd = row["UserPwd"] != null ? row["UserPwd"].ToString() : string.Empty;
            userInfo.UserEmail = row["UserEmail"] != null ? row["UserEmail"].ToString() : string.Empty;
            userInfo.RegTime = Convert.ToDateTime(row["RegTime"]);

        }
    }
}
