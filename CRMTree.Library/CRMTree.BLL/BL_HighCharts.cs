using CRMTree.DAL;
using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Model.Reports;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    /// <summary>
    /// 该类用于处理各类图表
    /// </summary>
    public class BL_HighCharts
    {
        BL_Reports _B_rp = new BL_Reports();
        public DataTable getPieData(int FL_FB_Code, out int RP_Code, int PR = 0)
        {
            CT_Reports _r = _B_rp.GetReprot_Chat(ChartType.Pie, FL_FB_Code);
            if (_r == null)
            {
                RP_Code = -1;
                return null;
            }
            //DataTable dt = DL_Reports.QueryExecution(ReportReplace.ReportParamReplace(_r.RP_Query));
            DataTable dt = BL_Reports.GetReportSqlWithTemplate(ReportReplace.ReportParamReplace(_r.RP_Query),PR);
            RP_Code = _r.RP_Code;
            return dt;
        }
        public DataTable getPieTitle(int RP_Code, int pr = 0)
        {
            DataTable dt = DL_PlacidData.getPieChatsTitle(RP_Code,pr);
            return dt;
        }
        public DataTable getBarTitle(int RP_Code)
        {
            DataTable dt = DL_PlacidData.getBarChatsTitle(RP_Code);
            return dt;
        }
        public DataTable getPieTitleII(int RP_Code)
        {
            DataTable dt = DL_PlacidData.getPieChatsTitleII(RP_Code);
            return dt;
        }
        public DataTable getMultiParam(int RP_Code)
        {
            DataTable dt = DL_PlacidData.getMultiChartParam(RP_Code);
            return dt;
        }
        public DataTable getDGaugParam(int RP_Code)
        {
            DataTable dt = DL_PlacidData.getDGaugChartParam(RP_Code);
            return dt;
        }
        public DataTable getDrillParam(int RP_Code)
        {
            DataTable dt = DL_PlacidData.getDrillChartParam(RP_Code);
            return dt;
        }
        public DataTable getColumnData(int RP_Code, int PR = 0)
        {
            CT_Reports _r = _B_rp.GetReprot_Chat(ChartType.Bar, RP_Code);
            if (_r == null)
            {
                return null;
            }
            DataTable dt = BL_Reports.GetReportSqlWithTemplate(ReportReplace.ReportParamReplace(_r.RP_Query), PR);
            return dt;
        }
        public DataTable getMultiData(int RP_Code, int PR = 0)
        {
            CT_Reports _r = _B_rp.GetReprot_Chat(ChartType.Multi, RP_Code);
            if (_r == null)
            {
                return null;
            }
            DataTable dt = BL_Reports.GetReportSqlWithTemplate(ReportReplace.ReportParamReplace(_r.RP_Query), PR);
            return dt;
        }
        public DataTable getGaugeData(int RP_Code, int PR = 0)
        {
            CT_Reports _r = _B_rp.GetReprot_Chat(ChartType.Gauge, RP_Code);
            if (_r == null)
            {
                return null;
            }
            DataTable dt = BL_Reports.GetReportSqlWithTemplate(ReportReplace.ReportParamReplace(_r.RP_Query), PR);
            return dt;
        }
        public DataTable getDrillData(int RP_Code, int PR = 0)
        {
            CT_Reports _r = _B_rp.GetReprot_Chat(ChartType.Drill, RP_Code);
            if (_r == null)
            {
                return null;
            }
            DataTable dt = BL_Reports.GetReportSqlWithTemplate(ReportReplace.ReportParamReplace(_r.RP_Query), PR);
            return dt;
        }
        public DataTable getSchTitle(int RP_Code)
        {
            DataTable dt = DL_PlacidData.getSchChatsTitle(RP_Code);
            return dt;
        }
        public DataTable getSchData(int RP_Code,int PR = 0)
        {
            CT_Reports _r = _B_rp.GetReprot_Chat(ChartType.Sch, RP_Code);
            if (_r == null)
            {
                return null;
            }
            DataTable dt = BL_Reports.GetReportSqlWithTemplate(ReportReplace.ReportParamReplace(_r.RP_Query), PR);
//            DataTable dt = DAL.DL_Reports.QueryExecution(ReportReplace.ReportParamReplace(_r.RP_Query));
            return dt;
        }
    }
}
