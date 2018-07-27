using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ShInfoTech.Common;
using CRMTree.Public;
using System.Collections;
using System.Text;
using System.Collections.Specialized;
using System.Web.Services;
using CRMTree.BLL;
using CRMTree.DAL;

namespace CRMTree
{
    public partial class Main : BasePage
    {       
        public int MI_Code = 0;
        public float MaxHeight = 0;       
        public string reportHtml = "";
        public string CurrentPage = "";
        public string MI_Name = "";
        public static bool Intern = true;
        public static int MI_Code_static = 0;
        public static int UG_Code_static = 0;

        public static long AU_Code = 0;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                UserBuss buss = new UserBuss();
                bool CheckLogin = buss.CheckLogin("PublicUser", "");

                if (!CheckLogin)
                {
                    Response.Write("<div id=\"_FG2\"><iframe id=\"TR2_iframe\" class=\"nui-msgbox\" src=\"/loginS.aspx\" style=\"width: 306px; height:234px\" frameborder=\"0\" border=\"0\" scrolling=\"no\"></iframe><div id=\"_df\" class=\"nui-mask\"></div></div>");
                }
                else
                {
                    setParamater();
                    Intern = Interna;
                    this.CrmTreetop.UserID = UserSession.User.AU_Code;

                    AU_Code = UserSession.User.AU_Code;

                    int UG_Code = UserSession.User.AU_UG_Code;
                    UG_Code_static = UG_Code;

                    CurrentPage = new BL_TabLinkList().GetLevelLink(UG_Code, Request.RawUrl, Intern, out MI_Name);


                    MI_Code = Tools.Request.Int("M", 0);
                    MI_Code_static = MI_Code;
                    reportHtml = Frame.GetReportHtmlByMI_Code(UG_Code, MI_Code, out MaxHeight, Intern, "2", 1);
                }
            }
        }
        private void setParamater()
        {
            NameValueCollection con = HttpContext.Current.Request.Params;
            Session["Paramater"] = con;
        }
        [WebMethod]
        public static object getReports(int MF_Code)
        {
            float MaxHeight = 0;
            return Frame.GetReportHtmlByMI_Code(UG_Code_static, MI_Code_static, out MaxHeight, true, "1", MF_Code);
        }

        [WebMethod]
        public static object getNextTime()
        {
            string QueryToGetReminderDate = "Select top 1 RT_Time from CT_Reminder_Timers where RT_AU_Code=" + AU_Code + " and RT_Status=1 order by RT_Time ASC";

            DataTable dt=BL_Reports.QueryExecution(QueryToGetReminderDate);

            if (dt != null && dt.Rows.Count>0)
                return dt.Rows[0]["RT_Time"];
            else
                return null;
             
        }
    }
}
