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
        // GET: Login
        public ActionResult Index()
        {
            DateTime dt = DateTime.Now;
            Session["user"] = new UserModel() {
                Id = 1,
                Mobile = "15210470906",
                Salt = "matrix",
                Password = EncryptHelper.MD5Encrypt(EncryptHelper.MD5Encrypt("123456") + "matrix"),
                Real_Name = "唐群",
                User_Stat = 1,
                Login_Error_Times = 0,
                Create_Time = dt,
                Update_Time = dt
            };
            return Redirect("/home");
        }
    }
}