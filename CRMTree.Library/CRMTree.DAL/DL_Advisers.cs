using CRMTree.Model;
using CRMTree.Model.Adviser;
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
    public class DL_Advisers
    {
        /// <summary>
        /// 获取推荐的顾问
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <param name="CI_Code"></param>
        /// <returns></returns>
        public MD_AdviseList getRecommendAdviser(long AU_Code, int CI_Code)
        {
            SqlParameter[] parameters = { new SqlParameter("@CI_Code", SqlDbType.Int),
                                          new SqlParameter("@CI_AU_Code", SqlDbType.BigInt)};
            parameters[0].Value = CI_Code;
            parameters[1].Value = AU_Code;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "AD_getCarRecommendAdviser", parameters);
            DataTable DT = new DataTable();
            if (ds == null || ds.Tables.Count == 0)
            {
                return null;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                DT = ds.Tables[0];
            }
            else if (ds.Tables[1].Rows.Count > 0)
            {
                DT = ds.Tables[1];
            }
            if (DT == null || DT.Rows.Count <= 0)
            {
                return null;
            }
            Model.Adviser.MD_AdviseList myAdviseList = new Model.Adviser.MD_AdviseList();
            myAdviseList.Adviser_List = new List<Model.Adviser.MD_Adviser>();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                myAdviseList.Adviser_List.Add(new Model.Adviser.MD_Adviser
                {
                    AU_Code = int.Parse(DT.Rows[i]["AU_Code"].ToString()),
                    AU_Name = DT.Rows[i]["AU_Name"].ToString(),
                    DE_Picture_FN = DT.Rows[i]["DE_Picture_FN"].ToString(),
                    AD_Code = int.Parse(DT.Rows[i]["AD_Code"].ToString()),
                    AD_Name_CN = DT.Rows[i]["AD_Name_CN"].ToString(),
                    AD_Name_EN = DT.Rows[i]["AD_Name_EN"].ToString(),
                    DE_Code = int.Parse(DT.Rows[i]["DE_Code"].ToString())
                });
            }
            return myAdviseList;
        }
        /// <summary>
        /// 获取顾问的联系信息
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <returns></returns>
        public MD_Adviser getAdviserMessage(int AU_Code)
        {
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.Int) };
            parameters[0].Value = AU_Code;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "AD_getAdviserMessage", parameters);
            DataTable DT = new DataTable();
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            DT = ds.Tables[0];
            MD_Adviser myAdvise = new MD_Adviser();
            myAdvise.Mobil = DT.Rows[0]["Mobil"].ToString();
            myAdvise.Office = DT.Rows[0]["Office"].ToString();
            myAdvise.DealerShip = DT.Rows[0]["DealerShip"].ToString();
            myAdvise.Address = DT.Rows[0]["Address"].ToString();
            myAdvise.Email = DT.Rows[0]["Email"].ToString();
            return myAdvise;
        }

        public CT_Dealer_Empl getDefaultRecomAdviser(int CI_Code, int AD_Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select top 2 * from
		(select DE.DE_Code,AD.AD_Code,AD.AD_Name_CN,AD.AD_Name_EN,AU.AU_Code,AU.AU_Name,DE.DE_Picture_FN,HS.HS_RO_Close 
			from CT_History_Service HS
			inner join CT_Dealer_Empl DE on HS.HS_Advisor=DE.DE_Code
			inner join CT_Auto_Dealers AD on AD.AD_Code=DE.DE_AD_OM_Code
			inner join CT_All_Users AU on DE.DE_AU_Code=AU.AU_Code where HS.HS_CI_Code=@CI_Code 
		union 
		Select DE.DE_Code,AD.AD_Code,AD.AD_Name_CN,AD.AD_Name_EN,AU.AU_Code,AU.AU_Name,DE.DE_Picture_FN,''
			From CT_Car_Inventory CI 
			inner join CT_Car_Style CS On CI.CI_CS_Code=CS.CS_Code 
			inner join CT_Car_Model CM on CS.CS_CM_Code=CM.CM_Code 
			inner join CT_Make MK on MK.MK_Code=CM.CM_MK_Code 
			inner join CT_Auto_Dealers AD on AD.AD_AM_Code=MK.MK_AM_Code
			inner join CT_Dealer_Empl DE on DE.DE_AD_OM_Code=AD.AD_Code
			inner join CT_All_Users AU on DE.DE_AU_Code=AU.AU_Code
			Where DE_type=2  and CI.CI_Code=@CI_Code
		union
		Select DE.DE_Code,AD.AD_Code,AD.AD_Name_CN,AD.AD_Name_EN,AU.AU_Code,AU.AU_Name,DE.DE_Picture_FN,''
			From CT_Auto_Dealers AD
			inner join CT_Dealer_Empl DE on DE.DE_AD_OM_Code=AD.AD_Code
			inner join CT_All_Users AU on DE.DE_AU_Code=AU.AU_Code
			Where DE_type=2 ) a order by HS_RO_Close Desc");
            SqlParameter[] parameters = { new SqlParameter("@CI_Code", SqlDbType.Int),
                                          new SqlParameter("@AD_Code", SqlDbType.Int)};
            parameters[0].Value = CI_Code;
            parameters[1].Value = AD_Code;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            { return null; }
            Model.CT_Dealer_Empl myServiceAdviser = new Model.CT_Dealer_Empl();
            myServiceAdviser.DE_Code = int.Parse(ds.Tables[0].Rows[0]["DE_Code"].ToString());
            myServiceAdviser.AU_Name = ds.Tables[0].Rows[0]["AU_Name"].ToString();
            return myServiceAdviser;
        }

        #region 微信端联系顾问推荐

        public DealerEmplList GetDealerEmplList()
        {
            string sql = @"SELECT * FROM CT_AUTO_DEALERS INNER JOIN( SELECT DE_AD_OM_CODE FROM CT_DEALER_EMPL DE 
                            INNER JOIN CT_EMPL_TYPE ON DE_TYPE=PET_CODE
                            INNER JOIN CT_AUTO_DEALERS ON AD_CODE=DE_AD_OM_CODE
							INNER JOIN CT_Wechat_Member ON MB_AU_Code=DE.DE_AU_Code
							INNER JOIN CT_All_Users AU ON AU.AU_Code=DE.DE_AU_Code AND AU.AU_UG_Code=26
                            WHERE PET_CODE=2 AND DE_UTYPE=1
                            GROUP BY DE_AD_OM_CODE) A ON AD_CODE=A.DE_AD_OM_CODE;

                            SELECT DE_UType,DE_AD_OM_Code,DE_Code,AU1.AU_Name FROM CT_DEALER_EMPL DE 
                            INNER JOIN CT_EMPL_TYPE ON DE_TYPE=PET_CODE
                            INNER JOIN CT_AUTO_DEALERS ON AD_CODE=DE_AD_OM_CODE
                            INNER JOIN CT_ALL_USERS AU1 ON AU1.AU_CODE=DE_AU_CODE
							INNER JOIN CT_Wechat_Member ON MB_AU_Code=DE.DE_AU_Code
							INNER JOIN CT_All_Users AU ON AU.AU_Code=DE.DE_AU_Code AND AU.AU_UG_Code=26
                            WHERE PET_CODE=2 AND DE_UTYPE=1
                            ORDER BY DE_UTYPE ,DE_AD_OM_CODE ,DE_CODE;";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            DealerEmplList o = new DealerEmplList();
            o.Dealer = DataHelper.ConvertToList<CT_Auto_Dealers>(ds);
            o.Empl = DataHelper.ConvertToList<CT_Dealer_Empl>(ds, 1);
            return o;
        }
        #endregion
    }
}
