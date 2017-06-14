using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web_9H.Controllers
{
    public class BaseController : Controller
    {
        protected UserInfoModel CurrentUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session != null && Session["user"] != null)
            {
                CurrentUser = Session["user"] as UserInfoModel;
            }
            else
            {
                filterContext.Result = new RedirectResult("/login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}