using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ShInfoTech.Common;
using CRMTree.Model.User;
using CRMTree.Model;
namespace CRMTree.DAL
{
    public partial class DL_UserEntity
    {
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <param name="UG_UType"></param>
        /// <returns></returns>
        public MD_GroupsList getGroupsList(int UG_UType)
        {
            string strSql = "select * from CT_User_Groups where UG_UType=" + UG_UType + " and UG_Name_EN is not null;";
            
            MD_GroupsList GropsList = new MD_GroupsList();
            GropsList.User_Groups_List = DataHelper.ConvertToList<CT_User_Groups>(strSql);
            return GropsList;
        }

        public string getGroupList(int ug_utype, bool intern)
        {
            string _name = intern ? "UG_Name_EN" : "UG_Name_CN";
            string sql = string.Format(@"SELECT UG_Code,{0} AS UG_Name,UG_UType FROM CT_User_Groups WHERE UG_Name_CN IS NOT NULL AND UG_Name_EN IS NOT NULL AND UG_UType = {1} ORDER BY UG_Code;",_name,ug_utype);
            return DataHelper.ConvertToJSON(sql);
        }
        /// <summary>
        /// 根据登陆获取登陆信息
        /// </summary>
        /// <param name="AU_Username"></param>
        /// <param name="AU_Password"></param>
        /// <returns></returns>
        public CT_All_Users GetUser(string AU_Username, string AU_Password)
        {
            string strSql = string.Format("select UG.UG_UType,AU.* from CT_All_Users AU with(nolock) inner join CT_User_Groups UG with(nolock) on AU_UG_Code=UG_Code where AU_Active_tag=1 and AU_Username='{0}' and AU_Password='{1}';", AU_Username, AU_Password);

            CT_All_Users User = DataHelper.ConvertToObject<CT_All_Users>(strSql);
            return User;
        }
        /// <summary>
        /// 获取登陆用户的员工信息
        /// </summary>
        /// <param name="DE_AU_Code"></param>
        /// <returns></returns>
        public CT_Dealer_Empl getEmpl(long DE_AU_Code)
        {
            string strSql = string.Format("select * from CT_Dealer_Empl with(nolock) where DE_AU_Code={0};", DE_AU_Code);
            CT_Dealer_Empl Empl = DataHelper.ConvertToObject<CT_Dealer_Empl>(strSql);
            return Empl;
        }
        /// <summary>
        /// 获取登陆用户对应的经销商信息
        /// </summary>
        /// <param name="DE_AU_Code"></param>
        /// <returns></returns>
        public CT_Auto_Dealers getDealer(long DE_AU_Code)
        {
            string strSql = "select AD.* from CT_Auto_Dealers AD with(nolock) inner join CT_Dealer_Empl with(nolock) on AD_Code=DE_AD_OM_Code and DE_UType=1 where DE_AU_Code=" + DE_AU_Code + ";";
            CT_Auto_Dealers Dealer = DataHelper.ConvertToObject<CT_Auto_Dealers>(strSql);
            return Dealer;
        }
        /// <summary>
        /// 获取登陆用户对应的集团信息
        /// </summary>
        /// <param name="DE_AU_Code"></param>
        /// <returns></returns>
        public CT_Dealer_Groups getDealerGroup(long DE_AU_Code)
        {
            string strSql = "select DG.* from CT_Dealer_Groups DG inner join CT_Dealer_Empl on DG_Code=DE_AD_OM_Code and DE_UType=2 where DE_AU_Code=" + DE_AU_Code + ";";
            CT_Dealer_Groups Dealer_Group = DataHelper.ConvertToObject<CT_Dealer_Groups>(strSql);
            return Dealer_Group;
        }
        public CT_OEM getOEM(long DE_AU_Code)
        {
            string strSql = "select OM.* from CT_OEM OM inner join CT_Dealer_Empl on OM_Code=DE_AD_OM_Code and DE_UType=3 where DE_AU_Code=" + DE_AU_Code + ";";
            CT_OEM OEM = DataHelper.ConvertToObject<CT_OEM>(strSql);
            return OEM;
        }

