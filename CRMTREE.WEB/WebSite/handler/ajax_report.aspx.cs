using CRMTree.Public;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Shinfotech.Tools;
using ShInfoTech.Common;
using CRMTree.Model.Reports;
using CRMTree.BLL;
using CRMTree.Model.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRMTree.Model;
using Newtonsoft.Json;

public partial class handler_ajax_campaign : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        string strAction = RequestClass.GetString("action").ToLower();//操作动作 
        if (!string.IsNullOrEmpty(strAction))
        {
            switch (strAction)
            {
                case "list_report":
                    list_report();//campaign 列表
                    break;
                case "get_report":
                    Get_Report();
                    break;
            }
        }
        else
            Response.Write("-1");//action 参数不正确，没权限
    }
    #region 查询
    private void list_report()
    {
        //排序字段   string C1,string C2,
        string strSortField = RequestClass.GetString("sortfield").Trim().Replace("'", "");
        //排序规则
        string strSortRule = RequestClass.GetString("sortrule").Trim().Replace("'", "");

        string C1 = RequestClass.GetString("C1").Trim().Replace("'", "");
        string C2 = RequestClass.GetString("C2").Trim().Replace("'", "");
        string C = RequestClass.GetString("C").Trim().Replace("'", "");

        string RP_Cat = "RP_Cat1=1 and RP_Cat2=1";

        if (C1.Length <= 0 && C2.Length > 0) { RP_Cat = "RP_Cat2 in (" + C2 + ") and (RP_Cat3 is null or RP_Cat3 <13)"; }
        if (C2.Length <= 0 && C1.Length > 0) { RP_Cat = "RP_Cat1 in (" + C1 + ") and (RP_Cat3 is null or RP_Cat3 <13)"; }
        if (C1.Length > 0 && C2.Length > 0) { RP_Cat = "RP_Cat1 in (" + C1 + ") and RP_Cat2 in (" + C2 + ") and (RP_Cat3 is null or RP_Cat3 <13)"; }

        if (C == "1")
        {
            if (C1.Length <= 0 && C2.Length > 0) { RP_Cat = "RP_Cat2 in (" + C2 + ") and RP_Cat3 >= 13"; }
            if (C2.Length <= 0 && C1.Length > 0) { RP_Cat = "RP_Cat1 in (" + C1 + ") and RP_Cat3 >= 13"; }
            if (C1.Length > 0 && C2.Length > 0) { RP_Cat = "RP_Cat1 in (" + C1 + ") and RP_Cat2 in (" + C2 + ") and  RP_Cat3 >= 13"; }
            //string[] CC1 = C1.Split(',');
            //C1 = "";
            //for (int i = 0; i <CC1.Length; i++)
            //{
            //    C1 = C1+"1" + CC1[i].Trim();
            //    if (i < CC1.Length-1) { C1 = C1 + ","; }
            //}
        }

        //排序字段的样式
        string orderClass = String.Empty;
        if (string.IsNullOrEmpty(strSortField))
        {
            strSortField = "RP_Update_dt";
            strSortRule = "desc";
            orderClass = "taxisDown";
        }
        else
        {
            if (strSortRule.Equals("asc"))
                orderClass = "taxisUp";
            else
                orderClass = "taxisDown";
        }
        string Rp_Names = Interna ? IN_Language.RP_Name(EM_Language.en_us) : IN_Language.RP_Name(EM_Language.zh_cn);
        string Description = Interna ? IN_Language.RP_Description(EM_Language.en_us) : IN_Language.RP_Description(EM_Language.zh_cn);
        string Update_dt = Interna ? IN_Language.RP_UpdateDate(EM_Language.en_us) : IN_Language.RP_UpdateDate(EM_Language.zh_cn);
        string Operate = Interna ? IN_Language.CGL_Operate(EM_Language.en_us) : IN_Language.CGL_Operate(EM_Language.zh_cn);
        StringBuilder sbList = new StringBuilder();
        sbList.Append("<table class=\"t-bd\" Style=\"width:100%\"><tr>");
        sbList.Append("<th style=\"cursor:pointer;width:270px;\" ");
        sbList.Append("class=\"w40\" onclick=\"changeOrder(this,'RP_Name_EN')\">" + Rp_Names + "<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
        sbList.Append("<th>" + Description + "</th>");
        sbList.Append("<th style=\"cursor:pointer;width:120px\" ");
        if (strSortField.Equals("RP_Update_dt"))
        {
            sbList.Append("class=\"taxisCurrent\" onclick=\"changeOrder(this,'RP_Update_dt')\">" + Update_dt + "<span class=\"");
            sbList.Append(orderClass);
            sbList.Append("\" title=\"Sort by the column \"></span></th>");
        }
        else
        {
            sbList.Append(" onclick=\"changeOrder(this,'RP_Update_dt')\">" + Update_dt + "<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
        }
        sbList.Append("<th class=\"w100\">" + Operate + "</th>");
        //初始化变量
        int intRowsCount = 0;//总行数
        int intPageCount = 0;//总页数  
        int intCurrentPage = RequestClass.GetInt("page", 1);//当前页
        int intPageSize = RequestClass.GetInt("pagesize", 10); ;//每页条数
        BL_Reports rp = new BL_Reports();

        DataTable dtTemp = new DataTable();
        System.Collections.Generic.List<string> lsTemp = new System.Collections.Generic.List<string>();
        bool CharOrNot = false;
        if (C == "1")
        {
            string LimitQuery = string.Empty;
            if (C1.Length <= 0 && C2.Length > 0) { LimitQuery = "RP_Cat2 in (" + C2 + ") and RP_Cat3 > 12"; }
            if (C2.Length <= 0 && C1.Length > 0) { LimitQuery = "RP_Cat1 in (" + C1 + ") and RP_Cat3 > 12"; }
            if (C1.Length > 0 && C2.Length > 0) { LimitQuery = "RP_Cat1 in (" + C1 + ") and RP_Cat2 in (" + C2 + ") and RP_Cat3  > 12"; }
            string Query = "select RP_Code,FB_Code,RP_Cat3 from (Select (Select CASE"
               + " When RP_Cat3 = 13 then"
               + " (Select top 1 FB_Code From CT_Reports Join CT_Fields_Bar on FB_RP_Code=RP_Code where RP_Code=RP.RP_Code)"
               + " When RP_Cat3 in (16,17) then"
               + " (Select top 1 FP_Code From CT_Reports Join CT_Fields_Pie on FP_RP_Code=RP_Code where RP_Code=RP.RP_Code)"
               + " When RP_Cat3 = 18 then"
               + " (Select top 1 FS_Code From CT_Reports Join CT_Fields_Sch on FS_RP_Code=RP_Code where RP_Code=RP.RP_Code)"
               + " When RP_Cat3 = 19 then"
               + " (Select top 1 FM_Code From CT_Reports Join CT_Fields_MultiChart on FM_RP_Code=RP_Code where RP_Code=RP.RP_Code)"
               + " When RP_Cat3 = 20 then"
               + " (Select top 1 FG_Code From CT_Reports Join CT_Fields_Gauge on FG_RP_Code=RP_Code where RP_Code=RP.RP_Code)"
               + " END"
               + ") AS FB_Code"
               + ",RP_Code,RP_Cat1,RP_Cat2,RP_Cat3 from CT_Reports RP) as Temp where ISNULL(Temp.FB_Code,0)!=0 and " + LimitQuery;

            dtTemp = BL_Reports.GetFieldsByQuery(Query);

            if (dtTemp != null && dtTemp.Rows.Count != 0)
            {
                foreach (DataRow dr in dtTemp.AsEnumerable())
                    lsTemp.Add(dr["RP_Code"].ToString());
            }
            CharOrNot = true;
        }

        IList<CT_Reports> RList = rp.GetReportList(Interna, UserSession.User.UG_UType, UserSession.DealerEmpl.DE_AD_OM_Code, strSortField, strSortRule, intCurrentPage, intPageSize, RP_Cat, out intPageCount, out intRowsCount, CharOrNot);
        if (null != RList && RList.Count > 0)
        {
            //生成分页html
            var strPager = new StringBuilder();
            if (1 < intPageCount) strPager = Pager.JavascriptPagination(true, intCurrentPage, intPageCount, intRowsCount);

            List<string> lsT = new List<string>();

            for (int i = 0; i < RList.Count; i++)
            {
                var intID = RList[i].RP_Code;
                if (lsT != null && lsT.Contains(intID.ToString()))
                    continue;
                else
                    lsT.Add(intID.ToString());

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    if (lsTemp.Contains(intID.ToString()))
                    {
                        System.Collections.Generic.List<Int16> ls = new System.Collections.Generic.List<Int16>();
                        ls = (from ss in dtTemp.AsEnumerable()
                              where ss.Field<int>("RP_Code") == Convert.ToInt32(intID)
                              select ss.Field<Int16>("FB_Code")).ToList();
                        foreach (int fbcode in ls)
                        {
                            var intRows = i + 1;
                            sbList.Append("<tr><td style=\"text-align:left\" title='" + RList[i].RP_Name_EN + "'>");
                            if (RList[i].RP_Name_EN != null)
                                sbList.Append(RList[i].RP_Name_EN.Length > 60 ? RList[i].RP_Name_EN.Substring(0, 60) : RList[i].RP_Name_EN);
                            else
                                sbList.Append(RList[i].RP_Desc_EN);
                            sbList.Append("</td><td style=\"text-align:left\" title='" + RList[i].RP_Desc_EN + "'>");
                            if (RList[i].RP_Desc_EN != null)
                                sbList.Append(RList[i].RP_Desc_EN.Length > 60 ? RList[i].RP_Desc_EN.Substring(0, 60) : RList[i].RP_Desc_EN);
                            else
                                sbList.Append(RList[i].RP_Desc_EN);
                            sbList.Append("</td><td>");
                            var RP_Update_dt = RList[i].RP_Update_dt.ToString("MM/dd/yyyy HH:mm:ss");
                            RP_Update_dt = RP_Update_dt == "01/01/0001 00:00:00" ? "" : RP_Update_dt;

                            sbList.Append(RP_Update_dt);
                            sbList.Append("</td><td>");
                            var dis = RList[i].PL_Code.ToString() == "0" ? "disable" : "";
                            if (string.IsNullOrEmpty(dis))
                            {
                                sbList.Append("<a href=\"javascript:;\"><i class=\"btnModify " + dis + "\" title=\"edit\" onclick=\"EditTags(");
                                sbList.Append(intID);
                                sbList.Append(")\"></i></a>");
                            }
                            else
                            {
                                sbList.Append("<a href=\"javascript:;\"><i class=\"btnModify " + dis + "\" title=\"edit\" ></i></a>");
                            }
                            sbList.Append("<a href=\"javascript:;\"><i class=\"btnRun\" title=\"Run\" _hidUrl=\"" + GetRunUrl(fbcode, dtTemp, RList[i].RP_Name_EN) + "\" onclick=\"Runs(");
                            sbList.Append(fbcode.ToString());
                            sbList.Append(",'");
                            sbList.Append(RList[i].RP_Name_EN);
                            sbList.Append("','");
                            sbList.Append(RList[i].RP_Name_CN);
                            sbList.Append("','");
                            sbList.Append(GetRunUrl(fbcode, dtTemp, RList[i].RP_Name_EN));
                            sbList.Append("')\"></i></a>");
                            sbList.Append("</td></tr>");
                        }
                    }
                }
                else
                {
                    var intRows = i + 1;
                    sbList.Append("<tr><td style=\"text-align:left\" title='" + RList[i].RP_Name_EN + "'>");
                    if (RList[i].RP_Name_EN != null)
                        sbList.Append(RList[i].RP_Name_EN.Length > 60 ? RList[i].RP_Name_EN.Substring(0, 60) : RList[i].RP_Name_EN);
                    else
                        sbList.Append(RList[i].RP_Desc_EN);
                    sbList.Append("</td><td style=\"text-align:left\" title='" + RList[i].RP_Desc_EN + "'>");
                    if (RList[i].RP_Desc_EN != null)
                        sbList.Append(RList[i].RP_Desc_EN.Length > 60 ? RList[i].RP_Desc_EN.Substring(0, 60) : RList[i].RP_Desc_EN);
                    else
                        sbList.Append(RList[i].RP_Desc_EN);
                    sbList.Append("</td><td>");
                    var RP_Update_dt = RList[i].RP_Update_dt.ToString("MM/dd/yyyy HH:mm:ss");
                    RP_Update_dt = RP_Update_dt == "01/01/0001 00:00:00" ? "" : RP_Update_dt;
                    sbList.Append(RP_Update_dt);
                    sbList.Append("</td><td>");
                    var dis = RList[i].PL_Code.ToString() == "0" ? "disable" : "";
                    if (string.IsNullOrEmpty(dis))
                    {
                        sbList.Append("<a href=\"javascript:;\"><i class=\"btnModify " + dis + "\" title=\"edit\" onclick=\"EditTags(");
                        sbList.Append(intID);
                        sbList.Append(")\"></i></a>");
                    }
                    else
                    {
                        sbList.Append("<a href=\"javascript:;\"><i class=\"btnModify " + dis + "\" title=\"edit\" ></i></a>");
                    }
                    sbList.Append("<a href=\"javascript:;\"><i class=\"btnRun\" _hidUrl=\"" + intID + "\" title=\"Run\" onclick=\"Runs(");
                    sbList.Append(intID);
                    sbList.Append(",'");
                    sbList.Append(RList[i].RP_Name_EN);
                    sbList.Append("','");
                    sbList.Append(RList[i].RP_Name_CN);
                    sbList.Append("')\"></i></a>");
                    sbList.Append("</td></tr>");
                }
            }
            sbList.Append("</table>");
            sbList.Append(strPager);
        }
        else
        {
            sbList.Append("<tr ><td colspan=\"10\"><div class=\"noData\">No matching data！</div></td></tr>");
            sbList.Append("</tbody>");
            sbList.Append("</table>");
        }
        Response.Write(sbList);
    }

    private string GetRunUrl(int FB_Code, DataTable dtTemp, string Title)
    {
        var temp = (from tt in dtTemp.AsEnumerable()
                    where tt.Field<Int16>("FB_Code") == FB_Code
                    select tt.Field<Byte>("RP_Cat3")).FirstOrDefault();

        int FB_Type = Convert.ToInt32(temp);

        string templeteFileUrl = string.Empty;
        switch (FB_Type)
        {
            case 13:
                templeteFileUrl = "/templete/report/BarChats.aspx?MF_FL_FB_Code=" + FB_Code + "&width=900&height=315&Title=" + HttpUtility.UrlEncode(Title, Encoding.UTF8);
                break;
            case 16:
                templeteFileUrl = "/templete/report/PieChats.aspx?MF_FL_FB_Code=" + FB_Code + "&width=900&height=315&Title=" + HttpUtility.UrlEncode(Title, Encoding.UTF8);
                break;
            case 17:
                templeteFileUrl = "/templete/report/PieChatsII.aspx?MF_FL_FB_Code=" + FB_Code + "&width=900&height=315&Title=" + HttpUtility.UrlEncode(Title, Encoding.UTF8); ;
                break;
            case 18:
                templeteFileUrl = "/templete/report/SchChart.aspx?MF_FL_FB_Code=" + FB_Code + "&width=900&height=315&Title=" + HttpUtility.UrlEncode(Title, Encoding.UTF8);
                break;
            case 19:
                templeteFileUrl = "/templete/report/MultiChart.aspx?MF_FL_FB_Code=" + FB_Code + "&width=900&height=315&Title=" + HttpUtility.UrlEncode(Title, Encoding.UTF8);
                break;
            case 20:
                templeteFileUrl = "/templete/report/GaugeChart.aspx?MF_FL_FB_Code=" + FB_Code + "&width=900&height=315&Title=" + HttpUtility.UrlEncode(Title, Encoding.UTF8);
                break;
            default:
                templeteFileUrl = "/templete/report/PieChats.aspx?MF_FL_FB_Code=" + FB_Code + "&width=900&height=315&Title=" + HttpUtility.UrlEncode(Title, Encoding.UTF8);
                break;
        }
        return templeteFileUrl;
    }
    #endregion
    /// <summary>
    /// 获取报表的详细信息，替换后的
    /// </summary>
    /// <returns></returns>
    private void Get_Report()
    {
        int RP_Code = RequestClass.GetInt("RP_Code", 0);
        int PR = RequestClass.GetInt("PR", 0);
        CT_Reports Re = new BL_Reports().GetReplaceReport(Interna,
                   new CT_Param_Value() { RP_Code = RP_Code, PV_Type = PR, PV_CG_Code = -1, PV_UType = UserSession.User.UG_UType, PV_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code });


        Response.Write(JsonConvert.SerializeObject(Re)); 
    }


}