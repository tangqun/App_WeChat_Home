using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_9H.Controllers;

namespace Web_9H.Areas.App.Controllers
{
    public class AppBaseController : BaseController
    {
        protected AuthorizerInfoModel CurrentAuthorizer { get; set; }

        protected override void OnAppActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session != null && Session["authorizer"] != null)
            {
                CurrentAuthorizer = Session["authorizer"] as AuthorizerInfoModel;
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(500);
            }

            base.OnAppActionExecuting(filterContext);
        }
    }
}