using CRMTree.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShInfoTech.Common
{
    public class ReportWechat
    {
        /// <summary>
        /// 抓取一个活动内的图片路径
        /// </summary>
        /// <param name="Con"></param>
        /// <returns></returns>
        public static string GetImgSrc(string Con)
        {
            Regex re = new Regex("<img( |.*?)src=('|\"|)([^\"|^\']+)('|\"|>| )( |.*?)( '>|\">|>|/>)", RegexOptions.IgnoreCase);
            MatchCollection matches = re.Matches(Con);
            string _tmpImageUrl = "";
            foreach (Match mh in matches)
            {
                _tmpImageUrl += mh.Groups[3].Value + ",";//src里面的路径
            }
            if (string.IsNullOrEmpty(_tmpImageUrl))
            {
                return "/images/1.jpg";
            }
            else
            {
                return (_tmpImageUrl.Split(','))[0];
            }
        }
        /// <summary>
        /// 读取文件信息
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetFileInfo(string filename)
        {
            string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
            try
            {
                if (!File.Exists(strPath + filename))
                {
                    return string.Empty;
                }
                FileStream fs = new FileStream(strPath + filename, FileMode.Open);
                StreamReader _s = new StreamReader(fs);
                _s.BaseStream.Seek(0, SeekOrigin.Begin);// 从数据流中读取每一行，直到文件的最后一行
                string _H = _s.ReadToEnd();
                _s.Dispose();
                _s.Close();
                return _H;
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 上传的图文消息素材模板
        /// </summary>
        /// <param name="thumb_media_id">缩列图ID</param>
        /// <param name="title">图文标题</param>
        /// <param name="content">图文内容</param>
        /// <param name="digest">图文消息的描述</param>
        /// <returns></returns>
        public static string GetMultimediaJsonStr(string thumb_media_id, string title, string content, string digest)
        {
            string Json = "{ \"articles\": [  { \"thumb_media_id\": \""
                + thumb_media_id + "\", \"title\": \""
                + title + "\", \"content\": \""
                + content + "\", \"digest\": \""
                + digest + "\", \"show_cover_pic\": \"0\"}]}";
            return Json;
        }
        /// <summary>
        /// 文本头文件处理
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ContextFilter(string text)
        {
            return text.Replace("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />", "").Replace("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />", "");
        }
        /// <summary>
        /// 对String[]进行分组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IList<string> GroupData(string[] data, int max)
        {
            if (data == null)
            {
                return null;
            }
            if (data.Length < max)
            {
                return new List<string>() { string.Join(",", data) };
            }
            int count = data.Length / max;
            int cou = data.Length % max;
            IList<string> _data = new List<string>();
            for (int i = 0; i < count; i++)
            {
                string[] Newdata = new string[max];
                int _index = max * i;
                Array.Copy(data, _index, Newdata, 0, max);
                _data.Add(string.Join(",", Newdata));
            }
            if (cou > 0)
            {
                string[] Newdata = new string[cou];
                int _index = max * count;
                Array.Copy(data, _index, Newdata, 0, cou);
                _data.Add(string.Join(",", Newdata));
            }
            return _data;
        }
        /// <summary>
        /// 对IList<string>进行分组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IList<string> GroupData(IList<string> data, int max)
        {
            if (data == null || data.Count <= 0)
            {
                return null;
            }
            if (data.Count < max)
            {
                string _newdata = string.Empty;
                foreach (string d in data)
                {
                    _newdata += "\"" + d + "\",";
                }
                return new List<string>() { _newdata.Substring(0, _newdata.Length - 1) };
            }
            int count = data.Count / max;
            int cou = data.Count % max;
            IList<string> _data = new List<string>();
            for (int i = 0; i < count; i++)
            {
                string _newdata = string.Empty;
                int _index = max * i;
                for (int j = _index; j < _index + max; j++)
                {
                    _newdata += "\"" + data[j] + "\",";
                }
                _data.Add(_newdata.Substring(0,_newdata.Length-1));
            }
            if (cou > 0)
            {
                string _newdata = string.Empty;
                int _index = max * count;
                for (int i = _index; i < data.Count; i++)
                {
                    _newdata += "\"" + data[i] + "\",";
                }
                _data.Add(_newdata.Substring(0, _newdata.Length - 1));
            }
            return _data;
        }
    }
}
