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
    public class NewInfoDao
    {
        //分页
        public List<NewInfo> GetPageList(int start, int end)
        {
            string sql = "select * from (select row_number() over(order by id) as num,* from T_News) as t where t.num>=@start and t.num<=@end";
            SqlParameter[] paras = {
                //new SqlParameter("@start",start),
                //new SqlParameter("@end",end)//这两句最好先确定类型，然后再给他们赋值，如下：
                new SqlParameter("@start",SqlDbType.Int),
                new SqlParameter("@end",SqlDbType.Int)
            };
            //再给他们赋值
            paras[0].Value = start;
            paras[1].Value = end;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, paras);
            List<NewInfo> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<NewInfo>();
                NewInfo newInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    //遍历所有的行
                    newInfo = new NewInfo();
                    LoadEntity(row, newInfo);
                    //Console.WriteLine("aaaaaaa");
                    //Console.WriteLine(newInfo);
                    list.Add(newInfo);
                }
            }
            return list;
        }
        public void LoadEntity(DataRow row, NewInfo newinfo)
        {
            newinfo.Id = Convert.ToInt32(row["ID"]);
            newinfo.Author = row["Author"] != DBNull.Value ? row["Author"].ToString() : string.Empty;
            newinfo.Title = row["Title"] != DBNull.Value ? row["Title"].ToString() : string.Empty;
            newinfo.Msg = row["Msg"] != DBNull.Value ? row["Msg"].ToString() : string.Empty;
            newinfo.ImagePath = row["ImagePath"] != DBNull.Value ? row["ImagePath"].ToString() : string.Empty;
            newinfo.SubDataTime = Convert.ToDateTime(row["SubDateTime"]);
        }
        public int GetRecordCount()
        {
            //求总的记录数
            string sql = "select count(*) from T_News";
            return Convert.ToInt32(SqlHelper.ExecuteScalare(sql, CommandType.Text));//总的记录数
        }
        public NewInfo GetModel(int id)
        {
            //获取详细内容中的一条新闻的信息
            string sql = "select * from T_News where id=@id";
            SqlParameter[] paras = {
                new SqlParameter("@id",SqlDbType.Int),
            };
            paras[0].Value = id;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, paras);
            NewInfo newInfo = null;
            if (da.Rows.Count > 0)
            {
                newInfo = new NewInfo();
                LoadEntity(da.Rows[0], newInfo);
            }
            return newInfo;//返回一条详细的新闻内容
        }
        public int DeleteNewInfo(int id)
        {
            //删除一条记录
            string sql = "delete from T_News where id=@id";
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, new SqlParameter("@id",id));
        }
        public int AddNewInfo(NewInfo newInfo)
        {
            //添加一条记录
            string sql = "insert into T_News(Author,Title,Msg,ImagePath,SubDateTime) " +
                "values(@Author,@Title,@Msg,@ImagePath,@SubDateTime) ";
            SqlParameter[] paras = {
                new SqlParameter("@Author",SqlDbType.NVarChar,32),
                new SqlParameter("@Title",SqlDbType.NVarChar,32),
                new SqlParameter("@Msg",SqlDbType.NVarChar),
                new SqlParameter("@ImagePath",SqlDbType.NVarChar,100),
                new SqlParameter("@SubDateTime",SqlDbType.DateTime) };
            paras[0].Value = newInfo.Author;
            paras[1].Value= newInfo.Title;
            paras[2].Value = newInfo.Msg;
            paras[3].Value = newInfo.ImagePath;
            paras[4].Value = newInfo.SubDataTime;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, paras);//插入一条记录的语句
        }
        public int UpdateEntityInfo(NewInfo newInfo)
        {
            string sql= "update T_News set Title=@Title,Msg=@Msg,Author=@Author,SubDateTime=@SubDateTime,ImagePath=@ImagePath where Id=@Id";
            SqlParameter[] paras = {
              new SqlParameter("@Title",SqlDbType.NVarChar,32),
              new SqlParameter("@Msg",SqlDbType.NVarChar),
              new SqlParameter("@Author",SqlDbType.NVarChar,32),
              new SqlParameter("@SubDateTime",SqlDbType.DateTime),
              new SqlParameter("@ImagePath",SqlDbType.NVarChar,100),
              new SqlParameter("@Id",SqlDbType.Int,4)
            };
            paras[0].Value = newInfo.Title;
            paras[1].Value = newInfo.Msg;
            paras[2].Value = newInfo.Author;
            paras[3].Value = newInfo.SubDataTime;
            paras[4].Value = newInfo.ImagePath;
            paras[5].Value = newInfo.Id;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, paras);
        }
    }
}
