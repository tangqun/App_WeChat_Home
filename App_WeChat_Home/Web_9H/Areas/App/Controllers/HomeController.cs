using BLL_9H;
using IBLL_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_9H.Controllers;

namespace Web_9H.Areas.App.Controllers
{
    public class HomeController : BaseController
    {
        private IAuthorizerInfoBLL authorizerInfoBLL = new AuthorizerInfoBLL();

        public ActionResult Index(int id)
        {
            Session["authorizer"] = authorizerInfoBLL.GetModel(id);
            return View();
        }
    }
}