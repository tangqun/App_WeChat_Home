using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_9H.Areas.App.Controllers
{
    public class HomeController : Controller
    {
        // GET: App/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}