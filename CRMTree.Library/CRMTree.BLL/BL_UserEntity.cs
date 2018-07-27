using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using CRMTree.Model;
using CRMTree.DAL;
using CRMTree.Model.User;
using CRMTree.Model.Common;
using System.Web;
namespace CRMTree.BLL
{
    /// <summary>
    /// 对于所有登陆注册用户的逻辑处理
    /// </summary>
    public partial class BL_UserEntity
    {
        /// <summary>
        /// 数据交互 User
        /// </summary>
        DL_UserEntity DL_User = new DL_UserEntity();
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <param name="UG_UType"></param>
        /// <returns></returns>
        public MD_GroupsList getGroupsList(int UG_UType)
        {
            return DL_User.getGroupsList(UG_UType);
        }

        public string getGroupList(int ug_utype, bool intern)
        {
            return DL_User.getGroupList(ug_utype, intern);
        }


        /// <summary>
        /// 根据用户名密码获取用户的登陆信息
        /// </summary>
        /// <param name="AU_Username"></param>
        /// <param name="AU_Password"></param>
        /// <returns></returns>
        public MD_UserEntity getUserInfo(string AU_Username, string AU_Password)
        {
            MD_UserEntity UserEntity = new MD_UserEntity();
            UserEntity.User = DL_User.GetUser(AU_Username, AU_Password);
            if (UserEntity.User == null) { return null; }
            if (UserEntity.User.UG_UType == (int)UserIdentity.Dealer)
            {
                UserEntity.DealerEmpl = DL_User.getEmpl(UserEntity.User.AU_Code);
                UserEntity.Dealer = DL_User.getDealer(UserEntity.User.AU_Code);
            }
            else if (UserEntity.User.UG_UType == (int)UserIdentity.DealerGroup)
            {
                UserEntity.DealerEmpl = DL_User.getEmpl(UserEntity.User.AU_Code);
                UserEntity.DealerGroup = DL_User.getDealerGroup(UserEntity.User.AU_Code);
            }
            else if (UserEntity.User.UG_UType == (int)UserIdentity.OEM)
            {
                UserEntity.DealerEmpl = DL_User.getEmpl(UserEntity.User.AU_Code);
                UserEntity.OEM = DL_User.getOEM(UserEntity.User.AU_Code);
            }
            return UserEntity;
        }


        #region 微信用户的相关

        /// <summary>
        /// 微信用户注册
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="Pwd"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public string WechatRegister(string mobile, string Pwd, string OpenId)
        {
            return DL_User.WechatLogin(OpenId, mobile, Pwd);
        }
        /// <summary>
        /// 验证当前注册的微信用户是否存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public int VerificationUsername(string mobile, string OpenId)
        {
            return DL_User.VerificationUsername(mobile, OpenId);
        }
        /// <summary>
        /// 获取当前的微信用户信息
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public CT_Wechat_Member GetWechat_Member(string OpenId)
        {
            return DL_User.GetWechat_Member(OpenId);
        }
        public string GetAll_User_Info_Json(string OpenId)
        {
            return DL_User.GetAll_User_Info_Json(OpenId);
        }
        public int Modify_User_Info(CT_All_Users _u)
        { 
            return DL_User.Modify_User_Info(_u); 
        }
        #endregion
        public static MD_UserEntity GetUserInfo()
        {
            if (HttpContext.Current.Session["PublicUser"] == null)
            {
                return null;
            }
            return (MD_UserEntity)HttpContext.Current.Session["PublicUser"];
        }

        public int add_personal(CT_Drivers_List_New dlmodel)
        {
            return new DL_UserEntity().add_personal(dlmodel);
        }
        public int getMaxAU_Code()
        {
            return new DL_UserEntity().getMaxAU_Code();
        }
        public bool Update_CT_All_Users(CT_All_Users model)
        {
            return new DL_UserEntity().Update_CT_All_Users(model);
        }
        public bool ExistsPwd(string AU_Password, string AU_Username)
        {
            return new DL_UserEntity().ExistsPwd(AU_Password, AU_Username);
        }

        public string getGroupUser(int ad_code, int UG_Code)
        {
            return DL_User.getGroupUser(ad_code, UG_Code);
        }
    }
}