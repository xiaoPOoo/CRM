using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace com.wbs.dao
{
    public class SqlHelper
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        public static DataTable GetTable(String sql, CommandType type, params SqlParameter[]paras)
        {
            //CommandType用于指定是不是存储过程
            using (SqlConnection conn=new SqlConnection(connString))
            {
                using (SqlDataAdapter apter=new SqlDataAdapter(sql, conn))
                {
                    apter.SelectCommand.CommandType = type;//类型
                    if (paras != null)//参数是否为空
                    {
                        apter.SelectCommand.Parameters.AddRange(paras);//参数不为空的时候，加入参数
                    }
                    DataTable da = new DataTable();
                    apter.Fill(da);
                    return da;
                }
            }
            
        }
        public static int ExecuteNonquery(string sql, CommandType type ,params SqlParameter[] paras)
        {
            //查询函数
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd=new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (paras != null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();//返回对应的行数
                }
            }

        }
        public static object ExecuteScalare(string sql, CommandType type, params SqlParameter[] paras)
        {
            //查询函数,返回一行一列
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = type;
                    if (paras != null)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();//返回一个object类型
                }
            }
        }

    }
}
