using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMTree.Model.Common;

namespace CRMTree.Model
{
    public class CT_All_Users
    {
        public long AU_Code { get; set; }
        public byte? AU_Type { get; set; }
        public bool? AU_Gender { get; set; }
        public bool? AU_Married { get; set; }
        public string AU_FName { get; set; }
        public string AU_MName { get; set; }
        public string AU_LName { get; set; }
        public string AU_Name { get; set; }
        public int AU_UG_Code { get; set; }
        public string AU_Race { get; set; }
        public short? AU_LL_Code1 { get; set; }
        public short AU_LL_Code2 { get; set; }
        public byte? AU_Occupation { get; set; }
        public byte? AU_Industry { get; set; }
        public long? AU_Contact_Code { get; set; }
        public string AU_Dr_Lic { get; set; }
        public DateTime? AU_B_date { get; set; }
        public byte? AU_ID_Type { get; set; }
        public string AU_ID_No { get; set; }
        public string AU_Hobbies { get; set; }
        public string AU_Username { get; set; }
        public string AU_Password { get; set; }
        public byte AU_Ind_Income { get; set; }
        public byte? AU_HH_Income { get; set; }
        public byte? AU_Education { get; set; }
        public byte AU_Active_tag { get; set; }
        public DateTime? AU_Update_dt { get; set; }
        public int UG_UType { get; set; }
        public string  OpenId { get; set; }
        public string MB_OpenID { get; set; }
        public string Phone { get; set; }
        public string PL_Number { get; set; }
    }
    /// <summary>
    /// 微信上传文件返回数据
    /// </summary>
    public class UploadFileInfo
    {
        public string MediaId { set; get; }
        public string FileName { set; get; }
        public DateTime UploadDate { set; get; }
        public DateTime Invalidation { get; set; }

    }
    /// <summary>
    /// 报表Run需要的参数
    /// </summary>
    public class ReportRunParam
    {
        public int pType { get; set; }
        public int CG_EV_Code { get; set; }
        public int UType { get; set; }
        public int AU_AD_OM_Code { get; set; }

