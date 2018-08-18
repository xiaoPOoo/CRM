using com.wbs.bll;
using com.wbs.model;
using com.wbs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        //约定大于配置
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            //基本登录
            string validateCode = Session["code"] == null ? string.Empty : Session["code"].ToString();//判断session中的验证码是否为空
            if (string.IsNullOrEmpty(validateCode))
            {
                //当为空或者null时候
                return Content("no:验证码未输入!!");
            }
            Session["code"] = null;//当不为空即验证通过，清空session，因为此时已经从session中把值取出来了
            string txtCode = Request["vCode"];
            if (!validateCode.Equals(txtCode,StringComparison.InvariantCultureIgnoreCase))
            {
                //如果输入的验证码不正确，一定不用区分大小写
                return Content("no:验证码错误");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];//拿到登录的密码和用户名
            UserInfoService userInfoService = new UserInfoService();
            UserInfo userInfo= userInfoService.GetUserInfo(userName, userPwd);//调用service层返回用户信息
            if (userInfo != null)
            {
                //如果用户存在//可以看看Ngnix
                Session["userinfo"] = userInfo;//必须存session
                return Content("ok:Login Success");
            }
            else
            {
                return Content("no:用户不存在");
            }              
        }
        public ActionResult ShowValidateCode()
        {
            //生成验证码的类，输出一张图片
            ValidateCode validateCode = new ValidateCode();
            string code=validateCode.CreateValidateCode(4);//输入4生成4位的验证码
            Session["code"] = code;//将验证码存入session，看用户输入的是不是正确的验证码
            byte []buffer=validateCode.CreateValidateGraphic(code);//将生成的code画到画布上边并返回
            return File(buffer, "image/jpeg");//返回生成的图片验证码
        }
    }
}