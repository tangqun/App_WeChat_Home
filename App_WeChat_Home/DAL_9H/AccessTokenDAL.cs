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
        public string Get(string authorizerAppID)
        {
            string url = ConfigHelper.DomainToken + "api/accesstoken/get?authorizerappid=" + authorizerAppID;

            string responseBody = HttpHelper.Get(url);
            LogHelper.Info("获取access_token" + "\r\n\r\nresponseBody: " + responseBody);

            RESTfulModel r = JsonConvert.DeserializeObject<RESTfulModel>(responseBody);

            return r.Data.ToString();
        }
    }
}