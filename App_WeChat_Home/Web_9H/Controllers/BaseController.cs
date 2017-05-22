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
        public UserModel LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session != null && Session["user"] != null)
            {
                LoginUser = Session["user"] as UserModel;
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(500);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}