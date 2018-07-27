using CRMTree.Model.MyCar;
using System;
using System.Text;
using CRMTree.Public;
using Shinfotech.Tools;
using System.Data;
using Resources;
using System.Net.Mail;
using System.Web.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using CRMTree.BLL;
using CRMTree.Model.Appointmens;
using CRMTree.Model.Adviser;
using CRMTree.Model.Surveys;

public partial class customer_Dashboard : BasePage
{
    static long userCode = -1;
    protected string AppointmenEn = "";
    protected string Surveys = "";
    protected string AppointmenEndd = "";
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            this.top1.UserID = UserSession.User.AU_Code;
            userCode = UserSession.User.AU_Code;
            getMyCar();
            getCommonApplication();
        }
    }
    /// <summary>
    /// 加载Car信息
    /// </summary>
    private void getMyCar()
    {
        BL_MyCar myCar = new BL_MyCar();
        MD_CarList myCarList = myCar.GetCarList(UserSession.User.AU_Code);
        if (myCarList != null)
        {
            //绑定
            this.myCarInfoList.DataSource = myCarList.Car_Inventory_List;
            this.myCarInfoList.DataBind();
        }
    }
    private void getCommonApplication()
    {
        BL_Appt_Service myAppointmens = new BL_Appt_Service();
        MD_Appointmens App = myAppointmens.getAppointment(UserSession.User.AU_Code);
        if (App == null)
        { AppointmenEn = "No Upcoming Appointment"; }
        else
        {
            AppointmenEn = App.AP_Time.ToString("MM/dd/yyyy HH:mm") + " " + App.CS_Style_EN;
        }
        BL_Surveys sur = new CRMTree.BLL.BL_Surveys();
        MD_SurveysList surList = sur.getSurveys(UserSession.User.AU_Code);
        if (surList == null)
        {
            Surveys += "<li>-";
            Surveys += " ";
            Surveys += "</li>";
        }
        else
        {
            for (int i = 0; i < surList.Surveys_List.Count; i++)
            {
                Surveys += "<li>-";
                Surveys += surList.Surveys_List[i].CG_Title + " ";
                Surveys += surList.Surveys_List[i].Dealer_Name + " ";
                Surveys += surList.Surveys_List[i].CH_Update_dt.ToString("MM/dd/yyyy");
                Surveys += "</li>";
            }
        }

    }
    //根据用户的汽车获取推荐的顾问
    [WebMethod]
    public static Object getCarRecommendAdviser(string Car_CI_Code)
    {
        BL_Advisers Adviser = new BL_Advisers();
        MD_AdviseList AdList = Adviser.getRecommendAdviser(userCode, int.Parse(Car_CI_Code));
        return AdList;
    }
    [WebMethod]
    public static Object getAdviserMessage(string AU_AD_Code)
    {
        BL_Advisers Adviser = new BL_Advisers();
        MD_Adviser Ad = Adviser.getAdviserMessage(int.Parse(AU_AD_Code));
        return Ad;
    }

    [WebMethod]
    public static string SendEmail(string Title, string Content)
    {
        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        message.From = new MailAddress("wangqi@shinfotech.cn");//必须是提供smtp服务的邮件服务器 
        message.To.Add(new MailAddress("wangqi_5203344@163.com"));
        message.Subject = Title;
        //message.CC.Add(new MailAddress("test@126.com"));
        //message.Bcc.Add(new MailAddress("test@126.com"));
        message.IsBodyHtml = true;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Body = Content;
        message.Priority = System.Net.Mail.MailPriority.High;
        SmtpClient client = new SmtpClient("mail.shinfotech.cn", 25); // 587;//Gmail使用的端口 
        client.Credentials = new System.Net.NetworkCredential("wangqi@shinfotech.cn", "shinfotech"); //这里是申请的邮箱和密码 
        client.EnableSsl = false; //必须经过ssl加密 
        try
        {
            client.Send(message);
            return "The message has been sent successfully to<br />" + message.To.ToString();
        }
        catch (Exception ee)
        {
            return "Send mail failed" + ee.Message;
        }
    }

    private string SendSuccess(string Title, string Content)
    {
        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        message.From = new MailAddress("wangqi@shinfotech.cn");//必须是提供smtp服务的邮件服务器 
        message.To.Add(new MailAddress("wangqi_5203344@163.com"));
        message.Subject = "测试邮件";
        //message.CC.Add(new MailAddress("test@126.com"));
        //message.Bcc.Add(new MailAddress("test@126.com"));
        message.IsBodyHtml = true;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Body = "邮件发送测试";
        message.Priority = System.Net.Mail.MailPriority.High;
        SmtpClient client = new SmtpClient("mail.shinfotech.cn", 25); // 587;//Gmail使用的端口 
        client.Credentials = new System.Net.NetworkCredential("wangqi@shinfotech.cn", "shinfotech"); //这里是申请的邮箱和密码 
        client.EnableSsl = false; //必须经过ssl加密 
        try
        {
            client.Send(message);
            return "邮件已经成功发送到" + message.To.ToString();
        }
        catch (Exception ee)
        {
            return "邮件发送失败" + ee.Message;
        }
    }
    /// <summary> 
    /// 发送邮件程序 
    /// </summary> 
    /// <param name="from">发送人邮件地址</param> 
    /// <param name="fromname">发送人显示名称</param> 
    /// <param name="to">发送给谁（邮件地址）</param> 
    /// <param name="subject">标题</param> 
    /// <param name="body">内容</param> 
    /// <param name="username">邮件登录名</param> 
    /// <param name="password">邮件密码</param> 
    /// <param name="server">邮件服务器</param> 
    /// <param name="fujian">附件</param> 
    /// <returns>send ok</returns> 
    /// 调用方法 SendMail("abc@126.com", "某某人", "cba@126.com", "你好", "我测试下邮件", "邮箱登录名", "邮箱密码", "smtp.126.com", ""); 
    private string SendMail(string from, string fromname, string to, string subject, string body, string username, string password, string server, string fujian)
    {
        try
        {
            //邮件发送类 
            MailMessage mail = new MailMessage();
            //是谁发送的邮件 
            mail.From = new MailAddress(from, fromname);
            //发送给谁 
            mail.To.Add(to);
            //标题 
            mail.Subject = subject;
            //内容编码 
            mail.BodyEncoding = Encoding.Default;
            //发送优先级 
            mail.Priority = MailPriority.High;
            //邮件内容 
            mail.Body = body;
            //是否HTML形式发送 
            mail.IsBodyHtml = true;
            //附件 
            if (fujian.Length > 0)
            {
                mail.Attachments.Add(new Attachment(fujian));
            }
            //邮件服务器和端口 
            SmtpClient smtp = new SmtpClient(server, 25);
            smtp.UseDefaultCredentials = true;
            //指定发送方式 
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //指定登录名和密码 
            smtp.Credentials = new System.Net.NetworkCredential(username, password);
            //超时时间 
            smtp.Timeout = 10000;
            smtp.Send(mail);
            return "send ok";
        }
        catch (Exception exp)
        {
            return exp.Message;
        }
    }
}