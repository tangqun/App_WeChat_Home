using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Helper_9H
{
    public class ConfigHelper
    {
        public static string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        public static string Component_AppId = GetConfig("Component_AppId");
        public static string Domain_Token = GetConfig("Domain_Token");
        public static string Domain = GetConfig("Domain");

        public static string GetConfig(string key)
        {
            string value = "";
            try
            {
                value = ConfigurationManager.AppSettings[key].Trim();
            }
            catch
            {
                value = "";
            }
            return string.IsNullOrEmpty(value) ? "" : value;
        }
    }
}
