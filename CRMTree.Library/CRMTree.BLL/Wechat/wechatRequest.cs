using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL.Wechat
{
    /// <summary>
    /// http请求
    /// </summary>
    public class wechatRequest
    {
        /// <summary>
        /// Http Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            try
            {
                WebRequest wRequest = WebRequest.Create(url);
                wRequest.Method = "GET";
                WebResponse wResponse = wRequest.GetResponse();
                Stream stream = wResponse.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string _o = reader.ReadToEnd();
                reader.Close();
                stream.Close();
                wResponse.Close();
                return _o;
            }
            catch (Exception e)
            {
                B_W_Exception.AddExcep("wechatRequest >> Get", url, e.Message);
                return "{'errcode':10000,'errmsg':'Get请求异常'}";
            }
        }
        /// <summary>
        /// Http Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Post(string url, string data)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                Stream reqstream = req.GetRequestStream();
                byte[] b = Encoding.UTF8.GetBytes(data);
                reqstream.Write(b, 0, b.Length);
                StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream(), System.Text.Encoding.UTF8);
                string _o = responseReader.ReadToEnd();
                responseReader.Close();
                reqstream.Close();
                return _o;
            }
            catch (Exception e)
            {
                B_W_Exception.AddExcep("wechatRequest >> Post", url + "^^^" + data, e.Message);
                return "{'errcode':10000,'errmsg':'Post请求异常'}";
            }
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileurl"></param>
        /// <returns></returns>
        public static string UploadFile(string url, string fileurl)
        {
            try
            {
                WebClient webclient = new WebClient();
                byte[] response = webclient.UploadFile(new Uri(url), fileurl);
                string o = Encoding.UTF8.GetString(response);
                return o;
            }
            catch (Exception e)
            {
                B_W_Exception.AddExcep("wechatRequest >> UploadFile", url + "^^^" + fileurl, e.Message);
                return "{'errcode':10000,'errmsg':'文件长传异常'}";
            }
        }
    }
}
