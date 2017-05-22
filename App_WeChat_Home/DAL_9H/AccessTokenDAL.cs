using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IDAL_9H;
using Helper_9H;
using Model_9H;
using Newtonsoft.Json;

namespace DAL_9H
{
    public class AccessTokenDAL : IAccessTokenDAL
    {
        public string Get(string appid)
        {
            string url_access_token = ConfigHelper.Domain_Token + "api/access_token/get?authorizer_appid=" + appid;

            LogHelper.Info("url_access_token: " + url_access_token);

            string resp_access_token = HttpHelper.Get(url_access_token);

            RESTfulModel r = JsonConvert.DeserializeObject<RESTfulModel>(resp_access_token);

            return r.Data.ToString();
        }
    }
}