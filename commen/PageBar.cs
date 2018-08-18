using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commen
{
    public class PageBar
    {
        //产生数字页码条，要求页面上边有十个页码
        public static string GetPageBar(int pageindex,int pagecount)
        {
            //pageindex:当前页码值；pageCount:总的页码数
            if (pagecount == 1)
            {
                return string.Empty;//如果只有一页的话就不用页码条
            }
            int start = pageindex - 5;//页面上的页码的起始位置
            if (start < 1)
            {
                start = 1;
            }
            int end = start + 9;//终止页码
            if (end > pagecount)
            {
                end = pagecount;//不能超过总的页码
            }
            StringBuilder sb = new StringBuilder();
            if (pageindex > 1)
            {
                sb.Append(string.Format("<a href='?pageIndex={0}'>上一页</a>", pageindex - 1));
                //添加上一页的链接
            }
            
            for(int i = start; i <= end; i++)
            {
                
                if (i == pageindex)
                {
                    sb.Append(i);//如果页数是当前的页码，不需要加超链接，否则加
                }
                else
                {
                    sb.Append(string.Format("<a href='?pageIndex={0}'>{0}</a>",i));
                }
            }
            if (pageindex < pagecount)
            {
                sb.Append(string.Format("<a href='?pageIndex={0}'>下一页</a>",pageindex+1)); 
                //添加下一页的链接
            }
            return sb.ToString();
        }
    }
}
