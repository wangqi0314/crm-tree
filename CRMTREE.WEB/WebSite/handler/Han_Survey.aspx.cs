using CRMTree.BLL;
using CRMTree.Public;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class handler_Han_Survey : BasePage
{
    BL_Survey _bl_Sur = new BL_Survey();
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        var o = Request.Params["o"];
        var data = JsonConvert.DeserializeObject<dynamic>(o);
        string acion = data.action;
        switch (acion)
        {
            case "Get_Survey":
                Get_Survey(data);
                break;
            case "Save_Survey":
                Save_Survey(data);
                break;
            case "GetSurveyCamCategory":
                GetSurveyCamCategory(data);
                break;
            case "GetSurveyCategoryCam":
                GetSurveyCategoryCam(data);
                break;
            case "Save_Survey_data":
                Save_Survey_data(data);
                break;
            case "Delete_Survey":
                Delete_Survey(data);
                break;
            case "GetSurveyAnswer":
                GetSurveyAnswer(data);
                break;
        }
    }
    private void Get_Survey(dynamic data)
    {
        int _CG_Code = (int)data.CG_Code;
        int _AU_Code = (int)data.AU_Code;
        int _Advisor = (int)data.DE_Advosor;
        dynamic o = new ExpandoObject();
        DataSet _dt = _bl_Sur.GetSurvey(_CG_Code, _AU_Code, _Advisor);
        if (_dt != null)
        {
            if (_dt.Tables[0] != null)
            {
                o.survey = _dt.Tables[0];
            }
            if (_dt.Tables[1] != null)
            {
                o.answer = _dt.Tables[1];
            }
        }        
        string _o = JsonConvert.SerializeObject(o);
        Response.Write(_o);
    }
    private void Save_Survey(dynamic data)
    {
        int _CG_Code = (int)data.CG_Code;
        int _AU_Code = (int)data.SR_AU_Code;
        int _Advisor = (int)data.SR_Advisor;
        string _Answers = (string)data.SR_Answers;
        string _Notes = (string)data.SR_Notes;
        int _errCode = _bl_Sur.Save_Survey(_CG_Code, _AU_Code, _Advisor, _Answers, _Notes);
        Response.Write(_errCode);
    }
    public void GetSurveyCamCategory(dynamic data)
    {
        dynamic o = new ExpandoObject();
        DataTable _dt = _bl_Sur.GetSurveyCamCategory(Interna);
        o.SurveyCata = _dt;
        string _o = JsonConvert.SerializeObject(o);
        Response.Write(_o);
    }
    /// <summary>
    /// 获取问卷类别内的活动
    /// </summary>
    /// <param name="CG_Type"></param>
    /// <returns></returns>
    public void GetSurveyCategoryCam(dynamic data)
    {
        int _CG_Type = (int)data.CG_Type;
        dynamic o = new ExpandoObject();
        DataTable _dt = _bl_Sur.GetSurveyCategoryCam(_CG_Type);
        o.SurveyCataCam = _dt;
        string _o = JsonConvert.SerializeObject(o);
        Response.Write(_o);
    }
    public void Save_Survey_data(dynamic data)
    {
        int _CG_Code = (int)data.CG_Code;
        dynamic _Save_Data = data.Save_Data;
        int _err = _bl_Sur.Save_Survey_data(_CG_Code, _Save_Data);
        //dynamic o = new ExpandoObject();
        Response.Write(_err);
    }
    public void Delete_Survey(dynamic data)
    {
        int _SQ_Code = (int)data.SQ_Code;
        int _SF_Code = (int)data.SF_Code;
        int _err = _bl_Sur.Delete_Survey(_SQ_Code, _SF_Code);
        Response.Write(_err);
    }
    public void GetSurveyAnswer(dynamic data)
    {
        int _HD_Code = -1;
        if (data.HD_Code != null)
        {
            _HD_Code = (int)data.HD_Code;
        }                 
        dynamic o = new ExpandoObject();
        IList<DataTable> _table = _bl_Sur.GetSurveyAnswer(_HD_Code);
        if (_table != null)
        {
            if (_table.Count == 1)
            {
                o.t1 = _table[0];
            }
            else if (_table.Count == 2)
            {
                o.t1 = _table[0];
                o.t2 = _table[1];
            }
        }
        Response.Write(JsonConvert.SerializeObject(o));
    }
}