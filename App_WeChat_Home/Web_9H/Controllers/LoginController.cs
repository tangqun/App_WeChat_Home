using Helper_9H;
using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_9H.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            //DateTime dt = DateTime.Now;
            //Session["user"] = new UserInfoModel() {
            //    ID = 1,
            //    Mobile = "15210470906",
            //    Salt = "matrix",
            //    Password = EncryptHelper.MD5Encrypt(EncryptHelper.MD5Encrypt("123456") + "matrix"),
            //    RealName = "唐群",
            //    UserStat = 1,
            //    LoginErrorTimes = 0,
            //    CreateTime = dt,
            //    UpdateTime = dt
            //};
            //return Redirect("/home");
            return View();
        }
    }
}