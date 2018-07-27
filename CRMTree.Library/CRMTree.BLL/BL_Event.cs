using CRMTree.DAL;
using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Model.Event;
using CRMTree.Model.Reports;
using CRMTree.Model.User;
using Shinfotech.Tools;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    public class BL_Event
    {
        DL_Event Event = new DL_Event();
        /// <summary>
        /// 获取Event列表
        /// </summary>
        /// <param name="EV_UType"></param>
        /// <param name="EV_AD_OM_Code"></param>
        /// <param name="primarykey"></param>
        /// <param name="fields"></param>
        /// <param name="ordefiled"></param>
        /// <param name="orderway"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="orderClass"></param>
        /// <returns></returns>
        public string getEventList(bool Interna, MD_UserEntity UserSession, string primarykey, string fields, string ordefiled,
                                        string orderway, int currentpage, int pagesize, string orderClass)
        {
            string Title = Interna ? IN_Language.CGL_Title(EM_Language.en_us) : IN_Language.CGL_Title(EM_Language.zh_cn);
            string Description = Interna ? IN_Language.CGL_Description(EM_Language.en_us) : IN_Language.CGL_Description(EM_Language.zh_cn);
            string Whom = Interna ? IN_Language.CGL_Whom(EM_Language.en_us) : IN_Language.CGL_Whom(EM_Language.zh_cn);
            string Active = Interna ? IN_Language.CGL_Active(EM_Language.en_us) : IN_Language.CGL_Active(EM_Language.zh_cn);
            string Update_dt = Interna ? IN_Language.CGL_Update_dt(EM_Language.en_us) : IN_Language.CGL_Update_dt(EM_Language.zh_cn);
            string Operate = Interna ? IN_Language.CGL_Operate(EM_Language.en_us) : IN_Language.CGL_Operate(EM_Language.zh_cn);
            StringBuilder sbList = new StringBuilder();
            sbList.Append("<table class=\"t-bd\" Style=\"width:930px\"><tr>");
            sbList.Append("<th style=\"cursor:pointer;width:150px;\" ");
            sbList.Append("class=\"w40\" onclick=\"changeOrder(this,'EV_Title')\">" + Title + "<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
            sbList.Append("<th Style=\"width:400px\">" + Description + "</th>");
            sbList.Append("<th>" + Whom + "</th>");
            sbList.Append("<th>" + Active + "</th>");
            sbList.Append("<th style=\"cursor:pointer;\" ");
            if (ordefiled.Equals("EV_Update_dt"))
            {
                sbList.Append("class=\"taxisCurrent\" onclick=\"changeOrder(this,'EV_Update_dt')\">" + Update_dt + "<span class=\"");
                sbList.Append(orderClass);
                sbList.Append("\" title=\"Sort by the column \"></span></th>");
            }
            else
            {
                sbList.Append(" onclick=\"changeOrder(this,'EV_Update_dt')\">" + Update_dt + "<span class=\"taxis\" title=\"Sort by the column \"></span></th>");
            }
            sbList.Append("<th class=\"w100\">" + Operate + "</th>");

            int pagecount = -1, rowscount = -1;
            MD_EventList EventList = Event.getEventList(UserSession.User.UG_UType, UserSession.DealerEmpl.DE_AD_OM_Code, primarykey, fields, ordefiled,
                                         orderway, currentpage, pagesize, out pagecount, out rowscount);
            if (null != EventList && EventList.EventList.Count > 0)
            {
                //生成分页html
                var strPager = new StringBuilder();
                if (1 < pagecount) strPager = Pager.JavascriptPagination(true, currentpage, pagecount, rowscount);
                for (int i = 0; i < EventList.EventList.Count; i++)
                {
                    CT_Events Ev = EventList.EventList[i];
                    Ev.Interna = Interna;
                    string Name = string.Empty;
                    BL_Reports Report = new BL_Reports();
                    CT_Reports RE = Report.GetReplaceReport(Interna,
                        new CT_Param_Value() { RP_Code = Ev.EV_RP_Code, PV_Type = 2, PV_CG_Code = Ev.EV_Code, PV_UType = UserSession.User.UG_UType, PV_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code });
                    if (RE == null)
                    {
                        Name = "";
                    }
                    else
                    {
                        Name = RE.RP_Name_EN;
                    }
                    sbList.Append("<tr value='" + Ev.EV_Code + "'><td  style=\"text-align:left\">");
                    sbList.Append(Ev.EV_Title.Length > 30 ? Ev.EV_Title.Substring(0, 30) : Ev.EV_Title);
                    sbList.Append("</td><td  style=\"text-align:left\" title='" + Ev.EV_Desc + "'>");
                    sbList.Append("<div style=\"border-bottom:1px solid #c3c3c3\">");
                    sbList.Append(Ev.EV_Desc.Length > 60 ? Ev.EV_Desc.Substring(0, 60) : Ev.EV_Desc);
                    sbList.Append("</div><div>");
                    sbList.Append(Name.Length > 60 ? Name.Substring(0, 60) : Name);
                    sbList.Append("</div>");
                    sbList.Append("</td><td >");
                    sbList.Append(Ev.S);
                    sbList.Append("</td><td >");
                    sbList.Append(Ev.SS);
                    sbList.Append("</td><td >");
                    sbList.Append(Ev.EV_Update_dt.ToString("MM/dd/yyyy HH:mm:ss"));
                    sbList.Append("</td><td >");
                    sbList.Append("<a href=\"/manage/Event/Edit_Event.aspx?id=");
                    sbList.Append(Ev.EV_Code);
                    sbList.Append("\"><i class=\"btnModify\" title=\"edit\"></i></a>");
                    sbList.Append("<a href=\"javascript:;\"><i class=\"btnDelete\" title=\"Delete\" onclick=\"Delete(event,");
                    sbList.Append(Ev.EV_Code);
                    sbList.Append(",'");
                    sbList.Append(Ev.EV_Filename);
                    sbList.Append("')\"></i></a>");
                    sbList.Append("<a href=\"javascript:;\"><i class=\"btnRun\" title=\"Run\" onclick=\"Run(event,");
                    sbList.Append(Ev.EV_Code);
                    sbList.Append(",");
                    sbList.Append(Ev.EV_RP_Code);
                    sbList.Append(")\"></i></a>");
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
        /// 更新Event
        /// </summary>
        /// <param name="Events"></param>
        /// <returns></returns>
        public int AddEvent(CT_Events Events, MD_SuccMatrixList o_succ)
        {
            return Event.AddEvent(Events, o_succ);
        }
        public int ModifyEvent(CT_Events Events, MD_SuccMatrixList o_succ)
        {
            return Event.ModifyEvent(Events, o_succ);
        }
        public int DeleteEvent(int EV_Code)
        {
            return Event.DeleteEvent(EV_Code);
        }
        /// <summary>
        /// 获取Event 的报表参数 整个列表的加载
        /// </summary>
        /// <param name="Intern">国际化参数</param>
        /// <returns></returns>
        public MD_ReportList getEventReprotList(bool Intern)
        {
            MD_ReportList Report = Event.getEventReprotList();
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
        public MD_GenreList getGenre(int Utype, int AD_OM_Code)
        {
            MD_GenreList dd = new MD_GenreList();
            return Event.getGenre(Utype, AD_OM_Code);
        }
        public CT_Event_Genre getSelectGenre(int EV_Code)
        {
            return Event.getSelectGenre(EV_Code);
        }
        public MD_SuccMatrixList getSuccMatrxList(bool Interna)
        {
            MD_SuccMatrixList Succ = Event.getSuccMatrxList();
            if (!Interna && Succ != null)
            {
                for (int i = 0; i < Succ.SuccMatrixList.Count; i++)
                {
                    Succ.SuccMatrixList[i].PSM_Desc_EN = Succ.SuccMatrixList[i].PSM_Desc_CN;
                }
            }
            return Succ;
        }
        public MD_SuccMatrixList getSuccMatrxList(bool Interna, CT_Succ_Matrix succ)
        {
            if (succ == null||string.IsNullOrEmpty(succ.PSM_Code_s)) { return null; }
            string[] _id = succ.PSM_Code_s.Split(',');
            Array.Sort(_id);
            MD_SuccMatrixList o = new MD_SuccMatrixList();
            o.SuccMatrixList = new List<CT_Succ_Matrix>();
            for (int i = 0; i < _id.Length; i++)
            {
                CT_Succ_Matrix _o = Event.getSuccMatrxList(int.Parse(_id[i]), succ.SMV_CG_Code, succ.SMV_Type);
                if (!Interna && _o != null)
                    _o.PSM_Desc_EN = _o.PSM_Desc_CN;
                if(_o!=null)
                    o.SuccMatrixList.Add(_o);
            }
            return o;
        }
        public MD_EventPersonList getEventPersonList(bool Interna)
        {
            MD_EventPersonList Person = Event.getEventPersonList();
            if (!Interna && Person != null)
            {
                for (int i = 0; i < Person.EventPersonList.Count; i++)
                {
                    Person.EventPersonList[i].PEP_Desc_EN = Person.EventPersonList[i].PEP_Desc_CN;
                }
            }
            return Person;
        }
        public MD_EventToolsList getEventToolsList(bool Interna)
        {
            MD_EventToolsList Tools = Event.getEventToolsList();
            if (!Interna && Tools != null)
            {
                for (int i = 0; i < Tools.EventToolsLIst.Count; i++)
                {
                    Tools.EventToolsLIst[i].PET_Desc_EN = Tools.EventToolsLIst[i].PET_Desc_CN;
                }
            }
            return Tools;
        }
        public CT_Events getEvents(int EV_Code)
        {
            return Event.getEvents(EV_Code);
        }
        #region 生成活动报告---Campaign活动Run
        /// <summary>
        /// 生成活动报表
        /// </summary>
        /// <param name="CG_Code">活动ID</param>
        /// <param name="AU_Type">登陆用户类型</param>
        /// <param name="AU_AD_OM_Code">登陆用户所在单位ID</param>
        /// <returns></returns>
        public int EventRun(int EV_Code, int AU_Type, int AU_AD_OM_Code)
        {
            BL_Reports R = new BL_Reports();
            return R.ReportRun(new ReportRunParam() { pType = 2, CG_EV_Code = EV_Code, UType = AU_Type, AU_AD_OM_Code = AU_AD_OM_Code });
        }
        #endregion
    }
}
