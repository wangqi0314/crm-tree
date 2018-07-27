using CRMTree.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMTree.BLL.Wechat
{
    /// <summary>
    /// 微信动作处理
    /// </summary>
    public class wechatHandle
    {
        #region Wechat 接口交互
        #region 获取Wechat 交互指令
        static object lockToken = new object();
        /// <summary>
        /// 从缓存中获取Token，缓存100分钟 全局变量
        /// </summary>
        /// <param name="reset">是否重置Token，true表示重新获取，false表示从缓存获取</param>
        /// <returns>AccessToken</returns>
        public static string GetAccessToken(bool reset)
        {
            string key = "AccessToken";
            string token = string.Empty;
            lock (lockToken)
            {
                if (HttpRuntime.Cache[key] != null && reset == false)
                {
                    token = HttpRuntime.Cache[key].ToString();
                }
                else
                {
                    token = GetToken(key).access_token;
                }
            }
            return token;
        }
        #endregion

        #region 手机客户端访问路径的认证
        /// <summary>
        /// 认证链接
        /// </summary>
        /// <param name="url">原始链接</param>
        public static string CreateOAuthUrl(string url)
        {
            string appid = ConfigurationManager.AppSettings["appId"];
            string _url = HttpUtility.UrlEncode(url, Encoding.GetEncoding("gb2312"));
            return "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + _url + "&response_type=code&scope=snsapi_base&state=123#wechat_redirect";
            /*
             * scope=snsapi_base 表示单一认证，只可获取OpenID
             * scope=snsapi_userinfo 表示全，可获取User所有信息
             */
        }
        /// <summary>
        /// 根据CODE获得OpenId,并且存储与Session中
        /// </summary>
        /// <param name="code">网页授权代码</param>
        /// <returns>成功获取到OpenId返回OpenId,否则返-1</returns> 
        public static Authorization_code GetAuthorization(string code)
        {
            string UriFormat, appId, secret;
            UriFormat = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
            appId = ConfigurationManager.AppSettings["appId"];
            secret = ConfigurationManager.AppSettings["secret"];
            string data = wechatRequest.Get(string.Format(UriFormat, appId, secret, code));
            Authorization_code _Authorization = JsonConvert.DeserializeObject<Authorization_code>(data);
            return _Authorization;
        }
        #endregion
        /// <summary>
        /// 获取与微信端服务器交互的指令锁
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static token GetToken(string key)
        {
            string url, appId, secret, data;
            url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
            appId = ConfigurationManager.AppSettings["appId"];
            secret = ConfigurationManager.AppSettings["secret"];
            data = wechatRequest.Get(string.Format(url, appId, secret));
            token _token = JsonConvert.DeserializeObject<token>(data);
            if (_token.errcode <= 0)
            {
                HttpRuntime.Cache.Insert(key, _token.access_token, null, DateTime.Now.AddMinutes(100), TimeSpan.Zero);
            }
            return _token;
        }
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static wechatFans requestUserInfo(string openId)
        {
            string url, token;
            token = GetAccessToken(false);
            url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}";
            string data = wechatRequest.Get(string.Format(url, token, openId));
            wechatFans _fans = JsonConvert.DeserializeObject<wechatFans>(data);
            return _fans;
        }
        /// <summary>
        ///  客服接口-发消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static errCode requestCustomSend(string data)
        {
            string url, token;
            token = GetAccessToken(false);
            url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
            string _err = wechatRequest.Post(string.Format(url, token), data);
            errCode _errCode = JsonConvert.DeserializeObject<errCode>(_err);
            return _errCode;
        }
        /// <summary>
        /// 上传素材 - 根据类型
        /// </summary>
        /// <param name="mediaType"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Material UploadFile(string mediaType, string filePath)
        {
            string token, urlTemplate;
            token = GetAccessToken(false);
            urlTemplate = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
            string data = wechatRequest.UploadFile(String.Format(urlTemplate, token, mediaType.ToLower()), filePath);
            Material _Material = JsonConvert.DeserializeObject<Material>(data);
            return _Material;
        }
        /// <summary>
        /// 上传图文消息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Material UploadNewsFile(string data)
        {
            string url, token;
            token = GetAccessToken(false);
            url = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}";
            string _err = wechatRequest.Post(string.Format(url, token), data);
            Material _errs = JsonConvert.DeserializeObject<Material>(_err);
            return _errs;
        }

        #region 高级群发接口
        /// <summary>
        /// 高级接口 - 根据OpenID列表群发【订阅号不可用，服务号认证后可用】
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static errCode requestMessSend(string data)
        {
            string url, token;
            token = GetAccessToken(false);
            url = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";
            string _err = wechatRequest.Post(string.Format(url, token), data);
            errCode _errCode = JsonConvert.DeserializeObject<errCode>(_err);
            return _errCode;
        }
        /// <summary>
        /// 高级接口 - 根据分组进行群发【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static errCode requestMesssendall(string data)
        {
            string url, token;
            token = GetAccessToken(false);
            url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
            string _err = wechatRequest.Post(string.Format(url, token), data);
            errCode _errCode = JsonConvert.DeserializeObject<errCode>(_err);
            return _errCode;
        }
        #endregion
        #endregion

        #region 获取粉丝的信息
        public static CT_Wechat_Fan GetFans(string openId)
        {
            wechatFans _fans = requestUserInfo(openId);
            if (_fans == null || _fans.errcode > 0)
            {
                B_W_Exception.AddExcep("wechatHandle >> GetFans", openId, "获取错误,errcode:" + _fans.errcode);
                return null;
            }
            #region 赋值
            CT_Wechat_Fan o = new CT_Wechat_Fan();
            o.WF_OpenId = _fans.openid;
            o.WF_NickName = _fans.nickname;
            switch (_fans.sex)
            {
                case 1:
                    o.WF_Sex = "男";
                    break;
                case 2:
                    o.WF_Sex = "女";
                    break;
                default:
                    o.WF_Sex = "未知";
                    break;
            }
            o.WF_Country = _fans.country;
            o.WF_Province = _fans.province;
            o.WF_City = _fans.city;
            o.WF_HeadImgurl = _fans.headimgurl;
            o.WF_SubscribeTime = GetLocalTime(_fans.subscribe_time);
            #endregion
            return o;
        }

        #endregion
        #region 发送被动相应消息
        public static string SendPassive_text(string ToUserName, string FromUserName, string Content)
        {
            string time = ConvertDateTimeInt(DateTime.Now).ToString();
            string resxml = "";
            resxml = "<xml>";
            resxml += "<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>";
            resxml += "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>";
            resxml += "<CreateTime>" + time + "</CreateTime>";
            resxml += "<MsgType><![CDATA[text]]></MsgType>";
            resxml += "<Content><![CDATA[" + Content + "]]></Content>";
            resxml += "</xml>";
            return resxml;
        }
        public static string SendPassive_image_text(string ToUserName, string FromUserName, string Title, string Description, string PicUrl, string url)
        {
            string time = ConvertDateTimeInt(DateTime.Now).ToString();
            string xml = "<xml>";
            xml += " <ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>";
            xml += "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>";
            xml += "<CreateTime>" + time + "</CreateTime>";
            xml += "<MsgType><![CDATA[news]]></MsgType>";
            xml += "<ArticleCount>2</ArticleCount>";
            xml += "<Articles>";
            xml += "<item>";
            xml += "<Title><![CDATA[" + Title + "]]></Title>";
            xml += "<Description><![CDATA[" + Description + "]]></Description>";
            xml += "<PicUrl><![CDATA[" + PicUrl + "]]></PicUrl>";
            xml += "<Url><![CDATA[" + url + "]]></Url>";
            xml += "</item>";
            xml += "<item>";
            xml += "<Title><![CDATA[" + Title + "]]></Title>";
            xml += "<Description><![CDATA[" + Description + "]]></Description>";
            xml += "<PicUrl><![CDATA[" + PicUrl + "]]></PicUrl>";
            xml += "<Url><![CDATA[" + url + "]]></Url>";
            xml += "</item>";
            xml += "</Articles>";
            xml += "</xml>";
            return xml;
        }
        #endregion
        #region 发送客服消息
        /// <summary>
        /// 发送客服消息，文本类型
        /// </summary>
        /// <param name="touser"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int SendCustom_text(string OpenId, string content)
        {
            string data = "{\"touser\":\""
                        + OpenId + "\",\"msgtype\":\"text\",\"text\":{\"content\":\""
                        + content + "\"}}";
            errCode _err = requestCustomSend(data);
            if (_err.errcode > 0)
            {
                B_W_Exception.AddExcep("wechatHandle >> SendCustom_text", "客服消息发送失败", _err.errcode.ToString());
            }
            return _err.errcode;
        }
        /// <summary>
        /// 发送客服消息，图文类型
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int SendCustom_news(string data)
        {
            errCode _err = requestCustomSend(data);
            if (_err.errcode > 0)
            {
                B_W_Exception.AddExcep("wechatHandle >> SendCustom_news", "客服图文消息发送失败", _err.errcode.ToString());
            }
            return _err.errcode;
        }
        /// <summary>
        /// 构建客服图文消息 - 文本
        /// </summary>
        /// <param name="_custom"></param>
        /// <returns></returns>
        private static string custom_News(customSend _custom)
        {
            if (_custom == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(_custom.title))
            {
                return null;
            }
            if (string.IsNullOrEmpty(_custom.description))
            {
                return null;
            }
            if (string.IsNullOrEmpty(_custom.url))
            {
                return null;
            }
            if (string.IsNullOrEmpty(_custom.picurl))
            {
                return null;
            }
            string _info = string.Empty;
            _info += "\"title\":\"" + _custom.title + "\",";
            _info += "\"description\":\"" + _custom.description + "\",";
            _info += "\"url\":\"" + _custom.url + "\",";
            _info += "\"picurl\":\"" + _custom.picurl + "\"";
            return "{" + _info + "}";
        }
        /// <summary>
        /// 构建客服图文消息 - 文本
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="_customs"></param>
        /// <returns></returns>
        public static string custom_News(string OpenId, IList<customSend> _customs)
        {
            if (_customs == null || _customs.Count > 10)
            {
                return null;
            }
            string _news = string.Empty;
            foreach (customSend custom in _customs)
            {
                string _custom = custom_News(custom);
                if (!string.IsNullOrEmpty(_custom))
                {
                    _news += _custom + ",";
                }
            }
            if (!string.IsNullOrEmpty(_news))
            {
                _news = "{\"articles\":[" + _news.Substring(0, _news.Length - 1) + "]}";
            }
            if (!string.IsNullOrEmpty(_news))
            {
                _news = "\"news\":" + _news + "";
            }
            if (!string.IsNullOrEmpty(_news))
            {
                _news = "{\"touser\":\"" + OpenId + "\",\"msgtype\":\"news\"," + _news + "}";
            }
            return _news;
        }
        /// <summary>
        /// 按照组发送图文消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int SendCustom_news_group(string mediaId)
        {
            string json = "{\"filter\":{ \"group_id\":\"0\" },\"mpnews\":{ \"media_id\":\"" + mediaId + "\" }, \"msgtype\":\"mpnews\"}";
            errCode _err = requestMesssendall(json);
            if (_err.errcode > 0)
            {
                B_W_Exception.AddExcep("wechatHandle >> SendCustom_news", "客服图文消息发送失败", _err.errcode.ToString());
            }
            return _err.errcode;
        }
        /// <summary>
        /// 用OpenID列表 群发图文消息
        /// </summary>
        /// <param name="StringOpenid"></param>
        /// <param name="media"></param>
        /// <returns></returns>
        public static int SendCustom_news_ArrayString(string mediaId, string StringOpenid)
        {
            string Jsonfile = "{\"touser\": [ " + StringOpenid + "],\"mpnews\": { \"media_id\": \"" + mediaId + "\"},\"msgtype\": \"mpnews\"}";
            errCode _err = requestMessSend(Jsonfile);
            if (_err.errcode > 0)
            {
                B_W_Exception.AddExcep("wechatHandle >> SendCustom_news", "客服图文消息发送失败", _err.errcode.ToString());
            }
            return _err.errcode;
        }
        #endregion
        #region 上传各类素材
        /// <summary>
        /// 上传高级模板素材
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static UploadFileInfo UploadImageText(string data)
        {
            Material _mater = UploadNewsFile(data);
            if (_mater == null || _mater.errcode > 0)
            {
                return null;
            }
            UploadFileInfo o = new UploadFileInfo();
            o.MediaId = _mater.media_id;
            o.FileName = data;
            o.UploadDate = GetLocalTime(_mater.created_at);
            o.Invalidation = o.UploadDate.AddDays(3).AddHours(-1);
            return o;
        }
        /// <summary>
        /// 构造单一的高级接口消息模式
        /// </summary>
        /// <param name="_high"></param>
        /// <returns></returns>
        private static string High_news(High_news _high)
        {
            string _news = string.Empty;
            if (string.IsNullOrEmpty(_high.thumb_media_id))
            {
                return null;
            }
            _news += "\"thumb_media_id\":\"" + _high.thumb_media_id + "\",";
            if (!string.IsNullOrEmpty(_high.author))
            {
                _news += "\"author\":\"" + _high.author + "\",";
            }
            if (string.IsNullOrEmpty(_high.title))
            {
                return null;
            }
            _news += "\"title\":\"" + _high.title + "\",";
            if (!string.IsNullOrEmpty(_high.content_source_url))
            {
                _news += "\"content_source_url\":\"" + _high.content_source_url + "\",";
            }
            if (string.IsNullOrEmpty(_high.content))
            {
                return null;
            }
            _news += "\"content\":\"" + _high.content + "\",";
            if (!string.IsNullOrEmpty(_high.digest))
            {
                _news += "\"digest\":\"" + _high.digest + "\",";
            }
            if (_high.show_cover_pic == 0 || _high.show_cover_pic == 1)
            {
                _news += "\"show_cover_pic\":\"" + _high.show_cover_pic.ToString() + "\",";
            }
            return "{" + _news.Substring(0, _news.Length - 1) + "}";
        }
        /// <summary>
        /// 构造高级接口消息模式
        /// </summary>
        /// <param name="_highs"></param>
        /// <returns></returns>
        public static string High_news(IList<High_news> _highs)
        {
            if (_highs == null || _highs.Count > 10) { return null; }
            string _news = string.Empty;
            foreach (High_news _high in _highs)
            {
                string _new = High_news(_high);
                if (!string.IsNullOrEmpty(_new))
                {
                    _news += _new + ",";
                }
            }
            if (!string.IsNullOrEmpty(_news))
            {
                _news = "{\"articles\":[" + _news.Substring(0, _news.Length - 1) + "]}";
            }
            return _news;
        }
        /// <summary>
        ///  转换上传的素材 1=图片，2=语音，3=视频,4=缩略图,
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static UploadFileInfo ConvertMaterial(int type, string fileName)
        {
            UploadFileInfo _FileInfo = null;
            if (type == 1)
            {
                _FileInfo = ConvertMaterial(MaterialType.image, fileName);
            }
            else if (type == 2)
            {
                _FileInfo = ConvertMaterial(MaterialType.voice, fileName);
            }
            else if (type == 3)
            {
                _FileInfo = ConvertMaterial(MaterialType.video, fileName);
            }
            else if (type == 4)
            {
                _FileInfo = ConvertMaterial(MaterialType.thumb, fileName);
            }
            return _FileInfo;
        }
        /// <summary>
        /// 转换上传的素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static UploadFileInfo ConvertMaterial(MaterialType type, string fileName)
        {
            string filePath = HttpContext.Current.Request.PhysicalApplicationPath + fileName;
            Material _mater = UploadMaterial(type, filePath);
            if (_mater == null || _mater.errcode > 0)
            {
                return null;
            }
            UploadFileInfo o = new UploadFileInfo();
            if (type == MaterialType.thumb)
            {
                o.MediaId = _mater.thumb_media_id;
            }
            else if (type == MaterialType.image)
            {
                o.MediaId = _mater.media_id;
            }
            else if (type == MaterialType.voice)
            {
                o.MediaId = _mater.voice_media_id;
            }
            else if (type == MaterialType.video)
            {
                o.MediaId = _mater.video_media_id;
            }
            o.FileName = fileName;
            o.UploadDate = GetLocalTime(_mater.created_at);
            o.Invalidation = o.UploadDate.AddDays(3).AddHours(-1);
            return o;
        }
        /// <summary>
        /// 上传不同类型的文件素材 1=图片，2=语音，3=视频,4=缩略图,
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Material UploadMaterial(int type, string filePath)
        {
            Material _material = null; ;
            if (type == 1)
            {
                _material = UploadMaterial(MaterialType.image, filePath);
            }
            else if (type == 2)
            {
                _material = UploadMaterial(MaterialType.voice, filePath);
            }
            else if (type == 3)
            {
                _material = UploadMaterial(MaterialType.video, filePath);
            }
            else if (type == 4)
            {
                _material = UploadMaterial(MaterialType.thumb, filePath);
            }
            return _material;
        }
        /// <summary>
        /// 上传不同类型的文件素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filePath"></param>
        public static Material UploadMaterial(MaterialType type, string filePath)
        {
            Material _material = UploadFile(type.ToString(), filePath);
            return _material;
        }
        #endregion
        #region 微信端交互获取OpenID
        /// <summary>
        /// 获取微信客户端请求时的微信OpenId
        /// </summary>
        /// <param name="_http"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetOpenId(HttpContext _http, string code)
        {
            string key = "OpenId", openId = string.Empty;
            if (_http.Session != null && _http.Session[key] != null)
            {
                openId = _http.Session[key].ToString();
                B_W_Exception.AddExcep("wechatHandle >> GetOpenId", "Session获取", openId);
            }
            if (string.IsNullOrEmpty(openId))
            {
                openId = GetAuthorization(code).openid;
                B_W_Exception.AddExcep("wechatHandle >> GetOpenId", "微信端交互获取", openId);
            }
            //保存OpenId到Session中
            if (!string.IsNullOrEmpty(openId))
            {
                if (_http.Session != null)
                {
                    _http.Session[key] = openId;
                }
            }
            return openId;
        }
        #endregion

        #region 时间戳处理
        /// <summary>
        /// 将服务器时间转为时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns>时间戳</returns>
        public static int ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        ///时间戳转为DateTime类型
        /// </summary>
        /// <param name="unixTimestamp"></param>
        /// <returns>DateTime</returns>
        public static DateTime GetLocalTime(long unixTimestamp)
        {
            DateTime start = new DateTime(1970, 1, 1); //Start time, Unix: 1970 / 1 / 1  00:00:00
            int rate = 10000000;
            DateTime utcTime = new DateTime(start.Ticks + unixTimestamp * rate);
            return utcTime.ToLocalTime();
        }
        /// <summary>
        /// 时间戳转为DateTime类型
        /// </summary>
        /// <param name="unixTimestamp"></param>
        /// <returns></returns>
        public static string GetLocalTime(string unixTimestamp)
        {
            return GetLocalTime(Convert.ToInt64(unixTimestamp)).ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        #endregion
    }
}