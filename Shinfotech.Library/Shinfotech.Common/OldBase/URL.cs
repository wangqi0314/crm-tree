using System;
using System.Web;
namespace ShInfoTech.Common
{
    /// <summary>
    /// URL处理类
    /// </summary>
    public class URL
    {
        /// <summary>
        /// 获取当前不带参数的网址比如:http://www.baidu.com/index.aspx
        /// </summary>
        /// <returns></returns>
        public static string GetThisShortUrl()
        {
            string str = "http://";
            return (str + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() + HttpContext.Current.Request.ServerVariables["URL"].ToString());
        }

        /// <summary>
        /// 获取当前带参数的网址比如:http://www.baidu.com/index.aspx?message=hello
        /// </summary>
        /// <returns></returns>
        public static string GetThisUrl(string port)
        {
            string str = "http://";
            if (port.Trim() != "")
            {
                port = ":" + port;
            }
            //string k = HttpContext.Current.Request.Url.ToString();
            str = str + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() + port + HttpContext.Current.Request.ServerVariables["URL"].ToString();
            if (HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString() != "")
            {
                str = str + "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }
            return str;
        }
        /// <summary>
        /// 获取当前带参数的网址比如:http://www.baidu.com/index.aspx?message=hello
        /// </summary>
        /// <returns></returns>
        public static string GetThisUrl()
        {
            return GetThisUrl("");
        }

        /// <summary>
        /// 根据网址获取文件名［注:此函数未经仔细测试］
        /// </summary>
        /// <param name="strHtmlPagePath"></param>
        /// <returns></returns>
        public static string GetUrlFileName(string strHtmlPagePath)
        {
            int num = 0;
            int startIndex = 0;
            int index = 0;
            int[] numArray = new int[10];
            string str = "";
            while ((startIndex < strHtmlPagePath.Length) && (num > -1))
            {
                num = strHtmlPagePath.IndexOf('/', startIndex);
                if (num == -1)
                {
                    break;
                }
                numArray[index] = num;
                startIndex = num + 1;
                index++;
            }
            for (int i = 0; i < index; i++)
            {
                if ((numArray[i] > 0) && (numArray[i + 1] == 0))
                {
                    str = strHtmlPagePath.Substring(numArray[i] + 1, (strHtmlPagePath.Length - numArray[i]) - 1);
                }
            }
            return str.ToLower();
        }

        /// <summary>
        /// 根据网址获取文件目录［注:此函数未经仔细测试］
        /// </summary>
        /// <param name="strHtmlPagePath"></param>
        /// <returns></returns>
        public static string[] GetUrlFolerName(string strHtmlPagePath)
        {
            int num = 0;
            int startIndex = 0;
            int index = 0;
            int[] numArray = new int[10];
            string[] strArray = null;
            while ((startIndex < strHtmlPagePath.Length) && (num > -1))
            {
                num = strHtmlPagePath.IndexOf('/', startIndex);
                if (num == -1)
                {
                    break;
                }
                numArray[index] = num;
                startIndex = num + 1;
                index++;
            }
            strArray = new string[index - 1];
            for (int i = 0; i < index; i++)
            {
                if ((numArray[i] > 0) && (numArray[i + 1] > 0))
                {
                    string str = strHtmlPagePath.Substring(numArray[i] + 1, (numArray[i + 1] - numArray[i]) - 1);
                    strArray.SetValue(str, i);
                }
            }
            return strArray;
        }
        /// <summary>
        /// 获取完整的域名:比如aa.baidu.com
        /// </summary>
        /// <param name="strHtmlPagePath"></param>
        /// <returns></returns>
        public static string GetUrlFullDomainName(string strHtmlPagePath)
        {
            string str = HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() + HttpContext.Current.Request.ServerVariables["URL"].ToString();
            try
            {
                int startIndex = strHtmlPagePath.IndexOf("http://") + 7;
                int index = strHtmlPagePath.IndexOf("/", startIndex);
                if (-1 == index)
                {
                    index = strHtmlPagePath.Length;
                }
                str = strHtmlPagePath.Substring(startIndex, index - startIndex);
            }
            catch
            {
                return "";
            }
            return str;
        }

        /// <summary>
        /// 是否为蜘蛛
        /// </summary>
        /// <returns></returns>
        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                string[] SearchEngine = new string[] { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
                string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
                for (int i = 0; i < SearchEngine.Length; i++)
                {
                    if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }






        /// <summary>
        /// 根据网址获取顶级域名
        /// </summary>
        /// <param name="strHtmlPagePath"></param>
        /// <returns></returns>
        public static string GetUrlTopDomainName(string strHtmlPagePath)
        {
            string[] strArray6 = new string[12];
            strArray6[0] = ".com.cn";
            strArray6[1] = ".com.hk";
            strArray6[2] = ".com.tw";
            strArray6[3] = ".idv.tw";
            strArray6[4] = ".net.cn";
            strArray6[5] = ".net.nz";
            strArray6[6] = ".org.cn";
            strArray6[7] = ".org.nz";
            strArray6[8] = ".org.tw";
            strArray6[9] = ".org.uk";
            strArray6[10] = ".travel";
            string[] strArray = strArray6;
            string[] strArray7 = new string[40];
            strArray7[0] = ".ac.cn";
            strArray7[1] = ".ah.cn";
            strArray7[2] = ".bj.cn";
            strArray7[3] = ".co.nz";
            strArray7[4] = ".co.uk";
            strArray7[5] = ".cq.cn";
            strArray7[6] = ".fj.cn";
            strArray7[7] = ".gd.cn";
            strArray7[8] = ".gs.cn";
            strArray7[9] = ".gx.cn";
            strArray7[10] = ".gz.cn";
            strArray7[11] = ".ha.cn";
            strArray7[12] = ".hb.cn";
            strArray7[13] = ".he.cn";
            strArray7[14] = ".hi.cn";
            strArray7[15] = ".hk.cn";
            strArray7[0x10] = ".hl.cn";
            strArray7[0x11] = ".hn.cn";
            strArray7[0x12] = ".jl.cn";
            strArray7[0x13] = ".js.cn";
            strArray7[20] = ".jx.cn";
            strArray7[0x15] = ".ln.cn";
            strArray7[0x16] = ".me.uk";
            strArray7[0x17] = ".mo.cn";
            strArray7[0x18] = ".nm.cn";
            strArray7[0x19] = ".nx.cn";
            strArray7[0x1a] = ".qh.cn";
            strArray7[0x1b] = ".sc.cn";
            strArray7[0x1c] = ".sd.cn";
            strArray7[0x1d] = ".sh.cn";
            strArray7[30] = ".sn.cn";
            strArray7[0x1f] = ".sx.cn";
            strArray7[0x20] = ".tj.cn";
            strArray7[0x21] = ".tw.cn";
            strArray7[0x22] = ".us.sh";
            strArray7[0x23] = ".xj.cn";
            strArray7[0x24] = ".xz.cn";
            strArray7[0x25] = ".yn.cn";
            strArray7[0x26] = ".zj.cn";
            string[] strArray2 = strArray7;
            string[] strArray8 = new string[4];
            strArray8[0] = ".info";
            strArray8[1] = ".mobi";
            strArray8[2] = ".name";
            string[] strArray3 = strArray8;
            string[] strArray9 = new string[5];
            strArray9[0] = ".biz";
            strArray9[1] = ".com";
            strArray9[2] = ".net";
            strArray9[3] = ".org";
            string[] strArray4 = strArray9;
            string[] strArray10 = new string[0x15];
            strArray10[0] = ".ac";
            strArray10[1] = ".at";
            strArray10[2] = ".be";
            strArray10[3] = ".ca";
            strArray10[4] = ".cc";
            strArray10[5] = ".cc";
            strArray10[6] = ".ch";
            strArray10[7] = ".cn";
            strArray10[8] = ".cn";
            strArray10[9] = ".de";
            strArray10[10] = ".fr";
            strArray10[11] = ".hk";
            strArray10[12] = ".io";
            strArray10[13] = ".it";
            strArray10[14] = ".jp";
            strArray10[15] = ".nl";
            strArray10[0x10] = ".TV";
            strArray10[0x11] = ".tw";
            strArray10[0x12] = ".us";
            strArray10[0x13] = ".ws";
            string[] strArray5 = strArray10;
            string urlFullDomainName = GetUrlFullDomainName(strHtmlPagePath);
            if (-1 == urlFullDomainName.IndexOf("."))
            {
                return urlFullDomainName;
            }
            string str2 = null;
            string str3 = null;
            if (urlFullDomainName.Length >= 8)
            {
                str2 = urlFullDomainName.Substring(urlFullDomainName.Length - 7);
                int index = 0;
                index = 0;
                while (strArray[index] != null)
                {
                    if (str2 == strArray[index])
                    {
                        break;
                    }
                    index++;
                }
                if (str2 == strArray[index])
                {
                    str3 = null;
                    int startIndex = 0;
                    startIndex = urlFullDomainName.Length - 8;
                    while (startIndex >= 0)
                    {
                        if ('.' == urlFullDomainName[startIndex])
                        {
                            break;
                        }
                        startIndex--;
                    }
                    if (startIndex == 0)
                    {
                        str3 = urlFullDomainName.Substring(startIndex, (urlFullDomainName.Length - 7) - startIndex);
                    }
                    else
                    {
                        str3 = urlFullDomainName.Substring(startIndex + 1, ((urlFullDomainName.Length - 7) - startIndex) - 1);
                    }
                    return (str3 + str2);
                }
            }
            if (urlFullDomainName.Length >= 7)
            {
                str2 = urlFullDomainName.Substring(urlFullDomainName.Length - 6);
                int num3 = 0;
                num3 = 0;
                while (strArray2[num3] != null)
                {
                    if (str2 == strArray2[num3])
                    {
                        break;
                    }
                    num3++;
                }
                if (str2 == strArray2[num3])
                {
                    str3 = null;
                    int num4 = 0;
                    num4 = urlFullDomainName.Length - 7;
                    while (num4 >= 0)
                    {
                        if ('.' == urlFullDomainName[num4])
                        {
                            break;
                        }
                        num4--;
                    }
                    if (num4 == 0)
                    {
                        str3 = urlFullDomainName.Substring(num4, (urlFullDomainName.Length - 6) - num4);
                    }
                    else
                    {
                        str3 = urlFullDomainName.Substring(num4 + 1, ((urlFullDomainName.Length - 6) - num4) - 1);
                    }
                    return (str3 + str2);
                }
            }
            if (urlFullDomainName.Length >= 6)
            {
                str2 = urlFullDomainName.Substring(urlFullDomainName.Length - 5);
                int num5 = 0;
                num5 = 0;
                while (strArray3[num5] != null)
                {
                    if (str2 == strArray3[num5])
                    {
                        break;
                    }
                    num5++;
                }
                if (str2 == strArray3[num5])
                {
                    str3 = null;
                    int num6 = 0;
                    num6 = urlFullDomainName.Length - 6;
                    while (num6 >= 0)
                    {
                        if ('.' == urlFullDomainName[num6])
                        {
                            break;
                        }
                        num6--;
                    }
                    if (num6 == 0)
                    {
                        str3 = urlFullDomainName.Substring(num6, (urlFullDomainName.Length - 5) - num6);
                    }
                    else
                    {
                        str3 = urlFullDomainName.Substring(num6 + 1, ((urlFullDomainName.Length - 5) - num6) - 1);
                    }
                    return (str3 + str2);
                }
            }
            if (urlFullDomainName.Length >= 5)
            {
                str2 = urlFullDomainName.Substring(urlFullDomainName.Length - 4);
                int num7 = 0;
                num7 = 0;
                while (strArray4[num7] != null)
                {
                    if (str2 == strArray4[num7])
                    {
                        break;
                    }
                    num7++;
                }
                if (str2 == strArray4[num7])
                {
                    str3 = null;
                    int num8 = 0;
                    num8 = urlFullDomainName.Length - 5;
                    while (num8 >= 0)
                    {
                        if ('.' == urlFullDomainName[num8])
                        {
                            break;
                        }
                        num8--;
                    }
                    if (num8 == 0)
                    {
                        str3 = urlFullDomainName.Substring(num8, (urlFullDomainName.Length - 4) - num8);
                    }
                    else
                    {
                        str3 = urlFullDomainName.Substring(num8 + 1, ((urlFullDomainName.Length - 4) - num8) - 1);
                    }
                    return (str3 + str2);
                }
            }
            if (urlFullDomainName.Length >= 4)
            {
                str2 = urlFullDomainName.Substring(urlFullDomainName.Length - 3);
                int num9 = 0;
                num9 = 0;
                while (strArray5[num9] != null)
                {
                    if (str2 == strArray5[num9])
                    {
                        break;
                    }
                    num9++;
                }
                if (str2 == strArray5[num9])
                {
                    str3 = null;
                    int num10 = 0;
                    num10 = urlFullDomainName.Length - 4;
                    while (num10 >= 0)
                    {
                        if ('.' == urlFullDomainName[num10])
                        {
                            break;
                        }
                        num10--;
                    }
                    if (num10 == 0)
                    {
                        str3 = urlFullDomainName.Substring(num10, (urlFullDomainName.Length - 3) - num10);
                    }
                    else
                    {
                        str3 = urlFullDomainName.Substring(num10 + 1, ((urlFullDomainName.Length - 3) - num10) - 1);
                    }
                    return (str3 + str2);
                }
            }
            return null;
        }
    }
}