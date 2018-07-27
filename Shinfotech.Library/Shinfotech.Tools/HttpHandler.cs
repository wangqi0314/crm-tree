using System.IO;
using System.Net;
using System.Text;

namespace Shinfotech.Tools
{
    /// <summary>
    ///  HTTP操作类
    ///  CreateUser Michael Wang
    ///  CreateDate 2012-02-28
    /// </summary>
    public  class HttpHandler
    {
        #region Get Response Method
        /// <summary>
        /// Get Response
        /// </summary>
        /// <param name="address"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string GetResponse(string address, int timeOut)
        {
            try
            {
                string ret = "";
                HttpWebRequest webRequest = null;
                webRequest = (HttpWebRequest)WebRequest.Create(address);
                webRequest.Method = "get";
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {

                    Stream st = response.GetResponseStream();
                    //在这里对接收到的页面内容进行处理
                    StreamReader sr = new StreamReader(st, Encoding.GetEncoding("utf-8"));
                    string inputLine;
                    while ((inputLine = sr.ReadToEnd()) != null && inputLine.Length > 0)
                    {
                        ret = ret + inputLine;
                    }
                    sr.Close();
                    sr = null;
                    address = null;
                    ret = ret.Trim();
                    return ret;
                }
            }
            catch (IOException e)
            {
                return "-100&" + e.ToString();
            }
        }


        /// <summary>
        /// http请求（post方式）
        /// </summary>
        /// <param name="address">url地址</param>
        /// <param name="method">请求参数</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static string GetResponse(string address, string param, int timeOut)
        {
            try
            {
                string ret = "";
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(address);
                byte[] bs = Encoding.ASCII.GetBytes(param);           //参数2进制

                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = bs.Length;
                using (Stream st = webRequest.GetRequestStream())
                {
                    st.Write(bs, 0, bs.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    //在这里对接收到的页面内容进行处理
                    StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                    string inputLine;
                    while ((inputLine = sr.ReadToEnd()) != null && inputLine.Length > 0)
                    {
                        ret = ret + inputLine;
                    }
                    sr.Close();
                    sr = null;
                    address = null;
                    ret = ret.Trim();
                    return ret;
                }

            }
            catch (IOException e)
            {
                return "-100&" + e.ToString();
            }
        }


        #endregion
    }
}
