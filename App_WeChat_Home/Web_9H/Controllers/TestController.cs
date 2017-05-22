using Model_9H;
using Newtonsoft.Json;
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
            //string json = JsonConvert.SerializeObject(new RESTfulModel() { Code = 1, Msg = "失败" });

            //return Content(json, "application/json");

            return View(new RESTfulModel() { Code = 1, Msg = "失败" });
        }
    }
}