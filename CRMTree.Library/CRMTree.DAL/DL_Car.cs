using CRMTree.Model;
using CRMTree.Model.MyCar;
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
    public class DL_Car
    {
        #region CarList
        public MD_CarList GetCarList(long UserCode)
        {
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.Int) };
            parameters[0].Value = UserCode;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "Car_GetMyCar", parameters);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            MD_CarList o = new MD_CarList();
            o.Car_Inventory_List = DataHelper.ConvertToList<CT_Car_Inventory>(ds);
            if (ds.Tables[1].Rows.Count > 0)
            {
                MD_CarList _o = new MD_CarList();
                _o.Car_Inventory_List = DataHelper.ConvertToList<CT_Car_Inventory>(ds, 1);
                for (int m = 0; m < o.Car_Inventory_List.Count; m++)
                {
                    for (int n = 0; n < _o.Car_Inventory_List.Count; n++)
                    {
                        if (_o.Car_Inventory_List[n].CI_Code == o.Car_Inventory_List[m].CI_Code)
                        {
                            o.Car_Inventory_List[m].RS_Desc_EN = _o.Car_Inventory_List[n].RS_Desc_EN;
                            o.Car_Inventory_List[m].RS_Desc_CN = _o.Car_Inventory_List[n].RS_Desc_CN;
                            break;
                        }
                    }
                }
            }
            return o;
        }
        public MD_CarList GetCarList(long UserCode, bool v)
        {
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.Int) };
            parameters[0].Value = UserCode;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "Car_GetMyCar", parameters);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            MD_CarList o = new MD_CarList();
            o.Car_Inventory_List = DataHelper.ConvertToList<CT_Car_Inventory>(ds);
            return o;
        }
        #endregion
        #region CarTypeList
        public MD_CarTypeList GetCarTypeList()
        {
            string strSql = @"select CT_Code,CT_Type_EN,CT_Type_CN from CT_Car_Type";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            { return null; }

            Model.MyCar.MD_CarTypeList CarTypeLis = new MD_CarTypeList();
            CarTypeLis.Car_Type_List = new List<CRMTree.Model.CT_Car_Type>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                CarTypeLis.Car_Type_List.Add(new CRMTree.Model.CT_Car_Type()
                {
                    CT_Code = int.Parse(ds.Tables[0].Rows[i]["CT_Code"].ToString()),
                    CT_Type_EN = ds.Tables[0].Rows[i]["CT_Type_EN"].ToString(),
                    CT_Type_CN = ds.Tables[0].Rows[i]["CT_Type_CN"].ToString()
                });
            }
            return CarTypeLis;
        }
        #endregion
        #region Make
        /// <summary>
        /// Make表
        /// </summary>
        /// <returns></returns>
        public string GetMakeList()
        {
            string sql = string.Format(@"SELECT * FROM CT_Make ORDER BY MK_Make_CN;");
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// Param_List 特定Sql i=1 type=11；i=2 type=12,MK_Code 代表CS_Code；
        /// i=0;根据MK_Code得到Make；
        /// i=1;根据CM_Code反推得到Make；
        /// i=2;根据SC_Code反推得到Make；
        /// </summary>
        /// <param name="i"></param>
        /// <param name="CM_Code"></param>
        /// <returns></returns>
        public CT_Make GetMake(int i, int MK_Code)
        {
            string strSql = string.Empty;
            if (i == 0)
            {
                strSql = string.Format(@"select MK_Code,MK_Make_EN,MK_Make_CN from CT_Make where MK_Code={0};", MK_Code);
            }
            else if (i == 1)
            {
                strSql = string.Format(@"select MK_Code,MK_Make_EN,MK_Make_CN from CT_Make inner join CT_Car_Model on MK_Code=CM_MK_Code where CM_Code={0};", MK_Code);
            }
            else if (i == 2)
            {
                strSql = string.Format(@"select MK_Code,MK_Make_EN,MK_Make_CN 
                            from CT_Make inner join CT_Car_Model on MK_Code=CM_MK_Code 
                            inner join CT_Car_Style on CM_Code=CS_CM_Code where CS_Code={0};", MK_Code);
            }

            CT_Make Make = DataHelper.ConvertToObject<CT_Make>(strSql);
            return Make;
        }
        public CT_Car_Style GetMake_Model_Style(int i, int Code)
        {
            string sqlStr = string.Empty;
            if (i == 1)
            {
                sqlStr = string.Format(@"select MK_Code,MK_Make_EN,MK_Make_CN from CT_Make where MK_Code={0};", Code);
            }
            else if (i == 2)
            {
                sqlStr = string.Format(@"select MK_Code,MK_Make_EN,MK_Make_CN,CM_Code,CM_Model_EN,CM_Model_CN from CT_Make inner join CT_Car_Model on MK_Code=CM_MK_Code where CM_Code={0};", Code);
            }
            else if (i == 3)
            {
                sqlStr = string.Format(@"select MK_Code,MK_Make_EN,MK_Make_CN,CM_Code,CM_Model_EN,CM_Model_CN,CS_Code,CS_Style_EN,CS_Style_CN from CT_Make inner join CT_Car_Model on MK_Code=CM_MK_Code inner join CT_Car_Style on CM_Code=CS_CM_Code where CS_Code={0};", Code);
            }
            CT_Car_Style Make = DataHelper.ConvertToObject<CT_Car_Style>(sqlStr);
            return Make;
        }
        #endregion
        #region Model
        /// <summary>
        /// Model下拉表
        /// </summary>
        /// <returns></returns>
        public string GetModeList(int MK_code)
        {
            string sql = string.Format(@"SELECT * FROM CT_Car_Model WHERE CM_MK_Code={0} ORDER BY CM_Model_CN;", MK_code);
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// i=0是表示，根据CM_Code 获取Model；i=1是表示，根据CS_Code反推Model
        /// </summary>
        /// <param name="i"></param>
        /// <param name="CM_Code"></param>
        /// <returns></returns>
        public CT_Car_Model GetModel(int i, int CM_Code)
        {
            string strSql = string.Empty;
            if (i == 0)
            {
                strSql = @"select CM_Code,CM_Model_EN,CM_Model_CN from CT_Car_Model where CM_Code=@CM_Code;";
            }
            else if (i == 1)
            {
                strSql = @"select CM_Code,CM_Model_EN,CM_Model_CN
                           from CT_Car_Model inner join CT_Car_Style on CM_Code=CS_CM_Code where CS_Code=@CM_Code;";
            }
            SqlParameter[] parameters = { new SqlParameter("@CM_Code", SqlDbType.Int) };
            parameters[0].Value = CM_Code;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql, parameters);


            CT_Car_Model Model = DataHelper.ConvertToObject<CT_Car_Model>(ds);
            return Model;
        }
        #endregion
        #region Style
        /// <summary>
        /// Style查询
        /// </summary>
        /// <param name="MK_code"></param>
        /// <returns></returns>
        public string GetStyleList(int CM_Code)
        {
            string sql = string.Format(@"SELECT * FROM CT_Car_Style WHERE CS_CM_Code={0} ORDER BY CS_Style_CN;", CM_Code);
            return DataHelper.ConvertToJSON(sql);
        }
        public CT_Car_Style GetStyle(int CS_Code)
        {
            string strSql = "SELECT * FROM CT_Car_Style WHERE CS_Code=" + CS_Code + " ORDER BY CS_Style_CN;";

            CT_Car_Style style = DataHelper.ConvertToObject<CT_Car_Style>(strSql);
            return style;
        }
        #endregion
        #region Years
        /// <summary>
        /// 汽车年龄
        /// </summary>
        /// <returns></returns>
        public MD_CarYears GetCarYearsList()
        {
            string strSql = "SELECT * FROM CT_Years ORDER BY YR_Code DESC;";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql);

            MD_CarYears _o = new MD_CarYears();
            _o.Car_Years = DataHelper.ConvertToList<CT_Years>(ds);
            return _o;
        }
        #endregion
        #region Colors
        /// <summary>
        /// 汽车颜色
        /// </summary>
        /// <returns></returns>
        public MD_CarColorsList GetColorsList()
        {
            string sql = "select * from CT_Color_List";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

            MD_CarColorsList _o = new MD_CarColorsList();
            _o.Car_Color = DataHelper.ConvertToList<CT_Color_List>(ds);
            return _o;
        }
        public MD_CarColorsList GetColorsListI()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CL_Code,CL_Color_EN,CL_Color_CN from CT_Color_List");
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString());
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            { return null; }

            Model.MyCar.MD_CarColorsList CarColorsList = new MD_CarColorsList();
            CarColorsList.Car_Color_I = new List<CRMTree.Model.CT_Color_List>();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                CarColorsList.Car_Color_I.Add(new CRMTree.Model.CT_Color_List()
                {
                    CL_Code = int.Parse(ds.Tables[0].Rows[i]["CL_Code"].ToString()),
                    CL_Color_EN = ds.Tables[0].Rows[i]["CL_Color_EN"].ToString(),
                    CL_Color_CN = ds.Tables[0].Rows[i]["CL_Color_CN"].ToString()
                });
            }
            return CarColorsList;
        }
        #endregion

        #region 新处理newCar
        /// <summary>
        /// 获取绑定顾问的个人车辆信息 IsBind=1 表示绑定了 IsBind=0 表示没有绑定
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <returns></returns>
        public string GetBindCarList_Json(long AU_Code)
        {
            string sql = string.Format(@"SELECT dbo.Get_Connect_Car(CI.CI_Code,1)AS CAR_CN,(CASE WHEN FE.FE_CI_Code IS NULL THEN 0 ELSE 1 END)IsBind,
                            AU.AU_Name,DE.DE_Code,AU.AU_UG_Code,UG.UG_Name_CN,DE.DE_AD_OM_Code,AD.AD_Name_CN,CI.* 
                            FROM CT_Car_Inventory CI 
                            LEFT JOIN CT_Fav_Empl FE ON CI.CI_Code=FE.FE_CI_Code
                            LEFT JOIN CT_DEALER_EMPL DE ON FE.FE_DE_CODE=DE.DE_Code
                            LEFT JOIN CT_All_Users AU ON AU.AU_CODE=DE.DE_AU_Code
                            LEFT JOIN CT_User_Groups UG ON AU.AU_UG_Code=UG.UG_CODE
                            LEFT JOIN CT_Auto_Dealers AD ON DE.DE_AD_OM_Code=AD.AD_CODE
                            WHERE CI_AU_Code={0}", AU_Code);
            return DataHelper.ConvertToJSON(sql);
        }


        public string GetCarInfo_Json(int CI_Code)
        {
            string sql = string.Format(@"SELECT
		                    dbo.Get_Connect_Car(CI.CI_Code,1)AS CAR_CN,
		                    dbo.Get_Connect_Car(CI.CI_Code,2)AS CAR_EN,
		                    CI.*
		                    FROM CT_Car_Inventory CI
		                    WHERE CI_Code={0};", CI_Code);
            return DataHelper.ConvertToJSON(sql);
        }

        public string GetCarInfo_List(long au_code)
        {
            string sql = string.Format(@"SELECT
		        dbo.Get_Connect_Car(CI.CI_Code,1)AS CAR_CN,
		        dbo.Get_Connect_Car(CI.CI_Code,2)AS CAR_EN,
		        CI.*
		        FROM CT_Car_Inventory CI
		        WHERE CI_AU_Code={0} ORDER BY CI_Create_dt DESC;", au_code);
            return DataHelper.ConvertToJSON(sql);
        }

        public string GetDafaultCarInfo(long au_code)
        {
            string sql = string.Format(@"SELECT TOP 1 CI_Code, dbo.Get_Connect_Car(CI.CI_Code,1)AS CAR_CN, 
                        dbo.Get_Connect_Car(CI.CI_Code,2)AS CAR_EN
                        FROM CT_Car_Inventory CI
                        INNER JOIN CT_All_Users AU ON AU_Code= CI_AU_Code
                        WHERE AU_Code={0} ORDER BY CI_Update_dt DESC", au_code);
            return DataHelper.ConvertToJSON(sql);
        }

        public int Modify_Car(CT_Car_Inventory _c)
        {
            #region 参数
            SqlParameter[] parameters = { 
                                            new SqlParameter("@CI_AU_Code", SqlDbType.BigInt),
                                            new SqlParameter("@CI_CS_Code", SqlDbType.Int),
                                            new SqlParameter("@CI_VIN", SqlDbType.VarChar,20),
                                            new SqlParameter("@CI_Mileage", SqlDbType.Int),
                                            new SqlParameter("@CI_Licence", SqlDbType.NVarChar,15),
                                            new SqlParameter("@CI_Licence_dt", SqlDbType.SmallDateTime),
                                            new SqlParameter("@CI_YR_Code", SqlDbType.TinyInt),
                                            new SqlParameter("@CI_Color_I", SqlDbType.Int),
                                            new SqlParameter("@CI_Color_E", SqlDbType.Int),
                                            new SqlParameter("@CI_Picture_FN", SqlDbType.NVarChar,100),
                                            new SqlParameter("@CI_Status", SqlDbType.Char,1),
                                            new SqlParameter("@CI_Activate_Tag", SqlDbType.TinyInt),
                                            new SqlParameter("@CI_Frame", SqlDbType.NVarChar,100),
                                            new SqlParameter("@CI_Driving", SqlDbType.NVarChar,100),
                                            new SqlParameter("@CI_Driving_Type", SqlDbType.NVarChar,100),
                                            new SqlParameter("@CI_Driving_dt", SqlDbType.SmallDateTime),
                                            new SqlParameter("@CI_Warr_St_dt", SqlDbType.SmallDateTime),
                                            new SqlParameter("@CI_remarks", SqlDbType.NVarChar,100)
                                        };
            parameters[0].Value = _c.CI_AU_Code;
            parameters[1].Value = _c.CI_CS_Code;
            parameters[2].Value = _c.CI_VIN;
            parameters[3].Value = _c.CI_Mileage;
            parameters[4].Value = _c.CI_Licence;
            parameters[5].Value = _c.CI_Licence_dt;
            parameters[6].Value = _c.CI_YR_Code;
            parameters[7].Value = _c.CI_Color_I;
            parameters[8].Value = _c.CI_Color_E;
            parameters[9].Value = _c.CI_Picture_FN;
            parameters[10].Value = _c.CI_Status;
            parameters[11].Value = _c.CI_Activate_Tag;
            parameters[12].Value = _c.CI_Frame;
            parameters[13].Value = _c.CI_Driving;
            parameters[14].Value = _c.CI_Driving_Type;
            parameters[15].Value = _c.CI_Driving_dt;
            parameters[16].Value = _c.CI_Warr_St_dt;
            parameters[17].Value = _c.CI_remarks;
            #endregion
            return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "02_Modify_Car_W", parameters);
        }

        public int CarBind_Adviser(CT_Car_Inventory o)
        {
            #region 参数
            SqlParameter[] parameters = {            
                        new SqlParameter("@AU_Code", SqlDbType.BigInt),         
                        new SqlParameter("@CI_Code", SqlDbType.Int),         
                        new SqlParameter("@DE_Code", SqlDbType.Int),         
                        new SqlParameter("@IS_Bind", SqlDbType.Bit)        
                                        };
            parameters[0].Value = o.CI_AU_Code;
            parameters[1].Value = o.CI_Code;
            parameters[2].Value = o.DE_Code;
            parameters[3].Value = o.IS_Bind;
            #endregion
            int i = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[dbo].[03_Handle_CarBind_Adviser]", parameters);
            return i;
        }

        #endregion

        #region zxm
        public DataTable GetCarInfoByAU_Code(int DL_AU_Code)
        {
            string sql = @"SELECT CI_Code, HS.HS_AU_Code as AU
	 ,(convert (varchar(100),CS_Year) + ' '+ MK_Make_cn + ' '+ CM_Model_cn +' ' +CS_Style_cn ) as CAR_CN 
     ,(convert (varchar(100),CS_Year) + ' '+ MK_Make_EN + ' '+ CM_Model_EN +' ' +CS_Style_EN ) as CAR_EN 
	 ,CI_Licence as CI_License
	 from (Select hs2.HS_CI_Code, max(hs2.hs_ro_open) as Last_RO 
				from CT_History_Service hs2
			 where HS2.HS_AD_Code=3 and HS2.HS_AU_Code=@DL_AU_Code 
				group by HS_CI_Code) hs3 
 	  Left Join CT_History_Service HS on HS.HS_CI_Code=HS3.HS_CI_Code and HS.HS_RO_Open=HS3.Last_RO
	  Left Join CT_Car_Inventory on CI_Code=HS.HS_CI_Code 
	  Left Join CT_Car_Style on CI_CS_Code=CS_Code
	  Left Join CT_Car_Model on CS_CM_Code=CM_Code
	  Left Join CT_Make on CM_MK_Code=MK_Code
	 where HS.HS_AD_Code =3 and HS.HS_AU_Code=@DL_AU_Code
        and (CT_Car_Inventory.CI_Activate_Tag is null or CT_Car_Inventory.CI_Activate_Tag=1)";

            SqlParameter[] parameters = {            
                        new SqlParameter("@DL_AU_Code", SqlDbType.Int)     
                                        };
            parameters[0].Value = DL_AU_Code;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AU_Code, int M_AU_Code)
        {
            string strSql = "delete from CT_Drivers_List where DL_AU_Code=" + AU_Code + " and DL_M_AU_Code=" + M_AU_Code + "";

            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteCar(int CI, int M_AU_Code)
        {
            string strSql = "update CT_Car_Inventory set CI_Activate_Tag=0  where CI_Code=" + CI + " and CI_AU_Code=" + M_AU_Code + "";

            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