        public int CM_Index { get; set; }

    }
    /// <summary>
    /// 报表的参数列表实体类
    /// </summary>
    public class CT_Paramters_list
    {
        public int PL_Code { set; get; }		
        public int PL_RP_Code { set; get; }	
        public string PL_Tag { set; get; }
        public int PL_Type { set; get; }	
        public string PL_Prompt_En { set; get; }
        public string PL_Prompt_Ch { set; get; }	
        public string PL_Default { set; get; }
        public string PV_Val { set; get; }
    }
    /// <summary>
    /// 报表的参数实体类
    /// </summary>
    public class CT_Param_Value
    {
        public int RP_Code { get; set; }
        public int PV_PL_Code { get; set; }
        public int PV_Type { get; set; }
        public int PV_CG_Code { get; set; }
        public int PV_UType { get; set; }
        public int PV_AD_OM_Code { get; set; }
        public int PV_Val { get; set; }
    }
    /// <summary>
    /// 所有的粉丝
    /// </summary>
    public class CT_Wechat_Fan : CT_All_Users
    {
        public int WF_Id { set; get; }
        public string WF_OpenId { set; get; }
        public string WF_NickName { set; get; }
        public string WF_NickName_EC { get; set; }
        public string WF_Sex { set; get; }
        public string WF_City { set; get; }
        public string WF_Province { set; get; }
        public string WF_Country { set; get; }
        public string WF_HeadImgurl { set; get; }
        public DateTime WF_SubscribeTime { set; get; }
        public DateTime WF_unSubscribeTime { set; get; }
        public int WF_DataStatus { set; get; }
        public string WT_LogContent { get; set; }
        public DateTime WT_CreateTime { get; set; }
        public string WT_CreateTime_S { get; set; }
    }
    public partial class CT_Wechat_ReplyLog
    {
        public int WR_id { set; get; }
        public string WR_WT_Openid { set; get; }
        public long WR_AU_Code { set; get; }
        public int WR_Type { set; get; }
        public string WR_Content { set; get; }
        public DateTime WR_CreateTime { set; get; }
    }
    public partial class CT_Wechat_HistoryCamEve
    {
        public int WH_Code { set; get; }
        public long WH_Send_AU_Code { set; get; }
        public int WH_Send_AU_Type { set; get; }
        public int WH_Send_AD_OM_Code { set; get; }
        public long WH_Receive_AU_Code { set; get; }
        public string WH_Receive_OpenID { set; get; }
        public int WH_Send_Type { set; get; }
        public string WH_XML_Text { set; get; }
        public string WH_Json_Text { set; get; }
        public int WH_Send_CG_EV_Code { set; get; }
        public int WH_SendCG_EV_Type { set; get; }
        public int WH_Send_Status { set; get; }
        public DateTime WH_Create_Dt { set; get; }
        public DateTime WH_Send_Dt { set; get; }
    }
    /// <summary>
    /// 微信上传多媒体文件实例
    /// </summary>
    public partial class CT_Wechat_Multimedium
    {
        public int WM_CG_EV_Code { set; get; }
        public int WM_CG_EV_Type { set; get; }
        public int WM_Tpe { set; get; }
        public string WM_Media { set; get; }
        public string WM_Media_Id { set; get; }
        public DateTime WM_Create_Dt { set; get; }
        public DateTime WM_Invalidation { set; get; }
        public string WM_fileName { get; set; }
    }
    public class CT_Wechat_Member
    {
        public long MB_ID { set; get; }
        public string MB_OpenID { set; get; }
        public long MB_AU_Code { set; get; }
        public string AU_Name { set; get; }
        public string MB_UserName { set; get; }
        public string MB_UserPwd { set; get; }
        public string MB_Remark { set; get; }
        public int MB_DataStatus { set; get; }
        public int MB_MemberType { set; get; }
        public DateTime MB_CreateTime { set; get; }
        public string MB_Creator { set; get; }
        public DateTime MB_ExpireDate { set; get; }
    }
    public partial class CT_Wechat_ImageTextMsg
    {
        public int WI_Id { get; set; }
        public string WI_Code { get; set; }
        public string WI_Title { get; set; }
        public string WI_Descption { get; set; }
        public string WI_MsgContent { get; set; }
        public string WI_Url { get; set; }
        public string WI_ImageUrl { get; set; }
        public string WI_MediaId { get; set; }
        public int? WI_ShowCoverPic { get; set; }
        public string WI_Author { get; set; }
        public DateTime? WI_CreateTime { get; set; }
    }
    public partial class CT_Wechat_Online
    {
        public string WO_OpenId { get; set; }
        public DateTime WO_DateTime { get; set; }
        public string WO_Key { get; set; }
    }
    public partial class CT_Wechat_CustomSservice
    {
        public int WCS_Code { get; set; }
        public string WCS_FromOpenId { get; set; }
        public string WCS_ToOpenId { get; set; }
        public int WCS_Type { get; set; }
        public string WCS_Content { get; set; }
        public DateTime WM_CreateTime { get; set; }
    }
    public partial class CT_Wechat_CustomerServiceConnection
    {
        public string CSC_AD_OpenId { get; set; }
        public string CSC_CS_OpenId { get; set; }
        public DateTime CSC_Connection_dt { get; set; }
        public int CSC_Connection_Status { get; set; }
        public string WF_NickName { get; set; }
    }
    public class CT_Car_Inventory
    {
        public int? CI_Code { set; get; }
        public int? CI_CS_Code { set; get; }
        public string CI_VIN { set; get; }
        public int? CI_Mileage { set; get; }
        public string CI_Licence { set; get; }
        public byte? CI_YR_Code { set; get; }
        public long? CI_AU_Code { set; get; }
        public int? CI_Color_I { set; get; }
        public int? CI_Color_E { set; get; }
        public string CI_Picture_FN { set; get; }
        public string CI_Status { set; get; }
        public byte? CI_Activate_Tag { set; get; }
        public DateTime? CI_Create_dt { set; get; }
        public DateTime? CI_Update_dt { set; get; }
        public string CS_Style_EN { set; get; }
        public string CM_Model_EN { set; get; }
        public string YR_Year { set; get; }
        public string MK_Make_EN { set; get; }
        public string MK_Make_CN { set; get; }
        public string CS_Style_CN { set; get; }
        public string CM_Model_CN { set; get; }
        public string RS_Desc_EN { set; get; }
        public string RS_Desc_CN { set; get; }
        public string CS_OpenId { get; set; }
        public DateTime? CI_Licence_dt{get;set;}
        public string CI_Frame { set; get; }
        public string CI_Driving { set; get; }
        public string CI_Driving_Type { set; get; }
        public DateTime? CI_Driving_dt { set; get; }
         [Columns("Warr")]
        public DateTime? CI_Warr_St_dt { set; get; }
        [Columns("remarks")]
         public string CI_remarks { set; get; }
        public int? DE_Code { set; get; }
        public bool IS_Bind { set; get; }

    }
    public class CT_Transportation
    {
        public byte PTP_Code { set; get; }
        public string PTP_Desc_EN { set; get; }
        public string PTP_Desc_CN { set; get; }
        public string PTP_Icon { set; get; }
    }
    public partial class CT_Appt_Service
    {
        public int? AP_Code { set; get; }
        public long AP_AU_Code { set; get; }
        public int AP_CI_Code { set; get; }
        public int AP_AD_Code { set; get; }
        public DateTime AP_Time { set; get; }
        public byte AP_Cont_Type { set; get; }
        public int AP_PL_ML_Code { set; get; }
        public int? AP_SA_Selected { set; get; }
        public int AP_SC_Code { set; get; }
        public int? AP_ST_Code { set; get; }
        public int? AP_MP_Code { set; get; }
        public byte AP_PAM_Code { set; get; }
        public bool AP_Notification { set; get; }
        public byte? AP_Transportation { set; get; }
        public DateTime Ap_Arrival { set; get; }
        public long AP_Created_by { set; get; }
        public long AP_Updated_by { set; get; }
        public DateTime AP_Update_Dt { set; get; }
        public int CI_Code { set; get; }
        public string CS_Style_EN { set; get; }
        public string AD_Name_EN { set; get; }
        public string DE_ID { set; get; }
        public string AP_Notes { get; set; }
    }
    public partial class App_Service_List
    {
        public IList<CT_Appt_Service> App_List { get; set; }
    }
    public class CT_Auto_Dealers
    {
        public int AD_Code { get; set; }
        public string AD_DMS_ID { get; set; }	
        public string AD_Name_EN { get; set; }
        public string AD_Name_CN { get; set; }
        public int AD_OM_Code { get; set; }	
        public int AD_AM_Code { get; set; }
        public int AD_OR_Code_Sales { get; set; }
        public int AD_OR_Code_Serv { get; set; }	
        public int AD_DG_Code { get; set; }
        public string AD_Call_Limits { get; set; }
        public string AD_logo_file_S { get; set; }
        public string AD_logo_file_M { get; set; }
        public string AD_logo_file_L { get; set; }
        public string AD_Rewards { get; set; }
        public int AD_Active_Tag { get; set; }
        public DateTime AD_Update_dt { get; set; }
        public bool SD_SA_Selection { get; set; }
        public bool SD_Serv_Package { get; set; }
    }
    public partial class CT_Dealer_Empl
    {
        public int DE_Code { get; set; }
        public string DE_ID { get; set; }
        public bool DE_Ignore { get; set; }
        public byte DE_UType { get; set; }
        public byte DE_Type { get; set; }
        public long DE_AU_Code { get; set; }
        public int DE_AD_OM_Code { get; set; }
        public string DE_Picture_FN { get; set; }
        public byte DE_Campaign { get; set; }
        public string DE_Security_Level { get; set; }
        public DateTime DE_Activate_dt { get; set; }
        public DateTime DE_DActivate_dt { get; set; }
        public DateTime DE_Update_dt { get; set; }
        public string AU_Name { get; set; }
    }
    public partial class CT_Color_List
    {
        public int CL_Code { get; set; }
        public string CL_Color_EN { get; set; }
        public string CL_Color_CN { get; set; }
        public DateTime CL_Update_dt { get; set; }
    }
    public class CT_Tab_Links
    {
        public int TL_Code { get; set; }
        public int TL_UG_Code { get; set; }
        public int TL_Parent { get; set; }
        public int TL_Level { get; set; }
        public int TL_Order { get; set; }
        public string TL_TagCD { get; set; }
        public string TL_Text_EN { get; set; }
        public string TL_Text_CN { get; set; }
        public string TL_Link { get; set; }
        public int TL_Children { get; set; }
    }
    public class CT_Campaigns
    {
        public int CG_Status { get; set; }
        public int CG_Code { get; set; }
        public bool CG_Template { get; set; }
        public int CG_UType { get; set; }
        public int CG_AD_OM_Code { get; set; }
        public bool CG_Share { get; set; }
        public string CG_Title { get; set; }
        public string CG_Desc { get; set; }
        public int CG_Cat { get; set; }
        public int CG_Type { get; set; }
        public int CG_Type_Frequency { get; set; }
        public string CG_Method { get; set; }
        public int CG_Whom { get; set; }
        public int CG_RP_Code { get; set; }
        public string CG_Filename { get; set; }
        public DateTime CG_Start_Dt { get; set; }
        public DateTime CG_End_Dt { get; set; }
        public string CG_Succ_Matrix { get; set; }
        public bool CG_TrackFlag { get; set; }
        public DateTime CG_LastRun { get; set; }
        public int CG_Active_Tag { get; set; }
        public Int64 CG_Created_By { get; set; }
        public Int64 CG_Updated_By { get; set; }
        public DateTime CG_Create_dt { get; set; }
        public DateTime CG_Update_dt { get; set; }
        public int CG_Mess_Type { get; set; }
        public string CG_S { get; set; }
        public string S
        {
            get
            {
                if (Interna)
                {
                    if (CG_Whom == 1)
                        return "Dealer";
                    else if (CG_Whom == 2)
                        return "CRM Tree";
                    else
                        return "Both Of";
                }
                else
                {
                    if (CG_Whom == 1)
                        return "经销商";
                    else if (CG_Whom == 2)
                        return "CRM Tree";
                    else
                        return "两者同时";
                }
            }
        }
        public string SS
        {
            get
            {
                if (Interna)
                {
                    if (CG_Active_Tag == 1)
                        return "Active";
                    else
                        return "Inactive";
                }
                else
                {
                    if (CG_Active_Tag == 1)
                        return "激活";
                    else
                        return "失效";
                }
            }
        }
        public bool Interna { get; set; }
        public string CM_Filename { get; set; }
    }
    public class CT_Drivers_List
    {
        public int DL_AU_Code { get; set; }
        public int DL_M_AU_Code { get; set; }
        public int DL_Relation { get; set; }
        public int DL_Type { get; set; }
        public int DL_Access { get; set; }
        public int DL_CI_Code { get; set; }
        public DateTime DL_DActivate_dt { get; set; }
        public DateTime DL_Add_dt { get; set; }
        public DateTime DL_Update_dt { get; set; } 
    } 
    public class CT_Car_Model
    {
        public int CM_Code { get; set; }
        public string CM_Model_EN { get; set; }
        public string CM_Model_CN { get; set; }
        public int CM_MK_Code { get; set; }
        public int CM_OM_Code { get; set; }
        public int CM_CT_Code { get; set; }
        public DateTime CM_Update_dt { get; set; }
    }
    public class CT_Car_Style
    {
        public int CS_Code { get; set; }
        public string CS_Style_EN { get; set; }
        public string CS_Style_CN { get; set; }
        public int MK_Code { get; set; }
        public int MK_AM_Code { get; set; }
        public string MK_Make_EN { get; set; }
        public string MK_Make_CN { get; set; }
        public int CM_Code { get; set; }
        public string CM_Model_EN { get; set; }
        public string CM_Model_CN { get; set; }
    }
    public class CT_Car_Type
    {
        public int CT_Code { get; set; }
        public string CT_desc { get; set; }
        public DateTime CT_Update_dt { get; set; }
        public string CT_Type_EN { get; set; }
        public string CT_Type_CN { get; set; }
    }
    public class CT_Dealer_Groups
    {
        public int DG_Code { get; set; }
        public string DG_Name { get; set; }
        public string DG_Logo_file_S { get; set; }
        public string DG_Logo_file_M { get; set; }
        public string DG_Logo_file_L { get; set; }
        public int DG_Active_Tag { get; set; }
        public DateTime DG_Update_dt { get; set; }

    }
    public class CT_Event_Genre
    {
        public int EG_Code { get; set; }
        public string EG_Desc { get; set; }
        public int EG_UType { get; set; }
        public int EG_AD_OM_Code { get; set; }
    }
    public class CT_Events
    {
        public int EV_Code{ get; set; }
        public int EV_UType{ get; set; }
        public int EV_AD_OM_Code{ get; set; }
        public bool EV_Share{ get; set; }
        public string EV_Title{ get; set; }
        public string EV_Desc{ get; set; }
        public int EV_Type{ get; set; }
        public string EV_Method{ get; set; }
        public int EV_Mess_Type{ get; set; }
        public int EV_Whom{ get; set; }
        public int EV_RP_Code{ get; set; }
        public string EV_Filename{ get; set; }
        public DateTime EV_Start_dt{ get; set; }
        public DateTime EV_End_dt{ get; set; }
        public string EV_Succ_Matrix{ get; set; }
        public bool EV_TrackFlag{ get; set; }
        public DateTime EV_LastRun{ get; set; }
        public int EV_Active_Tag{ get; set; }
        public Int64 EV_Created_By{ get; set; }
        public Int64 EV_Updated_By{ get; set; }
        public DateTime EV_Create_dt{ get; set; }
        public DateTime EV_Update_dt{ get; set; }
        public int EV_EG_Code{ get; set; }
        public bool EV_RSVP{ get; set; }
        public DateTime EV_Act_S_dt{ get; set; }
        public DateTime EV_Act_E_dt{ get; set; }
        public string EV_Respnsible{ get; set; }
        public decimal EV_Budget{ get; set; }
        public string EV_Tools{ get; set; }
        public string S
        {
            get
            {
                if (Interna)
                {
                    if (EV_Whom == 1)
                        return "Dealer";
                    else if (EV_Whom == 2)
                        return "CRM Tree";
                    else
                        return "Both Of";
                }
                else
                {
                    if (EV_Whom == 1)
                        return "经销商";
                    else if (EV_Whom == 2)
                        return "CRM Tree";
                    else
                        return "两者同时";
                }
            }
        }
        public string SS
        {
            get
            {
                if (Interna)
                {
                    if (EV_Active_Tag == 1)
                        return "Active";
                    else
                        return "Inactive";
                }
                else
                {
                    if (EV_Active_Tag == 1)
                        return "激活";
                    else
                        return "失效";
                }
            }
        }
        public bool Interna{ get; set; }
        public string PL_Code_List { get; set; }
        public string Pl_Val_List { get; set; }
    }
    public class CT_Events_Person
    {
        public int PEP_Code{ get; set; }
        public string PEP_Desc_EN{ get; set; }
        public string PEP_Desc_CN{ get; set; }
    }
    public class CT_Events_Tools
    {
        public int PET_Code{ get; set; }
        public string PET_Desc_EN{ get; set; }
        public string PET_Desc_CN{ get; set; }
    }
    public class CT_History_Campaigns
    {
        public int HG_CG_Code{ get; set; }
        public DateTime HG_Run_Time{ get; set; }
        public int HG_CG_UType{ get; set; }
        public int HG_CG_AD_AM_Code{ get; set; }
        public DateTime HG_CG_Start_dt{ get; set; }
        public DateTime HG_CG_End_dt{ get; set; }
        public string HG_CG_Script{ get; set; }
        public string HG_RP_Name_EN{ get; set; }
        public string HG_RP_Name_CN{ get; set; }
    }
    public class CT_History_Service
    {	
        public long HS_Code{ get; set; }	
        public string HS_RO_No{ get; set; }
        public int HS_AD_Code{ get; set; }	
        public int HS_CI_Code{ get; set; }
        public long HS_AU_Code{ get; set; }
        public long HS_RM_Code{ get; set; }
        public decimal HS_RO_Amount{ get; set; }
        public decimal HS_CustPay{ get; set; }
        public int HS_PointsUsed{ get; set; }
        public DateTime? HS_Update_dt{ get; set; }
        public int HS_Odometer{ get; set; }
        public decimal HS_Labor_Discount{ get; set; }
        public decimal HS_Parts_Discount{ get; set; }
        public string AD_Name_CN{ get; set; }
        public string AD_Name_EN{ get; set; }
        public string AU_Name{ get; set; }
        public string SC_Desc_EN{ get; set; }
        public string SC_Desc_CN{ get; set; }
        public DateTime BeginDate{ get; set; }
        public DateTime EndDate{ get; set; }
        public DateTime? HS_RO_Close{ get; set; }
    }
    public class CT_Maintenance_Pack
    {
        public int MP_Code{ get; set; }
        public int MP_SO_Choice{ get; set; }
        public int MP_Cat_ID{ get; set; }
        public string MP_Desc_EN{ get; set; }
        public string MP_Desc_CN{ get; set; }
        public int MP_AD_Code{ get; set; }
        public int MP_RS_Code{ get; set; }
        public Decimal MP_Price{ get; set; }
        public DateTime MP_Update_dt{ get; set; }
    }
    public class CT_Make
    {	
        public int MK_Code{ get; set; }	
        public int MK_AM_Code{ get; set; }
        public string MK_Make_EN{ get; set; }
        public string MK_Make_CN{ get; set; }
    }
    public class CT_OEM
    {
        public int OM_Code{ get; set; }
        public int OM_AM_Code{ get; set; }
        public string OM_Name_EN{ get; set; }
        public string OM_Name_CN{ get; set; }
        public string OM_Logo_file_S{ get; set; }
        public string OM_Logo_file_M{ get; set; }
        public string OM_Logo_file_L{ get; set; }
        public int OM_Active_Tag{ get; set; }
        public DateTime OM_Update_dt{ get; set; }
    }
    public class CT_Recom_Services
    {	
        public int RS_Code{ get; set; }	
        public int RS_CS_Code{ get; set; }	
        public string RS_Desc_EN{ get; set; }
        public string RS_Desc_CN{ get; set; }	
        public int RS_Mileage{ get; set; }	
        public int RS_Time{ get; set; }
        public string RS_Update_dt{ get; set; }
    }
    public class CT_Report_Hist
    {
        public int RH_RP_Code{ get; set; }	
        public int RH_RC_Code{ get; set; }
        public long RH_AU_Code{ get; set; }
        public string RH_Name_EN{ get; set; }
        public string RH_Name_CN{ get; set; }	
        public string RH_Query{ get; set; }
        public DateTime RH_Run_Time{ get; set; }
    }
    public class CT_Reports
    {	
        public int RP_Code{ get; set; }
        public string RP_Name{ get; set; }
        public string RP_Name_EN{ get; set; }
        public string RP_Name_CN{ get; set; }
        public string RP_Desc_EN{ get; set; }
        public string RP_Desc_CN{ get; set; }
        public int RP_UG_Code{ get; set; }
        /// </summary>		
        public byte RP_Type{ get; set; }
        public int RP_UType{ get; set; }
        public string RP_Query{ get; set; }
        public int RP_AD_OM_Code{ get; set; }
        public byte RP_Sort{ get; set; }
        public long RP_Owner{ get; set; }
        public DateTime RP_Activate_dt{ get; set; }
        public DateTime RP_Update_dt{ get; set; }
        public int PL_Code{ get; set; }
        public string PL_Prompt_En{ get; set; }
        public string PL_Prompt_Ch{ get; set; }
        public string PL_Tag{ get; set; }
        public int PL_Type{ get; set; }
        public string PL_Default{ get; set; }
        public string PV_Val{ get; set; }
    }
    public class CT_Serv_Category
    {	
        public int SC_Code{ get; set; }	
        public string SC_Desc_EN{ get; set; }
        public string SC_Desc_CN{ get; set; }
    }
    public class CT_Service_Types
    {	
        public int ST_Code{ get; set; }	
        public int ST_AD_Code{ get; set; }
        public int ST_Category{ get; set; }
        public string ST_Desc_EN{ get; set; }
        public string ST_Desc_CN{ get; set; }	
        public decimal ST_Price{ get; set; }
        public DateTime ST_Update_dt{ get; set; }
        public int ST_SC_Code{ get; set; }
    }
    public class CT_Succ_Matrix
    {
        public int PSM_Code { get; set; }
        public string PSM_Code_s { get; set; }
        public string PSM_Desc_EN { get; set; }
        public string PSM_Desc_CN { get; set; }
        public int PSM_Category { get; set; }
        public string PSM_Val_Type_EN { get; set; }
        public string PSM_Val_Type_CN { get; set; }
        public int SMV_PSM_Code { get; set; }
        public int SMV_Type { get; set; }
        public int SMV_CG_Code { get; set; }
        public int SMV_Days { get; set; }
        public int SMV_Val { get; set; }
    }
    public class CT_User_Groups
    {		
        public int UG_Code{ get; set; }	
        public int UG_UType{ get; set; }	
        public string UG_Name_EN{ get; set; }	
        public string UG_Name_CN{ get; set; }
    }
    public class CT_Years
    {
        public int YR_Code{ get; set; }
        public string YR_Year{ get; set; }
    }
    public partial class CT_Comm_History
    {
        public long CH_Code { get; set; }
        public byte CH_UType { get; set; }
        public int CH_AD_OM_Code { get; set; }
        public byte CH_Type { get; set; }
        public int CH_ML_PL_Code { get; set; }
        public long CH_AU_Code { get; set; }
        public bool CH_Event_Flg { get; set; }
        public int CH_CG_Code { get; set; }
        public byte CH_PTY_Code { get; set; }
        public byte CH_Status { get; set; }
        public DateTime CH_Update_dt { get; set; }
    }
    public partial class CT_Fields_list
    {
        public int FL_Code { get; set; }
        public int FL_FL_Code { get; set; }
        public byte FL_Type { get; set; }
        public string FL_NoofRows { get; set; }
        public byte FL_Border { get; set; }
        public byte FL_Col { get; set; }
        public byte FL_Row { get; set; }
        public int FL_RP_Code { get; set; }
        public string FL_Title_EN { get; set; }
        public string FL_Title_CN { get; set; }
    }


