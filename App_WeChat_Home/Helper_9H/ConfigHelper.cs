using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper_9H
{
    public class ConfigHelper
    {
        public static string ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

        public static string ComponentAppId = GetConfig("ComponentAppId");
        public static string DomainToken = GetConfig("DomainToken");
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
