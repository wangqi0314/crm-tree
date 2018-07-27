using CRMTree.Model;
using CRMTree.Model.Event;
using CRMTree.Model.Reports;
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
    /// <summary>
    /// 该类主要用于Event的数据处理
    /// </summary>
    public class DL_Event
    {
        /// <summary>
        /// 获取Event列表，分页显示
        /// </summary>
        /// <param name="EV_UType"></param>
        /// <param name="EV_AD_OM_Code"></param>
        /// <param name="primarykey"></param>
        /// <param name="fields"></param>
        /// <param name="ordefiled"></param>
        /// <param name="orderway"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="pagecount"></param>
        /// <param name="rowscount"></param>
        /// <returns></returns>
        public MD_EventList getEventList(int EV_UType, int EV_AD_OM_Code, string primarykey, string fields, string ordefiled,
                                         string orderway, int currentpage, int pagesize, out int pagecount, out int rowscount)
        {
            #region 参数
            SqlParameter[] parameters = { new SqlParameter("@EV_UType", SqlDbType.Int,4),
                                          new SqlParameter("@EV_AD_OM_Code", SqlDbType.Int,4),
                                          new SqlParameter("@primarykey", SqlDbType.VarChar,500),
                                          new SqlParameter("@fields", SqlDbType.VarChar,500),
                                          new SqlParameter("@ordefiled", SqlDbType.VarChar,500),
                                          new SqlParameter("@orderway", SqlDbType.VarChar,500),
                                          new SqlParameter("@currentpage", SqlDbType.Int,4),
                                          new SqlParameter("@pagesize", SqlDbType.Int,4),
                                          new SqlParameter("@pagecount", SqlDbType.Int,4),
                                          new SqlParameter("@rowscount", SqlDbType.Int,4)
                                    };
            parameters[0].Value = EV_UType;
            parameters[1].Value = EV_AD_OM_Code;
            parameters[2].Value = primarykey;
            parameters[3].Value = fields;
            parameters[4].Value = ordefiled;
            parameters[5].Value = orderway;
            parameters[6].Value = currentpage;
            parameters[7].Value = pagesize;
            parameters[8].Direction = ParameterDirection.Output;
            parameters[9].Direction = ParameterDirection.Output;
            #endregion
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure,
                                                  "EV_getEventList", parameters);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                pagecount = 0;
                rowscount = 0;
                return null;
            }
            pagecount = Convert.ToInt32(parameters[8].Value.ToString());
            rowscount = Convert.ToInt32(parameters[9].Value.ToString());
            MD_EventList Ev_List = new MD_EventList();
            Ev_List.EventList = DataHelper.ConvertToList<CT_Events>(ds);
            return Ev_List;
        }
        /// <summary>
        /// 新增一个新的Event
        /// </summary>
        /// <param name="Event"></param>
        /// <returns></returns>
        public int AddEvent(CT_Events Event, MD_SuccMatrixList o_succ)
        {
            #region 参数
            SqlParameter[] parameters = { new SqlParameter("@Results", SqlDbType.Int,4),
                                          new SqlParameter("@EV_UType", SqlDbType.Int,4),
                                          new SqlParameter("@EV_AD_OM_Code", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Share", SqlDbType.Bit),
                                          new SqlParameter("@EV_Title", SqlDbType.NVarChar,50),
                                          new SqlParameter("@EV_Desc", SqlDbType.NVarChar,250),
                                          new SqlParameter("@EV_Type", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Method", SqlDbType.NVarChar,100),
                                          new SqlParameter("@EV_Mess_Type", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Whom", SqlDbType.Int,4),
                                          new SqlParameter("@EV_RP_Code", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Filename", SqlDbType.NVarChar,250),
                                          new SqlParameter("@EV_Start_dt", SqlDbType.DateTime),
                                          new SqlParameter("@EV_End_dt", SqlDbType.DateTime),
                                          new SqlParameter("@EV_Succ_Matrix", SqlDbType.NVarChar,80),
                                          new SqlParameter("@EV_TrackFlag", SqlDbType.Bit),
                                          new SqlParameter("@EV_LastRun", SqlDbType.DateTime),
                                          new SqlParameter("@EV_Active_Tag", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Created_By", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Updated_By", SqlDbType.Int,4),
                                          new SqlParameter("@EV_EG_Code", SqlDbType.Int,4),
                                          new SqlParameter("@EV_RSVP", SqlDbType.Bit),
                                          new SqlParameter("@EV_Act_S_dt", SqlDbType.DateTime),
                                          new SqlParameter("@EV_Act_E_dt", SqlDbType.DateTime),
                                          new SqlParameter("@EV_Respnsible", SqlDbType.NVarChar,50),
                                          new SqlParameter("@EV_Budget", SqlDbType.Money),
                                          new SqlParameter("@EV_Tools", SqlDbType.NVarChar,50),
                                          new SqlParameter("@PL_Code_List", SqlDbType.NVarChar,500),
                                          new SqlParameter("@Pl_Val_List", SqlDbType.NVarChar,500),
                                    };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = Event.EV_UType;
            parameters[2].Value = Event.EV_AD_OM_Code;
            parameters[3].Value = Event.EV_Share;
            parameters[4].Value = Event.EV_Title;
            parameters[5].Value = Event.EV_Desc;
            parameters[6].Value = Event.EV_Type;
            parameters[7].Value = Event.EV_Method;
            parameters[8].Value = Event.EV_Mess_Type;
            parameters[9].Value = Event.EV_Whom;
            parameters[10].Value = Event.EV_RP_Code;
            parameters[11].Value = Event.EV_Filename;
            parameters[12].Value = Event.EV_Start_dt;
            parameters[13].Value = Event.EV_End_dt;
            parameters[14].Value = Event.EV_Succ_Matrix;
            parameters[15].Value = Event.EV_TrackFlag;
            parameters[16].Value = Event.EV_LastRun;
            parameters[17].Value = Event.EV_Active_Tag;
            parameters[18].Value = Event.EV_Created_By;
            parameters[19].Value = Event.EV_Updated_By;
            parameters[20].Value = Event.EV_EG_Code;
            parameters[21].Value = Event.EV_RSVP;
            parameters[22].Value = Event.EV_Act_S_dt;
            parameters[23].Value = Event.EV_Act_E_dt;
            parameters[24].Value = Event.EV_Respnsible;
            parameters[25].Value = Event.EV_Budget;
            parameters[26].Value = Event.EV_Tools;
            parameters[27].Value = Event.PL_Code_List;
            parameters[28].Value = Event.Pl_Val_List;
            #endregion
            SqlConnection _c = new SqlConnection(SqlHelper.GetConnectionString());
            _c.Open();
            SqlTransaction _t = _c.BeginTransaction();

            try
            {
                int i = SqlHelper.ExecuteNonQuery(_t, CommandType.StoredProcedure, "EV_Event_Add", parameters);
                //Succ的操作
                if (i > 0)
                {
                    int _id = (int)parameters[0].Value;
                    SqlHelper.ExecuteNonQuery(_t, CommandType.Text,
                                          "delete from CT_SM_Values where SMV_Type=2 and SMV_CG_Code=" + _id + ";");
                    for (int j = 0; j < o_succ.SuccMatrixList.Count; j++)
                    {
                        CT_Succ_Matrix _o = o_succ.SuccMatrixList[j];
                        SqlHelper.ExecuteNonQuery(_t, CommandType.Text,
                                          "insert into CT_SM_Values values(" + _o.PSM_Code + ",2," + _id + "," + _o.SMV_Days + "," + _o.SMV_Val + ");");
                    }
                }
                _t.Commit();
                _c.Close();
                return i;
            }
            catch { _t.Rollback(); _c.Close(); return -1; }
        }
        public int ModifyEvent(CT_Events Event, MD_SuccMatrixList o_succ)
        {
            #region 参数
            SqlParameter[] parameters = { new SqlParameter("@Results", SqlDbType.Int,4),
                                          new SqlParameter("@EV_UType", SqlDbType.Int,4),
                                          new SqlParameter("@EV_AD_OM_Code", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Share", SqlDbType.Bit),
                                          new SqlParameter("@EV_Title", SqlDbType.NVarChar,50),
                                          new SqlParameter("@EV_Desc", SqlDbType.NVarChar,250),
                                          new SqlParameter("@EV_Type", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Method", SqlDbType.NVarChar,4),
                                          new SqlParameter("@EV_Mess_Type", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Whom", SqlDbType.Int,4),
                                          new SqlParameter("@EV_RP_Code", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Filename", SqlDbType.NVarChar,250),
                                          new SqlParameter("@EV_Start_dt", SqlDbType.DateTime),
                                          new SqlParameter("@EV_End_dt", SqlDbType.DateTime),
                                          new SqlParameter("@EV_Succ_Matrix", SqlDbType.NVarChar,80),
                                          new SqlParameter("@EV_TrackFlag", SqlDbType.Bit),
                                          new SqlParameter("@EV_Active_Tag", SqlDbType.Int,4),
                                          new SqlParameter("@EV_Updated_By", SqlDbType.Int,4),
                                          new SqlParameter("@EV_EG_Code", SqlDbType.Int,4),
                                          new SqlParameter("@EV_RSVP", SqlDbType.Bit),
                                          new SqlParameter("@EV_Act_S_dt", SqlDbType.DateTime),
                                          new SqlParameter("@EV_Act_E_dt", SqlDbType.DateTime),
                                          new SqlParameter("@EV_Respnsible", SqlDbType.NVarChar,50),
                                          new SqlParameter("@EV_Budget", SqlDbType.Money),
                                          new SqlParameter("@EV_Tools", SqlDbType.NVarChar,50),
                                          new SqlParameter("@EV_Code", SqlDbType.Int,4),
                                          new SqlParameter("@PL_Code_List", SqlDbType.NVarChar,500),
                                          new SqlParameter("@Pl_Val_List", SqlDbType.NVarChar,500),
                                    };
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = Event.EV_UType;
            parameters[2].Value = Event.EV_AD_OM_Code;
            parameters[3].Value = Event.EV_Share;
            parameters[4].Value = Event.EV_Title;
            parameters[5].Value = Event.EV_Desc;
            parameters[6].Value = Event.EV_Type;
            parameters[7].Value = Event.EV_Method;
            parameters[8].Value = Event.EV_Mess_Type;
            parameters[9].Value = Event.EV_Whom;
            parameters[10].Value = Event.EV_RP_Code;
            parameters[11].Value = Event.EV_Filename;
            parameters[12].Value = Event.EV_Start_dt;
            parameters[13].Value = Event.EV_End_dt;
            parameters[14].Value = Event.EV_Succ_Matrix;
            parameters[15].Value = Event.EV_TrackFlag;
            parameters[16].Value = Event.EV_Active_Tag;
            parameters[17].Value = Event.EV_Updated_By;
            parameters[18].Value = Event.EV_EG_Code;
            parameters[19].Value = Event.EV_RSVP;
            parameters[20].Value = Event.EV_Act_S_dt;
            parameters[21].Value = Event.EV_Act_E_dt;
            parameters[22].Value = Event.EV_Respnsible;
            parameters[23].Value = Event.EV_Budget;
            parameters[24].Value = Event.EV_Tools;
            parameters[25].Value = Event.EV_Code;
            parameters[26].Value = Event.PL_Code_List;
            parameters[27].Value = Event.Pl_Val_List;
            #endregion
            SqlConnection _c = new SqlConnection(SqlHelper.GetConnectionString());
            _c.Open();
            SqlTransaction _t = _c.BeginTransaction();

            try
            {
                int i = SqlHelper.ExecuteNonQuery(_t, CommandType.StoredProcedure, "EV_Event_Modify", parameters);
                //Succ的操作
                if (i > 0)
                {
                    int _id = Event.EV_Code;
                    SqlHelper.ExecuteNonQuery(_t, CommandType.Text,
                                          "delete from CT_SM_Values where SMV_Type=2 and SMV_CG_Code=" + _id + ";");
                    for (int j = 0; j < o_succ.SuccMatrixList.Count; j++)
                    {
                        CT_Succ_Matrix _o = o_succ.SuccMatrixList[j];
                        SqlHelper.ExecuteNonQuery(_t, CommandType.Text,
                                          "insert into CT_SM_Values values(" + _o.PSM_Code + ",2," + _id + "," + _o.SMV_Days + "," + _o.SMV_Val + ");");
                    }
                }
                _t.Commit();
                _c.Close();
                return i;
            }
            catch { _t.Rollback(); _c.Close(); return -1; }
        }
        public int DeleteEvent(int EV_Code)
        {
            string strSql = "delete from CT_Events where EV_Code=" + EV_Code + ";";
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql);
            return i;
        }
        /// <summary>
        /// 获取Event 要选择调用的报表列表 type=6,7 ,8 
        /// 加载下拉列表时使用
        /// </summary>
        /// <returns></returns>
        public MD_ReportList getEventReprotList()
        {
            string strSql = @"select RP.RP_Code,RP.RP_Name_EN,RP.RP_Name_CN,PL.PL_Default,PL.PL_Tag,PL_Type  
                            from CT_Reports RP 
                            inner join CT_Paramters_list PL on RP.RP_Code=PL.PL_RP_Code
                            where RP.RP_Type in(2,5,7,9,10,11)";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql);
            MD_ReportList myReportList = new MD_ReportList();
            myReportList.CT_Reports_List = DataHelper.ConvertToList<CT_Reports>(ds);
            return myReportList;
        }
        /// <summary>
        /// CT_Event_Genre
        /// </summary>
        /// <param name="Utype"></param>
        /// <param name="AD_OM_Code"></param>
        /// <returns></returns>
        public MD_GenreList getGenre(int Utype, int AD_OM_Code)
        {
            string sqlGenre = "select * from CT_Event_Genre where EG_UType="
                                + Utype + " and EG_AD_OM_Code="
                                + AD_OM_Code + ";";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sqlGenre);
            MD_GenreList GenreList = new MD_GenreList();
            GenreList.GenreList = DataHelper.ConvertToList<CT_Event_Genre>(ds);
            return GenreList;
        }
        /// <summary>
        /// CT_Event_Genre
        /// </summary>
        /// <param name="Utype"></param>
        /// <param name="AD_OM_Code"></param>
        /// <returns></returns>
        public CT_Event_Genre getSelectGenre(int EV_Code)
        {
            string sqlGenre = "select EG.* from CT_Event_Genre EG inner join CT_Events on EG_Code=EV_EG_Code where EV_Code=" + EV_Code + ";";

            CT_Event_Genre o = DataHelper.ConvertToObject<CT_Event_Genre>(sqlGenre);
            return o;
        }
        /// <summary>
        /// CT_Succ_Matrix
        /// </summary>
        /// <returns></returns>
        public MD_SuccMatrixList getSuccMatrxList()
        {
            string sqlSuccMatrix = "select * from CT_Succ_Matrix;";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sqlSuccMatrix);

            MD_SuccMatrixList o = new MD_SuccMatrixList();
            o.SuccMatrixList = DataHelper.ConvertToList<CT_Succ_Matrix>(ds);
            return o;
        }
        public CT_Succ_Matrix getSuccMatrxList(int PSM_Code, int SMV_CG_Code, int SMV_Type)
        {
            string sqlSuccMatrix = @"Select * from CT_Succ_Matrix inner join CT_SM_Values on PSM_Code=SMV_PSM_Code
                                        where  PSM_Code =" + PSM_Code + " and SMV_CG_Code=" + SMV_CG_Code + " and SMV_Type=" + SMV_Type + ";";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sqlSuccMatrix);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            {
                sqlSuccMatrix = @"select * from CT_Succ_Matrix where PSM_Code=" + PSM_Code + ";";
                ds = SqlHelper.ExecuteDataset(CommandType.Text, sqlSuccMatrix);
            }

            CT_Succ_Matrix o = DataHelper.ConvertToObject<CT_Succ_Matrix>(ds);
            return o;
        }
        /// <summary>
        /// CT_Succ_Matrix
        /// </summary>
        /// <returns></returns>
        public MD_EventPersonList getEventPersonList()
        {
            string sqlSuccMatrix = "select * from CT_Events_Person;";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sqlSuccMatrix);

            MD_EventPersonList o = new MD_EventPersonList();
            o.EventPersonList = DataHelper.ConvertToList<CT_Events_Person>(ds);
            return o;
        }
        /// <summary>
        /// CT_Succ_Matrix
        /// </summary>
        /// <returns></returns>
        public MD_EventToolsList getEventToolsList()
        {
            string sqlSuccMatrix = "select * from CT_Events_Tools;";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sqlSuccMatrix);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0) { return null; }

            MD_EventToolsList o = new MD_EventToolsList();
            o.EventToolsLIst = DataHelper.ConvertToList<CT_Events_Tools>(ds);
            return o;
        }
        public CT_Events getEvents(int EV_Code)
        {
            string sqlEvents = "select * from CT_Events where EV_Code=" + EV_Code + ";";

            CT_Events o = DataHelper.ConvertToObject<CT_Events>(sqlEvents);
            return o;
        }
    }
}
