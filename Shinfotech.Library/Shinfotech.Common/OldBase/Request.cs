using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace ShInfoTech.Common
{
    public partial class Tools
    {
        public class Request
        {
            public static int Int(string key)
            {
                return Int(key, 0);
            }
            public static int Int(string key, int defaultInt)
            {
                bool flg = true;
                int re = 0;
                try
                {
                    re = int.Parse(System.Web.HttpContext.Current.Request.QueryString[key].ToString());
                    flg = false;
                }
                catch { }
                if (flg)
                {
                    try
                    {
                        re = int.Parse(System.Web.HttpContext.Current.Request.Form[key].ToString());
                        flg = false;
                    }
                    catch { }
                }
                if (flg)
                {
                    re = defaultInt;
                    flg = false;
                }
                return re;
            }

            public static long Long(string key)
            {
                return Long(key, 0);
            }

            public static long Long(string key, long defaultLong)
            {
                bool flg = true;
                long re = 0;
                try
                {
                    re = long.Parse(System.Web.HttpContext.Current.Request.QueryString[key].ToString());
                    flg = false;
                }
                catch { }
                if (flg)
                {
                    try
                    {
                        re = long.Parse(System.Web.HttpContext.Current.Request.Form[key].ToString());
                        flg = false;
                    }
                    catch { }
                }
                if (flg)
                {
                    re = defaultLong;
                    flg = false;
                }
                return re;
            }

            public static string String(string key)
            {
                return String(key, "");
            }
            public static string String(string key, string defaultString)
            {
                bool flg = true;
                string re = "";
                try
                {
                    re = System.Web.HttpContext.Current.Request.QueryString[key].ToString();
                    flg = false;
                }
                catch { }
                if (flg)
                {
                    try
                    {
                        re = System.Web.HttpContext.Current.Request.Form[key].ToString();
                        flg = false;
                    }
                    catch { }
                }
                if (flg)
                {
                    re = defaultString;
                    flg = false;
                }
                return re;
            }

            public static bool Bool(string key)
            {
                return Bool(key, true);
            }
            public static bool Bool(string key, bool defaultBool)
            {
                bool flg = true;
                bool re = true;
                try
                {
                    re = bool.Parse(System.Web.HttpContext.Current.Request.QueryString[key].ToString());
                    flg = false;
                }
                catch { }
                if (flg)
                {
                    try
                    {
                        re = bool.Parse(System.Web.HttpContext.Current.Request.Form[key].ToString());
                        flg = false;
                    }
                    catch { }
                }
                if (flg)
                {
                    re = defaultBool;
                    flg = false;
                }
                return re;
            }

            public static string IntArray(string key)
            {
                return IntArray(key, "");
            }
            public static string IntArray(string key, string defaultIntArray)
            {
                bool flg = true;
                string re = "";
                try
                {
                    re = System.Web.HttpContext.Current.Request.QueryString[key].ToString();
                    flg = false;
                }
                catch { }
                if (flg)
                {
                    try
                    {
                        re = System.Web.HttpContext.Current.Request.Form[key].ToString();
                        flg = false;
                    }
                    catch { }
                }
                if (flg)
                {
                    re = defaultIntArray;
                    flg = false;
                }

                string reStr = "";
                ArrayList res = new ArrayList();
                string[] resTmp = re.Split(',');
                if (resTmp.Length > 0)
                {
                    for (int i = 0; i < resTmp.Length; i++)
                    {
                        int tmpIntTryParse = 0;
                        if (int.TryParse(resTmp[i], out tmpIntTryParse))
                        {
                            res.Add(int.Parse(resTmp[i]));

                        }
                    }
                }
                for (int i = 0; i < res.Count; i++)
                {
                    if (reStr != "")
                    {
                        reStr += ",";
                    }
                    reStr += res[i].ToString();
                }
                re = reStr;

                return re;
            }

        }
    }
}