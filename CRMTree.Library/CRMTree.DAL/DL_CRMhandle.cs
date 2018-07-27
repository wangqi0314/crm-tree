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
    public class DL_CRMhandle
    {
        /// <summary>
        /// 获取某类型列表
        /// </summary>
        /// <param name="Interna"></param>
        /// <param name="p_id"></param>
        /// <returns></returns>
        public string GetWords(bool Interna, int p_id)
        {
            var sql_text_part = Interna ? "[text_en]" : "[text_cn]";
            var sql = string.Format(@"SELECT p_id ,{0} as [text],[value],[isSelect] AS selected,sort FROM [words]
                                      WHERE p_id ={1} ORDER BY p_id,sort", sql_text_part, p_id);
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 获取单用户所有的汽车
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <param name="isEn"></param>
        /// <returns></returns>
        public string Get_userAllcarName(long AU_Code, bool? isEn)
        {
            string _param = "dbo.Get_Connect_Car(CI.CI_Code,2)AS CAR_info";
            if (isEn == null || isEn == false)
            {
                _param = "dbo.Get_Connect_Car(CI.CI_Code,1)AS CAR_info";
            }
            string sql = string.Format(@"SELECT CI.CI_Code,{0}
                            FROM CT_Car_Inventory CI
                            WHERE CI_AU_Code={1}", _param, AU_Code);
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        ///  检测VIN 获取两个Table 1 =VIN 检测， 2= VIN对应的AU_code
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
        public DataSet check_get_VIN(string vin)
        {
            SqlParameter[] parameters = { new SqlParameter("@VINS", SqlDbType.NVarChar) };
            parameters[0].Value = vin;
            DataSet ds = SqlHelper.ExecuteDataset(  CommandType.StoredProcedure, "[dbo].[03_CHECK_VINS]", parameters);
            return ds;
        }
        /// <summary>
        /// 获取菜单名
        /// </summary>
        /// <param name="MI_Code"></param>
        /// <param name="FL_Code"></param>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public string GetMIName(int MI_Code, int FL_Code, bool Interna)
        {
            string text = Interna ? "MF_Title_EN" : "MF_Title_CN";
            string sql = string.Format(@"select {0} as Name from CT_Menu_Frames where mf_mi_code=" + MI_Code + " and MF_FL_FB_Code=" + FL_Code + " and MF_RP_Type in (11,12)", text);
            SqlDataReader _d = SqlHelper.ExecuteReader(sql);
            string _n = string.Empty;
            while (_d.Read())
            {
                _n = _d["Name"].ToString();
                break;
            }
            _d.Close();
            _d.Dispose();
            return _n;
        }

        #region KPI
        /// <summary>
        /// 获取部门KPI的设置组
        /// </summary>
        /// <param name="ad_code"></param>
        /// <returns></returns>
        public string getDepartmentKPIGroup(int ad_code)
        {
            string sql = string.Format(@"SELECT KPV.KPV_PDN_Code PDN_Code,KPV.KPV_KPT_Code KPT_Code
                     FROM CT_KPI_Values KPV
                     WHERE KPV.KPV_UType=2 AND KPV.KPV_DE_AD_Code={0}
                     GROUP BY KPV.KPV_PDN_Code,KPV.KPV_KPT_Code
                     ORDER BY PDN_Code", ad_code);
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 获取相关部门设置的KPI
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public string getDepartmentKPI(int ad_code, bool Interna)
        {
            string _d = Interna ? "PDN_Name_EN" : "PDN_Name_CN";
            string _k = Interna ? "KPT_Desc_EN" : "KPT_Desc_CN";
            string sql = string.Format(@"SELECT DN.PDN_Code,DN.{0} AS PDN_Name,KPT.KPT_Code,KPT.{1} AS KPT_Desc,KPV.KPV_Code,KPV.KPV_Cat,KPV.KPV_Value FROM CT_KPI_Values KPV
            INNER JOIN CT_DEPT_NAMES DN ON DN.PDN_Code=KPV.KPV_PDN_Code
            INNER JOIN CT_KPI_TYPES KPT ON KPT.KPT_Code=KPV.KPV_KPT_Code
             WHERE KPV.KPV_UType=2 AND KPV.KPV_DE_AD_Code={2} ORDER BY DN.PDN_Code", _d, _k, ad_code);
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public string getDepartment(int ad_code, bool Interna)
        {
            string _d = Interna ? "PDN_Name_EN" : "PDN_Name_CN";
            string sql = string.Format(@"SELECT DN.PDN_Code,DN.{0} AS PDN_Name FROM CT_DEPT_NAMES DN WHERE PDN_AD_CODE={1};", _d, ad_code);
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 获取KPI
        /// </summary>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public string getKPI(bool Interna)
        {
            string _k = Interna ? "KPT_Desc_EN" : "KPT_Desc_CN";
            string sql = string.Format(@"SELECT KPT_Code,{0} AS KPT_Desc FROM CT_KPI_TYPES;", _k);
            return DataHelper.ConvertToJSON(sql);
        }

        #region KPI员工 相关
        /// <summary>
        /// 用户获取部门下员工KPI 列表分组标示（用户帮助前端UI好分组数据结构）
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="pn_code"></param>
        /// <param name="pt_code"></param>
        /// <returns></returns>
        public string getDepartmentKPIUserGroup(int ad_code, int pn_code, int pt_code)
        {
            string sql = string.Format(@"SELECT KPV_DE_AD_Code DE_Code FROM CT_KPI_Values KPV
                INNER JOIN CT_DEPT_NAMES DN ON DN.PDN_Code=KPV.KPV_PDN_Code
                INNER JOIN CT_KPI_TYPES KPT ON KPT.KPT_Code=KPV.KPV_KPT_Code
                INNER JOIN CT_Dealer_Empl DE ON DE.DE_Code=KPV.KPV_DE_AD_Code AND KPV.KPV_UType=1
                 WHERE DE.DE_AD_OM_Code={0} AND DN.PDN_CODE ={1} AND KPT.KPT_Code={2}
                 GROUP BY KPV_DE_AD_Code
                 ORDER BY KPV.KPV_DE_AD_Code", ad_code,pn_code,pt_code);
            return DataHelper.ConvertToJSON(sql);
        }
        
        /// <summary>
        /// 根据部门和KPI 获取员工的KPI信息
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="pn_code"></param>
        /// <param name="pt_code"></param>
        /// <returns></returns>
        public string getDepartmentKPIUser(int ad_code, int pn_code, int pt_code)
        {
            string sql = string.Format(@"SELECT AU.AU_Code,AU.AU_Name,KPV.KPV_DE_AD_Code,KPV.KPV_Code,KPV.KPV_Cat,KPV.KPV_Value FROM CT_KPI_Values KPV
            INNER JOIN CT_DEPT_NAMES DN ON DN.PDN_Code=KPV.KPV_PDN_Code
            INNER JOIN CT_KPI_TYPES KPT ON KPT.KPT_Code=KPV.KPV_KPT_Code
            INNER JOIN CT_Dealer_Empl DE ON DE.DE_Code=KPV.KPV_DE_AD_Code AND KPV.KPV_UType=1
            INNER JOIN CT_All_Users AU ON AU.AU_CODE=DE.DE_AU_Code  
             WHERE DE.DE_AD_OM_Code={0} AND DN.PDN_CODE ={1} AND KPT.KPT_Code={2}
             ORDER BY KPV.KPV_DE_AD_Code,KPV.KPV_Cat", ad_code, pn_code, pt_code);
            return DataHelper.ConvertToJSON(sql);
        }

        /// <summary>
        /// 获取部门员工
        /// </summary>
        /// <param name="ad_code"></param>
        /// <param name="pn_code"></param>
        /// <returns></returns>
        public string getDepartmentUser(int ad_code, int pn_code)
        {
            string sql = string.Format(@" SELECT AU.AU_Code,AU.AU_Name,DE.DE_Code,DE.DE_PDN_Code FROM CT_Dealer_Empl DE 
                 INNER JOIN CT_All_Users AU ON AU.AU_CODE=DE.DE_AU_Code
                 WHERE DE.DE_AD_OM_Code={0} AND DE.DE_PDN_Code={1}", ad_code, pn_code);
            return DataHelper.ConvertToJSON(sql);
        }
        #endregion
        #endregion
    }
}
