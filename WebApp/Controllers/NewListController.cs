using com.wbs.bll;
using com.wbs.dao;
using com.wbs.model;
using commen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class NewListController : Controller
    {
        // GET: NewList
        NewInfoService newInfoService = new NewInfoService();
        NewCommentService commentService = new NewCommentService();

        #region 新闻前台首页
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize = 5;
            int pageCount = newInfoService.GetPagecount(pageSize);//得到页数
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
            List<NewInfo> list = newInfoService.GetPageList(pageIndex, pageSize);
            ViewData["list"] = list;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = pageCount;
            return View();
        }
        #endregion


        #region 前台展示新闻的详细信息
        public ActionResult ShowDetail()
        {
            int id = int.Parse(Request["id"]);
            NewInfo newInfo = newInfoService.GetModel(id);
            ViewData.Model = newInfo;
            return View();
        }
        #endregion



        #region 添加评论
        public ActionResult AddComment()
        {
            int id = int.Parse(Request["id"]);
            string msg = Request["msg"];
            T_NewComment comment = new T_NewComment();
            comment.Msg = msg;
            comment.NewId = id;
            comment.CreateDateTime = DateTime.Now;
            if (commentService.InsertComments(comment))
            {
                return Content("ok");//评论成功
            }
            else
            {
                return Content("no");//评论失败
            }
        }
        #endregion


        #region 加载评论
        public ActionResult LoadComment()
        {
            //加载评论
            int id = int.Parse(Request["id"]);
            List<T_NewComment> list = commentService.GetNewsComments(id);//查询 评论信息
            List<CommentViewModel> newlist = new List<CommentViewModel>();
            if (list != null)
            {
                //当有评论的时候，遍历
                foreach (var commentInfo in list)
                {
                    CommentViewModel viewModel = new CommentViewModel();
                    TimeSpan timeSpan = DateTime.Now - commentInfo.CreateDateTime; //表示一个时间间隔
                    viewModel.CreateDateTime = Commen.GetTimeSpan(timeSpan);//返回评论的时间
                    viewModel.Msg = commentInfo.Msg;
                    newlist.Add(viewModel);
                }
            }
            return Json(newlist, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}