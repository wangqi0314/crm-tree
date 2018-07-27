using System;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;

namespace Shinfotech.Tools
{
    /// <summary>
    /// HttpModule类(URL重写)
    /// </summary>
    public class HttpModule : System.Web.IHttpModule
    {
        //public readonly static Mutex m=new Mutex();

        /// <summary>
        /// 实现接口的Init方法
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);
        }

        public void Application_OnError(Object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 实现接口的Dispose方法
        /// </summary>
        public void Dispose()
        {
        }
        /// <summary>
        /// 重写Url,待优化
        /// </summary>
        /// <param name="sender">事件的源</param>
        /// <param name="e">包含事件数据的 EventArgs</param>
        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            //重写后的url如果有多级目录，最好将页面上的所有图片调用、css调用都做成绝对路径
            HttpContext context = ((HttpApplication)sender).Context;
            //string forumPath = "";
            string requestPath = context.Request.Path.ToLower();

            //遍历每个url重写规则
            //string strTemplateid = "";
            foreach (SiteUrls.URLRewrite url in SiteUrls.GetSiteUrls().Urls)
            {
                if (Regex.IsMatch(requestPath, url.Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
                {
                    //实际地址
                    //string newUrl = Regex.Replace(requestPath.Substring(context.Request.Path.LastIndexOf("/")), url.Pattern, url.QueryString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    string newUrl = Regex.Replace(context.Request.Path, url.Pattern, url.QueryString, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    //HttpContext.Current.Response.Write(context.Request.Path + "<br/>" + url.Pattern + "<br/>" + url.QueryString + "<br/>");
                    //HttpContext.Current.Response.Write(url.Page + "<br/>" + newUrl);
                    //HttpContext.Current.Response.End();
                    context.RewritePath("/" + url.Page, string.Empty, newUrl);

                    return;
                }
            }
            //没有对应的重写规则，不重写
            context.RewritePath("/" + requestPath);
            //--//
        }
    }
    /// <summary>
    /// 站点伪Url信息类
    /// </summary>
    public class SiteUrls
    {
        #region 内部属性和方法
        private static object lockHelper = new object();
        private static volatile SiteUrls instance = null;

        string SiteUrlsFile = HttpContext.Current.Server.MapPath("~/xml/urls.config");
        private System.Collections.ArrayList _Urls;
        public System.Collections.ArrayList Urls
        {
            get
            {
                return _Urls;
            }
            set
            {
                _Urls = value;
            }
        }

        private System.Collections.Specialized.NameValueCollection _Paths;
        public System.Collections.Specialized.NameValueCollection Paths
        {
            get
            {
                return _Paths;
            }
            set
            {
                _Paths = value;
            }
        }

        private SiteUrls()
        {
            Urls = new System.Collections.ArrayList();
            Paths = new System.Collections.Specialized.NameValueCollection();

            XmlDocument xml = new XmlDocument();

            xml.Load(SiteUrlsFile);

            XmlNode root = xml.SelectSingleNode("urls");
            foreach (XmlNode n in root.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "rewrite")
                {
                    XmlAttribute name = n.Attributes["name"];
                    XmlAttribute path = n.Attributes["path"];
                    XmlAttribute page = n.Attributes["page"];
                    XmlAttribute querystring = n.Attributes["querystring"];
                    XmlAttribute pattern = n.Attributes["pattern"];

                    if (name != null && path != null && page != null && querystring != null && pattern != null)
                    {
                        Paths.Add(name.Value, path.Value);
                        Urls.Add(new URLRewrite(name.Value, pattern.Value, page.Value.Replace("^", "&"), querystring.Value.Replace("^", "&")));
                    }
                }
            }
        }
        #endregion

        public static SiteUrls GetSiteUrls()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null)
                    {
                        instance = new SiteUrls();
                    }
                }
            }
            return instance;

        }

        public static void SetInstance(SiteUrls anInstance)
        {
            if (anInstance != null)
                instance = anInstance;
        }

        public static void SetInstance()
        {
            SetInstance(new SiteUrls());
        }


        /// <summary>
        /// 重写伪地址
        /// </summary>
        public class URLRewrite
        {
            #region 成员变量
            private string _Name;
            public string Name
            {
                get
                {
                    return _Name;
                }
                set
                {
                    _Name = value;
                }
            }

            private string _Pattern;
            public string Pattern
            {
                get
                {
                    return _Pattern;
                }
                set
                {
                    _Pattern = value;
                }
            }

            private string _Page;
            public string Page
            {
                get
                {
                    return _Page;
                }
                set
                {
                    _Page = value;
                }
            }

            private string _QueryString;
            public string QueryString
            {
                get
                {
                    return _QueryString;
                }
                set
                {
                    _QueryString = value;
                }
            }
            #endregion

            #region 构造函数
            public URLRewrite(string name, string pattern, string page, string querystring)
            {
                _Name = name;
                _Pattern = pattern;
                _Page = page;
                _QueryString = querystring;
            }
            #endregion
        }

    }
}
