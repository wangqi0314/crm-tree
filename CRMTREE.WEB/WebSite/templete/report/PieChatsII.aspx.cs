using CRMTree.BLL;
using CRMTree.Model;
using CRMTree.Model.Common;
using CRMTree.Public;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class templete_report_PieChats : BasePage
{
    public string MaxWidth = string.Empty;
    public string Maxheigth = string.Empty;
    public string PieData = string.Empty;
    public string PieTitle = string.Empty;
    public string Legend = string.Empty;  
    public int RP_Code = -1;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            setQres();
            setPieData();
            setPieTitle();
        }
    }
    private void setQres()
    {
        MaxWidth = (int.Parse(Request.QueryString["width"].ToString()) - 50).ToString();
        Maxheigth = (int.Parse(Request.QueryString["height"].ToString()) - 0).ToString();
        //        te = @"[{
        //                type: 'pie',
        //                name: 'Browser share',
        //                data: [
        //                    ['a', 30], 
        //                    ['b', 40],
        //                    {   
        //                        name: 'c',
        //                        y: 10, 
        //                        sliced: true,
        //                        selected: true
        //                    },
        //                    ['d', 20]
        //                ]
        //            }]";
    }
    private void setPieData()
    {
        int MF_FL_FB_Code = int.Parse(Request.QueryString["MF_FL_FB_Code"].ToString());

        int PR = 0;
        if (null != Request.QueryString["PR"])
        {
            PR = int.Parse(Request.QueryString["PR"].ToString());
        }

        BL_HighCharts HC = new BL_HighCharts();
        DataTable dt = HC.getPieData(MF_FL_FB_Code, out RP_Code, PR);
        StringBuilder SB = new StringBuilder();
        SB.Append("[{type: 'pie',");
        SB.Append("name: '',");
        SB.Append("data: [");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == dt.Rows.Count - 1)
            {
                SB.Append("{ ");
                SB.Append("name: '" + dt.Rows[i][0] + "', ");
                SB.Append(" y:  " + dt.Rows[i][1] + ", ");
                SB.Append(" sliced: true, ");
                SB.Append(" selected: true ");
                SB.Append(" } ");
            }
            else
            {
                SB.Append("['" + dt.Rows[i][0] + "', " + dt.Rows[i][1] + "],");
            }
        }
        SB.Append("]");
        SB.Append("}]");
        PieData = SB.ToString();
    }
    private void setPieTitle()
    {
        //int RP_Code = int.Parse(Request.QueryString["MF_FL_FB_Code"].ToString());
        //int pr = 0;
        //try
        //{
        //    pr = int.Parse(Request.QueryString["PR"].ToString());
        //}
        //catch (Exception)
        //{
        //    pr = 0;
        //}
        //BL_HighCharts HC = new BL_HighCharts();
        //DataTable dt = HC.getPieTitle(RP_Code,pr);
        //if (dt != null || dt.Rows.Count != 0)
        //{
        //    PieTitle = dt.Rows[0]["FP_Title_EN"].ToString();
        //}
        int PR = RequestClass.GetInt("PR", 0);
        CT_Reports Re = new BL_Reports().GetReplaceReport(Interna,
                   new CT_Param_Value() { RP_Code = RP_Code, PV_Type = PR, PV_CG_Code = -1, PV_UType = UserSession.User.UG_UType, PV_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code });
        //string _title = HttpUtility.UrlDecode(RequestClass.GetString("Title"), Encoding.UTF8);
        if (!string.IsNullOrEmpty(Re.RP_Name_EN))
        {
            PieTitle = Re.RP_Name_EN;
        }
    }
}