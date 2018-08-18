using com.wbs.dao;
using com.wbs.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wbs.bll
{
    public class NewInfoService
        //newInfo的service层
    {
        NewInfoDao newInfoDao = new NewInfoDao();
    
        /// <param name="pageIndex">当前页码值</param>
        /// <param name="pageSize">每页显示条数</param>
     
        public List<NewInfo> GetPageList(int pageIndex,int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            List<NewInfo> list = newInfoDao.GetPageList(start,end);
            return list;
        }
        //获取总的页数
        public int GetPagecount(int pageSize)
        {
            int recordCount = newInfoDao.GetRecordCount();//获取总的页数
            int pageCount=Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));//使用此函数可以防止页数是小数时候出错
            return pageCount;
        }
        //获取详细的某一条信息
        public NewInfo GetModel(int id)
        {
            return newInfoDao.GetModel(id);
        }
        //删除一条数据
        public bool DeleteNewInfo(int id)
        {
            return newInfoDao.DeleteNewInfo(id)>0;
        }
        public bool AddNewInfo(NewInfo newInfo)
        {
            return newInfoDao.AddNewInfo(newInfo)>0;//添加一条记录,返回的是改变的记录的行数
        }
        public bool UpdateEntityInfo(NewInfo newinfo)
        {
            return newInfoDao.UpdateEntityInfo(newinfo)>0;
        }
    }
}
