using CRMTree.BLL._IService;
using CRMTree.DAL;
using CRMTree.Model;
using CRMTree.Model.Common;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.BLL
{
    /// <summary>
    /// 关于服务器报表相关的新服务处理
    /// </summary>
    public class BL_Report_Service : IReport_Service
    {
        DL_Reports _d_report;

        public BL_Report_Service()
        {
            _d_report = new DL_Reports();
        }


        public string getReportTitle(int code, bool Ins = false)
        {
            string _title = Ins ? "FN_Desc_EN" : "FN_Desc_CN";
            string sql = string.Format(@"SELECT [FN_Code]
      ,[FN_FL_FB_Code]
      ,[FN_FieldName] AS field
      ,[{1}] AS title
      ,[FN_Format]
      ,[FN_Font]
      ,[FN_Width] AS width
      ,(CASE WHEN ISNULL(FN_Option,1) = 2 THEN 'center' WHEN ISNULL(FN_Option,1) = 3 THEN 'right' ELSE 'left' END) AS align
      ,[FN_Type]
      ,[FN_Order]
  FROM [CRMTREE].[dbo].[CT_Fields_Names] WHERE FN_FL_FB_Code = {0} ORDER BY FN_Order", code, _title);
            string _o = DataHelper.ConvertToJSON(sql);
            return _o;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<CT_Fields_Name> getReportTitle(int code)
        {
            string sql = string.Format(@"SELECT *
  FROM [CRMTREE].[dbo].[CT_Fields_Names] WHERE FN_FL_FB_Code = {0} ORDER BY FN_Order", code);
            return DataHelper.ConvertToList<CT_Fields_Name>(sql);
        }

        /// <summary>
        /// 获取Fl_s列表的信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public CT_Fields_list getFl_Date(int code)
        {
            string sql = string.Format(@"SELECT * FROM CT_Fields_lists WHERE FL_CODE = {0};", code);
            return DataHelper.ConvertToObject<CT_Fields_list>(sql);
        }
        public string getFl_Date(string code)
        {
            string sql = string.Format(@"SELECT * FROM CT_Fields_lists WHERE FL_CODE = {0};", code);
            return DataHelper.ConvertToJSON(sql);
        }

        /// <summary>
        /// 获取某一报表配置好完整SQL,[用动态参数配置报表sql]
        /// </summary>
        /// <param name="rp_code">报表Code</param>
        /// <param name="_params">动态参数</param>
        /// <returns></returns>
        public string SqlParamReplace(int rp_code, IList<_param> _params)
        {
            CT_Reports _c_report = _d_report.GetReprot(rp_code);
            if (_c_report == null)
            {
                return string.Empty;
            }

            string sql = ReportReplace.ReportParamReplace(_c_report.RP_Query);
            string[] sqls = sql.Split(new string[] { "|-|" }, StringSplitOptions.None);
            if (sqls.Length == 1)
            {
                sql = sqls[0];
            }
            if (_params != null)
            {
                foreach (_param _p in _params)
                {
                    string _pp = string.Format("@{0}", _p.Kay.Trim());
                    sql = sql.Replace(_pp, _p.Value);
                }
            }
            return sql;
        }

        /// <summary>
        /// 获取报表的数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string getReportData(int code, IList<_param> _params)
        {
            CT_Fields_list _fie_list = getFl_Date(code);
            if (_fie_list == null)
            {
                return string.Empty;
            }
            string _sql = SqlParamReplace(_fie_list.FL_RP_Code, _params);
            return DataHelper.ConvertToJSON(_sql);
        }


        /// <summary>
        /// 取报表排序后某一页的数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="page"></param>
        /// <param name="_params"></param>
        /// <returns></returns>
        public string getReportDataPage(int code, int page, IList<_param> _params)
        {
            CT_Fields_list _fie_list = getFl_Date(code);
            if (_fie_list == null)
            {
                return string.Empty;
            }
            string _sql = SqlParamReplace(_fie_list.FL_RP_Code, _params);
            _sql = _sql.Replace("'", "''");
            IList<CT_Fields_Name> _fie_name = getReportTitle(code);
            if (_fie_name == null || _fie_name.Count <= 0)
            {
                return string.Empty;
            }
            string _zj = _fie_name[0].FN_FieldName;
            string _px = _zj;
            string _or = "DESC";
            string z_exec = string.Format(@"DECLARE @SumPage INT ,@Sum INT 
                              EXEC [dbo].[public_pager_01] '{0}','{1}','{2}','{3}',{4},10,@SumPage OUTPUT,@Sum OUTPUT SELECT @SumPage AS SumPage,@Sum AS SumRow;", _sql, _zj, _px, _or, page);

            return DataHelper.ConvertToJSONPage(z_exec);
        }
    }


    namespace _IService
    {
        public interface IReport_Service
        {
            /// <summary>
            /// 用动态参数配置报表sql
            /// </summary>
            /// <param name="rp_code">报表code</param>
            /// <param name="_params"></param>
            /// <returns></returns>
            string SqlParamReplace(int rp_code, IList<_param> _params);
        }
    }

    /// <summary>
    /// 动态传递的参数对象
    /// </summary>
    public class _param
    {
        public string Kay { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
