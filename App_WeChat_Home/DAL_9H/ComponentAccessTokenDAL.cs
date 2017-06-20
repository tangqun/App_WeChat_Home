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
            string url = ConfigHelper.DomainToken + "api/componentaccesstoken/get";

            LogHelper.Info("获取component_access_token url", url);

            string responseBody = HttpHelper.Get(url);

            LogHelper.Info("获取component_access_token responseBody", responseBody);

            RESTfulModel resp = JsonConvert.DeserializeObject<RESTfulModel>(responseBody);

            return resp.Data.ToString();
        }
    }
}