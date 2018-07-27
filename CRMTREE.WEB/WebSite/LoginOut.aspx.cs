using CRMTree.Public;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int Source=Convert.ToInt32(Request.QueryString["Source"].ToString());
        string Url = string.Empty;
        if (Source == 1)
        {
            Url = "/login.aspx";
        }
        else
        {
            Url = "/login.html";
        }
        //UserBuss.LoginOut(true, "PublicUser", Url);
        HttpContext.Current.Session["PublicUser"] = null;
        RemoveAllCookies();
        Response.Redirect(Url);
        Response.End();
    }

    protected void RemoveAllCookies()
    {
        if (Request.Cookies["UserInfo"] != null) 
            Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(-1);//将这个Cookie过期掉. 
    }
    protected void RemoveAllCache()
    {

        System.Web.Caching.Cache _cache = HttpRuntime.Cache;
        IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
        ArrayList al = new ArrayList();
        while (CacheEnum.MoveNext())
        {
            al.Add(CacheEnum.Key);
        }

        foreach (string key in al)
        {
            _cache.Remove(key);
        }
    }
}