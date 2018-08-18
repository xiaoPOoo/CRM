using System;
using System.Collections.Generic;
using System.Text;

namespace com.wbs.model
{
    public class NewInfo
    {
        //新闻实体类
        public int Id { get; set; }
        public string Title { get; set; }
        public string Msg { get; set; }
        public string ImagePath { set; get; }
        public string  Author { set; get; }
        public DateTime SubDataTime { set; get; }
    }
}
