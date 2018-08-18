using System;
using System.Collections.Generic;
using System.Text;

namespace com.wbs.model
{
    public class T_NewComment
    {
        //评论实体
        public int Id { get; set; }
        public int NewId { get; set; }
        public string Msg { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