    public partial class CT_Fields_Name 
    {
        public int FN_Code { get; set; }
        public int? FN_FL_FB_Code { get; set; }
        public string FN_FieldName { get; set; }
        public string FN_Desc_EN { get; set; }
        public string FN_Desc_CN { get; set; }
        public byte? FN_Format { get; set; }
        public byte? FN_Font { get; set; }
        public short? FN_Width { get; set; }
        public byte? FN_Option { get; set; }
        public byte? FN_Type { get; set; }
        public byte FN_Order { get; set; }
    }
    public partial class CT_Team_Group
    {
        public int TG_Code { get; set; }
        public string TG_Name_EN { get; set; }
        public string TG_Name_CN { get; set; }
        public int TG_UG_Code { get; set; }
        public int TG_Value { get; set; }
        public int TG_Type { get; set; }
        public int TG_Order { get; set; }
    }
    public class CT_Drivers_List_New
    {
        public long? MAU_Code { get; set; }
        public long? AU_Code { get; set; }
        public string AU_Name { get; set; }
        public bool? AU_Married { get; set; }
        public bool? AU_Gender { get; set; }
        public DateTime? AU_B_date { get; set; }
        public int? AU_ID_Type { get; set; }
        public string AU_ID_No { get; set; }
        public string AU_Dr_Lic { get; set; }
        public int? AU_Education { get; set; }
        public int? DL_Rel { get; set; }
        public bool? DL_Type { get; set; }
        public int? DL_Acc { get; set; }
        public string DL_Cars { get; set; }
        public int? DL_CI_Code { get; set; }
    }

}
