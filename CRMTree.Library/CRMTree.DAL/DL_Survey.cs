using CRMTree.Model.Common;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMTree.DAL
{
    public class DL_Survey
    {
        /// <summary>
        /// 获取指定活动的问卷题目
        /// </summary>
        /// <param name="CG_Code"></param>
        /// <returns></returns>
        public DataSet GetSurvey(int CG_Code, int AU_Code, int DE_advisor)
        {
            string sql = "SELECT * FROM CT_Survey_Questions SQ LEFT JOIN CT_Survey_Formats SF ON SQ_SF_Code = SF_Code WHERE SQ_CG_Code =" + CG_Code + " order by SQ_No;SELECT * FROM CT_Survey_Responses WHERE SR_CG_Code=" + CG_Code + " AND SR_AU_Code=" + AU_Code + " AND SR_Advisor=" + DE_advisor + "";
            string z_exec = @"DECLARE @SumPage INT ,@Sum INT 
                              EXEC [dbo].[public_pager_01] '" + sql + "','SQ_Code','SQ_No','ASC',1,10,@SumPage OUTPUT,@Sum OUTPUT SELECT @SumPage AS SumPage,@Sum AS SumRow;";
            DataSet ds = SqlHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 保存指定活动问卷的回答
        /// </summary>
        /// <param name="CG_Code"></param>
        /// <param name="AU_Code"></param>
        /// <param name="DE_Code"></param>
        /// <param name="An"></param>
        /// <param name="Not"></param>
        /// <returns></returns>
        public int Save_Survey(int CG_Code, int AU_Code, int DE_Code, string An, string Not)
        {
            string sql = @"DELETE CT_Survey_Responses WHERE SR_CG_Code=" + CG_Code + " AND SR_AU_Code=" + AU_Code + " AND SR_Advisor=" + DE_Code + "; INSERT INTO CT_Survey_Responses([SR_CG_Code],[SR_AU_Code],[SR_Advisor],[SR_Answers] ,[SR_Notes],[SR_Update_dt],[SR_Create_dt]) VALUES(" + CG_Code + "," + AU_Code + "," + DE_Code + ",'" + An + "','" + Not + "',GETDATE(),GETDATE());";
            int i = SqlHelper.ExecuteNonQuery(sql);
            if (i > 0)
            {
                return (int)_errCode.success;
            }
            else
            {
                return (int)_errCode.isExecNull;
            }
        }
        /// <summary>
        /// 获取问卷活动的类别
        /// </summary>
        /// <returns></returns>
        public DataTable GetSurveyCamCategory(bool Interna)
        {
            string _Text = Interna ? "text_en" : "text_cn";
            string sql = string.Format(@"SELECT {0} as text,* FROM WORDS WHERE P_ID IN (4093,4170,4184) ORDER BY SORT;", _Text);
            DataSet ds = SqlHelper.ExecuteDataset( sql);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取问卷类别内的活动
        /// </summary>
        /// <param name="CG_Type"></param>
        /// <returns></returns>
        public DataTable GetSurveyCategoryCam(int CG_Type)
        {
            string sql = @"SELECT CG_Code,CG_Title,CG_Desc FROM CT_Campaigns WHERE CG_TYPE = " + CG_Type + ";";
            DataSet ds = SqlHelper.ExecuteDataset( sql);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            return ds.Tables[0];
        }
        public int Save_Survey_data(int CG_Code, dynamic o, int index = 0)
        {
            if (o == null)
            {
                return (int)_errCode.isObjectNull;
            }
            string _sql1 = string.Empty;
            if (o.SF_Code != null && (int)o.SF_Code > 0)
            {
                _sql1 += @"delete from CT_Survey_Formats where SF_Code = " + o.SF_Code + ";";
            }
            if (o.SQ_Code != null && (int)o.SQ_Code > 0)
            {
                _sql1 += @"delete from CT_Survey_Questions where SQ_Code=" + o.SQ_Code + "";
            }
            _sql1 += @"insert into CT_Survey_Formats(SF_Types,SF_Text,SF_ValType,SF_Value) values('" + o.SF_Types + "','" + o.SF_Text + "','" + o.SF_ValType + "','" + o.SF_Value + "');";
            _sql1 += "select @@IDENTITY as Code";
            SqlConnection _con = new SqlConnection(SqlHelper.GetConnectionString());
            _con.Open();
            SqlTransaction _tran = _con.BeginTransaction();
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(_tran, CommandType.Text, _sql1);
                if (ds == null || ds.Tables.Count <= 0)
                {
                    return (int)_errCode.systomError;
                }
                int _Code = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                string _sql2 = string.Empty;
                _sql2 = @"insert into CT_Survey_Questions(SQ_CG_Code,SQ_No,SQ_Question,SQ_Page,SQ_SF_Code)values(" + CG_Code + "," + index + ",'" + o.SQ_Question + "',1," + _Code + ");";
                int _err = SqlHelper.ExecuteNonQuery(_tran, CommandType.Text, _sql2);
                _tran.Commit();
                _tran.Dispose();
                _con.Close();
                return _err;
            }
            catch
            {
                _tran.Rollback();
                _tran.Dispose();
                _con.Close();
                return (int)_errCode.systomError;
            }
        }
        public int Delete_Survey(int SQ_Code, int SF_Code)
        {
            string sql = "delete from CT_Survey_Formats where SF_Code = " + SF_Code + ";delete from CT_Survey_Questions where SQ_Code=" + SQ_Code + "";
            int _err = SqlHelper.ExecuteNonQuery( sql);
            return _err;
        }

        public IList<DataTable> GetSurveyAnswer(int HD_Code)
        {
            string sql = string.Format(@"with tb as(
                                    select CH_AU_COde, CH_CG_Code,HD_AU_Code,HD_Code from CT_Handler hd
                                    left join CT_Comm_History ch on hd.HD_CH_Code=ch.CH_Code
                                    where HD_Code={0}
                                    )
                                    select * from CT_Survey_Questions sq 
                                     left join CT_Survey_Formats sf on sq.SQ_SF_Code=sf.SF_Code
                                     inner join tb on tb.CH_CG_Code = SQ_CG_Code 
                                    order by SQ_No;
                                    with tb1 as(
                                    select CH_AU_COde, CH_CG_Code,HD_AU_Code,HD_Code from CT_Handler hd
                                    left join CT_Comm_History ch on hd.HD_CH_Code=ch.CH_Code
                                    where HD_Code={0}
                                    )
                                    select * from  CT_Survey_Responses
                                    inner join tb1 on tb1.CH_CG_Code = SR_CG_Code and tb1.CH_AU_Code = SR_AU_Code and tb1.HD_AU_Code=SR_Advisor", HD_Code);
            DataSet ds = SqlHelper.ExecuteDataset( sql);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            IList<DataTable> _table = new List<DataTable>();
            if (ds.Tables[0] != null)
            {
                _table.Add(ds.Tables[0]);
            }
            if (ds.Tables[1] != null)
            {
                _table.Add(ds.Tables[1]);
            }
            return _table;
        }
    }
}
