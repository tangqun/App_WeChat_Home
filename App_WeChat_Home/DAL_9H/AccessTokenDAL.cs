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
        public AuthorizationInfoModel Get(string authorizerAppID)
        {
            string url = ConfigHelper.DomainToken + "api/accesstoken/get?authorizerappid=" + authorizerAppID;

            LogHelper.Info("获取access_token url", url);

            string responseBody = HttpHelper.Get(url);

            LogHelper.Info("获取access_token responseBody", responseBody);

            RESTfulModel resp = JsonConvert.DeserializeObject<RESTfulModel>(responseBody);

            AuthorizationInfoModel authorizationInfoModel = JsonConvert.DeserializeObject<AuthorizationInfoModel>(resp.Data.ToString());

            return authorizationInfoModel;
        }
    }
}