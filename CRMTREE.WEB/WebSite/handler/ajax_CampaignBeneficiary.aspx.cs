using CRMTree.Public;
using CRMTree.BLL;
using CRMTree.Model.Campaigns;
using Shinfotech.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.IO;
using System.Data;
using ShInfoTech.Common;

public partial class handler_ajax_CampaignBeneficiary : BasePage
{
    public static long AU_Code = -1;
    public static int AU_Type = -1;
    private static int AU_AD_OM_Code = -1;
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            AU_Code = UserSession.User.AU_Code;
            AU_Type = UserSession.User.UG_UType;
            AU_AD_OM_Code = UserSession.DealerEmpl.DE_AD_OM_Code;
        }
        string strAction = RequestClass.GetString("action").ToLower();//操作动作 
        if (strAction == "phone_list" || strAction == "messaging_list" || strAction == "email_list")
        {
            switch (strAction)
            {
                case "phone_list":
                    Phone_List();
                    break;
                case "messaging_list":
                    Messaging_List();
                    break;
                case "email_list":
                    Email_List();
                    break;
            }
        }
        else
            Response.Write("-1");//action 参数不正确，没权限
    }
    private void Phone_List()
    {
        StringBuilder sbList = new StringBuilder();
        sbList.Append("<table class=\"t-bd\"><tr class=\"ads\">");
        sbList.Append("<th class=\"w40 ads\">Sequence </th>");
        sbList.Append("<th>Title</th>");
        sbList.Append("<th style='width:10px;'>Name</th>");
        sbList.Append("<th>Phone</th>");
        sbList.Append("<th class=\"w100\">Operate</th></tr>");

        BL_Campaign Cam = new BL_Campaign();
        MD_BeneficiaryList beneficiaryList = Cam.getBeneficiary_PhoneList(UserSession.User.AU_Code);
        if (beneficiaryList == null || beneficiaryList.CampaignBeneficiaryList.Count <= 0)
        {
            sbList.Append("<tr ><td colspan=\"10\"><div class=\"noData\">No matching data！</div></td></tr>");
        }
        else
        {
            for (int i = 0; i < beneficiaryList.CampaignBeneficiaryList.Count; i++)
            {
                sbList.Append("<tr><td>");
                sbList.Append(i + 1);
                sbList.Append("</td><td>");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Title);
                sbList.Append("</td><td>");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].AU_Name);
                sbList.Append("</td><td>");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].PL_Number);
                sbList.Append("</td><td>");
                sbList.Append("<a href=\"javascript:;\"><i class=\"Phone\" title=\"Phone\" onclick=\"SendPhone(\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].AU_Code);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Code);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Filename);
                sbList.Append("\')\"></i></a>");
                sbList.Append("</td></tr>");
            }
        }
        sbList.Append("</table>");
        Response.Write(sbList);
    }
    private void Messaging_List()
    {
        StringBuilder sbList = new StringBuilder();
        sbList.Append("<table class=\"t-bd\"><tr>");
        sbList.Append("<th class=\"w40\">Sequence </th>");
        sbList.Append("<th>Title</th>");
        sbList.Append("<th>Name</th>");
        sbList.Append("<th>Messaging</th>");
        sbList.Append("<th class=\"w100\">Operate</th></tr>");

        BL_Campaign Cam = new BL_Campaign();
        MD_BeneficiaryList beneficiaryList = Cam.getBeneficiary_MessagingList(UserSession.User.AU_Code);
        if (beneficiaryList == null || beneficiaryList.CampaignBeneficiaryList.Count <= 0)
        {
            sbList.Append("<tr ><td colspan=\"10\"><div class=\"noData\">No matching data！</div></td></tr>");
        }
        else
        {
            for (int i = 0; i < beneficiaryList.CampaignBeneficiaryList.Count; i++)
            {
                sbList.Append("<tr><td>");
                sbList.Append(i + 1);
                sbList.Append("</td><td>");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Title);
                sbList.Append("</td><td>");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].AU_Name);
                sbList.Append("</td><td>");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].ML_Handle);
                sbList.Append("</td><td>");
                sbList.Append("<a href=\"javascript:;\"><i class=\"Mesageing\" title=\"Mesageing\" onclick=\"SendMesageing(\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].AU_Code);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Code);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Filename);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].ML_Handle);
                sbList.Append("\')\"></i></a>");
                sbList.Append("</td></tr>");
            }
        }
        sbList.Append("</table>");
        Response.Write(sbList);
    }
    private void Email_List()
    {
        StringBuilder sbList = new StringBuilder();
        sbList.Append("<table class=\"t-bd\"><tr>");
        sbList.Append("<th class=\"w40\">Sequence </th>");
        sbList.Append("<th>Title</th>");
        sbList.Append("<th>Name</th>");
        sbList.Append("<th>Email</th>");
        sbList.Append("<th class=\"w100\">Operate</th></tr>");

        BL_Campaign Cam = new BL_Campaign();
        MD_BeneficiaryList beneficiaryList = Cam.getBeneficiary_EmailList(UserSession.User.AU_Code);
        if (beneficiaryList == null || beneficiaryList.CampaignBeneficiaryList.Count <= 0)
        {
            sbList.Append("<tr ><td colspan=\"10\"><div class=\"noData\">No matching data！</div></td></tr>");
        }
        else
        {
            for (int i = 0; i < beneficiaryList.CampaignBeneficiaryList.Count; i++)
            {
                sbList.Append("<tr><td>");
                sbList.Append(i + 1);
                sbList.Append("</td><td>");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Title);
                sbList.Append("</td><td>");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].AU_Name);
                sbList.Append("</td><td>");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].EL_Address);
                sbList.Append("</td><td>");
                sbList.Append("<a href=\"javascript:;\"><i class=\"Email\" title=\"Email\" onclick=\"SendEmail(\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].EL_Address);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].AU_Code);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Code);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Filename);
                sbList.Append("\')\"></i></a>");
                sbList.Append("<a href=\"javascript:;\"><i class=\"EmailView\" title=\"EmailView\" onclick=\"EmailView(\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].AU_Code);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Code);
                sbList.Append("\',\'");
                sbList.Append(beneficiaryList.CampaignBeneficiaryList[i].CG_Filename);
                sbList.Append("\')\"></i></a>");
                sbList.Append("</td></tr>");
            }
        }
        sbList.Append("</table>");
        Response.Write(sbList);
    }

    [WebMethod]
    public static string SendPhone(string User_Code,string CG_Code, string filename)
    {
        string strLine = string.Empty;
        BL_Campaign Cam = new BL_Campaign();
        DataTable dt_query = Cam.getfile_Rep_value(int.Parse(CG_Code), AU_Type, AU_AD_OM_Code);
        DataTable User_Dt = Parameter_Replace.getUser_value(dt_query, User_Code);
        DataTable Tag_Dt= Cam.getCam_tags();        
        string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
        strLine = ShInfoTech.Common.Files.FileContext(strPath, filename);
        strLine=Parameter_Replace.ReplaceContents(strLine, Tag_Dt, User_Dt);
        return strLine;
    }
    [WebMethod]
    public static string SendMesageing(string User_Code, string CG_Code, string filename, string mobile)
    {
        string strLine = string.Empty;
        BL_Campaign Cam = new BL_Campaign();
        DataTable dt_query = Cam.getfile_Rep_value(int.Parse(CG_Code), AU_Type, AU_AD_OM_Code);
        DataTable User_Dt = Parameter_Replace.getUser_value(dt_query, User_Code);
        DataTable Tag_Dt = Cam.getCam_tags();
        string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
        strLine = ShInfoTech.Common.Files.FileContext(strPath, FileConversion(filename));
        strLine = Parameter_Replace.ReplaceContents(strLine, Tag_Dt, User_Dt);
        //SendMessage.Send("18516147361", strLine);
        return strLine;
    }
    [WebMethod]
    public static string sendEmail(string email, string User_Code, string CG_Code, string filename)
    {
        string strLine = string.Empty;
        BL_Campaign Cam = new BL_Campaign();
        DataTable dt_query = Cam.getfile_Rep_value(int.Parse(CG_Code), AU_Type, AU_AD_OM_Code);
        DataTable User_Dt = Parameter_Replace.getUser_value(dt_query, User_Code);
        DataTable Tag_Dt = Cam.getCam_tags();
        string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
        strLine = ShInfoTech.Common.Files.FileContext(strPath, filename);
        strLine = Parameter_Replace.ReplaceContents(strLine, Tag_Dt, User_Dt);
        if (string.IsNullOrEmpty(strLine))
        {
            return "-1";
        }
        bool isSur=Mail.SendMail("wangqi_5203344@163.com", "CRMTree_Mail", strLine, null, "wangqi@shinfotech.cn", "shinfotech", "wangqi@shinfotech.cn", "mail.shinfotech.cn");
        if (!isSur)
        {
            return "-2";
        }
        return "0";       
    }
     [WebMethod]
    public static string EmailView(string User_Code, string CG_Code, string filename)
    {
        string strLine = string.Empty;
        BL_Campaign Cam = new BL_Campaign();
        DataTable dt_query = Cam.getfile_Rep_value(int.Parse(CG_Code), AU_Type, AU_AD_OM_Code);
        DataTable User_Dt = Parameter_Replace.getUser_value(dt_query, User_Code);
        DataTable Tag_Dt = Cam.getCam_tags();
        string strPath = System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"];
        strLine = ShInfoTech.Common.Files.FileContext(strPath, filename);
        strLine = Parameter_Replace.ReplaceContents(strLine, Tag_Dt, User_Dt);
        return strLine;
    }

    private static string FileConversion(string fileName)
    {
        fileName = fileName.ToUpper();
        fileName = fileName.Replace(".HTML", ".txt");
        fileName = fileName.Replace(".HTM", ".txt");
        return fileName;
    }
}