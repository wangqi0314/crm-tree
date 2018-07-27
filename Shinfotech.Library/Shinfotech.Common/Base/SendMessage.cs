using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShInfoTech.Common
{
    /// <summary>
    /// 短信类处理
    /// </summary>
    public class SendMessage
    {
        #region 发送模板短信
        private static string Send(string mobile, string content)
        {
            string url = "http://116.213.72.20/SMSHttpService/send.aspx?username=srkjjk&password=shinfotech123";
            url += "&mobile=" + mobile;
            url += "&content=" + content;
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Method = "Get";
            req.ContentType = "text/HTML";
            try
            {
                HttpWebResponse response = req.GetResponse() as HttpWebResponse;
                Stream str = response.GetResponseStream();
                StreamReader reader = new StreamReader(str, Encoding.UTF8);
                string responseData = reader.ReadToEnd();
                reader.Close();
                str.Close();
                response.Close();
                return responseData;
            }
            catch 
            {
                return "-1";
            }
        }
        private static int Balance()
        {
            string url = "http://116.213.72.20/SMSHttpService/Balance.aspx?username=srkjjk&password=shinfotech123";
            HttpWebRequest req = HttpWebRequest.Create(url) as HttpWebRequest;
            req.Method = "Get";
            req.ContentType = "text/HTML";
            try
            {
                HttpWebResponse response = req.GetResponse() as HttpWebResponse;
                Stream str = response.GetResponseStream();
                StreamReader reader = new StreamReader(str, Encoding.UTF8);
                string responseData = reader.ReadToEnd();
                reader.Close();
                str.Close();
                response.Close();
                return Convert.ToInt32(responseData);
            }
            catch 
            {
                return -1;
            }
        }

        private static string SendTemplet(string mobile, string content)
        {
            if (Balance() > 0)
            {
                return Send(mobile, content);
            }
            else
            {
                return "-1";
            }
        }
        #endregion



        /// <summary>
        /// 微信注册发送验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="content"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static string SendMobileVerification(string mobile, string content, string period)
        {
            string Con = "【DaeKU】欢迎注册DaeKu,你的手机验证码为" + content + "，有效期为" + period + "秒";
            return SendTemplet(mobile, Con);
        }


        public static string SendReminder(string mobile, string content)
        {
            string Con = "[Daeku]在线预约成功，我们期待您的光临！"+content;
            return SendTemplet(mobile,Con);
        }
        /// <summary>
        /// 微信预约成功，发送确认短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string SendAppconfirm(string mobile)
        {
            string Con = "【DaeKU】在线预约成功，我们期待您的光临！";
            return SendTemplet(mobile, Con);
        }
        /// <summary>
        /// 微信预约成功，发送确认短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string SendAppconfirm(string mobile, string test = "")
        {
            string Con = "" + test;
            return SendTemplet(mobile, Con);
        }
        /// <summary>
        /// 创建活动成功后，短信推行消息
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string SendCampaignInfo(string mobile)
        {
            string Con = "【Da e Ku】尊敬的车主您好，XXX现推出XXX，您可通过如下链接：XXX查看详情";
            return SendTemplet(mobile, Con);
        }
    }
}
