using BLL_9H;
using Helper_9H;
using IBLL_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_9H.Controllers
{
    public class HomeController : BaseController
    {
        private IAuthorizationBLL authorizationBLL = new AuthorizationBLL();
        private IAuthorizerInfoBLL authorizerInfoBLL = new AuthorizerInfoBLL();

        public ActionResult Index()
        {
            return View(authorizerInfoBLL.GetList(CurrentUser.BusinessID));
        }

        public ActionResult GoAuth()
        {
            return Redirect(authorizationBLL.GetPreAuthCodeUrl());
        }

        public ActionResult SaveAuth(string auth_code, int expires_in)
        {
            authorizationBLL.SaveAuth(auth_code, expires_in, CurrentUser.BusinessID);
            return RedirectToAction("index");
        }
    }
}