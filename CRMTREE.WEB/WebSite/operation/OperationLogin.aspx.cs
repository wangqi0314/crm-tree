using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ShInfoTech.Common;
using System.Web.Services;
using System.Text;
using CRMTree.Model.User;
using CRMTree.Public;
using CRMTree.BLL;
namespace CRMTree
{
    public partial class operation_OperationLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e){ }
        [WebMethod]
        public static string logins(string username, string userpass)
        {
            string Link = string.Empty;
             
            string UserName = Shinfotech.Tools.Encryptions.EncryptDES(username, "shunovo2015");
            string Password = Shinfotech.Tools.Encryptions.EncryptDES(userpass, "shunovo2015");

            Password = ShInfoTech.Common.Security.Md5(userpass);

            BL_UserEntity UserEnt = new BL_UserEntity();
            MD_UserEntity Users = UserEnt.getUserInfo(username, Password);
            if (Users == null) { return ""; }
            HttpContext.Current.Session["PublicUser"] = Users;


            setUpCookies(UserName, Password);
            string sql = "select TL_Link from CT_Tab_Links with(nolock) where TL_Level=1 and TL_Order=1 and TL_UG_Code=" + Users.User.AU_UG_Code + "";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(Tools.GetConnString(), CommandType.Text, sql))
            {
                while (dr.Read())
                {
                    Link = Security.ToStr(dr["TL_Link"]);
                }
            }

            return Link;
        }

        public static void goToBeforeUrl()
        {
            string Url = System.Web.HttpContext.Current.Request.Cookies["URL"]["beforeUrl"].ToString();
            System.Web.HttpContext.Current.Response.Write("<script>top.location.href='"+Url+"'</script>");
        }

        public static void setUpCookies(string UserName,string Password)
        {   
            HttpCookie cookie = new HttpCookie("UserInfo");//initial and set up Cookie value
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 1, 0, 0, 0);//Expiration Time
            cookie.Expires = dt.Add(ts);//Set up for expiration time
            cookie.Values.Add("sessionN", UserName);
            cookie.Values.Add("sessionP", Password);
            System.Web.HttpContext.Current.Response.AppendCookie(cookie);
        }

        [WebMethod]
        public static int VerificationUsername(string username)
        {
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"Select * from CT_All_Users where AU_Username=@AU_Username;");
            SqlParameter[] parameters = {new SqlParameter("@AU_Username", SqlDbType.NVarChar,50)};
            parameters[0].Value = username;
            DataSet ds = SqlHelper.ExecuteDataset(Tools.GetConnString(), CommandType.Text, Sql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
                return 1;
            else
                return 0;
        }
         [WebMethod]
        public static int Registration(string username,string password)
        {
            string passwords = ShInfoTech.Common.Security.Md5(password);
            StringBuilder Sql = new StringBuilder();
            Sql.Append(@"insert into  CT_All_Users(AU_UG_Code,AU_Username,AU_Password,AU_Active_tag,AU_Update_dt)
                           values(3,@username,@password,1,GETDATE());");
            SqlParameter[] parameters = {
                                            new SqlParameter("@username", SqlDbType.NVarChar, 50),
                                            new SqlParameter("@password", SqlDbType.NVarChar, 50)
                                        };
            parameters[0].Value = username;
            parameters[1].Value = passwords;
            int i = SqlHelper.ExecuteNonQuery(Tools.GetConnString(), CommandType.Text, Sql.ToString(), parameters);
            return i;
        }
    }
}