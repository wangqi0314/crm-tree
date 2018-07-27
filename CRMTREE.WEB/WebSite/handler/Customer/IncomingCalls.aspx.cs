using CRMTree.Public;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using CRMTreeDatabase;
using System.Dynamic;
using PetaPoco;
using System.IO;
using CRMTree.BLL;

public partial class handler_Customer_IncomingCalls : BasePage
{
    protected override void OnLoad(EventArgs e)
    {
        try
        {
            base.OnLoad(e);

            var o = Request.Params["o"];
            var data = JsonConvert.DeserializeObject<dynamic>(o);
            string acion = data.action;
            switch (acion)
            {
                case "Save_Incoming_Calls":
                    Save_Incoming_Calls(data);
                    break;
                case "Get_Action":
                    Get_Action(data);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

    private void Get_Action(dynamic data)
    {
        var db = DBCRMTree.GetInstance();
        var o = db.Query<dynamic>(";exec CT_CommTypes @Dir,@Dept,@Lng"
            , new
            {
                Dir = (int)data.Dir,
                Dept = (int)data.Dept,
                Lng = Interna ? "_EN" : "_CN"
            }
            );

        Response.Write(JsonConvert.SerializeObject(o));
    }

    private void Save_Incoming_Calls(dynamic data)
    {
        //var ug_code = this.UserSession.User.AU_UG_Code;
        //if (ug_code != 28 && ug_code != 40)
        //{
        //    throw new Exception(Interna ? "Without the permission!" : "无此权限！");
        //}

        var db = DBCRMTree.GetInstance();
        try
        {
            using (var tran = db.GetTransaction())
            {
                //CT_Handler
                var s_data = JsonConvert.SerializeObject(data.data);
                if (string.IsNullOrWhiteSpace(s_data))
                {
                    throw new Exception(Interna ? "No data!" : "无数据！");
                }

                CT_Comm_History ch = JsonConvert.DeserializeObject<CT_Comm_History>(s_data);
                ch.CH_UType = (byte)UserSession.User.UG_UType;
                ch.CH_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
                ch.CH_Update_dt = DateTime.Now;
                ch.CH_Code = Convert.ToInt32(ch.Insert());

                CT_Handler handler = JsonConvert.DeserializeObject<CT_Handler>(s_data);
                handler.HD_CH_Code = ch.CH_Code;
                handler.HD_AU_Code = UserSession.User.AU_Code;
                handler.HD_Code = Convert.ToInt32(handler.Insert());

                CT_Dialog_Hist dh = JsonConvert.DeserializeObject<CT_Dialog_Hist>(s_data);
                if (!dh.DH_Duration.HasValue)
                {
                    dh.DH_Duration = -1;
                }
                if (!dh.DH_legacy.HasValue)
                {
                    dh.DH_legacy = false;
                }
                dh.DH_HD_Code = handler.HD_Code;
                dh.DH_Update_dt = DateTime.Now;
                dh.Insert();

                //db.AbortTransaction();
                tran.Complete();
            }
            Response.Write(JsonConvert.SerializeObject(new { isOK = true }));
        }
        catch (Exception ex)
        {
            db.AbortTransaction();
            Response.Write(JsonConvert.SerializeObject(new { isOK = false, msg = ex.Message }));
        }
    }

}