using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_9H.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        public RESTfulModel Test()
        {
            return new RESTfulModel() { Code = 1, Msg = "呵呵" };
        }
    }
}
