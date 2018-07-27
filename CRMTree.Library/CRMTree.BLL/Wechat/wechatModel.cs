using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL.Wechat
{
    /// <summary>
    /// 请求错误信息
    /// </summary>
    public class errCode
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string openid { get; set; }
    }
    /// <summary>
    /// Wechat 令牌
    /// </summary>
    public class token : errCode
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
    /// <summary>
    /// 页面授权验证
    /// </summary>
    public class Authorization_code : token
    {
        public string refresh_token { get; set; }
        public string scope { get; set; }
    }
    /// <summary>
    /// 关注的粉丝
    /// </summary>
    public class wechatFans : errCode
    {
        public int subscribe { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public long subscribe_time { get; set; }
        public string unionid { get; set; }
    }
    /// <summary>
    /// 素材
    /// </summary>
    public class Material : errCode
    {
        public string type { get; set; }
        public string media_id { get; set; }
        public string thumb_media_id { get; set; }
        public string voice_media_id { get; set; }
        public string video_media_id { get; set; }
        public long created_at { get; set; }
    }
    /// <summary>
    /// 素材类型
    /// </summary>
    public enum MaterialType
    {
        /// <summary>
        /// 缩略图
        /// </summary>
        thumb,
        /// <summary>
        /// 图片
        /// </summary>
        image,
        /// <summary>
        /// 语音
        /// </summary>
        voice,
        /// <summary>
        /// 视频
        /// </summary>
        video,
        /// <summary>
        /// 图文消息
        /// </summary>
        news
    }
    /// <summary>
    /// 高级接口-图文消息对象
    /// </summary>
    public class High_news 
    {
        /// <summary>
        /// 图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面
        /// </summary>
        public string content_source_url { get; set; }
        /// <summary>
        /// 图文消息页面的内容，支持HTML标签
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图文消息的描述
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 是否显示封面，1为显示，0为不显示
        /// </summary>
        public int show_cover_pic { get; set; }

    }
    public class customSend
    {
        /// <summary>
        /// 调用接口凭证
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 普通用户openid
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 消息类型，文本为text，图片为image，语音为voice，视频消息为video，音乐消息为music，图文消息为news
        /// </summary>
        public string msgtype { get; set; }
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 发送的图片/语音/视频的媒体ID
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 缩略图的媒体ID
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 图文消息/视频消息/音乐消息的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图文消息/视频消息/音乐消息的描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string musicurl { get; set; }
        /// <summary>
        /// 高品质音乐链接，wifi环境优先使用该链接播放音乐
        /// </summary>
        public string hqmusicurl { get; set; }
        /// <summary>
        /// 图文消息被点击后跳转的链接
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
        /// </summary>
        public string picurl { get; set; } 
    }
    /// <summary>
    /// 接受微信接口发送的XML数据对象
    /// </summary>
    public class xml
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string CreateTime { get; set; }
        public string MsgType { get; set; }
        public string Event { get; set; }
        public string EventKey { get; set; }
        public string Content { get; set; }
        public string MsgId { get; set; }

    }
}
