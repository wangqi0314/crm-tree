using CRMTree.DAL;
using CRMTree.Model;
using CRMTree.Model.Campaigns;
using CRMTree.Model.Common;
using CRMTree.Model.Reports;
using CRMTree.Model.User;
using Shinfotech.Tools;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CRMTree.BLL
{
    public class BL_Campaign
    {
        DL_Campaign Cam = new DL_Campaign();
        public string getCampaignList(bool Interna, MD_UserEntity UserSession, string primarykey, string fields, string ordefiled, string orderway, int currentpage, int pagesize, string orderClass, int CT, int CA)
        {
            string Title = Interna ? IN_Language.CGL_Title(EM_Language.en_us) : IN_Language.CGL_Title(EM_Language.zh_cn);
            string Description = Interna ? IN_Language.CGL_Description(EM_Language.en_us) : IN_Language.CGL_Description(EM_Language.zh_cn);
            string Whom = Interna ? IN_Language.CGL_Whom(EM_Language.en_us) : IN_Language.CGL_Whom(EM_Language.zh_cn);
            string Active = Interna ? IN_Language.CGL_Active(EM_Language.en_us) : IN_Language.CGL_Active(EM_Language.zh_cn);
            string Status = Interna ? IN_Language.CGL_Status(EM_Language.en_us) : IN_Language.CGL_Status(EM_Language.zh_cn);
            string Update_dt = Interna ? IN_Language.CGL_Update_dt(EM_Language.en_us) : IN_Language.CGL_Update_dt(EM_Language.zh_cn);
            string Operate = Interna ? IN_Language.CGL_Operate(EM_Language.en_us) : IN_Language.CGL_Operate(EM_Language.zh_cn);
            StringBuilder sbList = new StringBuilder();
            sbList.Append("<table class=\"t-bd\" Style=\"width:796px\"><tr>");
            sbList.Append("<th style=\"cursor:pointer;width:120px\" ");
            sbList.Append("class=\"w40\" onclick=\"changeOrder(this,'CG_Title')\">" + Title + "<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
            sbList.Append("<th Style=\"width:350px\">" + Description + "</th>");
            //sbList.Append("<th>" + Whom + "</th>");
            sbList.Append("<th Style=\"width:100px\" class=\"w40\" onclick=\"changeOrder(this,'CG_Status')\">" + Status + "<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
            sbList.Append("<th Style=\"width:120px\" style=\"cursor:pointer;\" ");
            if (ordefiled.Equals("CG_Update_dt"))
            {
                sbList.Append("class=\"taxisCurrent\" onclick=\"changeOrder(this,'CG_Update_dt')\">" + Update_dt + "<span class=\"");
                sbList.Append(orderClass);
                sbList.Append("\" title=\"Sort by the column \"></span></th>");
            }
            else
            {
                sbList.Append(" onclick=\"changeOrder(this,'CG_Update_dt')\">" + Update_dt + "<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
            }
            sbList.Append("<th Style=\"width:60px\" class=\"w100\">" + Operate + "</th>");

            int pagecount = -1, rowscount = -1;
            MD_CampaignList CampaignList = Cam.GetCampaignList(UserSession.User.UG_UType, UserSession.DealerEmpl.DE_AD_OM_Code, primarykey, fields, ordefiled,
                                         orderway, currentpage, pagesize, CT, out pagecount, out rowscount, CA);
            if (null != CampaignList && null != CampaignList.CampaignList)
            {

                //生成分页html
                var strPager = new StringBuilder();
                if (1 < pagecount) strPager = Pager.JavascriptPagination(true, currentpage, pagecount, rowscount);
                for (int i = 0; i < CampaignList.CampaignList.Count; i++)
                {
                    CT_Campaigns Camp = CampaignList.CampaignList[i];

                    Camp.Interna = Interna;
                    string Name = string.Empty;
                    BL_Reports Report = new BL_Reports();
                    CT_Reports RE = Report.GetReplaceReport(Interna,
                        new CT_Param_Value() { RP_Code = Camp.CG_RP_Code, PV_Type = 1, PV_CG_Code = Camp.CG_Code, PV_UType = UserSession.User.UG_UType, PV_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code });
                    if (RE == null)
                    {
                        Name = "";
                    }
                    else
                    {
                        Name = RE.RP_Name_EN;
                    }
                    sbList.Append("<tr value='" + Camp.CG_Code + "'><td  style=\"text-align:left\">");
                    sbList.Append(Camp.CG_Title.Length > 30 ? Camp.CG_Title.Substring(0, 30) : Camp.CG_Title);
                    sbList.Append("</td><td  style=\"text-align:left\" title='" + Camp.CG_Desc + "'>");
                    sbList.Append("<div style=\"border-bottom:1px solid #c3c3c3\">");
                    sbList.Append(Camp.CG_Desc.Length > 60 ? Camp.CG_Desc.Substring(0, 60) : Camp.CG_Desc);
                    sbList.Append("</div><div>");
                    if (null != Name)
                    {
                        sbList.Append(Name.Length > 60 ? Name.Substring(0, 60) : Name);
                    }

                    sbList.Append("</div>");
                    sbList.Append("</td>");
                    sbList.Append("<td >" + Camp.CG_S + "</td>");
                    //sbList.Append("</td><td >");
                    //sbList.Append(Camp.SS + "</td>");
                    sbList.Append("<td >");
                    var CG_Update_dt = Camp.CG_Update_dt.ToString("MM/dd/yyyy HH:mm:ss");
                    CG_Update_dt = CG_Update_dt == "01/01/0001 00:00:00" ? "" : CG_Update_dt;
                    sbList.Append(CG_Update_dt);
                    sbList.Append("</td><td style=\"text-align:left\">");
                    sbList.Append("<a href=\"/manage/Campaign/Campaign.aspx?CA=" + Camp.CG_Cat + "&CG_Code=");
                    sbList.Append(Camp.CG_Code);
                    sbList.Append("\"><i class=\"btnModify\" title=\"edit\"></i></a>");
                    sbList.Append("<a href=\"javascript:;\"><i class=\"btnDelete\" title=\"Delete\" onclick=\"Delete(event,");
                    sbList.Append(Camp.CG_Code);
                    sbList.Append(",'");
                    sbList.Append(Camp.CG_Filename);
                    sbList.Append("')\"></i></a>");
                    if (Camp.CG_Status == 10 && DateTime.Now < Camp.CG_End_Dt && Camp.CG_Start_Dt < DateTime.Now)
                    {
                        sbList.Append("<a href=\"javascript:;\"><i class=\"btnRun\" title=\"Run\" onclick=\"Run(event,");
                        sbList.Append(Camp.CG_Code);
                        sbList.Append(",");
                        sbList.Append(Camp.CG_RP_Code);
                        sbList.Append(")\"></i></a>");
                    }
                    sbList.Append("</td></tr>");
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
            return sbList.ToString();
        }
        /// <summary>
        /// 获取Campaign 的报表参数 整个列表的加载
        /// </summary>
        /// <param name="Intern">国际化参数</param>
        /// <returns></returns>
        public MD_ReportList getCampaignReprotList(bool Intern)
        {
            MD_ReportList Report = Cam.GetCampaignReprotList();
            if (Report == null)
            {
                return null;
            }
            //国际化
            if (!Intern)
            {
                Report = ReportReplace.NameReplace(Report);
            }
            if (Report != null)
            {
                Report = BL_ReportReplace.ReportParamReplace(Report);
            }
            return Report;
        }

        /// <summary>
        /// 获取单条活动信息
        /// </summary>
        /// <param name="CG_Code"></param>
        /// <returns></returns>
        public CT_Campaigns GetCampaign(int CG_Code)
        {
            return Cam.GetCampaign(CG_Code);
        }
        public CT_Campaigns GetCampaign(int AD_Code, bool i)
        {
            return Cam.GetCampaign(AD_Code, i);
        }
        public MD_BeneficiaryList getBeneficiary_PhoneList(long AU_Code)
        {
            return Cam.getBeneficiary_PhoneList(AU_Code);
        }
        public MD_BeneficiaryList getBeneficiary_MessagingList(long AU_Code)
        {
            return Cam.getBeneficiary_MessagingList(AU_Code);
        }
        public MD_BeneficiaryList getBeneficiary_EmailList(long AU_Code)
        {
            return Cam.getBeneficiary_EmailList(AU_Code);
        }
        public List<MD_BeneficiaryList> getBeneficiaryList(int AU_Code)
        {
            return Cam.getBeneficiaryList(AU_Code);
        }

        public DataTable getfile_Rep_value(int CG_Code, int AU_Type, int AU_AD_OM_Code)
        {
            CT_Campaigns _o = GetCampaign(CG_Code);
            CT_Param_Value _p = new CT_Param_Value()
            {
                RP_Code = _o.CG_RP_Code,
                PV_Type = 1,
                PV_CG_Code = _o.CG_Code,
                PV_UType = AU_Type,
                PV_AD_OM_Code = AU_AD_OM_Code
            };
            BL_Reports R = new BL_Reports();
            CT_Reports _r = R.GetReplaceReport(_p);
            DataTable dt_Query = BL_Reports.QueryExecution(_r.RP_Query);
            return dt_Query;
        }
        public DataTable getCam_tags()
        {
            return DL_PlacidData.getCam_tags();
        }
        public bool AddCT_Auth_Process(CT_Auth_Process model)
        {
            return Cam.AddCT_Auth_Process(model);
        }

        #region 生成活动报告---Campaign活动Run
        /// <summary>
        /// 生成活动报表
        /// </summary>
        /// <param name="CG_Code">活动ID</param>
        /// <param name="AU_Type">登陆用户类型</param>
        /// <param name="AU_AD_OM_Code">登陆用户所在单位ID</param>
        /// <returns></returns>
        public int CampaignRun(int CG_Code, int AU_Type, int AU_AD_OM_Code)
        {
            BL_Reports R = new BL_Reports();
            int err = R.ReportRun(new ReportRunParam() { pType = 1, CG_EV_Code = CG_Code, UType = AU_Type, AU_AD_OM_Code = AU_AD_OM_Code });
            if (err >= 0)
            {
                BL_Wechat _w = new BL_Wechat();
                _w.SendWechat_news(CG_Code, "", new string[] { "1" });
            }
            return err;
        }
        #endregion
        #region 微信处理
        public static MD_CampaignList GetRecommend_Trpe(int type)
        {
            return new DL_Campaign().GetRecommend_Trpe(type);
        }
        public int Modify_Comm_History(int Modify_Type, string OpenID, int CG_Code)
        {
            return Cam.Modify_Comm_History(Modify_Type, OpenID, CG_Code);
        }
        #endregion
        #region 电话添加

        public DataTable GetApprovalInfo(int DE_UType, int DE_AD_OM_Code, bool Interna, int UG_UType)
        {
            return Cam.GetApprovalInfo(DE_UType, DE_AD_OM_Code, Interna, UG_UType);
        }
        public DataTable GetApprovalInfo(int DE_UType, int DE_AD_OM_Code, bool Interna, int UG_UType, int AT_CG_Cat, int AT_IType, string AT_SType)
        {
            return Cam.GetApprovalInfo(DE_UType, DE_AD_OM_Code, Interna, UG_UType, AT_CG_Cat, AT_IType, AT_SType);
        }
        public bool Delete_CT_Auth_Process(int AT_Code)
        {
            return Cam.Delete_CT_Auth_Process(AT_Code);
        }
        public string GetTarget(int MF_FL_FB_Code, int BF_Code)
        {
            return Cam.GetTarget(MF_FL_FB_Code, BF_Code);
        }
        #endregion

        public string GetProcess(bool Interna, int UType, int Ad_Code, int AT_Cat, int AT_IType)
        {
            return Cam.GetProcess(Interna, UType, Ad_Code, AT_Cat, AT_IType);
        }

        /// <summary>
        /// 获取已经设置好的电话分配情况
        /// </summary>
        /// <param name="CG_AD_Code"></param>
        /// <param name="Interna"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetCamCall(int CG_AD_Code, bool Interna = false, int type = 1)
        {
            return Cam.GetCamCall(CG_AD_Code, Interna, type);
        }
        /// <summary>
        /// 获取TeamGroup
        /// </summary>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public DataTable GetTeamGroup(bool Interna = false)
        {
            return Cam.GetTeamGroup(Interna);
        }
        /// <summary>
        /// 获取组内用户
        /// </summary>
        /// <param name="AD_Code"></param>
        /// <param name="UG_Code"></param>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public DataTable GetTeamGroupUser(int AD_Code, int UG_Code, bool Interna = false)
        {
            return Cam.GetTeamGroupUser(AD_Code, UG_Code, Interna);
        }
        public int Save_Camp_Call(IList<CT_Camp_Calls> _Camp_Calls, int CG_AD_Code, int ccUType = 0)
        {
            return Cam.Save_Camp_Call(_Camp_Calls, CG_AD_Code, ccUType);
        }
        public CT_Campaigns GetTXCam(int UType, int AD_Code)
        {
            return Cam.GetTXCam(UType, AD_Code);
        }
        public CT_All_Users GetTXCam_Number(int AU_Code, int AP_Code)
        {
            return Cam.GetTXCam_Number(AU_Code,AP_Code);
        }

        #region 2015-06-09

        public string[] GetRunAu_Code(int CG_Code, int UType, int AD_Code)
        {
            CT_Campaigns _c = GetCampaign(CG_Code);
            CT_Param_Value _p = new CT_Param_Value()
            {
                RP_Code = _c.CG_RP_Code,
                PV_Type = 1,
                PV_CG_Code = CG_Code,
                PV_UType = UType,
                PV_AD_OM_Code = AD_Code
            };
            BL_Reports _bl_report = new BL_Reports();
            CT_Reports _Report = _bl_report.GetReplaceReport(_p);

            List<CRMTreeDatabase.EX_Param> eps = new List<CRMTreeDatabase.EX_Param>();
            CRMTreeDatabase.EX_Param ep = new CRMTreeDatabase.EX_Param();
            ep.EX_Name = "PR";
            ep.EX_Value = "0";
            ep.EX_DataType = "int";
            eps.Add(ep);
            _Report.RP_Query = BL_Reports.GetReportSql(_Report.RP_Code, eps).SQL;
            string[] _au_code = _bl_report.GetUserCode(_Report.RP_Query, "AU_Code").Distinct().ToArray();
            return _au_code;
        }

        public DataTable GetRunUser(int CG_Code, int UType, int AD_Code)
        {
            string[] _user = GetRunAu_Code(CG_Code, UType, AD_Code);
            return Cam.GetRunUser(_user);
        }

        #endregion
    }
}
