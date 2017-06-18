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
    public class HomeController : Controller
    {
        private IAuthBLL authBLL = new AuthBLL();
        private IAuthorizerInfoBLL authorizerInfoBLL = new AuthorizerInfoBLL();

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult GoAuth()
        //{
        //    string component_appid = ConfigHelper.ComponentAppId;
        //    string pre_auth_code = authBLL.GetPreAuthCode();
        //    string redirect_uri = ConfigHelper.Domain + "home/recvauth";
        //    return Redirect("https://mp.weixin.qq.com/cgi-bin/componentloginpage?component_appid=" + component_appid + "&pre_auth_code=" + pre_auth_code + "&redirect_uri=" + redirect_uri);
        //}

        //public ActionResult RecvAuth(string auth_code, int expires_in)
        //{
        //    return View(authBLL.RecvAuth(auth_code, expires_in, CurrentUser.ID));
        //}
    }
}