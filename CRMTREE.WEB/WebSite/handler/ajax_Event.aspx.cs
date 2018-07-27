using CRMTree.BLL;
using CRMTree.Model;
using CRMTree.Public;
using Microsoft.JScript;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using CRMTree.Model.Event;

public partial class handler_ajax_Event :BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        string strAction = RequestClass.GetString("action").ToLower();//操作动作 
        if ("list_event" == strAction || "add_event" == strAction || "event_modify" == strAction || "enent_delete"==strAction)
        {
            switch (strAction)
            {
                case "list_event":
                    getEventList();//event 列表
                    break;
                case "add_event":
                    event_add();
                    break;
                case "event_modify":
                    event_Modify();
                    break;
                case "enent_delete":
                    enent_Delete();
                    break;
            }
        }
        else
            Response.Write("-1");//action 参数不正确，没权限
    }
    public void getEventList()
    {
        //排序字段
        string strSortField = RequestClass.GetString("sortfield").Trim().Replace("'", "");
        //排序规则
        string strSortRule = RequestClass.GetString("sortrule").Trim().Replace("'", "");
        int intCurrentPage = RequestClass.GetInt("page", 1);//当前页
        //排序字段的样式
        string orderClass = String.Empty;
        if (string.IsNullOrEmpty(strSortField))
        {
            strSortField = "EV_Update_dt";
            strSortRule = "desc";
            orderClass = "taxisDown";
        }
        else
        {
            if (strSortRule.Equals("asc"))
                orderClass = "taxisUp";
            else
                orderClass = "taxisDown";
        }
        BL_Event Event = new BL_Event();
        string HTML = Event.getEventList(Interna, UserSession, "EV_Code", "*", strSortField,strSortRule, intCurrentPage, 5, orderClass);
        Response.Write(HTML);
    }

    public void event_add()
    {
        CT_Events Ev = new CT_Events();
        Ev.EV_UType = UserSession.User.UG_UType;
        Ev.EV_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        Ev.EV_Created_By = UserSession.User.AU_Code;
        Ev.EV_Updated_By = UserSession.User.AU_Code;
        Ev.EV_Title = GlobalObject.unescape(RequestClass.GetString("EV_Title")).Replace("'", "").Trim();
        Ev.EV_Share = System.Convert.ToBoolean(RequestClass.GetInt("EV_Share",0));
        Ev.EV_Active_Tag = RequestClass.GetInt("EV_Active_Tag",0);
        Ev.EV_Desc = GlobalObject.unescape(RequestClass.GetString("EV_Desc")).Replace("'", "").Trim();
        Ev.EV_Type = RequestClass.GetInt("EV_Type", 0);
        Ev.EV_RP_Code = RequestClass.GetInt("EV_RP_Code", 0);
        Ev.EV_Start_dt = RequestClass.GetFormDateTime("EV_Start_dt");
        Ev.EV_End_dt = RequestClass.GetFormDateTime("EV_End_dt");
        Ev.EV_Method = RequestClass.GetString("EV_Method");
        Ev.EV_Whom = RequestClass.GetInt("EV_Whom", 0);
        Ev.EV_Mess_Type = RequestClass.GetInt("EV_Mess_Type", 0);
        Ev.EV_EG_Code = RequestClass.GetInt("EV_EG_Code", 1);
        Ev.EV_RSVP = System.Convert.ToBoolean(RequestClass.GetInt("EV_RSVP",0));
        Ev.EV_Act_S_dt = RequestClass.GetFormDateTime("EV_Act_S_dt");
        Ev.EV_Act_E_dt = RequestClass.GetFormDateTime("EV_Act_E_dt");
        Ev.EV_Respnsible = GlobalObject.unescape(RequestClass.GetString("EV_Respnsible")).Replace("'", "").Trim();
        Ev.EV_Tools = GlobalObject.unescape(RequestClass.GetString("EV_Tools")).Replace("'", "").Trim();
        Ev.EV_Budget = RequestClass.GetFormDecimal("EV_Budget", 0);
        Ev.EV_Succ_Matrix = GlobalObject.unescape(RequestClass.GetString("EV_Succ_Matrix")).Replace("'", "").Trim();
        Ev.EV_Filename = GlobalObject.unescape(RequestClass.GetString("EV_Filename")).Replace(",", "").Trim();
        Ev.EV_TrackFlag=false;
        Ev.EV_LastRun = Ev.EV_Act_S_dt;
        Ev.PL_Code_List = RequestClass.GetString("PL_Code_List");
        Ev.Pl_Val_List = GlobalObject.unescape(RequestClass.GetString("PL_Val_List")).Replace("'", "").Trim();
        string o_succ = GlobalObject.unescape(RequestClass.GetString("o_succ")).Replace("'", "").Trim();
        MD_SuccMatrixList o = new MD_SuccMatrixList();
        o.SuccMatrixList = JsonConvert.DeserializeObject<List<CT_Succ_Matrix>>(o_succ);
        BL_Event events = new BL_Event();
        int i = events.AddEvent(Ev, o);
        Response.Write(i);
    }
    public void event_Modify()
    {
        CT_Events Ev = new CT_Events();
        Ev.EV_UType = UserSession.User.UG_UType;
        Ev.EV_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        Ev.EV_Created_By = UserSession.User.AU_Code;
        Ev.EV_Updated_By = UserSession.User.AU_Code;
        Ev.EV_Title = GlobalObject.unescape(RequestClass.GetString("EV_Title")).Replace("'", "").Trim();
        Ev.EV_Share = System.Convert.ToBoolean(RequestClass.GetInt("EV_Share", 0));
        Ev.EV_Active_Tag = RequestClass.GetInt("EV_Active_Tag", 0);
        Ev.EV_Desc = GlobalObject.unescape(RequestClass.GetString("EV_Desc")).Replace("'", "").Trim();
        Ev.EV_Type = RequestClass.GetInt("EV_Type", 0);
        Ev.EV_RP_Code = RequestClass.GetInt("EV_RP_Code", 0);
        Ev.EV_Start_dt = RequestClass.GetFormDateTime("EV_Start_dt");
        Ev.EV_End_dt = RequestClass.GetFormDateTime("EV_End_dt");
        Ev.EV_Method = RequestClass.GetString("EV_Method");
        Ev.EV_Whom = RequestClass.GetInt("EV_Whom", 0);
        Ev.EV_Mess_Type = RequestClass.GetInt("EV_Mess_Type", 0);
        Ev.EV_EG_Code = RequestClass.GetInt("EV_EG_Code", 1);
        Ev.EV_RSVP = System.Convert.ToBoolean(RequestClass.GetInt("EV_RSVP", 0));
        Ev.EV_Act_S_dt = RequestClass.GetFormDateTime("EV_Act_S_dt");
        Ev.EV_Act_E_dt = RequestClass.GetFormDateTime("EV_Act_E_dt");
        Ev.EV_Respnsible = GlobalObject.unescape(RequestClass.GetString("EV_Respnsible")).Replace("'", "").Trim();
        Ev.EV_Tools = GlobalObject.unescape(RequestClass.GetString("EV_Tools")).Replace("'", "").Trim();
        Ev.EV_Budget = RequestClass.GetFormDecimal("EV_Budget", 0);
        Ev.EV_Succ_Matrix = GlobalObject.unescape(RequestClass.GetString("EV_Succ_Matrix")).Replace("'", "").Trim();
        Ev.EV_Filename = GlobalObject.unescape(RequestClass.GetString("EV_Filename")).Replace(",", "").Trim();
        Ev.EV_TrackFlag = false;
        Ev.EV_LastRun = Ev.EV_Act_S_dt;
        Ev.EV_Code = RequestClass.GetInt("EV_Code", 0);
        Ev.PL_Code_List = RequestClass.GetString("PL_Code_List");
        Ev.Pl_Val_List = GlobalObject.unescape(RequestClass.GetString("PL_Val_List")).Replace("'", "").Trim();
        string o_succ = GlobalObject.unescape(RequestClass.GetString("o_succ")).Replace("'", "").Trim();
        MD_SuccMatrixList o = new MD_SuccMatrixList();
        o.SuccMatrixList = JsonConvert.DeserializeObject<List<CT_Succ_Matrix>>(o_succ);
        BL_Event events = new BL_Event();
        int i = events.ModifyEvent(Ev, o);
        Response.Write(i);
    }
    public void enent_Delete()
    {
        int EV_Code= RequestClass.GetInt("id", 0);
        if (0 >= EV_Code) Response.Write("-2");//参数 没有全填写
        else
        {
            BL_Event events = new BL_Event();
            int i = events.DeleteEvent(EV_Code);
            Response.Write(i);
        }
    }
}