using CRMTree.Model;
using CRMTree.Model.Appointmens;
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
    /// 预约服务数据处理
    /// </summary>
    public class DL_Appt_Service
    {
        #region 添加获取APP
        /// <summary>
        /// 获取所有预约信息
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="AU_code"></param>
        /// <returns></returns>
        public string GetAppList_Json(long AU_code)
        {
            #region 参数
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.BigInt) };
            parameters[0].Value = AU_code;
            #endregion
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "01_Select_Appt_All_W", parameters);
            return DataHelper.ConvertToJSON(ds);
        }
        /// <summary>
        /// 获取单条预约
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        public string GetApp_Json(int AP_Code)
        {
            #region
            SqlParameter[] parameters = { new SqlParameter("@AP_Code", SqlDbType.BigInt) };
            parameters[0].Value = AP_Code;
            #endregion
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "01_Select_Appt_Single_All_W", parameters);
            return DataHelper.ConvertToJSON(ds);
        }
        /// <summary>
        /// 获取交通方式
        /// </summary>
        /// <returns></returns>
        public string GetTransportation_Json()
        {
            string sql = "SELECT * FROM CT_Transportation;";
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 添加APP
        /// </summary>
        /// <param name="App"></param>
        /// <returns></returns>
        public CT_Appt_Service InsertApp(CT_Appt_Service App)
        {
            #region 参数
            SqlParameter[] parameters = {
			            new SqlParameter("@AP_AU_CODE", SqlDbType.Int) ,            
                        new SqlParameter("@AP_CI_Code", SqlDbType.Int) ,            
                        new SqlParameter("@AP_AD_Code", SqlDbType.Int) ,            
                        new SqlParameter("@AP_Time", SqlDbType.SmallDateTime) ,            
                        new SqlParameter("@AP_SA_Selected", SqlDbType.Int) ,            
                        new SqlParameter("@AP_ST_Code", SqlDbType.Int) ,            
                        new SqlParameter("@AP_MP_Code", SqlDbType.Int) ,            
                        new SqlParameter("@AP_Transportation", SqlDbType.Int) , 
                                        };
            parameters[0].Value = App.AP_AU_Code;
            parameters[1].Value = App.AP_CI_Code;
            parameters[2].Value = App.AP_AD_Code;
            parameters[3].Value = App.AP_Time;
            parameters[4].Value = App.AP_SA_Selected;
            parameters[5].Value = App.AP_ST_Code;
            parameters[6].Value = App.AP_MP_Code;
            parameters[7].Value = App.AP_Transportation;
            #endregion
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "02_Modify_Appt_All_W", parameters);
            return App;
        }
        /// <summary>
        /// 新增或修改预约
        /// </summary>
        /// <param name="App"></param>
        /// <returns></returns>
        public int Modify_Appt(CT_Appt_Service App)
        {
            #region 参数
            SqlParameter[] parameters = {
			            new SqlParameter("@AP_Code", SqlDbType.Int) ,            
			            new SqlParameter("@AP_AU_Code", SqlDbType.BigInt) ,            
                        new SqlParameter("@AP_CI_Code", SqlDbType.Int) ,            
                        new SqlParameter("@AP_AD_Code", SqlDbType.Int) ,            
                        new SqlParameter("@AP_Time", SqlDbType.SmallDateTime) ,            
                        new SqlParameter("@AP_SA_Selected", SqlDbType.Int) ,            
                        new SqlParameter("@AP_SC_Code", SqlDbType.Int) ,            
                        new SqlParameter("@AP_ST_Code", SqlDbType.Int) ,            
                        new SqlParameter("@AP_MP_Code", SqlDbType.Int) ,            
                        new SqlParameter("@AP_PAM_Code", SqlDbType.Int) , 
                        new SqlParameter("@AP_Notification", SqlDbType.Int) , 
                        new SqlParameter("@AP_Transportation", SqlDbType.Int) , 
                        new SqlParameter("@AP_Notes", SqlDbType.NVarChar,100) ,
                                        };
            parameters[0].Value = App.AP_Code;
            parameters[1].Value = App.AP_AU_Code;
            parameters[2].Value = App.AP_CI_Code;
            parameters[3].Value = App.AP_AD_Code;
            parameters[4].Value = App.AP_Time;
            parameters[5].Value = App.AP_SA_Selected;
            parameters[6].Value = App.AP_SC_Code;
            parameters[7].Value = App.AP_ST_Code;
            parameters[8].Value = App.AP_MP_Code;
            parameters[9].Value = App.AP_PAM_Code;
            parameters[10].Value = App.AP_Notification;
            parameters[11].Value = App.AP_Transportation;
            parameters[12].Value = App.AP_Notes;
            #endregion
            return SqlHelper.ExecuteNonQuery( CommandType.StoredProcedure, "02_Modify_Appt_All_W", parameters);
        }
        /// <summary>
        /// 获取APP列表
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public MD_Appointmens getAppointmensI(long UserCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 AP.AP_Code,CI.CI_Code,CS.CS_Style_EN,AD.AD_Name_EN,SA.DE_ID,AP.AP_Time from CT_Appt_Service AP inner join CT_Car_Inventory CI on AP.AP_CI_Code=CI.CI_Code inner join CT_Auto_Dealers AD on AP.AP_AU_Code=AD.AD_Code inner join CT_Dealer_Empl SA on AP.AP_SA_Selected=SA.DE_Code inner join CT_All_Users AU on AP.AP_AU_Code=AU.AU_Code inner join CT_Car_Style CS on CI.CI_CS_Code=CS.CS_Code where AP_Time>=getDate() and  AP.AP_AU_Code=@AP_AU_Code order by AP_Time");
            SqlParameter[] parameters = { new SqlParameter("@AP_AU_Code", SqlDbType.BigInt) };
            parameters[0].Value = UserCode;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            { return null; }
            CRMTree.Model.Appointmens.MD_Appointmens AppointmensI = new Model.Appointmens.MD_Appointmens();
            AppointmensI.AP_Code = int.Parse(ds.Tables[0].Rows[0]["AP_Code"].ToString());
            AppointmensI.CI_Code = int.Parse(ds.Tables[0].Rows[0]["CI_Code"].ToString());
            AppointmensI.CS_Style_EN = ds.Tables[0].Rows[0]["CS_Style_EN"].ToString();
            AppointmensI.AD_Name_EN = ds.Tables[0].Rows[0]["AD_Name_EN"].ToString();
            AppointmensI.DE_ID = ds.Tables[0].Rows[0]["DE_ID"].ToString();
            AppointmensI.AP_Time = Convert.ToDateTime(ds.Tables[0].Rows[0]["AP_Time"].ToString());

            return AppointmensI;
        }
        /// <summary>
        /// 删除预约
        /// </summary>
        /// <param name="AP_Code"></param>
        /// <returns></returns>
        public int DeleteAppointmens(int AP_Code)
        {
            string strSql = "Delete from CT_Appt_Service where AP_Code=" + AP_Code + "";
            int i = SqlHelper.ExecuteNonQuery(strSql);
            return i;
        }
        #endregion

        #region Car
        /// <summary>
        /// 获取我的MyCar
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public MD_AppointmensList getMyCarStyle(int UserCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CI.CI_Code,CS.CS_Style_EN from CT_Car_Inventory CI inner join CT_Car_Style CS on CI.CI_Code=CS.CS_Code inner join CT_All_Users AU on CI.CI_AU_Code=AU.AU_Code where AU.AU_Code=@AU_Code");
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.Int) };
            parameters[0].Value = UserCode;
            DataSet ds = SqlHelper.ExecuteDataset( CommandType.Text, strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            { return null; }
            CRMTree.Model.Appointmens.MD_AppointmensList myAppointmensList = new Model.Appointmens.MD_AppointmensList();
            myAppointmensList.Appointmens_List = new List<Model.Appointmens.MD_Appointmens>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                myAppointmensList.Appointmens_List.Add(new CRMTree.Model.Appointmens.MD_Appointmens
                    {
                        CI_Code = int.Parse(ds.Tables[0].Rows[i]["CI_Code"].ToString()),
                        CS_Style_EN = ds.Tables[0].Rows[i]["CS_Style_EN"].ToString()
                    });
            }
            return myAppointmensList;
        }
        #endregion

        #region 经销商 顾问

        /// <summary>
        /// 获取经销商列表
        /// </summary>
        /// <returns></returns>
        public MD_DealerList getDealerList(long AU_Code)
        {
            #region sql
            string strSql = @"select top 3 * from (Select AD_Code,
		                                AD_Name_EN,
		                                AD_Name_CN,
		                                AD_logo_file_S,
		                                AD_logo_file_M,
		                                SD.SD_SA_Selection, 
		                                SD.SD_Serv_Package 
                                From CT_Car_Inventory CI 
	                                inner join CT_Car_Style CS On CI.CI_CS_Code=CS.CS_Code 
	                                inner join CT_Car_Model CM on CS.CS_CM_Code=CM.CM_Code 
	                                inner join CT_Make MK on MK.MK_Code=CM.CM_MK_Code 
	                                inner join CT_Auto_Dealers AD on AD.AD_AM_Code=MK.MK_AM_Code
	                                inner join CT_Service_Dep SD on AD.AD_Code=SD.SD_AD_Code 
	                                inner join CT_Dept_Names DN on SD.SD_PDN_Code=DN.PDN_Code
	                                inner join CT_Dept_Type DT on DT.PDT_Code=DN.PDN_PDT_Code
                                Where  CI.CI_AU_Code=@AU_Code and DT.PDT_Category = 1 and DN.PDN_Code=2
                                union
                                select AD_Code,
		                                AD_Name_EN,
		                                AD_Name_CN, 
		                                AD_logo_file_S,
		                                AD_logo_file_M,
		                                SD.SD_SA_Selection,
		                                SD.SD_Serv_Package 
                                from CT_Auto_Dealers AD 
	                                inner join CT_Service_Dep SD on AD.AD_Code=SD.SD_AD_Code 
	                                inner join CT_Dept_Names DN on SD.SD_PDN_Code=DN.PDN_Code
	                                inner join CT_Dept_Type DT on DT.PDT_Code=DN.PDN_PDT_Code
                                where DT.PDT_Category = 1 and DN.PDN_Code=2) a";
            #endregion
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.BigInt) };
            parameters[0].Value = AU_Code;
            DataSet ds = SqlHelper.ExecuteDataset( CommandType.Text, strSql, parameters);
            MD_DealerList o = new MD_DealerList();
            o.Dealers_List = DataHelper.ConvertToList<CT_Auto_Dealers>(ds);
            return o;
        }

        /// <summary>
        /// 获取默认的经销商
        /// </summary>
        /// <param name="au_code"></param>
        /// <param name="ci_code"></param>
        /// <returns></returns>
        public string GetDefaultDealer(long au_code, int ci_code)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM(
                    SELECT 1 AS B, DL.AL_District,AD.* FROM CT_Auto_Dealers AD 
                                                 LEFT JOIN CT_Address_List DL ON AD.AD_Code=DL.AL_AU_AD_Code AND DL.AL_UType=2 
							                     INNER JOIN CT_History_Service HS ON HS_AD_Code = AD_Code
                                                  WHERE AD_Active_Tag=1 AND HS_AU_Code ={0} and HS_CI_Code ={1}
                    UNION 
                    SELECT 2 AS B, DL.AL_District,AD.* FROM CT_Auto_Dealers AD 
                                                 LEFT JOIN CT_Address_List DL ON AD.AD_Code=DL.AL_AU_AD_Code AND DL.AL_UType=2 
							                     LEFT JOIN CT_DEALER_EMPL DE ON DE.DE_AD_OM_CODE = AD.AD_CODE
							                     INNER JOIN CT_Fav_Empl FE ON FE.FE_DE_CODE = DE.DE_CODE
                                                  WHERE AD_Active_Tag=1 AND FE_AU_CODE ={0} AND FE_CI_CODE = {1}
                    UNION 
                    SELECT 3 AS B, DL.AL_District,AD.* FROM CT_Auto_Dealers AD 
                                                 LEFT JOIN CT_Address_List DL ON AD.AD_Code=DL.AL_AU_AD_Code AND DL.AL_UType=2 
                                                  WHERE AD_Active_Tag=1) A ORDER BY B", au_code, ci_code);
            
            return DataHelper.ConvertToJSON(sql);
        }

        /// <summary>
        /// 获取分页的经销商信息
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public string GetDealerList_Json(long AU_Code, int page)
        {
            #region sql
            string z_sql = string.Format(@"SELECT DL.AL_District,AD.* FROM CT_Auto_Dealers AD 
                             LEFT JOIN CT_Address_List DL ON AD.AD_Code=DL.AL_AU_AD_Code AND DL.AL_UType=2 
                              WHERE AD_Active_Tag=1");
            string z_exec = string.Format(@"DECLARE @SumPage INT ,@Sum INT 
                              EXEC [dbo].[public_pager_01] '{0}','AD_Code','AD_Update_dt','DESC',{1},10,@SumPage OUTPUT,@Sum OUTPUT SELECT @SumPage AS SumPage,@Sum AS SumRow;", z_sql, page);
            #endregion
            
            return DataHelper.ConvertToJSONPage(z_exec);
        }

        /// <summary>
        /// 获取顾问列表,根据经销商ID
        /// </summary>
        /// <param name="AD_Code"></param>
        /// <returns></returns>
        public MD_AdviserList getAdviserList(int AD_Code)
        {
            string strSql = string.Format(@"select top 3 DE_Code,DE_Picture_FN,AU_Name from CT_Dealer_Empl DE 
                            inner join CT_All_Users AU on DE.DE_AU_Code=AU.AU_Code
                            where DE_type=2 and DE_AD_OM_Code={0}",AD_Code);
            MD_AdviserList o = new MD_AdviserList();
            o.Adviser_List = DataHelper.ConvertToList<CT_Dealer_Empl>(strSql);
            return o;
        }

        /// <summary>
        /// 获取对应的默认顾问
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <param name="CI_Code"></param>
        /// <param name="AD_Code"></param>
        /// <returns></returns>
        public string GetDefaultAdviser(long AU_Code, int CI_Code, int AD_Code)
        {
            string sql = string.Format(@"SELECT TOP 1 * FROM (
SELECT 1 AS B, AU_Name,DE.* FROM CT_Dealer_Empl DE 
                    INNER JOIN CT_All_Users AU ON DE.DE_AU_Code=AU.AU_Code
					INNER JOIN CT_Fav_Empl FE ON FE.FE_DE_Code=DE.DE_Code
                    WHERE DE_UType=1 AND DE_Activate_dt IS NOT NULL AND DE_DActivate_dt IS NULL AND DE_AD_OM_Code={2}
					      AND FE_AU_CODE ={0} AND FE_CI_CODE = {1} 
UNION
SELECT 2 AS B, AU_Name,DE.* FROM CT_Dealer_Empl DE 
                    INNER JOIN CT_All_Users AU ON DE.DE_AU_Code=AU.AU_Code
					INNER JOIN CT_History_Service HS ON HS.HS_Advisor = DE.DE_Code
                    WHERE DE_UType=1 AND DE_Activate_dt IS NOT NULL AND DE_DActivate_dt IS NULL AND DE_AD_OM_Code={2}
					      AND HS.HS_AU_Code ={0} AND HS.HS_CI_Code = {1}
UNION
SELECT 3 AS B, AU_Name,DE.* FROM CT_Dealer_Empl DE 
                    INNER JOIN CT_All_Users AU ON DE.DE_AU_Code=AU.AU_Code
                    WHERE DE_UType=1 AND DE_Activate_dt IS NOT NULL AND DE_DActivate_dt IS NULL AND DE.DE_AD_OM_Code={2}) A
					ORDER BY B", AU_Code, CI_Code, AD_Code);
            
            return DataHelper.ConvertToJSON(sql);
        }
        
        /// <summary>
        /// 获取分页的顾问列表
        /// </summary>
        /// <param name="AD_Code"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public string GetAdviserList_Json(int AD_Code, int page)
        {
            string z_sql = string.Format(@"SELECT UG.UG_Code,UG.UG_Name_EN,UG.UG_Name_CN,AU_Name,PL.PL_Number,AL.AL_District,DE.* ,DP.*
                    FROM CT_Dealer_Empl DE 
                    INNER JOIN CT_All_Users AU ON DE.DE_AU_Code=AU.AU_Code
					INNER JOIN CT_User_Groups UG ON AU.AU_UG_Code=UG.UG_Code AND UG.UG_Code IN(28,26)
					LEFT JOIN CT_Phone_List PL ON PL.PL_AU_AD_Code=AU.AU_Code AND PL.PL_UType=1 AND PL.PL_Pref=1
					LEFT JOIN CT_Address_List AL ON AL.AL_AU_AD_Code=AU.AU_Code AND AL.AL_UType=1 AND AL.AL_Pref=1
					LEFT JOIN CT_Daily_PLanner DP ON DP.DP_AU_AD_Code = AU.AU_Code AND DP.DP_UType = 1
                    WHERE  DE_UType=1 AND DE_Activate_dt IS NOT NULL AND DE_DActivate_dt IS NULL AND DE_AD_OM_Code={0}", AD_Code);

            string z_exec = string.Format(@"DECLARE @SumPage INT ,@Sum INT 
                              EXEC [dbo].[public_pager_01] '{0}','DE_Code','DE_Activate_dt','DESC',{1},10,@SumPage OUTPUT,@Sum OUTPUT 
                               SELECT @SumPage AS SumPage,@Sum AS SumRow;", z_sql, page);
            
            return DataHelper.ConvertToJSONPage(z_exec);
        }

        /// <summary>
        /// 获取顾问的信息
        /// </summary>
        /// <param name="DE_Code"></param>
        /// <returns></returns>
        public CT_Dealer_Empl GetAdviser(string DE_Code)
        {
            string z_sql = string.Format(@"SELECT AU_Name,DE.* FROM CT_Dealer_Empl DE 
                    INNER JOIN CT_All_Users AU ON DE.DE_AU_Code=AU.AU_Code
                    WHERE DE_UType=1 AND DE_Activate_dt IS NOT NULL AND DE_DActivate_dt IS NULL AND DE_Code={0};", DE_Code);
            return DataHelper.ConvertToObject<CT_Dealer_Empl>(z_sql);
        }

        public string checkAdviserTime(int ad_code, int de_code, string ap_date, float ap_time, bool Interna)
        {
            string sql = string.Format(@"EXEC P_Appt_GetAdvisorTime {0},{1},'{2}',{3},{4}", ad_code, de_code, ap_date, ap_time, Interna);
            
            return DataHelper.ConvertToJSON(sql);
        }
        public string GetWordText(int pid, int id)
        {
            string z_sql = @"SELECT text_EN as text
                FROM words  Where p_id =" + pid + " and value=" + id;
            DataSet ds = SqlHelper.ExecuteDataset(z_sql);
            return ds.Tables[0].Rows[0]["text"].ToString();
 
        }
        
        #endregion

        #region 服务
        /// <summary>
        /// 获取预约服务类型
        /// </summary>
        /// <returns></returns>
        public MD_ServCategoryList getServList()
        {
            string strSql = "select * from CT_Serv_Category";
            
            MD_ServCategoryList o = new MD_ServCategoryList();
            o.Serv_Category_List = DataHelper.ConvertToList<CT_Serv_Category>(strSql);
            return o;
        }
        public string GetServCategory(int ad_code)
        {
            string sql = string.Format(@"SELECT * FROM CT_Serv_Category where SC_AD_Code = {0}", ad_code);
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 获取服务预约类别信息，当前的经销商
        /// </summary>
        /// <param name="ServiceType"></param>
        /// <returns></returns>
        public MD_ServiceTypesList getServiceTypeList(CT_Service_Types ServiceType)
        {
            string strSql = "select * from CT_Service_Types where ST_SC_Code=" + ServiceType.ST_SC_Code + " and ST_AD_Code=" + ServiceType.ST_AD_Code + ";";
            
            MD_ServiceTypesList o = new MD_ServiceTypesList();
            o.Service_Types_List = DataHelper.ConvertToList<CT_Service_Types>(strSql);

            return o;
        }
        public string GetServTypes_Json(int AD_Code, int SC_Code)
        {
            string sql = string.Format(@"SELECT * FROM CT_Service_Types WHERE ST_AD_Code={0} AND ST_SC_Code={1};",AD_Code,SC_Code);
            
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 获取服务预约保养套餐
        /// </summary>
        /// <param name="ServiceType"></param>
        /// <returns></returns>
        public MD_MaintenancePackList getMaintenancePackList(CT_Maintenance_Pack MaintenancePack)
        {
            string strSql = "select * from CT_Maintenance_Pack where MP_AD_Code=" + MaintenancePack.MP_AD_Code + "";
            
            MD_MaintenancePackList o = new MD_MaintenancePackList();
            o.Maintenance_Pack_List = DataHelper.ConvertToList<CT_Maintenance_Pack>(strSql);

            return o;
        }
        public string GetMaintenance_Json(int AD_Code)
        {
            string sql = string.Format(@"SELECT * FROM CT_Maintenance_Pack WHERE MP_AD_Code={0};",AD_Code);
            
            return DataHelper.ConvertToJSON(sql);
        }
        public TranList GetTransportation()
        {
            string sql = "select * from CT_Transportation;";
            
            TranList o = new TranList();
            o.Tran = DataHelper.ConvertToList<CT_Transportation>(sql);
            return o;
        }
        #endregion
    }
}
