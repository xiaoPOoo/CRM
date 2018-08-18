using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wbs.dao;
using com.wbs.model;

namespace com.wbs.bll
{
    public class NewCommentService
    {
        NewsCommentDao comments = new NewsCommentDao();
        public bool InsertComments(T_NewComment newCommentInfo)
        {
            return comments.InsertNewsComments(newCommentInfo) > 0;//插入评论
        }
        public List<T_NewComment> GetNewsComments(int newsid)
        {
            return comments.GetNewCommentList(newsid);//返回评论列表
        }
    }
}
