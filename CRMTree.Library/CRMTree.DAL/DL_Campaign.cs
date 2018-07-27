using CRMTree.Model;
using CRMTree.Model.Campaigns;
using CRMTree.Model.Common;
using CRMTree.Model.Reports;
using CRMTREE.BasePage;
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
    public class DL_Campaign
    {
        /// <summary>
        /// 获取分页后的Campaign列表
        /// </summary>
        /// <param name="AU_UType"></param>
        /// <param name="CE_AD_OM_Code"></param>
        /// <param name="primarykey"></param>
        /// <param name="fields"></param>
        /// <param name="ordefiled"></param>
        /// <param name="orderway"></param>
        /// <param name="currentpage"></param>
        /// <param name="pagesize"></param>
        /// <param name="pagecount"></param>
        /// <param name="rowscount"></param>
        /// <returns></returns>
        public MD_CampaignList GetCampaignList(int AU_UType, int CE_AD_OM_Code, string primarykey, string fields, string ordefiled, string orderway, int currentpage, int pagesize, int CT, out int pagecount, out int rowscount, int CA)
        {
            #region 参数
            SqlParameter[] parameters = { new SqlParameter("@AU_TYPE", SqlDbType.Int,4),
                                          new SqlParameter("@CG_AD_OM_Code", SqlDbType.Int,4),
                                          new SqlParameter("@primarykey", SqlDbType.VarChar,500),
                                          new SqlParameter("@fields", SqlDbType.VarChar,500),
                                          new SqlParameter("@ordefiled", SqlDbType.VarChar,500),
                                          new SqlParameter("@orderway", SqlDbType.VarChar,500),
                                          new SqlParameter("@currentpage", SqlDbType.Int,4),
                                          new SqlParameter("@pagesize", SqlDbType.Int,4),
                                          new SqlParameter("@pagecount", SqlDbType.Int,4),
                                          new SqlParameter("@rowscount", SqlDbType.Int,4),
                                          new SqlParameter("@CT", SqlDbType.Int,4),
                                          new SqlParameter("@CA",SqlDbType.Int,4)
                                    };
            parameters[0].Value = AU_UType;
            parameters[1].Value = CE_AD_OM_Code;
            parameters[2].Value = primarykey;
            parameters[3].Value = fields;
            parameters[4].Value = ordefiled;
            parameters[5].Value = orderway;
            parameters[6].Value = currentpage;
            parameters[7].Value = pagesize;
            parameters[8].Direction = ParameterDirection.Output;
            parameters[9].Direction = ParameterDirection.Output;
            parameters[10].Value = CT;
            parameters[11].Value = CA;
            #endregion
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure,
                                                  "CG_getCampaignsList_N", parameters);
            if (ds == null || ds.Tables.Count == 0)
            {
                pagecount = 0;
                rowscount = 0;
                return null;
            }
            pagecount = Convert.ToInt32(parameters[8].Value.ToString());
            rowscount = Convert.ToInt32(parameters[9].Value.ToString());

            MD_CampaignList o = new MD_CampaignList();
            o.CampaignList = DataHelper.ConvertToList<CT_Campaigns>(ds);
            return o;
        }

        /// <summary>
        /// 获取单条活动信息
        /// </summary>
        /// <param name="CG_Code"></param>
        /// <returns></returns>
        public CT_Campaigns GetCampaign(int CG_Code)
        {
            string SqlCam = @"SELECT * FROM CT_Campaigns WHERE CG_Code=" + CG_Code + "";

            CT_Campaigns o = DataHelper.ConvertToObject<CT_Campaigns>(SqlCam);
            return o;
        }
        public CT_Campaigns GetCampaign(int AD_Code, bool i)
        {
            string SqlCam = @"SELECT * FROM CT_Campaigns WHERE CG_Type=81 AND CG_Cat=2 AND CG_UType=1 AND ISNULL(CG_Template,0)=0 AND CG_Active_Tag=1 AND CG_AD_OM_Code=" + AD_Code + "";

            CT_Campaigns o = DataHelper.ConvertToObject<CT_Campaigns>(SqlCam);
            return o;
        }
        /// <summary>
        /// 获取Campaign 要选择调用的报表列表 type=1,4  
        /// 加载下拉列表时使用
        /// </summary>
        /// <returns></returns>
        public MD_ReportList GetCampaignReprotList()
        {
            string strSql = @"select RP.RP_Code,RP.RP_Name_EN,RP.RP_Name_CN,PL.PL_Default,PL.PL_Tag,PL_Type  
                            from CT_Reports RP 
                            inner join CT_Paramters_list PL on RP.RP_Code=PL.PL_RP_Code
                            where RP.RP_Type in(1,5,6,7,8,9)";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql);

            MD_ReportList o = new MD_ReportList();
            o.CT_Reports_List = DataHelper.ConvertToList<CT_Reports>(ds);
            return o;
        }
        #region 关系活动受益人的信息
        public MD_BeneficiaryList getBeneficiary_PhoneList(long AU_Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM CT_Comm_History 
                            INNER JOIN CT_Handler ON CH_Code=HD_CH_Code 
                            LEFT JOIN CT_Phone_List ON CH_ML_PL_Code=PL_Code
                            INNER JOIN CT_All_Users ON CH_AU_Code=AU_Code
                            INNER JOIN CT_Campaigns ON CH_CG_Code=CG_Code
                            WHERE HD_AU_Code=@AU_Code AND (CH_Type=1 OR CH_Type=3) AND CH_ML_PL_Code>0;");
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.BigInt) };
            parameters[0].Value = AU_Code;

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            MD_BeneficiaryList Bene_list = new MD_BeneficiaryList();
            if (ds == null || ds.Tables.Count == 0)
            {
                return Bene_list;
            }
            Bene_list.CampaignBeneficiaryList = new List<MD_CampaignBeneficiary>();
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            {
                return Bene_list;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MD_CampaignBeneficiary Cam_Bene = new MD_CampaignBeneficiary();
                Cam_Bene.CG_Code = int.Parse(ds.Tables[0].Rows[i]["CH_CG_Code"].ToString());
                Cam_Bene.CG_Title = ds.Tables[0].Rows[i]["CG_Title"].ToString();
                Cam_Bene.CG_RP_Code = int.Parse(ds.Tables[0].Rows[i]["CG_RP_Code"].ToString());
                Cam_Bene.AU_Code = int.Parse(ds.Tables[0].Rows[i]["AU_Code"].ToString());
                Cam_Bene.AU_Name = ds.Tables[0].Rows[i]["AU_Name"].ToString();
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["PL_Code"].ToString()))
                {
                    Cam_Bene.PL_Code = -1;
                    Cam_Bene.PL_Number = string.Empty;
                }
                else
                {
                    Cam_Bene.PL_Code = int.Parse(ds.Tables[0].Rows[i]["PL_Code"].ToString());
                    Cam_Bene.PL_Number = ds.Tables[0].Rows[i]["PL_Number"].ToString();
                }
                Cam_Bene.CG_Filename = ds.Tables[0].Rows[i]["CG_Filename"].ToString();
                Bene_list.CampaignBeneficiaryList.Add(Cam_Bene);
            }
            return Bene_list;
        }
        public MD_BeneficiaryList getBeneficiary_MessagingList(long AU_Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM CT_Comm_History 
                            INNER JOIN CT_Handler ON CH_Code=HD_CH_Code 
                            LEFT JOIN CT_Messaging_List ON CH_ML_PL_Code=ML_Code
                            INNER JOIN CT_All_Users ON CH_AU_Code=AU_Code
                            INNER JOIN CT_Campaigns ON CH_CG_Code=CG_Code
                            WHERE HD_AU_Code=@AU_Code AND (CH_Type=21 OR CH_Type=23) AND CH_ML_PL_Code>0
                            ;");
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.Int) };
            parameters[0].Value = AU_Code;

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            MD_BeneficiaryList Bene_list = new MD_BeneficiaryList();
            if (ds == null || ds.Tables.Count == 0)
            {
                return Bene_list;
            }
            Bene_list.CampaignBeneficiaryList = new List<MD_CampaignBeneficiary>();
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            {
                return Bene_list;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MD_CampaignBeneficiary Cam_Bene = new MD_CampaignBeneficiary();
                Cam_Bene.CG_Code = int.Parse(ds.Tables[00].Rows[i]["CH_CG_Code"].ToString());
                Cam_Bene.CG_Title = ds.Tables[0].Rows[i]["CG_Title"].ToString();
                Cam_Bene.AU_Code = int.Parse(ds.Tables[0].Rows[i]["AU_Code"].ToString());
                Cam_Bene.AU_Name = ds.Tables[0].Rows[i]["AU_Name"].ToString();
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["ML_Code"].ToString()))
                {
                    Cam_Bene.ML_Code = -1;
                    Cam_Bene.ML_Handle = string.Empty;
                }
                else
                {
                    Cam_Bene.ML_Code = int.Parse(ds.Tables[0].Rows[i]["ML_Code"].ToString());
                    Cam_Bene.ML_Handle = ds.Tables[0].Rows[i]["ML_Handle"].ToString();
                }
                Cam_Bene.CG_Filename = ds.Tables[0].Rows[i]["CG_Filename"].ToString();
                Bene_list.CampaignBeneficiaryList.Add(Cam_Bene);
            }
            return Bene_list;
        }
        public MD_BeneficiaryList getBeneficiary_EmailList(long AU_Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM CT_Comm_History 
                            INNER JOIN CT_Handler ON CH_Code=HD_CH_Code 
                            LEFT JOIN CT_Email_List ON CH_ML_PL_Code=EL_Code
                            INNER JOIN CT_All_Users ON CH_AU_Code=AU_Code
                            INNER JOIN CT_Campaigns ON CH_CG_Code=CG_Code
                            WHERE HD_AU_Code=@AU_Code AND (CH_Type=43 OR CH_Type=61) AND CH_ML_PL_Code>0;");
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.Int) };
            parameters[0].Value = AU_Code;

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            MD_BeneficiaryList Bene_list = new MD_BeneficiaryList();
            if (ds == null || ds.Tables.Count == 0)
            {
                return Bene_list;
            }
            Bene_list.CampaignBeneficiaryList = new List<MD_CampaignBeneficiary>();
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            {
                return Bene_list;
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MD_CampaignBeneficiary Cam_Bene = new MD_CampaignBeneficiary();
                Cam_Bene.CG_Code = int.Parse(ds.Tables[0].Rows[i]["CH_CG_Code"].ToString());
                Cam_Bene.CG_Title = ds.Tables[0].Rows[i]["CG_Title"].ToString();
                Cam_Bene.AU_Code = int.Parse(ds.Tables[0].Rows[i]["AU_Code"].ToString());
                Cam_Bene.AU_Name = ds.Tables[0].Rows[i]["AU_Name"].ToString();
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["EL_Code"].ToString()))
                {
                    Cam_Bene.EL_Code = -1;
                    Cam_Bene.EL_Address = string.Empty;
                }
                else
                {
                    Cam_Bene.EL_Code = int.Parse(ds.Tables[0].Rows[i]["EL_Code"].ToString());
                    Cam_Bene.EL_Address = ds.Tables[0].Rows[i]["EL_Address"].ToString();
                }
                Cam_Bene.CG_Filename = ds.Tables[0].Rows[i]["CG_Filename"].ToString();
                Bene_list.CampaignBeneficiaryList.Add(Cam_Bene);
            }
            return Bene_list;
        }
        /// <summary>
        /// 获取受益人的列表
        /// </summary>
        /// <param name="AU_Code"></param>
        /// <returns></returns>
        public List<MD_BeneficiaryList> getBeneficiaryList(int AU_Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM CT_Comm_History 
                            INNER JOIN CT_Handler ON CH_Code=HD_CH_Code 
                            LEFT JOIN CT_Phone_List ON CH_ML_PL_Code=PL_Code
                            INNER JOIN CT_All_Users ON CH_AU_Code=AU_Code
                            INNER JOIN CT_Campaigns ON CH_CG_Code=CG_Code
                            WHERE HD_AU_Code=@AU_Code AND (CH_Type=1 OR CH_Type=3) AND CH_ML_PL_Code>0
                            ;
                            SELECT * FROM CT_Comm_History 
                            INNER JOIN CT_Handler ON CH_Code=HD_CH_Code 
                            LEFT JOIN CT_Messaging_List ON CH_ML_PL_Code=ML_Code
                            INNER JOIN CT_All_Users ON CH_AU_Code=AU_Code
                            INNER JOIN CT_Campaigns ON CH_CG_Code=CG_Code
                            WHERE HD_AU_Code=@AU_Code AND (CH_Type=21 OR CH_Type=23) AND CH_ML_PL_Code>0
                            ;
                            SELECT * FROM CT_Comm_History 
                            INNER JOIN CT_Handler ON CH_Code=HD_CH_Code 
                            LEFT JOIN CT_Email_List ON CH_ML_PL_Code=EL_Code
                            INNER JOIN CT_All_Users ON CH_AU_Code=AU_Code
                            INNER JOIN CT_Campaigns ON CH_CG_Code=CG_Code
                            WHERE HD_AU_Code=@AU_Code AND (CH_Type=43 OR CH_Type=61) AND CH_ML_PL_Code>0;");
            SqlParameter[] parameters = { new SqlParameter("@AU_Code", SqlDbType.Int) };
            parameters[0].Value = AU_Code;

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql.ToString(), parameters);
            List<MD_BeneficiaryList> list_Bene_list = new List<MD_BeneficiaryList>();
            if (ds == null || ds.Tables.Count == 0)
            {
                return list_Bene_list;
            }

            MD_BeneficiaryList Bene_list1 = new MD_BeneficiaryList();
            Bene_list1.CampaignBeneficiaryList = new List<MD_CampaignBeneficiary>();
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            {
                list_Bene_list.Add(Bene_list1);
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    MD_CampaignBeneficiary Cam_Bene = new MD_CampaignBeneficiary();
                    Cam_Bene.CG_Code = int.Parse(ds.Tables[0].Rows[i]["CH_CG_Code"].ToString());
                    Cam_Bene.CG_Title = ds.Tables[0].Rows[i]["CG_Title"].ToString();
                    Cam_Bene.AU_Code = int.Parse(ds.Tables[0].Rows[i]["AU_Code"].ToString());
                    Cam_Bene.AU_Name = ds.Tables[0].Rows[i]["AU_Name"].ToString();
                    if (string.IsNullOrEmpty(ds.Tables[0].Rows[i]["PL_Code"].ToString()))
                    {
                        Cam_Bene.PL_Code = -1;
                        Cam_Bene.PL_Number = string.Empty;
                    }
                    else
                    {
                        Cam_Bene.PL_Code = int.Parse(ds.Tables[0].Rows[i]["PL_Code"].ToString());
                        Cam_Bene.PL_Number = ds.Tables[0].Rows[i]["PL_Number"].ToString();
                    }
                    Bene_list1.CampaignBeneficiaryList.Add(Cam_Bene);
                }
                list_Bene_list.Add(Bene_list1);
            }

            MD_BeneficiaryList Bene_list2 = new MD_BeneficiaryList();
            Bene_list2.CampaignBeneficiaryList = new List<MD_CampaignBeneficiary>();
            if (ds.Tables[1] == null || ds.Tables[1].Rows.Count <= 0)
            {
                list_Bene_list.Add(Bene_list2);
            }
            else
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    MD_CampaignBeneficiary Cam_Bene = new MD_CampaignBeneficiary();
                    Cam_Bene.CG_Code = int.Parse(ds.Tables[1].Rows[i]["CH_CG_Code"].ToString());
                    Cam_Bene.CG_Title = ds.Tables[1].Rows[i]["CG_Title"].ToString();
                    Cam_Bene.AU_Code = int.Parse(ds.Tables[1].Rows[i]["AU_Code"].ToString());
                    Cam_Bene.AU_Name = ds.Tables[1].Rows[i]["AU_Name"].ToString();
                    if (string.IsNullOrEmpty(ds.Tables[1].Rows[i]["ML_Code"].ToString()))
                    {
                        Cam_Bene.ML_Code = -1;
                        Cam_Bene.ML_Handle = string.Empty;
                    }
                    else
                    {
                        Cam_Bene.ML_Code = int.Parse(ds.Tables[1].Rows[i]["ML_Code"].ToString());
                        Cam_Bene.ML_Handle = ds.Tables[1].Rows[i]["ML_Handle"].ToString();
                    }
                    Bene_list2.CampaignBeneficiaryList.Add(Cam_Bene);
                }
                list_Bene_list.Add(Bene_list2);
            }

            MD_BeneficiaryList Bene_list3 = new MD_BeneficiaryList();
            Bene_list3.CampaignBeneficiaryList = new List<MD_CampaignBeneficiary>();
            if (ds.Tables[2] == null || ds.Tables[2].Rows.Count <= 0)
            {
                list_Bene_list.Add(Bene_list3);
            }
            else
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    MD_CampaignBeneficiary Cam_Bene = new MD_CampaignBeneficiary();
                    Cam_Bene.CG_Code = int.Parse(ds.Tables[2].Rows[i]["CH_CG_Code"].ToString());
                    Cam_Bene.CG_Title = ds.Tables[2].Rows[i]["CG_Title"].ToString();
                    Cam_Bene.AU_Code = int.Parse(ds.Tables[2].Rows[i]["AU_Code"].ToString());
                    Cam_Bene.AU_Name = ds.Tables[2].Rows[i]["AU_Name"].ToString();
                    if (string.IsNullOrEmpty(ds.Tables[2].Rows[i]["EL_Code"].ToString()))
                    {
                        Cam_Bene.EL_Code = -1;
                        Cam_Bene.EL_Address = string.Empty;
                    }
                    else
                    {
                        Cam_Bene.EL_Code = int.Parse(ds.Tables[2].Rows[i]["EL_Code"].ToString());
                        Cam_Bene.EL_Address = ds.Tables[1].Rows[2]["EL_Address"].ToString();
                    }
                    Bene_list3.CampaignBeneficiaryList.Add(Cam_Bene);
                }
                list_Bene_list.Add(Bene_list3);
            }
            return list_Bene_list;
        }
        #endregion
        #region zxm
        /// <summary>
        /// 获取已经设置好的电话分配情况
        /// </summary>
        /// <param name="CG_AD_Code"></param>
        /// <param name="Interna"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetCamCall(int CG_AD_Code, bool Interna = false, int type = 1)
        {
            string ug_Name = Interna ? "TG_Name_EN" : "TG_Name_CN";
            string _AU_Name = Interna ? "Any User" : "任何用户";
            string sql = string.Format(@"select tg.TG_Code,tg.{0} UG_Name,tg.TG_UG_Code,-1 AU_Code,'{3}' AU_Name,cc.* from CT_Camp_Calls cc 
                    inner join CT_Team_Group tg on cc.CC_Team=tg.TG_Code
                    where isnull(cc.CC_DE_Code,0)=0  and cc.CC_UType={1} and cc.CC_CG_AD_Code={2}
                    union
                    select tg.TG_Code,tg.{0} UG_Name,tg.TG_UG_Code,AU_Code,AU_Name,cc.* from CT_Camp_Calls cc 
                    inner join CT_Team_Group tg on cc.CC_Team=tg.TG_Code 
                    inner join CT_Dealer_Empl de on de.de_code = cc.CC_DE_Code
                    inner join ct_all_users au on au.au_code = de.de_au_code 
                    where cc.CC_UType={1} and cc.CC_CG_AD_Code={2}", ug_Name, type, CG_AD_Code, _AU_Name);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取TeamGroup
        /// </summary>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public DataTable GetTeamGroup(bool Interna = false)
        {
            string ug_Name = Interna ? "TG_Name_EN" : "TG_Name_CN";
            string sql = string.Format(@"SELECT TG_Code,{0} UG_Name,TG_UG_Code,TG_Value,TG_Type FROM CT_Team_Group WHERE TG_Type in (1,100,200)", ug_Name);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取组内用户
        /// </summary>
        /// <param name="AD_Code"></param>
        /// <param name="UG_Code"></param>
        /// <param name="Interna"></param>
        /// <returns></returns>
        public DataTable GetTeamGroupUser(int AD_Code, int UG_Code, bool Interna = false)
        {
            string _Name = Interna ? "Any User" : "任何用户";
            string sql = string.Format(@"select -1 AU_Code,'{0}' AU_Name,0 DE_Code
                            union
                            select AU_Code,AU_Name,DE_Code from CT_Dealer_Empl de
                            inner join CT_All_Users au on au_code = DE_AU_Code
                             where DE_UType=1 and DE_AD_OM_Code=" + AD_Code + " and AU_UG_Code=" + UG_Code + "", _Name);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            return ds.Tables[0];
        }
        #endregion
        #region 微信处理
        public MD_CampaignList GetRecommend_Trpe(int type)
        {
            string sql = "SELECT TOP 5 * FROM CT_Campaigns WHERE CG_Act_S_Dt+30>GETDATE() AND CG_Type=" + type + " ORDER BY CG_Act_S_Dt DESC;";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);

            MD_CampaignList o = new MD_CampaignList();
            o.CampaignList = DataHelper.ConvertToList<CT_Campaigns>(ds);
            return o;
        }
        public int Modify_Comm_History(int Modify_Type, string OpenID, int CG_Code)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Result", SqlDbType.NVarChar,100), 
                    new SqlParameter("@Modify_Type", SqlDbType.Int),
                    new SqlParameter("@OpenID", SqlDbType.NVarChar,200),
                    new SqlParameter("@CG_Code",SqlDbType.Int) };
            parameters[0].Direction = System.Data.ParameterDirection.Output;
            parameters[1].Value = Modify_Type;
            parameters[2].Value = OpenID;
            parameters[3].Value = CG_Code;
            int i = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "02_Modify_Comm_History_W", parameters);
            string result = Convert.ToString(parameters[0].Value);
            if (result == "OK")
            {
                return 0;
            }
            else { return -1; }
        }
        #endregion
        #region 朱习梦
        public int GetMaxLevel(int AT_AD_OM_Code)
        {
            string sql = "select MAX(AT_Level) from CT_Auth_Process where AT_AD_OM_Code=" + AT_AD_OM_Code + "";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete_CT_Auth_Process(int AT_Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CT_Auth_Process ");
            strSql.Append(" where AT_Code=@AT_Code");
            SqlParameter[] parameters = {
					new SqlParameter("@AT_Code", SqlDbType.Int,4)
			};
            parameters[0].Value = AT_Code;

            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update_CT_Auth_Process(CT_Auth_Process model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CT_Auth_Process set ");
            strSql.Append("AT_UType=@AT_UType,");
            strSql.Append("AT_AD_OM_Code=@AT_AD_OM_Code,");
            strSql.Append("AT_AType=@AT_AType,");
            strSql.Append("AT_AU_UG_Code=@AT_AU_UG_Code ");
            strSql.Append(" where AT_Code=@AT_Code");
            SqlParameter[] parameters = {
					new SqlParameter("@AT_UType", SqlDbType.TinyInt,1),
					new SqlParameter("@AT_AD_OM_Code", SqlDbType.Int,4), 
					new SqlParameter("@AT_AType", SqlDbType.TinyInt,1),
					new SqlParameter("@AT_AU_UG_Code", SqlDbType.BigInt,8), 
					new SqlParameter("@AT_Code", SqlDbType.Int,4)};
            parameters[0].Value = model.AT_UType;
            parameters[1].Value = model.AT_AD_OM_Code;
            parameters[2].Value = model.AT_AType;
            parameters[3].Value = model.UG_Code;
            parameters[4].Value = model.AT_Code;
            int rows = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool AddCT_Auth_Process(CT_Auth_Process model)
        {
            if (model.AT_Code != 0 && !string.IsNullOrEmpty(model.AT_Code.ToString()))
            {
                return Update_CT_Auth_Process(model);
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into CT_Auth_Process(");
                strSql.Append("AT_UType,AT_AD_OM_Code,AT_Level,AT_AType,AT_AU_UG_Code,AT_IType,AT_CG_Cat,AT_SType)");
                strSql.Append(" values (");
                strSql.Append("@AT_UType,@AT_AD_OM_Code,@AT_Level,@AT_AType,@AT_AU_UG_Code,@AT_IType,@AT_CG_Cat,@AT_SType)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@AT_UType", SqlDbType.TinyInt,1),
					new SqlParameter("@AT_AD_OM_Code", SqlDbType.Int,4),
					new SqlParameter("@AT_Level", SqlDbType.TinyInt,1),
					new SqlParameter("@AT_AType", SqlDbType.TinyInt,1),
					new SqlParameter("@AT_AU_UG_Code", SqlDbType.BigInt,8),
					new SqlParameter("@AT_IType", SqlDbType.TinyInt,1),
					new SqlParameter("@AT_CG_Cat", SqlDbType.TinyInt,1),
					new SqlParameter("@AT_SType", SqlDbType.VarChar,50)};
                parameters[0].Value = model.AT_UType;
                parameters[1].Value = model.AT_AD_OM_Code;
                parameters[2].Value = GetMaxLevel((int)model.AT_AD_OM_Code) + 1;
                parameters[3].Value = model.AT_AType;
                parameters[4].Value = model.UG_Code;
                parameters[5].Value = model.AT_IType;
                parameters[6].Value = model.EX_Camp_Category;
                parameters[7].Value = model.AT_SType;

                int i = SqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);

                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public DataTable GetApprovalInfo(int DE_UType, int DE_AD_OM_Code, bool Interna, int UG_UType)
        {
            var lng = Interna ? "EN" : "CN";
            string strSql = string.Format(@"with tb as 
                        (
                        SELECT  AT_Code,[AU_Code]=NULL,AU_UG_Code UG_Code,UG_UType,UG_Name_{2} AS UG_Code_Text
                        ,AU_Name=null FROM CT_Auth_Process
                        LEFT JOIN [CT_User_Groups] on AT_AU_UG_Code=UG_Code
                        INNER JOIN [CT_All_Users] on CT_All_Users.AU_UG_Code=UG_Code
                        INNER JOIN CT_Dealer_Empl on AU_Code = DE_AU_Code  
                        WHERE  ISNULL(AU_Active_tag,1) = 1
                                AND DE_UType = {1} AND DE_AD_OM_Code={0} and UG_UType={3} and AT_AD_OM_Code={0} and CT_Auth_Process.AT_AType=2 
                            )
                        select AT_Code,[AU_Code],UG_Code,UG_UType,UG_Code_Text,AU_Name as AU_Code_Text from tb   
                        Group by AT_Code,[AU_Code],UG_Code,UG_Code_Text,AU_Name,UG_UType
                        UNION ALL

                        SELECT AT_Code,[AU_Code],AU_UG_Code UG_Code,UG_UType,UG_Name_{2} UG_Code_Text
                                    ,AU_Name AS AU_Code_Text FROM CT_Auth_Process
                        INNER JOIN [CT_All_Users] on CT_All_Users.AU_Code=AT_AU_UG_Code            
                        LEFT JOIN [CT_User_Groups] on AU_UG_Code=UG_Code 
                        INNER JOIN CT_Dealer_Empl on AU_Code = DE_AU_Code  
                        WHERE  ISNULL(AU_Active_tag,1) = 1
                                AND DE_UType = {1} AND DE_AD_OM_Code={0} and UG_UType={3} 
                                and AT_AD_OM_Code={0} and CT_Auth_Process.AT_AType=1 and AT_UType={1}", DE_AD_OM_Code, DE_UType, lng, UG_UType);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql);
            return ds.Tables[0];
        }
        public DataTable GetApprovalInfo(int DE_UType, int DE_AD_OM_Code, bool Interna, int UG_UType, int AT_CG_Cat, int AT_IType, string AT_SType)
        {
            var lng = Interna ? "EN" : "CN";
            string strSql = string.Format(@"with tb as 
                        (
                        SELECT  AT_Code,[AU_Code]=NULL,AU_UG_Code UG_Code,UG_UType,UG_Name_{2} AS UG_Code_Text
                        ,AU_Name=null,AT_SType FROM CT_Auth_Process
                        LEFT JOIN [CT_User_Groups] on AT_AU_UG_Code=UG_Code
                        INNER JOIN [CT_All_Users] on CT_All_Users.AU_UG_Code=UG_Code
                        INNER JOIN CT_Dealer_Empl on AU_Code = DE_AU_Code  
                        INNER JOIN f_split('{6}',',') a on CHARINDEX(a.col,AT_SType)>0
                        WHERE  ISNULL(AU_Active_tag,1) = 1
                                AND DE_UType = {1} AND DE_AD_OM_Code={0} and UG_UType={3} and AT_AD_OM_Code={0} and CT_Auth_Process.AT_AType=2 
                                 and AT_CG_Cat={4} and AT_IType={5}
                                )
                        select AT_Code,[AU_Code],UG_Code,UG_UType,UG_Code_Text,AU_Name as AU_Code_Text,AT_SType from tb   
                        Group by AT_Code,[AU_Code],UG_Code,UG_Code_Text,AU_Name,UG_UType,AT_SType
                        UNION ALL
                        select AT_Code,[AU_Code],UG_Code,UG_UType,UG_Code_Text,AU_Code_Text,AT_SType
						 from (
                        SELECT AT_Code,[AU_Code],AU_UG_Code UG_Code,UG_UType,UG_Name_{2} UG_Code_Text
                                    ,AU_Name AS AU_Code_Text,AT_SType FROM CT_Auth_Process
                        INNER JOIN [CT_All_Users] on CT_All_Users.AU_Code=AT_AU_UG_Code            
                        LEFT JOIN [CT_User_Groups] on AU_UG_Code=UG_Code 
                        INNER JOIN CT_Dealer_Empl on AU_Code = DE_AU_Code 
                        INNER JOIN f_split('{6}',',') a on CHARINDEX(a.col,AT_SType)>0 
                        WHERE  ISNULL(AU_Active_tag,1) = 1
                                AND DE_UType = {1} AND DE_AD_OM_Code={0} and UG_UType={3} 
                                and AT_AD_OM_Code={0} and CT_Auth_Process.AT_AType=1 and AT_UType={1}
                                and AT_CG_Cat={4} and AT_IType={5}) tb1 
                                Group by AT_Code,[AU_Code],UG_Code,UG_Code_Text,AU_Code_Text,UG_UType,AT_SType", DE_AD_OM_Code, DE_UType, lng, UG_UType, AT_CG_Cat, AT_IType, AT_SType.Trim(','));
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, strSql);
            return ds.Tables[0];
        }
        public string GetTarget(int MF_FL_FB_Code, int BF_Code)
        {
            string strretu = "";
            string strSql = @"SELECT BF_Target from CT_List_Btns_Func where BF_Code=" + BF_Code + " and BF_FL_Code=" + MF_FL_FB_Code + "";
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                strretu = dt.Rows[0][0].ToString();
            }
            return strretu;

        }
        #endregion

        public string GetProcess(bool Interna, int UType, int Ad_Code, int AT_Cat, int AT_IType)
        {
            string ug_Name = Interna ? "UG_Name_EN" : "UG_Name_CN";
            string ug_Name_as = Interna ? "UG_Name" : "UG_Name";
            string AU_Name = Interna ? "Person In Charge" : "负责人";
            string sql = string.Format(@"with app as   (
                SELECT AT.* FROM CT_Auth_Process AT
                WHERE AT_UType = {0} AND AT_AD_OM_Code = {1} AND AT_CG_Cat = {2} AND AT_IType = {3}
                )
                select isNull(AU_Code,0) AU_Code,IsNull(AU_Name,'{6}') AU_Name,UG_Code,isNull({4},'{5}') as UG_Name,AT_Code,AT_AType,AT_IType,AT_CG_Cat,AT_SType from (
                select null as AU_Code, null as AU_Name ,ug.*,a.* from app a
                inner join CT_User_Groups ug on ug.UG_Code=a.AT_AU_UG_Code
                where a.AT_AType=2
                union
                select au.AU_Code,au.AU_Name as AU_Name ,ug.*,a.* from app a
                inner join CT_All_Users au on au.AU_Code=a.AT_AU_UG_Code
                inner join CT_User_Groups ug on au.AU_UG_Code=UG_Code
                where a.AT_AType=1) b
                order by b.AT_Level", UType, Ad_Code, AT_Cat, AT_IType, ug_Name, ug_Name_as, AU_Name);
            return DataHelper.ConvertToJSON(sql);
        }

        public int Save_Camp_Call(IList<CT_Camp_Calls> _Camp_Calls, int CG_AD_Code, int ccUType = 0)
        {
            if (_Camp_Calls == null || _Camp_Calls.Count <= 0)
            {
                return (int)_errCode.isNull;
            }
            string _sql = @"DELETE CT_Camp_Calls WHERE CC_UType=" + ccUType + " AND CC_CG_AD_Code=" + CG_AD_Code + ";";
            foreach (CT_Camp_Calls _c in _Camp_Calls)
            {
                _sql += @"INSERT INTO CT_Camp_Calls(CC_UType,CC_CG_AD_Code,CC_Team,CC_DE_Code,CC_Percent) VALUES(" + ccUType + "," + CG_AD_Code + "," + _c.CC_Team + "," + _c.CC_DE_Code + "," + _c.CC_Percent + ");";
            }
            int i = SqlHelper.ExecuteNonQuery(CommandType.Text, _sql);
            if (i > 0)
            {
                return (int)_errCode.success;
            }
            else
            {
                return (int)_errCode.systomError;
            }
        }

        public CT_Campaigns GetTXCam(int UType, int AD_Code)
        {
            string sql = string.Format(@"select top 1 CM_Filename,* from CT_Campaigns 
            inner join CT_Camp_Methods on CG_Code=CM_CG_Code where CG_Cat=2 and CG_Type=81 and CG_Status=10 and
             CM_Method like '%9%' and CG_UType={0} and CG_AD_OM_Code={1} order by CG_Update_dt desc,CM_Contact_Index ASC", UType, AD_Code);
            CT_Campaigns _o = DataHelper.ConvertToObject<CT_Campaigns>(sql);
            return _o;
        }
        public CT_All_Users GetTXCam_Number(int AU_Code, int AP_Code)
        {
            string sql = string.Format(@"select top 1 PL_Number from (
                select pl.* from CT_Phone_List pl inner join CT_Appt_Service a on a.AP_PL_ML_Code=pl.PL_Code and a.AP_Cont_Type=3 where ISNULL(PL_Active,1)=1 and PL_Type=3 and PL_Code={0}
                Union
                select * from CT_Phone_List where ISNULL(PL_Active,1)=1 and PL_Type=3  and PL_AU_AD_Code={1}
                )a
                order by PL_Pref", AP_Code, AU_Code);
            CT_All_Users _o = DataHelper.ConvertToObject<CT_All_Users>(sql);
            return _o;
        }

        public DataTable GetRunUser(string[] Users)
        {
            foreach (var id in Users)
            {
                Convert.ToInt32(id);
            }
            var sql = string.Format(@"SELECT AU_Code,AU_Name FROM CT_All_Users WHERE AU_Code IN ({0});", Users);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }
            return ds.Tables[0];
        }
    }
}