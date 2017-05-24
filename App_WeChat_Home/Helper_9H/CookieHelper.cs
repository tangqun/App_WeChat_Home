using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helper_9H
{
    public class CookieHelper
    {
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            DateTime expireTime = DateTime.Now.AddDays(7);
            SetCookie(cookieName, cookieValue, expireTime, ConfigHelper.Domain);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="cookieValue"></param>
        /// <param name="expireTime"></param>
        /// <param name="domain"></param>
        public static void SetCookie(string cookieName, string cookieValue, DateTime expireTime, string domain)
        {
            if (!string.IsNullOrEmpty(cookieName) && !string.IsNullOrEmpty(cookieValue))
            {
                HttpContext.Current.Response.Cookies.Remove(cookieName);
                HttpCookie cookie = new HttpCookie(cookieName);
                cookie.Value = (cookieValue.Length == 0) ? string.Empty : cookieValue;
                if (expireTime != null)
                {
                    cookie.Expires = expireTime;
                }
                if (!string.IsNullOrEmpty(domain))
                {
                    cookie.Domain = domain;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string GetCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            string cookieValue = string.Empty;
            if (cookie != null)
            {
                cookieValue = cookie.Value;
            }
            return cookieValue;
        }

        /// <summary>
        /// 设置Cookie过期
        /// </summary>
        /// <param name="cookieName"></param>
        public static void ClearCookie(string cookieName)
        {
            ClearCookie(cookieName, ConfigHelper.Domain);
        }

        /// <summary>
        /// 设置Cookie过期
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="domain"></param>
        public static void ClearCookie(string cookieName, string domain)
        {
            if (!string.IsNullOrEmpty(cookieName))
            {
                HttpContext.Current.Response.Cookies.Remove(cookieName);
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    cookie.Domain = domain;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }
    }
}
