using CRMTree.BLL;
using CRMTree.Model.Common;
using CRMTree.Model.User;
using CRMTree.Public;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRMDB = CRMTreeDatabase;

public partial class manage_Report_Report_Download : BasePage
{
    private IList<string[]> _q_param = new List<string[]>() { 
        new string[] { "AU_Code", "int" }, 
        new string[] { "AU_Name", "string" }, 
        new string[] { "HS_Code", "int" }, 
        new string[] { "CG_Code", "int" }, 
        new string[] { "AD_Code", "int" }, 
        new string[] { "DG_Code", "int" }, 
        new string[] { "AP_Code", "int" }, 
        new string[] { "OM_Code", "int" }, 
        new string[] { "EV_Code", "int" }, 
        new string[] { "RS_Code", "int" }, 
        new string[] { "PR", "int" }, 
        new string[] { "P1", "int" }, 
        new string[] { "P2", "null" }, 
        new string[] { "P3", "null" }, 
        new string[] { "P4", "int" }, 
        new string[] { "P5", "int" }, 
        new string[] { "P6", "int" }, 
        new string[] { "P7", "int" }, 
        new string[] { "P8", "int" }, 
        new string[] { "P9", "int" }, 
        new string[] { "P10", "int" }, 
        new string[] { "CT", "int" } };
    private int FL_Code = 0;
    private int MI_Code = 0;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        
        if (Request.Params["M"] != null && Request.Params["MF_FL_FB_Code"] != null)
        {
            FL_Code = Convert.ToInt32(Request.Params["MF_FL_FB_Code"].ToString());
            MI_Code = Convert.ToInt32(Request.Params["M"].ToString());
            string FileName = SetFileName();
            IList<CRMDB.EX_Param> _p = GetRequestParam();
            int _err = Expore_Ex(FL_Code, _p, FileName.Replace("'",""));
            Response.Redirect("/handler/Downloads.aspx?T=1&fileName="+FileName);
            Response.Write(FileName);
        }
    }
    /// <summary>
    /// 获取下载请求的前端参数
    /// </summary>
    /// <returns></returns>
    private IList<CRMDB.EX_Param> GetRequestParam()
    {
        var _p_all = Request.Params;
        IList<CRMDB.EX_Param> _params = new List<CRMDB.EX_Param>();
        foreach (string[] _p in _q_param)
        {
            if( _p_all[_p[0]] != null)
            {
                _params.Add(new CRMDB.EX_Param() { EX_Name = _p[0], EX_DataType = _p[1], EX_Value = _p_all[_p[0]] });
            }
        }
        return _params;
    }
    /// <summary>
    /// 设置文件名
    /// </summary>
    /// <returns></returns>
    private string SetFileName()
    {
        MD_UserEntity _user = BL_UserEntity.GetUserInfo();
        return new BL_CRMhandle().GetMIName(MI_Code, FL_Code, Interna) + "_" + _user.User.AU_Code + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls"; ;
    }
    public int Expore_Ex(int Pl_Code, IList<CRMDB.EX_Param> _p_s, string fileName)
    {
        BL_Export _b_ex = new BL_Export();
        NPOI_Common Excel = new NPOI_Common();
        //获取Title
        Excel.Ex_Titel = _b_ex.getEx_Title(Pl_Code);
        Excel.Ex_FieldName = _b_ex.getFieldName(Pl_Code);
        Excel.Ex_Data = _b_ex.getEx_Data(Pl_Code, _p_s);
        BL_Reports _bl_r = new BL_Reports();
        DataTable _type50 =  _bl_r.GetExport_Type50(Pl_Code,50);
        if (_type50 != null && _type50.Rows.Count > 0)
        {
            //FN_FieldName,FN_Desc_EN,FN_Desc_CN
            List<string> _Title_Field = new List<string>();
            List<string> _Title = new List<string>();
            List<string> _Title_Data = new List<string>();
            for (int i = 0; i < _type50.Rows.Count; i++)
            {
                _Title_Field.Add(_type50.Rows[i]["FN_FieldName"].ToString());
                if (Language.GetLang2() == EM_Language.en_us)
                {
                    _Title.Add(_type50.Rows[i]["FN_Desc_EN"].ToString());
                }
                else
                {
                    _Title.Add(_type50.Rows[i]["FN_Desc_CN"].ToString());
                }
            }
            for (int i = 0; i < Excel.Ex_Data.Columns.Count; i++)
            {
                _Title_Data.Add(Excel.Ex_Data.Columns[i].Caption);
            }
            foreach (string _t in _Title_Field)
            {
                Excel.Ex_FieldName.Remove(_t);
            }
            foreach (string _t in _Title)
            {
                Excel.Ex_Titel.Remove(_t);
            }
            foreach (string _t in _Title_Field)
            {
                foreach (string _t_t in _Title_Data)
                {
                    if (_t_t.StartsWith(_t))
                    {
                        Excel.Ex_Titel.Add(_t_t);
                        Excel.Ex_FieldName.Add(_t_t);
                    }
                }
            }
        }
        Excel.Create_Ex(fileName);
        Excel.Seave_Ex(fileName);
        return (int)_errCode.success;
    }
}