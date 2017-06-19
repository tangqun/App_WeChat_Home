using BLL_9H;
using Helper_9H;
using IBLL_9H;
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
        private IUserInfoBLL userInfoBLL = new UserInfoBLL();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            RESTfulModel restfulModel = userInfoBLL.Login(model);
            if (restfulModel.Code == 0)
            {
                Session["user"] = "";
            }

            return Content(userInfoBLL.Login(model).ToString(), "application/json");
        }
    }
}