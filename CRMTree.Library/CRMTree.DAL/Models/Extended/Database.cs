using PetaPoco.DatabaseTypes;
using PetaPoco.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using PetaPoco;

namespace CRMTreeDatabase
{
    

    public partial class EX_Auth_Process
    {
        public int AT_Code { get; set; }
        public int? EX_Camp_Category { get; set; }
        public string AT_SType { get; set; }
        public int? AT_IType { get; set; }
        public int? AU_Code { get; set; }
        public int? UG_Code { get; set; }
        public int? AT_Level { get; set; }
        public int? AT_UType { get; set; }
        public int? AT_AD_OM_Code { get; set; }
        public int? AT_CG_Cat { get; set; }
    }

    public partial class CT_Camp_Method : DBCRMTree.Record<CT_Camp_Method>  
    {
        public string CM_Filename_Temp { get; set; }
        public bool? EX_IsParamValue { get; set; }
        public string EX_ParamValue { get; set; }
    }
    public partial class EX_Email
    {
        public string EX_ToEmail { get; set; }
        public string EX_Subject { get; set; }
        public string EX_Body { get; set; }
    }
    public partial class EX_Message
    {
        public string EX_ToMsg { get; set; }
        public string EX_Msg { get; set; }
    }
    public partial class EX_Approve
    {
        public int User_Code { get; set; }
        public int AT_Code { get; set; }
        public string User_Name { get; set; }
        public string EL_From { get; set; }
        public string EL_To { get; set; }

        public string From_Name { get; set; }
    }

    public partial class EX_CT_Auth_Activities
    {
        public int? EX_AT_Code { get; set; }
        public int EX_State { get; set; }
        public int EX_CG_Code { get; set; }
        public string EX_Remark { get; set; }
    }

    public partial class CT_Campaign : DBCRMTree.Record<CT_Campaign>
    {
        public int? Ex_CG_Act_E_Dt { get; set; }
        public int? Ex_CG_Start_Dt { get; set; }
        public int? Ex_CG_End_Dt { get; set; }
        public string CG_Filename_Temp { get; set; }
        public string EX_CG_Status { get; set; }
        public byte? EX_T { get; set; }
        public byte? EX_Approve { get; set; }
    }

    public partial class CT_Dept_Name : DBCRMTree.Record<CT_Dept_Name>
    {
        public List<CT_Dept_Value> EX_Values { get; set; }
    }

    public partial class EX_Param : DBCRMTree.Record<EX_Param>
    {
        public string EX_Name { get; set; }
        public string EX_Value { get; set; }
        public string EX_DataType { get; set; }
    }

    public partial class EX_Tag : DBCRMTree.Record<EX_Tag>
    {
        public string PL_Tag { get; set; }
        public string PV_Val { get; set; }
    }

    public partial class CT_Frame_Sel_Row : DBCRMTree.Record<CT_Frame_Sel_Row>
    {
        public IEnumerable<dynamic> EX_Data { get; set; }
    }

    public partial class EX_Combobox : DBCRMTree.Record<EX_Combobox>
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public partial class EX_CT_Params
    {
        public int AU_Code { get; set; }
        public int HS_Code { get; set; }
        public int R_Code { get; set; }
    }

    public partial class EX_CT_Contacts : DBCRMTree.Record<EX_CT_Contacts>
    {
        public int type { get; set; }
        public byte? pref { get; set; }
        public object o { get; set; }
    }

    public partial class CT_Car_Inventory : DBCRMTree.Record<CT_Car_Inventory>
    {
        public int CM_Code { get; set; }
        public int MK_Code { get; set; }
        public string CP_Picture_FN { get; set; }
        public string Picture_Removed { get; set; }
        public dynamic Drivers { get; set; }
    }

    public partial class CT_Dealer_Empl : DBCRMTree.Record<CT_Dealer_Empl>
    {
        public string EX_DE_Activate_dt { get; set; }
        public string EX_DE_Vacation_St { get; set; }
        public string EX_DE_Vacation_En { get; set; }
        public string DE_Picture_FN_Temp { get; set; }
    }

    public partial class CT_Appt_Service
    {
        public string EX_AP_Date { get; set; }
        public string EX_AP_Time { get; set; }
        [ResultColumn]
        public string AU_Name { get; set; }
        [ResultColumn]
        public string AP_PL_ML_Code_Text { get; set; }
        [ResultColumn]
        public string AP_SA_Selected_text { get; set; }
        [ResultColumn]
        public string AP_Transportation_text { get; set; }
        [ResultColumn]
        public string AP_AD_Code_text { get; set; }
        [ResultColumn]
        public string SN_Note { get; set; }
        [ResultColumn]
        public string EX_GeneralNote { get; set; }
    }

    public partial class CT_Drivers_List : DBCRMTree.Record<CT_Drivers_List>
    {
        [ResultColumn]
        public string AU_Name { get; set; }
    }
    public partial class CT_Drivers_ListNew : DBCRMTree.Record<CT_Drivers_ListNew>
    {
         ///
    }

    public partial class CT_All_User : DBCRMTree.Record<CT_All_User>
    {
        public string EX_AU_Activate_dt { get; set; }
        public string relation_c { get; set; }
        public string relation_e { get; set; }
        public int relation_id { get; set; }
    }
    public partial class CT_Relation_User : DBCRMTree.Record<CT_Relation_User>
    {
        public int? AU_Code { get; set; }
        public string AU_Name { get; set; }
        public byte? relation_id { get; set; }
        public string relation_type { get; set; }
        public int? contact_id { get; set; }
        public string info { get; set; }
        public dynamic o { get; set; }
        public int? Keys { get; set; }
    }
    public partial class CT_Relation_Con : DBCRMTree.Record<CT_Relation_Con>
    {
        public byte? AL_Type { get; set; }
        public string AL_Add1 { get; set; }
        public string AL_Add2 { get; set; }
        public string AL_District { get; set; }
        public string AL_City { get; set; }
        public string AL_State { get; set; }
        public string AL_Zip { get; set; }
        public bool? AL_DonotUse { get; set; }

        public byte? PL_Type { get; set; }
        public short? PL_PP_Code { get; set; }
        public string PL_Number { get; set; }
        public bool? PL_DonotUse { get; set; }

        public byte? EL_Type { get; set; }
        public string EL_Address { get; set; }
        public bool? EL_DonotUse { get; set; }

        public byte? ML_Type { get; set; }
        public string ML_Handle { get; set; }
        public bool? ML_DonotUse { get; set; }
    }
    public partial class CT_Auto_Dealer : DBCRMTree.Record<CT_Auto_Dealer>
    {
        public string AD_logo_file_S_Temp { get; set; }
        public string AD_logo_file_M_Temp { get; set; }
        public string AD_logo_file_L_Temp { get; set; }
    }
}
