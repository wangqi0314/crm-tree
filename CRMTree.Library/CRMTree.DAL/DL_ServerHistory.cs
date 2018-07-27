using CRMTree.Model;
using CRMTree.Model.ServerHistory;
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
    public class DL_ServerHistory
    {
        public MD_ServerHistory getMyServiceHis(CT_History_Service HistoryService)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select HS.HS_Code,HS.HS_AD_Code,HS.HS_RO_Close,AD.AD_Name_CN,AD.AD_Name_EN,AU.AU_Name,
                                    HS.HS_RO_No,Hs.HS_RO_Amount,hs.HS_CustPay,hs.HS_PointsUsed 
                             from CT_History_Service HS  
                             inner join CT_Auto_Dealers AD on HS.HS_AD_Code=Ad.AD_Code 
                             inner join CT_Dealer_Empl DE on HS.HS_Advisor=DE.DE_Code 
                             inner join CT_All_Users AU on DE.DE_AU_Code=AU.AU_Code ");
            strSql.Append(@"where HS_AU_Code=@HS_AU_Code and HS_CI_Code=@HS_CI_Code ");
            StringBuilder StrSql1 = new StringBuilder();
            StrSql1.Append(@"select HS.HS_Code,SC.SC_Desc_EN,SC.SC_Desc_CN,SC.SC_Code 
                               from CT_History_Service HS  
                                inner join CT_RO_OPcodes RO on RO.RO_HS_Code=HS.HS_Code 
                                inner join CT_Service_Codes SC on RO.RO_SC_Code=SC.SC_Code ");
            StrSql1.Append(@"where HS_AU_Code=@HS_AU_Code and HS_CI_Code=@HS_CI_Code ");
            SqlParameter[] parameters = { new SqlParameter("@HS_AU_Code", SqlDbType.Int),
                                          new SqlParameter("@HS_CI_Code", SqlDbType.Int)};
            parameters[0].Value = HistoryService.HS_AU_Code;
            parameters[1].Value = HistoryService.HS_CI_Code;
            if (HistoryService.BeginDate != null && HistoryService.BeginDate.ToString() != "0001/1/1 0:00:00" && HistoryService.EndDate != null && HistoryService.EndDate.ToString() != "0001/1/1 0:00:00")
            {
                strSql.Append(@" and HS_RO_Close>='" + HistoryService.BeginDate + "' and HS_RO_Close<='" + HistoryService.EndDate + "';");
                StrSql1.Append(@" and HS_RO_Close>='" + HistoryService.BeginDate + "' and HS_RO_Close<='" + HistoryService.EndDate + "';");
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            Model.ServerHistory.MD_ServerHistory myServerHistory = new Model.ServerHistory.MD_ServerHistory();
            myServerHistory.History_Service = new List<Model.CT_History_Service>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Model.CT_History_Service HisService = new Model.CT_History_Service();
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["HS_Code"].ToString()))
                {
                    HisService.HS_Code = int.Parse(ds.Tables[0].Rows[i]["HS_Code"].ToString());
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["HS_AD_Code"].ToString()))
                {
                    HisService.HS_AD_Code = int.Parse(ds.Tables[0].Rows[i]["HS_AD_Code"].ToString());
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["HS_RO_Close"].ToString()))
                {
                    HisService.HS_RO_Close = Convert.ToDateTime(ds.Tables[0].Rows[i]["HS_RO_Close"].ToString());
                }
                HisService.AD_Name_CN = ds.Tables[0].Rows[i]["AD_Name_CN"].ToString();
                HisService.AD_Name_EN = ds.Tables[0].Rows[i]["AD_Name_EN"].ToString();
                HisService.AU_Name = ds.Tables[0].Rows[i]["AU_Name"].ToString();
                HisService.HS_RO_No = ds.Tables[0].Rows[i]["HS_RO_No"].ToString();
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["HS_RO_Amount"].ToString()))
                {
                    HisService.HS_RO_Amount = Convert.ToDecimal(ds.Tables[0].Rows[i]["HS_RO_Amount"].ToString());
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["HS_CustPay"].ToString()))
                {
                    HisService.HS_CustPay = Convert.ToDecimal(ds.Tables[0].Rows[i]["HS_CustPay"].ToString());
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["HS_PointsUsed"].ToString()))
                {
                    HisService.HS_PointsUsed = int.Parse(ds.Tables[0].Rows[i]["HS_PointsUsed"].ToString());
                }
                myServerHistory.History_Service.Add(HisService);
            }
            DataSet dateTab = SqlHelper.ExecuteDataset(CommandType.Text, StrSql1.ToString(), parameters);
            if (ds != null || ds.Tables[0].Rows.Count >= 0)
            {
                for (int m = 0; m < dateTab.Tables[0].Rows.Count; m++)
                {
                    for (int n = 0; n < myServerHistory.History_Service.Count; n++)
                    {
                        if (myServerHistory.History_Service[n].HS_Code.ToString() == dateTab.Tables[0].Rows[m]["HS_Code"].ToString())
                        {
                            myServerHistory.History_Service[n].SC_Desc_CN = dateTab.Tables[0].Rows[m]["SC_Desc_CN"].ToString();
                            myServerHistory.History_Service[n].SC_Desc_EN = dateTab.Tables[0].Rows[m]["SC_Desc_EN"].ToString();
                        }
                    }
                }
            }
            return myServerHistory;
        }
        public CT_History_Service getMyServiceHisInfo(CT_History_Service HistoryService)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select HS_Code,AD.AD_Name_EN,HS_Odometer,HS_Labor_Discount,HS_Parts_Discount,HS_RO_Amount,HS_CustPay from CT_History_Service HS inner join CT_Auto_Dealers AD on HS.HS_AD_Code=AD.AD_Code where HS_Code=@HS_Code");
            SqlParameter[] parameters = { 
                                            new SqlParameter("@HS_Code", SqlDbType.Int)
                                        };
            parameters[0].Value = HistoryService.HS_Code;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count <= 0)
            { return null; }
            Model.CT_History_Service myServerHistory = new Model.CT_History_Service();
            myServerHistory.HS_Code = int.Parse(ds.Tables[0].Rows[0]["HS_Code"].ToString());
            myServerHistory.AD_Name_EN = ds.Tables[0].Rows[0]["AD_Name_EN"].ToString();
            myServerHistory.HS_Odometer = int.Parse(ds.Tables[0].Rows[0]["HS_Odometer"].ToString());
            myServerHistory.HS_Labor_Discount = Convert.ToDecimal(ds.Tables[0].Rows[0]["HS_Labor_Discount"].ToString());
            myServerHistory.HS_Parts_Discount = Convert.ToDecimal(ds.Tables[0].Rows[0]["HS_Parts_Discount"].ToString());
            myServerHistory.HS_RO_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["HS_RO_Amount"].ToString());
            myServerHistory.HS_CustPay = Convert.ToDecimal(ds.Tables[0].Rows[0]["HS_CustPay"].ToString());
            return myServerHistory;
        }
        /// <summary>
        /// 获取进店历史服务
        /// 具体服务列表可以调用方法：GetService_Info
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <param name="SearchDate"></param>
        /// <returns></returns>
        public string GetHistory_Service(long AU_Code, DateTime? SearchDate)
        {
            string sql = @"SELECT top 100 AD.AD_Code,AD.AD_Name_CN,AD.AD_Name_EN,
                           AU.AU_Name AU_Name_AE,AU1.AU_Name,AU1.AU_Code,
                           dbo.Get_Connect_Car(HS.HS_CI_Code,1)AS CAR_CN,
                           dbo.Get_Connect_Car(HS.HS_CI_Code,2)AS CAR_EN,
                           HS.* FROM CT_History_Service HS
                INNER JOIN CT_Auto_Dealers AD ON HS.HS_AD_Code=AD.AD_Code
                INNER JOIN CT_Dealer_Empl DE on HS.HS_Advisor=DE.DE_Code AND DE.DE_UType=1 AND DE.DE_Type=2
                INNER JOIN CT_All_Users AU ON AU.AU_Code=DE.DE_AU_Code
                INNER JOIN CT_All_Users AU1 ON AU1.AU_Code=HS.HS_CI_Code";
                //WHERE AU1.AU_CODE=@AU_Code AND HS.HS_RO_Close > ''
                //ORDER BY AD.AD_Code ASC,HS.HS_RO_Close DESC";
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 获取单次服务的具体服务列表
        /// </summary>
        /// <param name="HS_Code"></param>
        /// <returns></returns>
        public string GetService_Info(int HS_Code)
        {
            string sql = string.Format(@"SELECT RO.RO_HS_Code HS_Code,SC.* FROM CT_RO_OPcodes RO
                            INNER JOIN CT_Service_Codes SC ON RO.RO_SC_Code=SC.SC_Code
                            WHERE RO.RO_HS_Code={0}",HS_Code);
            return DataHelper.ConvertToJSON(sql);
        }
    }
}
