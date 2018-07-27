using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShInfoTech.Common;
using CRMTree.Model.User;
using CRMTree.BLL;

namespace CRMTree.Public
{
    /// <summary>
    /// UserBuss
    /// </summary>
    public class UserBuss
    {
        private static void SetUserInfoCok(DataTable ADataTable, ref MD_UserEntity AUserEntity)
        {
            //if (((ADataTable != null) && (AUserEntity != null)) && (ADataTable.Rows.Count > 0))
            //{
            //    if (ADataTable.Rows[0]["AU_Code"] != null)
            //    {
            //        HttpContext.Current.Response.Cookies["AU_Code"].Value = ADataTable.Rows[0]["AU_Code"].ToString();
            //        HttpContext.Current.Response.Cookies["AU_Code"].Expires = DateTime.Now.AddMinutes(20d);
            //    }
            //    if (ADataTable.Rows[0]["AU_UG_Code"] != null)
            //    {
            //        HttpContext.Current.Response.Cookies["AU_UG_Code"].Value = ADataTable.Rows[0]["AU_UG_Code"].ToString();
            //        HttpContext.Current.Response.Cookies["AU_UG_Code"].Expires = DateTime.Now.AddMinutes(20d);
            //    }
            //    if (ADataTable.Rows[0]["AU_Username"] != null)
            //    {
            //        HttpContext.Current.Response.Cookies["AU_Username"].Value = ADataTable.Rows[0]["AU_Username"].ToString();
            //        HttpContext.Current.Response.Cookies["AU_Username"].Expires = DateTime.Now.AddMinutes(20d);
            //    }
            //    if (ADataTable.Rows[0]["AU_Password"] != null)
            //    {
            //        HttpContext.Current.Response.Cookies["AU_Password"].Value = ADataTable.Rows[0]["AU_Password"].ToString();
            //        HttpContext.Current.Response.Cookies["AU_Password"].Expires = DateTime.Now.AddMinutes(20d);
            //    }
            //    if (ADataTable.Rows[0]["AU_Active_tag"] != null)
            //    {
            //        HttpContext.Current.Response.Cookies["AU_Active_tag"].Value = ADataTable.Rows[0]["AU_Active_tag"].ToString();
            //        HttpContext.Current.Response.Cookies["AU_Active_tag"].Expires = DateTime.Now.AddMinutes(20d);
            //    }
            //    if (ADataTable.Rows[0]["AU_LName"] != null)
            //    {
            //        HttpContext.Current.Response.Cookies["AU_LName"].Value = ADataTable.Rows[0]["AU_LName"].ToString();
            //        HttpContext.Current.Response.Cookies["AU_LName"].Expires = DateTime.Now.AddMinutes(20d);
            //    }

            //    AUserEntity.IsExist = true;
            //}
        }
        /// <summary>
        /// 检查用户是否登录
        /// </summary>
        /// <param name="SessionName">SessionName ex:PublicUser</param>
        /// <param name="CSIDBConnectionString">数据库连接串</param>
        /// <returns>true or false</returns>
        public bool CheckLogin(string SessionName, string CSIDBConnectionString)
        {
            if (HttpContext.Current.Session["PublicUser"] == null)
            {
                if (System.Web.HttpContext.Current.Request.Cookies["UserInfo"]==null || string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["UserName"]) || string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["UserPwd"]))
                    return false;

                string UserName = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["SessionN"].ToString();
                string userpass = System.Web.HttpContext.Current.Request.Cookies["UserInfo"]["SessionP"].ToString();

                UserName=Shinfotech.Tools.Encryptions.DecryptDES(UserName,"shunovo2015");
                userpass = Shinfotech.Tools.Encryptions.DecryptDES(userpass,"shunovo2015");


                string Password = ShInfoTech.Common.Security.Md5(userpass);
                BL_UserEntity UserEnt = new BL_UserEntity();
                MD_UserEntity Users = UserEnt.getUserInfo(UserName, Password);
                if (Users == null) { return false; }
                HttpContext.Current.Session["PublicUser"] = Users;

                //return false;
            }
            return true;
        }

        /// <summary>
        /// 退出登录并返回指定页面,指定页面清除Cookie
        /// </summary>
        /// <param name="Sessionname">Sessionname</param>
        /// <param name="Url">清除Session后返回的指定页面，清除Cookie</param>
        public static void LoginOut(string Sessionname, string Url)
        {
            HttpContext.Current.Session[Sessionname] = null;

            if ("" == Url)
            {
                Url = WebConfig.GetAppSettingString("LoginOutUrl");
            }
            HttpContext.Current.Response.Redirect(Url);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="AllowEmailNonActivated">如果为false退出登录并返回指定页面,指定页面清除Cookie，true直接清除Cookie跟Session</param>
        /// <param name="Sessionname">Sessionname</param>
        /// <param name="Url">清除Session后返回的指定页面，清除Cookie</param>
        public static void LoginOut(bool AllowEmailNonActivated, string Sessionname, string Url)
        {
            if (!AllowEmailNonActivated)
            {
                LoginOut(Sessionname, Url);
            }
            HttpContext.Current.Session[Sessionname] = null;
            //Security.ClearCookies();
            if ("" == Url)
            {
                Url = WebConfig.GetAppSettingString("LoginOutUrl");
            }
            HttpContext.Current.Response.Redirect(Url);
        }
    }
}