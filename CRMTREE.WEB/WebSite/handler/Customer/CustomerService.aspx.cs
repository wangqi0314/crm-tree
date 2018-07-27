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

public partial class handler_Customer_CustomerService : BasePage
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
                case "Get_CustomerInfo":
                    Get_CustomerInfo(data);
                    break;
                case "GetFileContent":
                    GetFileContent(data);
                    break;
                case "GetFileContentByFileName":
                    GetFileContentByFileName(data);
                    break;
                case "Save_CustomerService":
                    Save_CustomerService(data);
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

    private void Save_CustomerService(dynamic data)
    {
        //var ug_code = this.UserSession.User.AU_UG_Code;
        //if (ug_code != 28 && ug_code != 40)
        //{
        //    throw new Exception(Interna ? "Without the permission!" : "无此权限！");
        //}

        var db = DBCRMTree.GetInstance();
        var timerSet = false;
        try
        {
            using (var tran = db.GetTransaction())
            {
                //CT_Handler
                var s_hd = JsonConvert.SerializeObject(data.data);
                if (string.IsNullOrWhiteSpace(s_hd))
                {
                    throw new Exception(Interna ? "No History!" : "无信息！");
                }
                CT_Handler handler = JsonConvert.DeserializeObject<CT_Handler>(s_hd);
                var hd = CT_Handler.SingleOrDefault(handler.HD_Code);

                //CT_Comm_History
                var ch = CT_Comm_History.SingleOrDefault(hd.HD_CH_Code);

                //Count the number of similar Actions
                var o = db.SingleOrDefault<dynamic>(@"SELECT count(*) as Act_cnt from CT_Dialog_Hist where DH_legacy=1 and DH_HD_Code=@0 and DH_Click=@1", hd.HD_Code, handler.HD_Action);
                //o.Act_cnt

                //Get Dealer Limits and call back duration
                int Start, End;
                int WaitTime = 15;
                int MaxCnt = 2;
                var strStart = "||" + handler.HD_Action.ToString() + ",";
                if (UserSession.Dealer.AD_Call_Limits.Contains(strStart))
                {
                    Start = UserSession.Dealer.AD_Call_Limits.IndexOf(strStart, 0) + strStart.Length;
                    End = UserSession.Dealer.AD_Call_Limits.IndexOf("||", Start);
                    string tStr = UserSession.Dealer.AD_Call_Limits.Substring(Start, End - Start);
                    MaxCnt = System.Convert.ToInt32(tStr.Split(',')[0]);
                    WaitTime = System.Convert.ToInt32(tStr.Split(',')[1]);
                }

                var s_ct = JsonConvert.SerializeObject(data.data);
                //Clear all existing reminders for this person
                db.Execute("UPDATE CT_Reminder_Timers SET RT_Status=0 WHERE RT_Type=1 AND [RT_AU_Code]=" + hd.HD_AU_Code + " AND [RT_Pointer]=" + hd.HD_Code + " AND RT_STatus=1");
                var rt = JsonConvert.DeserializeObject<CT_Reminder_Timers>(s_ct);
                rt.RT_Pointer = hd.HD_Code;
                rt.RT_AU_Code = hd.HD_AU_Code;
                rt.RT_Status = 1;
                rt.RT_Type = 1;
                if (rt.RT_Time != null)
                {
                    rt.Insert();//Set the new timer
                    timerSet = true;
                }
                //var s_ct = JsonConvert.SerializeObject(data.data);
                //var ct = JsonConvert.DeserializeObject<CT_Callback_Sch>(s_ct);
                //ct.CT_CH_Code = hd.HD_CH_Code;
                //CT_Callback_Sch.Delete("where CT_CH_Code=@0", ct.CT_CH_Code);
                //ct.Insert();

                //Save the process into CT_Dialog History, before hd.HD_Action is changed.
                var prev_Action = hd.HD_Action;
                var prev_Status = ch.CH_Status;
                hd.HD_Action = handler.HD_Action;

                Save_Dialog(data, hd);


                //call me back
                if (handler.HD_Action == 31  || handler.HD_Action == 40)
                {
                    hd.HD_Code = 0;
                    hd.HD_Action = 0;
                    hd.HD_Code = Convert.ToInt32(hd.Insert());

                    ch.CH_Status = 5;
                    if (!timerSet) rt.Insert();//Set the new timer
                }
                //Do Not Call, Do not use this number
                else if (handler.HD_Action == 25 || handler.HD_Action == 27)
                {
                    hd.HD_Action = handler.HD_Action;
                    hd.Update(new string[] { "HD_Action" });
 
                    ch.CH_Status = 10;

                    var s = JsonConvert.SerializeObject(data.data);
                    var phone = JsonConvert.DeserializeObject<CT_Phone_List>(s);
                    phone.PL_Update_dt = DateTime.Now;
                    phone.PL_DonotUse = true;
                    phone.Update(new string[]{
                                "PL_Update_dt",
                                "PL_DonotUse"
                                });
                    //Cancel all the current calls to this phone number
                    db.Execute("UPDATE CT_Reminder_Timers SET RT_Status=0 WHERE RT_Type=1 AND [RT_AU_Code]=" + hd.HD_AU_Code + " AND [RT_Pointer]=" + hd.HD_Code + " AND RT_STatus=1");
                    db.Execute("Update CT_Handler Set HD_Action=95 where HD_CH_Code in (Select CH_Code from CT_Comm_History where CH_Status <10 and CH_Type in (3,4,5,6) and [CH_ML_PL_Code] = @0)", phone.PL_Code);
                    db.Execute("Update CT_Comm_History set CH_Update_dt=GetDate(), CH_Status =35 WHERE CH_Status <10 and CH_Type in (3,4,5,6) and [CH_ML_PL_Code] = @0", phone.PL_Code);

                }
                // Duplicate Call
                else if (handler.HD_Action == 22)
                {
                    hd.HD_Action = handler.HD_Action;
                    hd.Update(new string[] { "HD_Action" });

                    ch.CH_Status = 10;

                    var s = JsonConvert.SerializeObject(data.data);
                    var phone = JsonConvert.DeserializeObject<CT_Phone_List>(s);
                    phone.PL_Update_dt = DateTime.Now;
                    phone.PL_DonotUse = false;
                    phone.Update(new string[]{
                                "PL_Update_dt",
                                "PL_DonotUse"
                                });
                    //Cancel all the current calls to this phone number
                    db.Execute("UPDATE CT_Reminder_Timers SET RT_Status=0 WHERE RT_Type=1 AND [RT_AU_Code]=" + hd.HD_AU_Code + " AND [RT_Pointer]=" + hd.HD_Code + " AND RT_STatus=1");
                    db.Execute("Update CT_Handler Set HD_Action=95 where HD_CH_Code in (Select CH_Code from CT_Comm_History where CH_Status <10 and CH_Type in (3,4,5,6) and [CH_ML_PL_Code] = @0)", phone.PL_Code);
                    db.Execute("Update CT_Comm_History set CH_Update_dt=GetDate(), CH_Status =35 WHERE CH_Status <10 and CH_Type in (3,4,5,6) and [CH_ML_PL_Code] = @0", phone.PL_Code);

                }
                //Phone Change,Do not use this number, Invalid #, Wrong#, Not Owner(Will not provide info)
                else if (handler.HD_Action == 41 || handler.HD_Action == 42 || handler.HD_Action == 44 || handler.HD_Action == 45)
                {
                    hd.HD_Action = handler.HD_Action;
                    hd.Update(new string[] { "HD_Action" });

                    ch.CH_Status = 10;

                    var s = JsonConvert.SerializeObject(data.data);
                    var phone = JsonConvert.DeserializeObject<CT_Phone_List>(s);
                    phone.PL_Update_dt = DateTime.Now;
                    phone.PL_Active = false;
                    phone.Update(new string[]{
                                "PL_Update_dt",
                                "PL_Active"
                                });
                    //Cancel all the current calls to this phone number
                    db.Execute("UPDATE CT_Reminder_Timers SET RT_Status=0 WHERE RT_Type=1 AND [RT_AU_Code]=" + hd.HD_AU_Code + " AND [RT_Pointer]=" + hd.HD_Code + " AND RT_STatus=1");
                    db.Execute("Update CT_Handler Set HD_Action=95 where HD_CH_Code in (Select CH_Code from CT_Comm_History where CH_Status <10 and CH_Type in (3,4,5,6) and [CH_ML_PL_Code] = @0)", phone.PL_Code);
                    db.Execute("Update CT_Comm_History set CH_Update_dt=GetDate(), CH_Status =35 WHERE CH_Status <10 and CH_Type in (3,4,5,6) and [CH_ML_PL_Code] = @0", phone.PL_Code);

                }
                 //busy, Nobody Answered, Stop Service,Phone Shutdown,   Wiat one day after 3 times, 2 days after 5 times,...
                else if (handler.HD_Action == 10 || handler.HD_Action == 15 || handler.HD_Action == 16 || handler.HD_Action == 14)
                {
                    if ((o.Act_cnt+1) >= MaxCnt )
                    {
                        ch.CH_Status = 10;
                    }
                    else
                    {
                        ch.CH_Status = 5;
                        hd.Update(new string[] { "HD_Action" });
                        rt.RT_Time = DateTime.Now.AddMinutes(WaitTime);
                        if (!timerSet) rt.Insert();//Set the new timer
                    }

                }
                //Car Sold, Customer REfused to continue
                else if (handler.HD_Action == 50 || handler.HD_Action == 38)
                {
                    hd.HD_Action = handler.HD_Action;
                    hd.Update(new string[] { "HD_Action" });

                    ch.CH_Status = 10;

                    var s = JsonConvert.SerializeObject(data.data);
                    var car = JsonConvert.DeserializeObject<CT_Car_Inventory>(s);
                    car.CI_Update_dt = DateTime.Now;
                    car.CI_Activate_Tag = 0;
                    car.Update(new string[]{
                                "CI_Update_dt",
                                "CI_Activate_Tag"
                                });
                    //Cancel all the campaigns for this car
                    db.Execute("UPDATE CT_Reminder_Timers SET RT_Status=0 WHERE RT_Type=1 AND [RT_AU_Code]=" + hd.HD_AU_Code + " AND [RT_Pointer]=" + hd.HD_Code + " AND RT_STatus=1");
                    db.Execute("Update CT_Handler Set HD_Action=95 where HD_CH_Code in (Select CH_Code from CT_Comm_History where CH_Status <10 and CH_Type in (3,4,5,6) and CH_CI_Code = @0)", car.CI_Code);
                    db.Execute("Update CT_Comm_History set CH_Update_dt=GetDate(), CH_Status =35 WHERE CH_Status <10 and CH_Type in (3,4,5,6) and CH_CI_Code = @0", car.CI_Code);
                }
                else  //Others
                {
                    hd.HD_Action = handler.HD_Action;
                    hd.Update(new string[] { "HD_Action" });

                    //Dialog
            //        Save_Dialog(data, hd);

                    ch.CH_Status = 10;
                }

                //db.AbortTransaction();
                ch.CH_Update_dt = DateTime.Now;
                ch.Update(new string[] { "CH_Status", "CH_Update_dt" });
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

    private void Save_Dialog(dynamic data, CT_Handler hd)
    {
        var s_dh = JsonConvert.SerializeObject(data.data);
        if (string.IsNullOrWhiteSpace(s_dh))
        {
            return;
        }

        CT_Dialog_Hist dh = JsonConvert.DeserializeObject<CT_Dialog_Hist>(s_dh);
        dh.DH_HD_Code = hd.HD_Code;
        dh.DH_Click = hd.HD_Action;
        dh.DH_Update_dt = DateTime.Now;
        dh.Insert();
    }

    private void GetFileContent(dynamic data)
    {
        string fileContent = string.Empty;

        var exParams = new List<CRMTreeDatabase.EX_Param>();
        var s_q = JsonConvert.SerializeObject(data.queryParams);
        if (!string.IsNullOrWhiteSpace(s_q))
        {
            exParams = JsonConvert.DeserializeObject<List<CRMTreeDatabase.EX_Param>>(s_q);
        }
        var CG_Code = (int)data.CG_Code;
        fileContent = BL_Reports.GetFileContent(CG_Code, exParams);
        Response.Write(fileContent);
    }

    private void GetFileContentByFileName(dynamic data)
    {
        string fileContent = string.Empty;

        //var exParams = new List<CRMTreeDatabase.EX_Param>();
        //var s_q = JsonConvert.SerializeObject(data.queryParams);
        //if (!string.IsNullOrWhiteSpace(s_q))
        //{
        //    exParams = JsonConvert.DeserializeObject<List<CRMTreeDatabase.EX_Param>>(s_q);
        //}
        var FileName = (string)data.FileName;
        var CG_Code = (int)data.CG_Code;
        var AU_Code = (int)data.AU_Code;
        fileContent = BL_Reports.GetFileContent(FileName, new
        {
            AU = AU_Code,
            CG = CG_Code,
            AD = null != UserSession.Dealer ? UserSession.Dealer.AD_Code : 0,
            DG = null != UserSession.Dealer ? UserSession.Dealer.AD_DG_Code : 0,
            OM = null != UserSession.OEM ? UserSession.OEM.OM_Code : 0,
            CT = 0,
            CI = 0,
            DE = null != UserSession.User ? UserSession.User.AU_Code : 0,
            AS = 0,
            PA = 0
        });
        Response.Write(fileContent);
    }

    private void Get_CustomerInfo(dynamic data)
    {
        dynamic o = new ExpandoObject();

        var hd_code = (int)data.HD_Code;

        var db = DBCRMTree.GetInstance();
        o.service = db.SingleOrDefault<dynamic>(@"SELECT hd.*
        ,ch.*
        ,ch.CH_CI_Code as CI_Code
        ,AU_Name
        ,PL_Number
        ,PL_Code
        ,CM_FileName
        ,CG_Type
        ,CG_Cat
        FROM CT_Handler hd 
        LEFT JOIN CT_Comm_History ch ON hd.HD_CH_Code = ch.CH_Code
        LEFT JOIN CT_All_Users au ON ch.CH_AU_Code = au.AU_Code
        LEFT JOIN CT_Phone_List pl ON ch.CH_ML_PL_Code = pl.PL_Code
        LEFT JOIN CT_Campaigns on ch.CH_CG_Code=CG_Code 
        LEFT JOIN CT_Camp_Methods on ch.CH_CG_Code=CM_CG_Code and ch_CG_M_Indx=CM_Contact_Index
        WHERE HD_Code = @0 ", hd_code);

        if (null != o.service)
        {
            o.fileContent = BL_Reports.GetFileContent(o.service.CH_CG_Code, o.service.CH_CG_M_Indx, new
            {
                AU = o.service.CH_AU_Code,
                CG = o.service.CH_CG_Code,
                AD = null != UserSession.Dealer ? UserSession.Dealer.AD_Code : 0,
                DG = null != UserSession.Dealer ? UserSession.Dealer.AD_DG_Code : 0,
                OM = null != UserSession.OEM ? UserSession.OEM.OM_Code : 0,
                CT = 0,
                CI = 0,
                DE = o.service.HD_AU_Code,
                AS = 0,
                PA = 0
            });
        }

        Response.Write(JsonConvert.SerializeObject(o));
    }

}