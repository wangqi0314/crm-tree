using CRMTree.BLL;
using CRMTree.Public;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_CRMhandle : BasePage
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
            case "DataGrid":
                DataGrid(data);
                break;
            case "getData":
                getData(data);
                break;
        }
    }

    #region 个人通讯信息处理
    private void DataGrid(dynamic data)
    {
        int _code = (int)data._code;
        var _winID = Guid.NewGuid();

        string _title = _rep_ser.getReportTitle(_code, Interna);
        string _f_d = _rep_ser.getFl_Date(_code.ToString());

        var dataGridInfo = "{\"_winID\":\"" + _winID + "\",\"column\":" + _title + ",\"List_data\":" + _f_d + "}";

        Response.Write(dataGridInfo);
    }
    private List<_param> getParam(dynamic data)
    {
        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        Dictionary<string, object> json = (Dictionary<string, object>)jss.DeserializeObject(data._param.ToString());
        List<_param> _params = new List<_param>();
        foreach (var item in json)
        {
            _params.Add(new _param() { Kay = item.Key, Value = item.Value.ToString() });
        }

        return _params;
    }
    private void getData(dynamic data)
    {
        int _code = (int)data._code;
        int _page = 1;
        if (data.page != null)
        {
            _page = (int)data.page;
        }
        List<_param> _params = getParam(data);
        string _json = "";
        if (data.isPage != null && data.isPage == true)
        {
            _json = _rep_ser.getReportDataPage(_code, _page, _params);
        }
        else
        {
            _json = _rep_ser.getReportData(_code, _params);
        }

        Response.Write(_json);
    }
    #endregion
}