        /// <summary>
        /// zxm 个人资料添加
        /// </summary>
        /// <param name="dlmodel"></param>
        /// <returns></returns>
        public int add_personal(CT_Drivers_List_New dlmodel)
        {
            SqlParameter[] parameters = { 
                                            new SqlParameter("@MAU_Code", SqlDbType.BigInt),
                                            new SqlParameter("@AU_Code", SqlDbType.BigInt),
                                            new SqlParameter("@AU_Name", SqlDbType.NVarChar,50), 
                                            new SqlParameter("@AU_Married", SqlDbType.Bit),
                                            new SqlParameter("@AU_Gender", SqlDbType.Bit),
                                            new SqlParameter("@AU_B_date", SqlDbType.DateTime), 
                                            new SqlParameter("@AU_ID_Type", SqlDbType.Int),
                                            new SqlParameter("@AU_ID_No", SqlDbType.NVarChar,20),
                                            new SqlParameter("@AU_Dr_Lic", SqlDbType.NVarChar,20), 
                                            new SqlParameter("@AU_Education", SqlDbType.Int),
                                            new SqlParameter("@DL_Rel", SqlDbType.Int),
                                            new SqlParameter("@DL_Type", SqlDbType.Int),
                                            new SqlParameter("@DL_Acc", SqlDbType.Int),
                                            new SqlParameter("@DL_Cars", SqlDbType.NVarChar,4000),
                                            new SqlParameter("@DL_CI_Code", SqlDbType.Int),
                                        };
            parameters[0].Value = dlmodel.MAU_Code;
            parameters[1].Value = dlmodel.AU_Code;
            parameters[2].Value = dlmodel.AU_Name; ;
            parameters[3].Value = dlmodel.AU_Married;
            parameters[4].Value = dlmodel.AU_Gender;
            parameters[5].Value = dlmodel.AU_B_date;
            parameters[6].Value = dlmodel.AU_ID_Type;
            parameters[7].Value = dlmodel.AU_ID_No;
            parameters[8].Value = dlmodel.AU_Dr_Lic;
            parameters[9].Value = dlmodel.AU_Education;
            parameters[10].Value = dlmodel.DL_Rel;
            if (dlmodel.DL_Type == null)
            { parameters[11].Value = null; }
            else
                if ((bool)dlmodel.DL_Type)
                    parameters[11].Value = 0;
                else
                    parameters[11].Value = 1;
            parameters[12].Value = dlmodel.DL_Acc;
            parameters[13].Value = dlmodel.DL_Cars.Trim(',');
            parameters[14].Value = dlmodel.DL_CI_Code;
            return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "zxm_add_personal", parameters);
        }
        public int getMaxAU_Code()
        {
            string strSql = "select MAX(AU_Code) from CT_All_Users ";
            DataSet ds = SqlHelper.ExecuteDataset(strSql);
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update_CT_All_Users(CT_All_Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CT_All_Users set "); 
            strSql.Append("AU_Username=@AU_Username,");
            strSql.Append("AU_Password=@AU_Password"); 
            strSql.Append(" where AU_Code=@AU_Code");
            SqlParameter[] parameters = { 
					new SqlParameter("@AU_Username", SqlDbType.NVarChar,40),
					new SqlParameter("@AU_Password", SqlDbType.NVarChar,32), 
					new SqlParameter("@AU_Code", SqlDbType.BigInt,8)}; 
            parameters[0].Value = model.AU_Username;
            parameters[1].Value = model.AU_Password; 
            parameters[2].Value = model.AU_Code;
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

        public bool ExistsPwd(string AU_Password, string AU_Username)
        {
            string strSql = @"select COUNT(1) from CT_All_Users where AU_Username='" + AU_Username + "' AND AU_Password='" + AU_Password + "'"; 
            DataTable dt = SqlHelper.ExecuteDataset(strSql).Tables[0];
            int rows = (int)dt.Rows[0][0];
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }  
        }

        public string getGroupUser(int ad_code,int UG_Code)
        {
            string sql = string.Format(@"SELECT AU.AU_Name,AU.AU_Code,DE.* FROM CT_Dealer_Empl DE 
INNER JOIN CT_All_Users AU ON AU.AU_Code = DE.DE_AU_Code
INNER JOIN CT_User_Groups UG ON UG.UG_Code = AU.AU_UG_Code
WHERE DE_AD_OM_Code = {0} AND UG_Code = {1}
ORDER BY AU_Code", ad_code,UG_Code);
            return DataHelper.ConvertToJSON(sql);
        }


        #region 微信用户的相关
        public int VerificationUsername(string mobile, string OpenId)
        {
            string Sql = "SELECT W.* FROM CT_WECHAT_MEMBER W INNER JOIN CT_ALL_USERS U ON W.MB_AU_Code=U.AU_Code WHERE U.AU_USERNAME='" + mobile + "' AND W.MB_OpenID='" + OpenId + "';";
            object o = SqlHelper.ExecuteScalar(Sql);
            if (o == null)
                return 0;
            else
                return -1;
        }
        /// <summary>
        /// 给ALL_User 表添加微信用户
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="UserName"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        public string WechatLogin(string OpenId, string UserName, string UserPwd)
        {
            try
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@Result", System.Data.SqlDbType.NVarChar,10), 
                    new SqlParameter("@OpenID", System.Data.SqlDbType.NVarChar,200),
                    new SqlParameter("@UserName", System.Data.SqlDbType.NVarChar,50),
                    new SqlParameter("@UserPwd",System.Data.SqlDbType.NVarChar,200) };
                parameters[0].Direction = System.Data.ParameterDirection.Output;
                parameters[1].Value = OpenId;
                parameters[2].Value = UserName;
                parameters[3].Value = UserPwd;
                int i = SqlHelper.ExecuteNonQuery( CommandType.StoredProcedure, "02_Modify_WechatRegister_W", parameters);
                return Convert.ToString(parameters[0].Value);
            }
            catch (Exception)
            {
                return "S2";
            }
        }
        /// <summary>
        /// 获取注册后的微信用户ID
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public CT_Wechat_Member GetWechat_Member(string OpenId)
        {
            string sql = string.Format(@"SELECT * FROM CT_Wechat_Member WHERE MB_OpenID='{0}';",OpenId);

            CT_Wechat_Member o = DataHelper.ConvertToObject<CT_Wechat_Member>(sql);
            return o;
        }
        /// <summary>
        /// 修改用户的信息
        /// </summary>
        /// <param name="_u"></param>
        /// <returns></returns>
        public int Modify_User_Info(CT_All_Users _u)
        {
            #region 参数
            SqlParameter[] parameters = { new SqlParameter("@CS_OpenId", SqlDbType.VarChar,200),
                                          new SqlParameter("@AU_Name", SqlDbType.VarChar,30),
                                          new SqlParameter("@AU_B_DATE", SqlDbType.DateTime),
                                          new SqlParameter("@Phone", SqlDbType.VarChar,15),
                                          new SqlParameter("@AU_Gender", SqlDbType.Bit),
                                    };
            parameters[0].Value = _u.OpenId;
            parameters[1].Value = _u.AU_Name;
            parameters[2].Value = _u.AU_B_date;
            parameters[3].Value = _u.Phone;
            parameters[4].Value = _u.AU_Gender;
            #endregion
            return SqlHelper.ExecuteNonQuery( CommandType.StoredProcedure, "02_Modify_User_W", parameters);
        }
        /// <summary>
        /// 获取用户的所有信息
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public string GetAll_User_Info_Json(string OpenId)
        {
            string sql = "SELECT * FROM CT_All_Users AU INNER JOIN CT_Wechat_Member MB ON AU.AU_Code=MB.MB_AU_Code INNER JOIN CT_Wechat_Fans WF ON WF.WF_OpenId=MB.MB_OpenID LEFT JOIN CT_Phone_List PL ON PL.PL_AU_AD_Code=AU.AU_Code AND PL_UType=1 WHERE WF_OpenId='" + OpenId + "';";
            return DataHelper.ConvertToJSON(sql);
        }

        #endregion
    }

}

