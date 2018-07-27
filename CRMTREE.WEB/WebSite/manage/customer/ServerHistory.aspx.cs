using CRMTree.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_customer_ServerHistory : BasePage
{
    static long userCode = -1;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            userCode = UserSession.User.AU_Code;
        }
    }
    [WebMethod]
    public static Object getMyServiceHis(string CI_Code)
    {
        CRMTree.Model.CT_History_Service historyService = new CRMTree.Model.CT_History_Service();
        historyService.HS_CI_Code = int.Parse(CI_Code);
        historyService.HS_AU_Code = userCode;
        CRMTree.BLL.BL_ServerHistory myServerHistory = new CRMTree.BLL.BL_ServerHistory();
        CRMTree.Model.ServerHistory.MD_ServerHistory serverHistory = myServerHistory.getMyServiceHis(historyService);
        return serverHistory;
    }
    [WebMethod]
    public static Object getMyServiceHisSearch(string CI_Code,string BeginDate,string EndDate)
    {
        CRMTree.Model.CT_History_Service historyService = new CRMTree.Model.CT_History_Service();
        historyService.HS_CI_Code = int.Parse(CI_Code);
        historyService.HS_AU_Code = userCode;
        historyService.BeginDate = Convert.ToDateTime(BeginDate);
        historyService.EndDate = Convert.ToDateTime(EndDate); 
        CRMTree.BLL.BL_ServerHistory myServerHistory = new CRMTree.BLL.BL_ServerHistory();
        CRMTree.Model.ServerHistory.MD_ServerHistory serverHistory = myServerHistory.getMyServiceHis(historyService);
        return serverHistory;
    }
    [WebMethod]
    public static Object getMyServiceHisInfo(string HS_Code)
    {
        CRMTree.Model.CT_History_Service historyService = new CRMTree.Model.CT_History_Service();
        historyService.HS_Code = int.Parse(HS_Code);
        CRMTree.BLL.BL_ServerHistory myServerHistory = new CRMTree.BLL.BL_ServerHistory();
        CRMTree.Model.CT_History_Service serverHistory = myServerHistory.getMyServiceHisInfo(historyService);
        return serverHistory;
    }
}