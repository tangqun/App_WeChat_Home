using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_9H.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return Json(new RESTfulModel() { Code = 0, Msg = "成功" }, JsonRequestBehavior.AllowGet);
        }
    }
}