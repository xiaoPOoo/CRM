using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wbs.model;

using System.Data;
using System.Data.SqlClient;
namespace com.wbs.dao
{
    public class NewsCommentDao
    {
        //评论的添加
        public int InsertNewsComments(T_NewComment comment )
        {
            string sql = "insert into T_NewComments(NewId,Msg,CreateDateTime) values(@NewId,@New,@CreateDateTime)";
            SqlParameter[] paras =
            {
                new SqlParameter("@NewId",SqlDbType.Int,4),//长度为4
                new SqlParameter("@New",SqlDbType.NVarChar),
                new SqlParameter("@CreateDateTime",SqlDbType.DateTime)
            };
            paras[0].Value = comment.NewId;
            paras[1].Value = comment.Msg;
            paras[2].Value = comment.CreateDateTime;
            return SqlHelper.ExecuteNonquery(sql, CommandType.Text, paras);//插入一条评论
        }
        //根据新闻的id查找新闻的评论
        public List<T_NewComment> GetNewCommentList(int Newsid)
        {
            string sql = "select * from T_NewComments where NewId=@NewId";
            DataTable dt = SqlHelper.GetTable(sql, CommandType.Text, new SqlParameter("@NewId",Newsid));
            List<T_NewComment> commentList = null;//评论的列表
            if (dt.Rows.Count > 0)
            {
                commentList = new List<T_NewComment>();
                foreach(DataRow row in dt.Rows)
                {
                    T_NewComment temp = new T_NewComment();//临时存放每一条评论记录
                    LoadEntity(row, temp);
                    commentList.Add(temp);
                }                
            }
            return commentList;
        }
        public void LoadEntity(DataRow row, T_NewComment temp)
        {
            temp.Id = Convert.ToInt32(row["id"]);
            temp.NewId = Convert.ToInt32(row["NewId"]);
            temp.Msg = row["Msg"] != null ? row["Msg"].ToString() : string.Empty;
            temp.CreateDateTime = Convert.ToDateTime(row["CreateDateTime"]);
        }


    }
}
