using System;
using ShInfoTech.Common;
using System.Threading;
using System.Globalization;
using System.Web;
using CRMTree.Model.User;
using CRMTree.Model.Common;



namespace CRMTree.Public
{
    /// <summary>
    /// Summary description for BasePage
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        protected bool Interna = false;
        public BasePage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Page_Error(object sender, EventArgs e)
        {
            Exception objErr = Server.GetLastError().GetBaseException();
            string err = "<b>Error Caught in Page_Error event</b><hr><br>" +
                    "<br><b>Error in: </b>" + Request.Url.ToString() +
                    "<br><b>Error Message: </b>" + objErr.Message.ToString() +
                    "<br><b>Stack Trace:</b><br>" +
                              objErr.StackTrace.ToString();
            Response.Write(err.ToString());
            Response.End();
        }

        protected MD_UserEntity UserSession = new MD_UserEntity();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if(!IsPostBack)
            {
            }

            UserBuss buss = new UserBuss();
            string path=Request.Url.Host;
            bool CheckLogin = buss.CheckLogin("PublicUser", "");

            if (!CheckLogin)
            {
                //Response.Write("<div id=\"_FG2\"><iframe id=\"TR2_iframe\" class=\"nui-msgbox\" src=\"/loginS.aspx\" style=\"width: 306px; height:234px\" frameborder=\"0\" border=\"0\" scrolling=\"no\"></iframe><div id=\"_df\" class=\"nui-mask\"></div></div>");
                //return;
                    //Response.Write("<script>alert('超时,请重新登录系统!');top.location.href='/login.aspx'</script>");
                    //Response.End();
                //Response.Write("<script>window.open('/loginS.aspx', 'ShuNovo Login', 'scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=350,height=255,left = 490,top = 200')</script>");
                //Response.Write("<div id=\"_FG2\"><iframe id=\"TR2_iframe\" class=\"nui-msgbox\" src=\"/loginS.aspx\" style=\"width: 306px; height:234px\" frameborder=\"0\" border=\"0\" scrolling=\"no\"></iframe><div id=\"_df\" class=\"nui-mask\"></div></div>");
                //Response.End();
                //Response.Write("<div id=\"_FG2\"><iframe id=\"TR2_iframe\" class=\"nui-msgbox\" src=\"/loginS.aspx\" style=\"width: 306px; height:234px\" frameborder=\"0\" border=\"0\" scrolling=\"no\"></iframe><div id=\"_df\" class=\"nui-mask\"></div></div>");

                Response.Write("<script>window.open('/loginS.aspx', 'ShuNovo Login', 'scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=350,height=255,left = 490,top = 200')</script>");
                Response.End();
                
            }

            this.UserSession = (MD_UserEntity)HttpContext.Current.Session["PublicUser"];
            if (Language.GetLang2() == EM_Language.en_us)
            {
                Interna = true;
            }
            else
            {
                Interna = false;
            }
        }

        #region  
        protected void Button1_Click(object sender, EventArgs e)
        {

            HttpCookie cookie = new HttpCookie("MyCook");//初使化并设置Cookie的名称

            DateTime dt = DateTime.Now;

            TimeSpan ts = new TimeSpan(0, 0, 1, 0, 0);//过期时间为1分钟

            cookie.Expires = dt.Add(ts);//设置过期时间

            cookie.Values.Add("userid", "userid_value");

            cookie.Values.Add("userid2", "userid2_value2");

            Response.AppendCookie(cookie);

            //输出该Cookie的所有内容

            //Response.Write(cookie.Value);//输出为：userid=userid_value&userid2=userid2_value2 

        }

        //读取Cookie案例：

        protected void Button2_Click(object sender, EventArgs e)
        {

            // HttpCookie cokie = new HttpCookie("MyCook");//初使化

            if (Request.Cookies["MyCook"] != null)
            {

                //Response.Write("Cookie中键值为userid的值:" + Request.Cookies["MyCook"]["userid"]);//整行

                //Response.Write("Cookie中键值为userid2的值" + Request.Cookies["MyCook"]["userid2"]);

                Response.Write(Request.Cookies["MyCook"].Value);//输出全部的值

            }

        }

        //修改Cookie案例：

        protected void Button3_Click(object sender, EventArgs e)
        {

            //获取客户端的Cookie对象

            HttpCookie cok = Request.Cookies["MyCook"];

            if (cok != null)
            {

                //修改Cookie的两种方法

                cok.Values["userid"] = "alter-value";

                cok.Values.Set("userid", "alter-value");

                //往Cookie里加入新的内容

                cok.Values.Set("newid", "newValue");

                Response.AppendCookie(cok);

            }

        }

        //删除Cookie案例：

        protected void Button4_Click(object sender, EventArgs e)
        {

            HttpCookie cok = Request.Cookies["MyCook"];

            //if (cok != null)
            //{

            //    if (!CheckBox1.Checked)
            //    {

            //        cok.Values.Remove("userid");//移除键值为userid的值

            //    }

            //    else
            //    {

            //        TimeSpan ts = new TimeSpan(-1, 0, 0, 0);

            //        cok.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在

            //    }

            //    Response.AppendCookie(cok);

            //}

        }

        #endregion

        protected override void InitializeCulture()
        {
            Language.SetLang();
        }
        protected void Redirect(string Path)
        {

        }
    }
}