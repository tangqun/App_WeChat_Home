using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helper_9H;
using IDAL_9H;
using Model_9H;

namespace DAL_9H
{
    public class ComponentAccessTokenDAL : IComponentAccessTokenDAL
    {
        public string Get()
        {
            string url_component_access_token = ConfigHelper.Domain_Token + "api/component_access_token/get";

            LogHelper.Info("url_component_access_token: " + url_component_access_token);

            // token 中控器
            string resp_component_access_token = HttpHelper.Get(url_component_access_token);

            RESTfulModel r = JsonConvert.DeserializeObject<RESTfulModel>(resp_component_access_token);

            return r.Data.ToString();
        }
    }
}