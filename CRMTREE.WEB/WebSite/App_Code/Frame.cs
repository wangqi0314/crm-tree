using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ShInfoTech.Common;
using Shinfotech.Tools;
using CRMTreeDatabase;


namespace CRMTree
{
    /// <summary>
    ///Frame 的摘要说明
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// 通过MI_Code获取报告HTML
        /// </summary>
        /// <param name="UG_Code"></param>
        /// <param name="MI_Code"></param>
        /// <param name="MaxHeight"></param>
        /// <param name="Intern"></param>
        /// <returns></returns>
        public static string GetReportHtmlByMI_Code(int UG_Code, int MI_Code, out float MaxHeight, bool Intern, string Single, int MF_Codes)
        {
            MaxHeight = 0;
            string html = string.Empty, MF_Title_EN = string.Empty, MF_Title_CN = string.Empty, RP_Code = string.Empty, MF_Btn_Links = string.Empty;
            int MF_RP_Type = 0, MF_Code = 0;
            float FT_Size_W = 0, FT_Size_H = 0, FT_Pos_X = 0, FT_Pos_Y = 0;
            string ReportSql = getReportSql(Single, UG_Code, MI_Code, MF_Codes);
            SqlDataReader dr = SqlHelper.ExecuteReader(ReportSql);
            while (dr.Read())
            {
                FT_Size_W = Security.ToFloat(dr["FT_Size_W"]); //获取元素框的宽
                FT_Size_H = Security.ToFloat(dr["FT_Size_H"]) - 35; //获取元素框的高减去原色头的高
                FT_Pos_X = Security.ToFloat(dr["FT_Pos_X"]); //获取元素框X的位置
                FT_Pos_Y = Security.ToFloat(dr["FT_Pos_Y"]); //获取元素框Y的位置
                MF_Title_CN = Security.ToStr(dr["MF_Title_CN"]); //获取元素的标题
                if (Intern)
                {
                    MF_Title_EN = Security.ToStr(dr["MF_Title_EN"]);
                }
                else
                {
                    MF_Title_EN = MF_Title_CN;
                }
                RP_Code = Security.ToStr(dr["MF_FL_FB_Code"]);
                MF_RP_Type = Security.ToNum(dr["MF_RP_Type"]);
                MF_Code = Security.ToNum(dr["MF_Code"]);

                string templeteContent = GetTempletContentByReportType(RP_Code, MF_RP_Type, FT_Size_W, FT_Size_H);

                string BL_Param = string.Empty;
                string BL_Param_Value = string.Empty;
                var data = getBtnLink(MF_Code, templeteContent, Intern);
                MF_Btn_Links = data.MF_Btn_Links;
                //var urlParams = data.urlParams;
                //if (!string.IsNullOrWhiteSpace(urlParams))
                //{
                //    templeteContent += "&" + urlParams;
                //}
                html += setModuleheader(FT_Pos_X, FT_Pos_Y, FT_Size_W, FT_Size_H, MF_Title_EN, MF_Btn_Links, templeteContent, MF_Code);
                MaxHeight = FT_Pos_Y + FT_Size_H;
            }
            dr.Close();
            dr.Dispose();
            MaxHeight += 70;
            return html;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FT_Pos_X"></param>
        /// <param name="FT_Pos_Y"></param>
        /// <param name="FT_Size_W"></param>
        /// <param name="FT_Size_H"></param>
        /// <param name="MF_Title_EN"></param>
        /// <param name="MF_Btn_Links"></param>
        /// <param name="templeteContent"></param>
        /// <returns></returns>
        public static string setModuleheader(float FT_Pos_X, float FT_Pos_Y, float FT_Size_W, float FT_Size_H, string MF_Title_EN, string MF_Btn_Links, string templeteContent, int MF_Code)
        {
            StringBuilder Title = new StringBuilder();
            Title.Append("<div class='report_left' style='width:" + FT_Size_W + "px; left:" + FT_Pos_X + "px; top:" + FT_Pos_Y + "px;position:absolute;margin-bottom:0px'>");
            Title.Append("<div class='report_title'><span style='float:right;'>" + MF_Btn_Links + "</span>" + MF_Title_EN + "</div>");
            Title.Append("<div id='container" + MF_Code + "' style='height:" + (FT_Size_H) + "px; margin: 0 auto'>");
            Title.Append("<iframe id='mainFrame' name='" + MF_Code + "'style='width: 100%; height: 100%; text-align: center;' allowtransparency='true' name='mainFrame' src='" + templeteContent + "' frameborder='0'>");
            Title.Append(" </iframe>");
            Title.Append("</div>");
            Title.Append("</div>");
            return Title.ToString();
        }
        /// <summary>
        /// 根据报告类型获取模板内容
        /// </summary>
        /// <param name="RP_Code"></param>
        /// <param name="RP_Type"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static string GetTempletContentByReportType(string RP_Code, int RP_Type, float Width, float Height)
        {
            string templeteFileUrl = string.Empty;
            string PUrlparameter = RequestClass.getUrlParameter();
            if (!string.IsNullOrEmpty(PUrlparameter))
                PUrlparameter = "&" + PUrlparameter;
            switch (RP_Type)
            {
                case 11:
                    templeteFileUrl = "/templete/report/DataGrid.html?MF_FL_FB_Code=" + RP_Code + "&width=" + Width + "&height=" + Height + "" + PUrlparameter + "";
                    break;
                case 12:
                    templeteFileUrl = "/templete/report/DataGrid.html?MF_FL_FB_Code=" + RP_Code + "&width=" + Width + "&height=" + Height + "" + PUrlparameter + "";
                    break;
                case 13:
                    templeteFileUrl = "/templete/report/BarChats.aspx?MF_FL_FB_Code=" + RP_Code + "&width=" + Width + "&height=" + Height + "";
                    break;
                case 16:
                    templeteFileUrl = "/templete/report/PieChats.aspx?MF_FL_FB_Code=" + RP_Code + "&width=" + Width + "&height=" + Height + "";
                    break;
                case 17:
                    templeteFileUrl = "/templete/report/PieChatsII.aspx?MF_FL_FB_Code=" + RP_Code + "&width=" + Width + "&height=" + Height + "";
                    break;
                case 18:
                    templeteFileUrl = "/templete/report/SchChart.aspx?MF_FL_FB_Code=" + RP_Code + "&width=" + Width + "&height=" + Height + "";
                    break;
                case 19:
                    templeteFileUrl = "/templete/report/MultiChart.aspx?MF_FL_FB_Code=" + RP_Code + "&width=" + Width + "&height=" + Height + "";
                    break;
                case 20:
                    templeteFileUrl = "/templete/report/MGauge_Charts.aspx?MF_FL_FB_Code=" + RP_Code + "&width=" + Width + "&height=" + Height + "";
                    break;
                default:
                    templeteFileUrl = "/templete/report/PieChats.aspx?MF_FL_FB_Code=" + RP_Code + "&width=" + Width + "&height=" + Height + "";
                    break;
            }
            return templeteFileUrl;
        }

        private static string getReportSql(string Single, int UG_Code, int MI_Code, int MF_Code)
        {
            string Sql = string.Empty;
            switch (Single)
            {
                case "1":
                    Sql = @"select A.*,B.* FROM CT_Menu_Frames A 
                            LEFT JOIN CT_Frame_Templ B ON  A.MF_FT_Code=B.FT_Code 
                            WHERE A.MF_MI_Code=" + MI_Code + " AND A.MF_UG_Code=" + UG_Code + " and MF_Code=" + MF_Code + "";
                    break;
                default:
                    Sql = @"select A.*,B.* FROM CT_Menu_Frames A 
                            LEFT JOIN CT_Frame_Templ B ON  A.MF_FT_Code=B.FT_Code
                            WHERE A.MF_MI_Code=" + MI_Code + " AND A.MF_UG_Code=" + UG_Code + " ";
                    break;
            }
            return Sql;
        }

        private static dynamic getBtnLink(int MF_Code, string Links, bool Intern)
        {
            string urlParams = string.Empty;
            string MF_Btn_Links = string.Empty;
            string Sql = @"select * from ct_btn_links where BL_MF_Code=" + MF_Code + " order by BL_Order asc";
            SqlDataReader drBtnLinks = SqlHelper.ExecuteReader(Sql);
            while (drBtnLinks.Read())
            {
                string BL_Text = drBtnLinks["BL_Text_EN"].ToString();
                if (!Intern)
                {
                    BL_Text = drBtnLinks["BL_Text_CN"].ToString();
                }
                string Target = setTarget(drBtnLinks["BL_Link_Type"].ToString());
                string Link = Links;
                Link = drBtnLinks["BL_Link"].ToString();

                if(null != drBtnLinks["BL_Type"] && drBtnLinks["BL_Type"].ToString() == "2")
                {
                    //if (null != drBtnLinks["BL_RP_Code"])
                    //{
                    //    var rp_code = Convert.ToInt32(drBtnLinks["BL_RP_Code"]);
                    //    var paramName = null != drBtnLinks["BL_Param"] ? drBtnLinks["BL_Param"].ToString() : "";
                    //    CRMTree.Function fun = new CRMTree.Function();
                    //    var sql_query = fun.getReprotQuery(rp_code);
                    //    var db = DBCRMTree.GetInstance();
                    //    var cbxs = db.Query<EX_Combobox>(sql_query);

                       
                    //    MF_Btn_Links += "<select class='__ReLoadURL' _BL_Param='" + paramName + "'>";

                    //    var i = 0;
                    //    foreach (var cbx in cbxs)
                    //    {
                    //        if (i == 0)
                    //        {
                    //            urlParams = paramName+"=" +cbx.value;
                    //        }
                    //        MF_Btn_Links += "<option value='" + cbx.value + "'>" + cbx.text + "</option>";
                    //        i++;
                    //    }
                    //    MF_Btn_Links += "</select>";
                    //}
                }
                else if (null != drBtnLinks["BL_Type"] && drBtnLinks["BL_Type"].ToString() == "3")
                {
                    MF_Btn_Links += "<div style=\"float:left;width:60px;\" Ma=\"" + MF_Code + "\">&nbsp;</div>";
                }
                else
                {
                    if (drBtnLinks["BL_Link_Type"].ToString() == "1")
                    {
                        MF_Btn_Links += "<div style=\"float:left;\"><a class=\"BL_Link\" target=\"_self\" Ma=\"" + MF_Code + "\" href=\"#1\">" + BL_Text + "</a></div>";
                    }
                    else if (drBtnLinks["BL_Link_Type"].ToString() == "2")
                    {
                        MF_Btn_Links += "<div style=\"float:left;\"><a class=\"BL_Link\" target=\"" + MF_Code + "\" Ma=\"" + MF_Code + "\" href=\"" + Link + "\">" + BL_Text + "</a></div>";
                    }
                    else if (drBtnLinks["BL_Link_Type"].ToString() == "14")
                    {
                        MF_Btn_Links += "<div style=\"float:left;\"><a class=\"BL_Link\" onclick=\"WindowOpen2(\'" + Link + "\'," + Target + ",\'" + BL_Text + "\')\"  href=\"javascript:void();\">" + BL_Text + "</a></div>";
                    }
                    else if (drBtnLinks["BL_Link_Type"].ToString() == "4")
                    {
                        MF_Btn_Links += "<div style=\"float:left;\"><a class=\"BL_Link\" onclick=\'" + Link + "\' Ma=\"" + MF_Code + "\"  href=\"javascript:void();\">" + BL_Text + "</a></div>";
                    }
                    else if (drBtnLinks["BL_Link_Type"].ToString() == "15")
                    {
                        MF_Btn_Links += "<div style=\"float:left;\"><a class=\"BL_Link\" onclick=\'" + Link + "\' Ma=\"" + MF_Code + "\"  href=\"javascript:void();\">" + BL_Text + "</a></div>";
                    }
                    else
                    {
                        MF_Btn_Links += "<div style=\"float:left;\"><a class=\"BL_Link\" onclick=\"WindowOpen(\'" + Link + "\'," + Target + ",\'" + BL_Text + "\')\"  href=\"javascript:void();\">" + BL_Text + "</a></div>";

                    }
                }
            }
            drBtnLinks.Close();
            drBtnLinks.Dispose();
            return new { MF_Btn_Links = MF_Btn_Links, urlParams = urlParams };
        }
        private static string setTarget(string BL_Link_Type)
        {
            string Target = string.Empty;
            switch (BL_Link_Type)
            {
                case "0":
                    Target = "\'_self\'";
                    break;
                case "1":
                    Target = "\'_self\'";
                    break;
                case "2":
                    Target = "\'_self\'";
                    break;
                case "3":
                    Target = "\'_self\'";
                    break;
                case "10":
                    Target = "\'_parent\'";
                    break;
                case "11":
                    Target = "\'_blank\'";
                    break;
                case "12":
                case "14":
                    Target = "\'TreeWin1\',\'status=no,location=no,resizable,toolbar=no,scrollbars,width=700, height=400\',\'_blank\'";
                    break;
                default:
                    Target = "\'\'";
                    break;
            }
            return Target;
        }
    }
}