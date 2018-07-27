using CRMTree.BLL;
using CRMTree.Public;
using Newtonsoft.Json;
using ShInfoTech.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CRM_ReportSearch : BasePage
{
    readonly private BL_Report_Service _rep_ser = new BL_Report_Service();
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        var o = Request.Params["o"];
        var data = JsonConvert.DeserializeObject<dynamic>(o);
        string acion = data.action;
        switch (acion)
        {
            case "SearchCategory":
                SearchCategory(data);
                break;
            case "CategoryInfo":
                CategoryInfo(data);
                break;
            case "getReportQuery":
                getReportQuery(data);
                break;
        }
    }
    private void SearchCategory(dynamic data)
    {
        string _name = Interna ? "text_en" : "text_cn";
        string sql = string.Format(@"SELECT {0} AS text,value FROM words WHERE p_id = 4219 ORDER BY ID;", _name);
        string _o = DataHelper.ConvertToJSON(sql);
        Response.Write(_o);
    }
    private void CategoryInfo(dynamic data)
    {
        int _code = -1;
        if (data.RF_Code != null)
        {
            _code = (int)data.RF_Code;
        }

        string _name = Interna ? "RF_Name_EN" : "RF_Name_CN";
        string sql = string.Format(@"SELECT RF_Code,{0} as RF_Name,RF_Cat,RF_Query,RF_ID_Field FROM CT_Report_Fields WHERE RF_Cat = {1} AND RF_NAME_CN IS NOT NULL;", _name, _code);
        string _o = DataHelper.ConvertToJSON(sql);
        Response.Write(_o);
    }
    private void getReportQuery(dynamic data)
    {
        int rps_code = -1;
        if (data.RPS_Code != null)
        {
            rps_code = (int)data.RPS_Code;
        }
        string _name = Interna ? "[RQ_Name_EN]" : "[RQ_Name_CN]";
        string sql = string.Format(@"SELECT [RQ_Code]
      ,{0} as RQ_Name
      ,[RQ_Desc_EN]
      ,[RQ_Desc_CN]
      ,[isMain]
      ,[RQ_Foreign_Code]
      ,[RQ_RPS_Code]
      ,[RQ_Search_Tag]
  FROM [CT_Report_Query]where RQ_RPS_Code = {1};", _name, rps_code);
        string _o = DataHelper.ConvertToJSON(sql);
        Response.Write(_o);
    }
}