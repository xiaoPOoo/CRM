using com.wbs.bll;
using com.wbs.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace WebApp.Controllers
{
    public class AdminNewsListController : BaseController//为了统一完成 校验，继承BaseController来完成用户登录的校验
    {
        //新闻列表

        // GET: AdminNewsList
        NewInfoService newInfoService = new NewInfoService();
        #region 分页列表
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 5;
            int pageCount = newInfoService.GetPagecount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;//判断页码是否小于1
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex; //判断页码是否大于总的页数
            List<NewInfo> list = newInfoService.GetPageList(pageIndex, pageSize);//获取分页数据
            ViewData["list"] = list;//用viewData可以将页面的数据传入到后台
            ViewData["pageindex"] = pageIndex;
            ViewData["pagecoune"] = pageCount;
            return View();
        }
        #endregion
        
        #region 获取某条新闻的详细记录
        
        public ActionResult GetNewInfoModel()
        {
            int id = int.Parse(Request["id"]);
            NewInfo newInfo=newInfoService.GetModel(id);//获取到了详细信息
            return Json(newInfo, JsonRequestBehavior.AllowGet);//JsonRequestBehavior.AllowGet加了这个防止以get方式提交时候报异常
            //return Content("aaaaaa");
            
        }
        #endregion
        #region 删除信息
        public ActionResult DeleteNewInfoModel()
        {
            int id = int.Parse(Request["id"]);
            if (newInfoService.DeleteNewInfo(id))
            {
                return Content("ok");//删除成功
            }
            else
            {
                return Content("no");//删除失败
            }
            

        }

        #endregion
        #region 展示添加信息
        public ActionResult ShowAndInfo()
        {
            //添加模板
            return View();
        }
        #endregion
        #region 文件上传
        public ActionResult FileUpLoad() 
        {
            HttpPostedFileBase postFile = Request.Files["fileUp"];
            if (postFile == null)
            {
                return Content("no:上传文件失败");
            }
            else
            {
                string fileName = Path.GetFileName(postFile.FileName);//获取上传的文件名
                string fileExt = Path.GetExtension(fileName);//获取文件的扩展名
                if (fileExt == ".jpg")
                {
                    string dir = "/ImagePath/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建文件夹
                    string newfineName = Guid.NewGuid().ToString();//新的文件名
                    string fullDir = dir + newfineName + fileExt;//完整的路径
                    postFile.SaveAs(Request.MapPath(fullDir));//保存这个文件
                    return Content("ok:" + fullDir);
                }
                else
                {
                    return Content("no:文件格式错误");
                }
            }
        }
        #endregion
        #region 添加信息
        [ValidateInput(false)]//可以接收提交过来的input标签
        public ActionResult AddNewInfo(NewInfo newInfo)//此处用到了自动填充，如果提交的name属性和newinfo的属性的名称一样的时候，自动填充到newInfo对象中
        {
            newInfo.SubDataTime = DateTime.Now;
            if (newInfoService.AddNewInfo(newInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 展示编辑用户信息
        [ValidateInput(false)]//可以接收提交过来的input标签
        public ActionResult ShowEdit()
        {
            int id = int.Parse(Request["id"]);
            NewInfo newInfo = newInfoService.GetModel(id);//得到新闻信息
            ViewData.Model = newInfo;
            return View();
        }
        #endregion

        #region 保存更改的用户信息
        public ActionResult EditNewInfo(NewInfo newinfo)
        {
            newinfo.SubDataTime = DateTime.Now;
            if (newInfoService.UpdateEntityInfo(newinfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
    }
}