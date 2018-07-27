using System;
using System.Collections;
using System.Web;
using System.Collections.Generic;

namespace ShInfoTech.Common
{
    /// <summary>
    /// COOKIE处理类
    /// </summary>
    public class CookieHelp
    {

        /// <summary>
        ///  删除COOKIE对象
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        public static void DelCookie(string strCookieName)
        {
            DelCookie(strCookieName, "");
        }

        /// <summary>
        /// 删除COOKIE对象
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strDomain">作用域,多个域名用;隔开</param>
        public static void DelCookie(string strCookieName, string strDomain)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            if (strDomain.Length > 0)
            {
                objCookie.Domain = strDomain;
            }
            objCookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// 删除某个COOKIE对象某个Key子键，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串null
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">>Key键名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期</param>
        /// <returns>如果对象本就不存在，则返回字符串null，如果操作成功返回字符串"success"。</returns>
        public static string DelCookieKey(string strCookieName, string strKeyName, int iExpires)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
            objCookie.Values.Remove(strKeyName);
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
            return "success";
        }

        /// <summary>
        ///  修改某个COOKIE对象某个Key键的键值 或 给某个COOKIE对象添加Key键 都调用本方法，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串null。
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">Key键名</param>
        /// <param name="KeyValue">Key键值</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期</param>
        /// <returns>如果对象本就不存在，则返回字符串null，如果操作成功返回字符串"success"。</returns>
        public static string EditCookie(string strCookieName, string strKeyName, string KeyValue, int iExpires)
        {
            return EditCookie(strCookieName, strKeyName, KeyValue, iExpires, "");
        }


        /// <summary>
        ///  修改某个COOKIE对象某个Key键的键值 或 给某个COOKIE对象添加Key键 都调用本方法，操作成功返回字符串"success"，如果对象本就不存在，则返回字符串null。
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">Key键名</param>
        /// <param name="KeyValue">Key键值</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)。注意：虽是修改功能，实则重建覆盖，所以时间也要重设，因为没办法获得旧的有效期</param>
        /// <param name="strDomains">作用域,多个域名用;隔开</param>
        /// <param name="strPath">作用路径</param>
        /// <returns>如果对象本就不存在，则返回字符串null，如果操作成功返回字符串"success"。</returns>
        public static string EditCookie(string strCookieName, string strKeyName, string KeyValue, int iExpires, string strDomain)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[strCookieName];
            objCookie[strKeyName] = HttpUtility.UrlEncode(KeyValue.Trim());
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
            return "success";
        }


        /// <summary>
        /// 读取Cookie某个对象的Value值，返回Value值，如果对象本就不存在，则返回字符串null
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <returns>Value值，如果对象本就不存在，则返回字符串null</returns>
        public static string GetCookie(string strCookieName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[strCookieName].Value);
        }

        /// <summary>
        /// 读取Cookie某个对象的某个Key键的键值，返回Key键值，如果对象本就不存在，则返回字符串null，如果Key键不存在，则返回字符串"KeyNonexistence"
        /// </summary>
        /// <param name="strCookieName">Cookie对象名称</param>
        /// <param name="strKeyName">Key键名</param>
        /// <returns>Key键值，如果对象本就不存在，则返回字符串null，如果Key键不存在，则返回字符串"KeyNonexistence"</returns>
        public static string GetCookie(string strCookieName, string strKeyName)
        {
            if (HttpContext.Current.Request.Cookies[strCookieName] == null)
            {
                return null;
            }
            string _value = HttpContext.Current.Request.Cookies[strCookieName][strKeyName];
            return HttpUtility.UrlDecode(_value);
        }

        /// <summary>
        /// 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="strValue">COOKIE对象Value值</param>
        public static void SetCookie(string strCookieName, string strValue)
        {
            SetCookie(strCookieName, 1, strValue, "");
        }

        /// <summary>
        ///  创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)</param>
        /// <param name="strValue">COOKIE对象Value值</param>
        public static void SetCookie(string strCookieName, int iExpires, string strValue)
        {
            SetCookie(strCookieName, iExpires, strValue, "");
        }

        /// <summary>
        /// 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)</param>
        /// <param name="dc">COOKIE对象Dictionary string, string 集合</param>
        /// <param name="strDomain">作用域</param>
        public static void SetCookie(string strCookieName, int iExpires, Dictionary<string, string> dc, string strDomain)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            foreach (KeyValuePair<string, string> kv in dc)
            {
                objCookie[kv.Key] = HttpUtility.UrlEncode(kv.Value.Trim());
            }
            if (strDomain.Length > 0)
            {
                objCookie.Domain = strDomain;
            }
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        /// <summary>
        /// 创建COOKIE对象并赋Value值，修改COOKIE的Value值也用此方法，因为对COOKIE修改必须重新设Expires
        /// </summary>
        /// <param name="strCookieName">COOKIE对象名</param>
        /// <param name="iExpires">COOKIE对象有效时间（秒数），1表示永久有效，0和负数都表示不设有效时间，大于等于2表示具体有效秒数，31536000秒=1年=(60*60*24*365)</param>
        /// <param name="strValue">COOKIE对象Value值</param>
        /// <param name="strDomain">作用域</param>
        public static void SetCookie(string strCookieName, int iExpires, string strValue, string strDomain)
        {
            HttpCookie objCookie = new HttpCookie(strCookieName.Trim());
            objCookie.Value = HttpUtility.UrlEncode(strValue.Trim());
            if (strDomain.Length > 0)
            {
                objCookie.Domain = strDomain;
            }
            if (iExpires > 0)
            {
                if (iExpires == 1)
                {
                    objCookie.Expires = DateTime.MaxValue;
                }
                else
                {
                    objCookie.Expires = DateTime.Now.AddSeconds((double)iExpires);
                }
            }
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
    }
}