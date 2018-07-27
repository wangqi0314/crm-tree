using CRMTree.BLL;
using CRMTree.Public;
using Newtonsoft.Json;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_CRMhandle : BasePage
{
    readonly private BL_CRMhandle _crmHand = new BL_CRMhandle();
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        var o = Request.Params["o"];
        var data = JsonConvert.DeserializeObject<dynamic>(o);
        string acion = data.action;
        switch (acion)
        {
            case "Contact_info_init":
                Contact_info_init(data);
                break;
            case "getDepartmentKPI":
                getDepartmentKPI(data);
                break;
            case "getDepartment":
                getDepartment(data);
                break;
            case "getKPI":
                getKPI(data);
                break;
            case "getDepartmentKPIUser":
                getDepartmentKPIUser(data);
                break;
            case "getDepartmentUser":
                getDepartmentUser(data);
                break;
            case "Seve_KPI":
                Seve_KPI(data);
                break;
            case "deleteKPI":
                deleteKPI(data);
                break;
            case "get_Pri":
                get_Pri(data);
                break;
            case "getGroupList":
                getGroupList(data);
                break;
            case "getGroupUser":
                getGroupUser(data);
                break;
            case "getSysFunction":
                getSysFunction(data);
                break;
            case "getRpName":
                getRpName(data);
                break;


        }
    }

    #region 个人通讯信息处理
    private void Contact_info_init(dynamic data)
    {
        Response.Write(_crmHand.Contact_info(Convert.ToInt64(data.AU_Code), Interna));
    }
    #endregion

    #region KPI
    private void getDepartmentKPI(dynamic data)
    {
        int _ad_code = (int)data.AD_Code;
        _ad_code = UserSession.DealerEmpl.DE_AD_OM_Code;
        //_ad_code = 2;
        string _depar_group = _crmHand.getDepartmentKPIGroup(_ad_code);
        string _depar = _crmHand.getDepartmentKPI(_ad_code, Interna);
        var _o = "{\"_d_group\":" + _depar_group + ",\"_d_info\":" + _depar + "}";
        Response.Write(_o);
    }
    private void getDepartment(dynamic data)
    {
        int _ad_code = UserSession.DealerEmpl.DE_AD_OM_Code;
        string _depar = _crmHand.getDepartment(_ad_code, Interna);
        Response.Write(_depar);
    }
    private void getKPI(dynamic data)
    {
        string _KPI = _crmHand.getKPI(Interna);
        Response.Write(_KPI);
    }
    /// <summary>
    /// 用户获取部门下员工KPI 列表分组标示（用户帮助前端UI好分组数据结构）
    /// </summary>
    /// <param name="ad_code"></param>
    /// <param name="pn_code"></param>
    /// <param name="pt_code"></param>
    /// <returns></returns>
    private void getDepartmentKPIUser(dynamic data)
    {
        int _ad_code = UserSession.DealerEmpl.DE_AD_OM_Code;
        int _pn_code = (int)data.PN_Code;
        int _pt_code = (int)data.PT_Code;
        string _user_group = _crmHand.getDepartmentKPIUserGroup(_ad_code, _pn_code, _pt_code);
        string _user = _crmHand.getDepartmentKPIUser(_ad_code, _pn_code, _pt_code);
        var _o = "{\"_d_group\":" + _user_group + ",\"_d_info\":" + _user + "}";
        Response.Write(_o);
    }
    private void getDepartmentUser(dynamic data)
    {
        int _ad_code = UserSession.DealerEmpl.DE_AD_OM_Code;
        int _pn_code = (int)data.PN_Code;
        string _user = _crmHand.getDepartmentUser(_ad_code, _pn_code);
        Response.Write(_user);
    }

    private void Seve_KPI(dynamic data)
    {
        if (data._o != null && data._o.Count > 0)
        {
            var o = data._o;
            for (int i = 0; i < o.Count; i++)
            {
                var obj = o[i];
                int KPV_UType = 2;
                int KPV_DE_AD_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
                int KPV_PDN_Code = (int)obj.PDN_Code;
                int KPV_KPT_Code = (int)obj.KPT_Code;
                string _2_sql = string.Format(@"DELETE CT_KPI_Values WHERE KPV_UType = {0} AND KPV_DE_AD_Code = {1} AND KPV_PDN_Code = {2} AND KPV_KPT_Code = {3};", KPV_UType, KPV_DE_AD_Code, KPV_PDN_Code, KPV_KPT_Code);
                _2_sql += string.Format(@"INSERT INTO CT_KPI_Values(KPV_KPT_Code,KPV_PDN_Code,KPV_Cat,KPV_UType,KPV_DE_AD_Code,KPV_Value)VALUES");
                string _2_sql_c = string.Empty;
                if (obj.pdn_Annual != null)
                {
                    _2_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 1, KPV_UType, KPV_DE_AD_Code, obj.pdn_Annual);
                }
                if (obj.pdn_Quarterly != null)
                {
                    _2_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 2, KPV_UType, KPV_DE_AD_Code, obj.pdn_Quarterly);
                }
                if (obj.pdn_Monthly != null)
                {
                    _2_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 3, KPV_UType, KPV_DE_AD_Code, obj.pdn_Monthly);
                }
                if (obj.pdn_Weekly != null)
                {
                    _2_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 4, KPV_UType, KPV_DE_AD_Code, obj.pdn_Weekly);
                }
                if (obj.pdn_Daily != null)
                {
                    _2_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 5, KPV_UType, KPV_DE_AD_Code, obj.pdn_Daily);
                }
                if (string.IsNullOrEmpty(_2_sql_c))
                {
                    continue;
                }
                _2_sql += _2_sql_c.Substring(0, _2_sql_c.Length - 1) + ";";
                ShInfoTech.Common.SqlHelper.ExecuteNonQuery(_2_sql);
                if (obj._d != null && obj._d.Count > 0)
                {
                    var obj_o = obj._d;
                    for (int j = 0; j < obj_o.Count; j++)
                    {
                        if (obj_o[j].DE_Code == null)
                        {
                            continue;
                        }
                        var _o_c = obj_o[j];
                        KPV_UType = 1;
                        int DE_Code = (int)_o_c.DE_Code;
                        string _1_sql = string.Format(@"DELETE CT_KPI_Values WHERE KPV_UType = {0} AND KPV_DE_AD_Code = {1} AND KPV_PDN_Code = {2} AND KPV_KPT_Code = {3};", KPV_UType, DE_Code, KPV_PDN_Code, KPV_KPT_Code);
                        _1_sql += string.Format(@"INSERT INTO CT_KPI_Values(KPV_KPT_Code,KPV_PDN_Code,KPV_Cat,KPV_UType,KPV_DE_AD_Code,KPV_Value)VALUES");
                        string _1_sql_c = string.Empty;
                        if (_o_c.pdn_Annual_user != null)
                        {
                            _1_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 1, KPV_UType, DE_Code, _o_c.pdn_Annual_user);
                        }
                        if (_o_c.pdn_Quarterly_user != null)
                        {
                            _1_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 2, KPV_UType, DE_Code, _o_c.pdn_Quarterly_user);
                        }
                        if (_o_c.pdn_Monthly_user != null)
                        {
                            _1_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 3, KPV_UType, DE_Code, _o_c.pdn_Monthly_user);
                        }
                        if (_o_c.pdn_Weekly_user != null)
                        {
                            _1_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 4, KPV_UType, DE_Code, _o_c.pdn_Weekly_user);
                        }
                        if (_o_c.pdn_Daily_user != null)
                        {
                            _1_sql_c += string.Format(@"({0},{1},{2},{3},{4},'{5}'),", KPV_KPT_Code, KPV_PDN_Code, 5, KPV_UType, DE_Code, _o_c.pdn_Daily_user);
                        }
                        if (string.IsNullOrEmpty(_1_sql_c))
                        {
                            continue;
                        }
                        _1_sql += _1_sql_c.Substring(0, _1_sql_c.Length - 1) + ";";
                        ShInfoTech.Common.SqlHelper.ExecuteNonQuery(_1_sql);
                    }
                }
            }
        }

    }

    private void deleteKPI(dynamic data)
    {
        int _UType = (int)data.UType;
        if (_UType == 1)
        {
            int DE_Code = (int)data.DE_Code;
            int KPV_PDN_Code = (int)data.PN_Code;
            int KPV_KPT_Code = (int)data.PT_Code;
            string _1_sql = string.Format(@"DELETE CT_KPI_Values WHERE KPV_UType = {0} AND KPV_DE_AD_Code = {1} AND KPV_PDN_Code = {2} AND KPV_KPT_Code = {3};", 1, DE_Code, KPV_PDN_Code, KPV_KPT_Code);
            ShInfoTech.Common.SqlHelper.ExecuteNonQuery(_1_sql);
        }
        else
        {
            int KPV_DE_AD_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
            int KPV_PDN_Code = (int)data.PN_Code;
            int KPV_KPT_Code = (int)data.PT_Code;
            string _2_sql = string.Format(@"DELETE CT_KPI_Values WHERE KPV_UType = 1 AND KPV_PDN_Code = {1} AND KPV_KPT_Code = {2}
AND KPV_DE_AD_Code IN (SELECT DE_Code FROM CT_Dealer_Empl WHERE DE_AD_OM_Code = {0} AND DE_PDN_Code = {1});", KPV_DE_AD_Code, KPV_PDN_Code, KPV_KPT_Code);
            _2_sql += string.Format(@"DELETE CT_KPI_Values WHERE KPV_UType = {0} AND KPV_DE_AD_Code = {1} AND KPV_PDN_Code = {2} AND KPV_KPT_Code = {3};", 2, KPV_DE_AD_Code, KPV_PDN_Code, KPV_KPT_Code);
            ShInfoTech.Common.SqlHelper.ExecuteNonQuery(_2_sql);
        }
    }
    #endregion

    #region 权限
    private void getGroupList(dynamic data)
    {
        BL_UserEntity _user = new BL_UserEntity();
        string user_group = _user.getGroupList(1, Interna);
        Response.Write(user_group);
    }
    private void getGroupUser(dynamic data)
    {
        int UG_Code = -1;
        if (data.UG_Code != null)
        {
            UG_Code = (int)data.UG_Code;
        }
        int ad_code = UserSession.DealerEmpl.DE_AD_OM_Code;
        BL_UserEntity _user = new BL_UserEntity();
        string _users = _user.getGroupUser(ad_code, UG_Code);
        Response.Write(_users);
    }
    private void getSysFunction(dynamic data)
    {
        string _name = Interna ? "SF_Desc_EN" : "SF_Desc_CN";
        string sql = string.Format(@"SELECT SF_Code,{0} AS SF_Desc FROM CT_Sys_Functions WITH(NOLOCK);", _name);
        string _o = DataHelper.ConvertToJSON(sql);
        Response.Write(_o);
    }

    private void getRpName(dynamic data)
    {
        int UG_Code = -1;
        if (data.UG_Code != null)
        {
            UG_Code = (int)data.UG_Code;
        }
        int SF_Code = -1;
        if (data.SF_Code != null)
        {
            SF_Code = (int)data.SF_Code;
        }
        int ad_code = UserSession.DealerEmpl.DE_AD_OM_Code;
        int _UType = UserSession.DealerEmpl.DE_UType;
        string sql = string.Empty;
        if (SF_Code == 1)
        {
            string _title = Interna ? "TL_Text_EN" : "TL_Text_CN";
            sql = string.Format(@"SELECT TL_Code AS _Code,{0} AS _Title,TL_UG_Code FROM CT_Tab_Links WITH(NOLOCK) WHERE TL_UG_Code = {1} ORDER BY TL_UG_Code;", _title, UG_Code);
        }
        else if (SF_Code == 2)
        {
            string _title = Interna ? "MI_Name_EN" : "MI_Name_EN";
            sql = string.Format(@"SELECT MI_Code AS _Code,{0} AS _Title,MI_UG_Code FROM CT_Menu_Items WITH(NOLOCK) WHERE MI_UG_Code = {1} ORDER BY MI_Code;",_title,UG_Code);
        }
        else if (SF_Code == 3)
        {
            string _title = Interna ? "MF_Title_EN" : "MF_Title_CN";
            sql = string.Format(@"SELECT MF_Code AS _Code,{0} AS _Title,MF_UG_Code FROM CT_Menu_Frames WITH(NOLOCK) WHERE MF_UG_CODE = {1} ORDER BY MF_Code;",_title, UG_Code);
        }
        
        string _name = Interna ? "RP_Name_EN" : "RP_Name_CN";
        sql = string.Format(@"SELECT dbo.F_Format_Paramters({0},RP_Code,{1},{2}) AS CT_RP,RP_Code FROM CT_Reports WITH(NOLOCK)
WHERE RP_UG_Code = {3} OR RP_UG_Code IS NULL AND (RP_Name_CN IS NOT NULL OR RP_Name_EN IS NOT NULL) 
ORDER BY RP_UG_Code DESC,RP_CODE ASC;", _name, _UType, ad_code, UG_Code);
        string _o = DataHelper.ConvertToJSON(sql);
        Response.Write(_o);
    }

    private void get_Pri(dynamic data)
    {
        int UG_Code = -1;
        if (data.UG_Code != null)
        {
            UG_Code = (int)data.UG_Code;
        }
        string sql = string.Format(@"SELECT AU_Name,SF_Desc_EN SF_Desc,DBO.Get_RP_MI(UP_UType,UP_RP_MI_Code,1) CT_RP,UP.* FROM CT_Users_Privileges UP WITH(NOLOCK)
INNER JOIN CT_All_Users AU WITH(NOLOCK) ON AU_Code = UP_AU_Code
INNER JOIN CT_Sys_Functions SF WITH(NOLOCK) ON SF.SF_Code = UP_SF_Code
WHERE UP_UG_Code = {0};", UG_Code);
        string _o = DataHelper.ConvertToJSON(sql);
        Response.Write(_o);
    }
    #endregion
}