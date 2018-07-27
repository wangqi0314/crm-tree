using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MigrationService.IService;
using System.Data;
using MigrationService.ImplementMethod;
using System.IO;
using MigrationService.DBConnection;
using System.Data.SqlClient;
using System.Collections;
using System.Net.Mail;
using System.Net;

using System.Security.Permissions;
using System.Windows.Forms;
using MigrationService.Helper;
using System.Security.Cryptography;


namespace ManullyModification
{

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

    [System.Runtime.InteropServices.ComVisibleAttribute(true)] 
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            dataSourceBind();

             

            dataSourceBindForItemSource();
        }

        private string queryRules = "select * from CT_DataMapping_MyFields";
        private string queryGroupMapping = "select * from CT_DataMapping_Group";
        private string queryResourceFields = "select * from CT_DataMapping_MigrationResouceFilelds";
        private SqlDataAdapter sda_PC_HD = new SqlDataAdapter();
        private SqlDataAdapter sda_PC_LN = new SqlDataAdapter();
        private SqlDataAdapter sda_PC_FM = new SqlDataAdapter();
        private DataSet ds = new DataSet();

        //private SqlDataAdapter sda_PC_HD = new SqlDataAdapter();

        private void dataSourceBindForItemSource()
        {
           
            System.Windows.Controls.Button bt = new System.Windows.Controls.Button();

            string query = "SELECT AD_Code,AD_Name_CN,AD_Update_dt FROM CT_AUTO_Dealers WHERE AD_Code>100";

            DataTable dt = ExecuteSQL.ExecuteQuery(query);

            List<DealerInfo> DIList = new List<DealerInfo>();

            foreach (DataRow dr in dt.AsEnumerable())
            {
                DealerInfo DI = new DealerInfo();
                DI.AD_Code = dr["AD_Code"].ToString();
                DI.AD_Name_CN = dr["AD_Name_CN"].ToString();
                DI.AD_Update_dt = Convert.ToDateTime(dr["AD_Update_dt"].ToString()).ToString("yyyy/MM/dd");
                DIList.Add(DI);
            }

            Lst_OnBoarding.ItemsSource = DIList;
            //foreach (DataRow dr in dt.AsEnumerable())
            //{
            //    ListBoxItem lsb = new ListBoxItem();
            //    Thickness tk = new Thickness();
            //    tk.Top = 6;
            //    lsb.BorderThickness = tk;
            //    lsb.BorderThickness = tk;
             
            //    string contentName = "AD_Code:" + dr["AD_Code"].ToString() + "Dealer Full Name:" + dr["AD_Name_CN"].ToString();
            //    //"On Boarding Date:" + Convert.ToDateTime(dr["AD_Update_dt"].ToString()).ToString("yyyy/MM/dd");


            //    lsb.Content = contentName;
            //    Lst_OnBoarding.Items.Add(lsb);
            //}
            
             


            //Lst_OnBoarding.ItemTemplate.

            //
        }


        private void MyRoutedEventHandler(object sender, RoutedEventArgs e)
        {
            //System.Windows.MessageBox.Show(string.Format("\t{0:mm:ss}\t{1}\t{2}", DateTime.Now, ((System.Windows.Controls.Button)sender).Tag, e.RoutedEvent.Name));
            string DealerNum = ((System.Windows.Controls.TextBlock)(((System.Windows.Controls.ContentControl)(sender)).Content)).Text;


            string query = "SELECT MR_Fields_Name,MR_Fields_RegardingName,MR_Fields_Rules,MR_Fields_RegardingName,MR_Fields_Description,MR_Table_Sequence,MR_Fields_Insert_Parameter,MR_Fields_Insert_Query,MR_Fields_Select_Query,MR_Fields_Select_Parameter,MR_AD_Code,MR_Fields_Update_Or_Not,MR_Fields_Select FROM [dbo].[CT_DataMapping_MigrationResouceFilelds] WHERE MR_AD_Code=" + DealerNum;

            DataTable dt = ExecuteSQL.ExecuteQuery(query);

            dealer_FieldsMapping.ItemsSource = dt.DefaultView;

        }
 

        private void dataSourceBind()
        {
            SqlConnection scn = ExecuteSQL.GetADOConnection();

            scn.Open();
            
            SqlCommand scmdpc_hd = new SqlCommand();
            scmdpc_hd.Connection = scn;
            scmdpc_hd.CommandText = queryRules;
            scmdpc_hd.CommandType = CommandType.Text;

            SqlCommand scmdpc_ln = new SqlCommand();
            scmdpc_ln.Connection = scn;
            scmdpc_ln.CommandText = queryGroupMapping;
            scmdpc_ln.CommandType = CommandType.Text;

            SqlCommand scmdpc_fm = new SqlCommand();
            scmdpc_fm.Connection = scn;
            scmdpc_fm.CommandText = queryResourceFields;
            scmdpc_fm.CommandType = CommandType.Text;
            

            sda_PC_HD = new SqlDataAdapter(queryRules,scn );
            

            sda_PC_HD.SelectCommand = scmdpc_hd;

            ds = new DataSet();
                     
            sda_PC_HD.Fill(ds, "pc_hd");

            sda_PC_LN = new SqlDataAdapter(queryGroupMapping, scn);
            sda_PC_LN.SelectCommand = scmdpc_ln;
            
            sda_PC_LN.Fill(ds, "pc_ln");

            sda_PC_FM= new SqlDataAdapter(queryGroupMapping, scn);
            sda_PC_FM.SelectCommand = scmdpc_fm;

            sda_PC_FM.Fill(ds, "pc_fm");

            gd_RulesMapping.ItemsSource = null;
            gd_GroupMapping.ItemsSource = null;
            gd_FieldsMapping.ItemsSource = null;
        
            gd_RulesMapping.ItemsSource = ds.Tables["pc_hd"].DefaultView;

            gd_GroupMapping.ItemsSource = ds.Tables["pc_ln"].DefaultView;

            gd_FieldsMapping.ItemsSource = ds.Tables["pc_fm"].DefaultView;

            //var customer = from c in context.Customers
            //               select c;
            ////this.DataContext = customer;
            //listBox1.ItemsSource = customer;
            //listBox1.DisplayMemberPath = "CompanyName";

            DataTable dtTmpTable = ExecuteSQL.ExecuteQuery("select name from sysobjects where xtype='u' and name like 'CT%' order by name");

            foreach (DataRow dr in dtTmpTable.AsEnumerable())
            {
                Ls_TableList.Items.Add(dr["name"].ToString());
            }


            DataTable dtTmp = new DataTable();

            dtTmp = ExecuteSQL.ExecuteQuery("select distinct DG_GroupName,DG_ID_Group from CT_DataMapping_Group");

            Dictionary<string, int> dic = new Dictionary<string, int>();

            foreach (DataRow dr in dtTmp.AsEnumerable())
            {
                dic.Add(dr["DG_GroupName"].ToString(), Convert.ToInt32(dr["DG_ID_Group"].ToString()));
            }

            txt_group_name.ItemsSource = dic;

            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonHelper.instance.dealerActuallyName = "OTHER_125";
            CommonHelper.instance.dealerParameter = "125";
            if (cbFromFolderOrFile.IsChecked==true)
            {
                ITxtService itx = new ImportFromSourceFile();
                itx.scanFolderUpload(tbFilePath.Text);
            }
            else
            {
                ITxtService itx = new ImportFromSourceFile();


                //testing
                //DataTable dt = itx.importToDB(tbFilePath.Text);

                //dg_templist.ItemsSource = dt.AsDataView();
            }


        }

        private void BrowserFile_Click(object sender, RoutedEventArgs e)
        {
            if (cbFromFolderOrFile.IsChecked==true)
                folderBrowser();
            else
                fileBrowser();
        }
        public void fileBrowser()
        {
            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
            op.InitialDirectory = @"c:\";
            op.RestoreDirectory = true;
            op.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            op.ShowDialog();
            tbFilePath.Text = op.FileName;
        }

        /// <summary>
        /// 
        /// </summary>
        public void folderBrowser()
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Interop.HwndSource source = PresentationSource.FromVisual(this) as System.Windows.Interop.HwndSource;
            System.Windows.Forms.IWin32Window win = new OldWindow(source.Handle);
            System.Windows.Forms.DialogResult result = dlg.ShowDialog(win);
            tbFilePath.Text = dlg.SelectedPath;
        }

        private void tbFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
           

        }

        private void btn_GroupMapping_Click(object sender, RoutedEventArgs e)
        {
            dataSourceBind();
        }

        private void btn_FieldsMapping_Click(object sender, RoutedEventArgs e)
        {
            dataSourceBind();
        }

        private void btn_FieldsMapping_SaveUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ds.GetChanges() == null)
            {
                System.Windows.MessageBox.Show("no changes updated");
                return;
            }
 
            SqlCommandBuilder scb_pc_fm = new SqlCommandBuilder(sda_PC_FM);


            sda_PC_FM.Update(ds, "pc_fm");

            gd_FieldsMapping.ItemsSource = null;
            
            gd_FieldsMapping.ItemsSource = ds.Tables["pc_fm"].DefaultView;

            System.Windows.MessageBox.Show("Changes Updated");
        }

        private void btn_GroupMapping_SaveUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ds.GetChanges() == null)
            {
                System.Windows.MessageBox.Show("no changes updated");
                return;
            }

            
            SqlCommandBuilder scb_pc_ln = new SqlCommandBuilder(sda_PC_LN);

            sda_PC_LN.Update(ds, "pc_ln");
            // sda_PC_HD.
            gd_GroupMapping.ItemsSource = null;

            gd_GroupMapping.ItemsSource = ds.Tables["pc_ln"].DefaultView;

            System.Windows.MessageBox.Show("Changes Updated");
        }

        private void btn_RulesMapping_SaveUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (ds.GetChanges() == null)
            {
                System.Windows.MessageBox.Show("no changes updated");
                return;
            }

            SqlCommandBuilder scb_pc_hd = new SqlCommandBuilder(sda_PC_HD);
            sda_PC_HD.Update(ds, "pc_hd");

            gd_RulesMapping.ItemsSource = null;

            gd_RulesMapping.ItemsSource = ds.Tables["pc_hd"].DefaultView;

            System.Windows.MessageBox.Show("Changes Updated");
        }

        private void btn_RulesMapping_Click(object sender, RoutedEventArgs e)
        {
            dataSourceBind();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<string> selectTableList = new List<string>();

            if (Ls_getList.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("Please select table");
                return;
            }
            if (string.IsNullOrEmpty(txt_group_name.Text))
            {
                System.Windows.MessageBox.Show("Please select group");
                return;
            }
            //Ls_TableList.Items
            foreach (string ls in Ls_getList.Items)
            {
                selectTableList.Add(ls);
            }
            
            string groupname =  txt_group_name.Text.Split(',')[0].Replace("[","");	
            int groupId =Convert.ToInt32(txt_group_name.Text.Split(',')[1].Replace("]",""));
            string tableLevel = Id_TbLevel.Text;
            if (string.IsNullOrEmpty(tableLevel))
                tableLevel = "2";
            //List<string> fieldsList = new List<string>();
            DataTable fieldsList=new DataTable();
            DataTable dtTmp=new DataTable();
            StringBuilder queryMyFields = new StringBuilder();
            StringBuilder queryDMapping = new StringBuilder();
            ArrayList al=new ArrayList();
            string fkey = string.Empty;
            foreach (string tmp in selectTableList)
            {
               al = new ArrayList();
               al.Add(tmp);
               fieldsList = ExecuteSQL.ExecuteQuery("select c.name from sysobjects o,syscolumns c where o.id =c.id and o.name= ?", al);
                
               foreach (DataRow dr in fieldsList.AsEnumerable())
               {
                   dtTmp=ExecuteSQL.getFieldAttribute(tmp);
                   queryMyFields.Append("insert into CT_DataMapping_MyFields (DM_Fields_Group_ID,DM_Fields_Name,DM_Fields_PrimaryKey,DM_Fields_PrimaryKeyTable,DM_Fields_Remark,DM_Fields_Rules,DM_Fields_Table,DM_Fields_TYPE,DM_Fields_UpdateBy,DM_Fields_UpdateDT,DM_Table_Level) values (");
                   queryMyFields.Append(groupId + ",\'" + dr["name"].ToString() + "\'," + "null,null,null,null" + ",\'" + tmp + "\',\'" + TypeConvert(dtTmp.Columns[dr["name"].ToString()].DataType.ToString()) + "\',\'" + "Nicolas\'" + ",GETDATE()," + tableLevel+")");
                   fkey = ExecuteSQL.SaveReturnIndentityKey(queryMyFields.ToString(), "CT_DataMapping_MyFields");
                   if (String.IsNullOrEmpty(fkey))
                   {
                       queryMyFields = new StringBuilder();
                       continue;
                   }
                   queryMyFields = new StringBuilder();
                   queryDMapping.Append("insert into CT_DataMapping_MigrationResouceFilelds (MR_Fields_Description,MR_Fields_ID,MR_Fields_Name,MR_Fields_RegardingName,MR_Fileds_DeleteOrNot,MR_Fields_Rules) values(");
                   
                   queryDMapping.Append("null," + fkey + ",\'" + dr["name"].ToString() + "\',"+"null,0,null)");

                   ExecuteSQL.RunSqlExecution(queryDMapping.ToString());

                   queryDMapping = new StringBuilder();
               }
         

            }
         }

        private string TypeConvert(string systemType)
        { 
            switch(systemType){
                case "System.Decimal":
                    return "DECIMAL";
                        
                case "System.Int16":
                        return "INT";
             
                case "System.Int32":
                        return "INT";
          
                case "System.Int64":
                        return "INT";
             
                case "System.DateTime":
                        return "DATETIME";
              
                case "System.Double":
                        return "DOUBLE";
             
                case "System.TimeSpan":
                        return "DATETIME";
                default:
                        return "STRING";

                //Byte
                //Char
                //SByte
                //Single
                //String
                //TimeSpan
                //UInt16
                //UInt32
                //UInt64
            
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Ls_TableList.SelectedValue == null)
            {
                System.Windows.MessageBox.Show("Please select table value");
                return;
            }
               

            string selectValue = Ls_TableList.SelectedValue.ToString();

            Ls_TableList.Items.Remove(selectValue);

            Ls_getList.Items.Add(selectValue);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Ls_getList.SelectedValue == null)
            {
                System.Windows.MessageBox.Show("Please select table value");
                return;
            }

            string selectValue = Ls_getList.SelectedValue.ToString();

            Ls_getList.Items.Remove(selectValue);

            Ls_TableList.Items.Add(selectValue);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //sendMail();

            List<string> toList=new List<string>();
            toList.Add("xianan@shinfotech.cn");
            List<string> ccList=new List<string>();
            //ccList.Add("fagahdel@gmail.com");
            ccList.Add("shihong881214@163.com");
            ccList.Add("xianan@shinfotech.cn");
            ccList.Add("234163000@qq.com");
            List<string> bccList=new List<string>();
            bccList.Add("fagahdel@gmail.com");

            SendMail("crm@shinfotech.cn", "shinfotech", toList, ccList, bccList, "Testing From Thinking Tree", "support@crmtree.com", "ThinkingTree", "mail.shinfotech.cn", "Testing From Nicolas");
        }

        public static bool SendMail(string AUsername, string APassword, List<string> AToList, List<string> ACcList, List<string> BCcList, string ASubject, string AFrom, string AFromDisplayName, string ASmtpServer, string AContext)
        {
            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(AFrom, AFromDisplayName);
                if (AToList != null && AToList.Count != 0)
                {
                    foreach (string to in AToList)
                    {
                        message.To.Add(new MailAddress(to));
                    }
                }
                if (ACcList != null && ACcList.Count != 0)
                {
                    foreach (string cc in ACcList)
                    {
                        message.CC.Add(new MailAddress(cc));
                    }
                }
                if (BCcList != null && BCcList.Count != 0)
                {
                    foreach (string bcc in BCcList)
                    {
                        message.Bcc.Add(new MailAddress(bcc));
                    }
                }

                message.Subject = ASubject;
                message.Body = AContext;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.GetEncoding("UTF-8");
                message.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                message.Priority = MailPriority.Normal;
                 
                SmtpClient client = new SmtpClient(ASmtpServer);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(AUsername, APassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void sendMail()
        {
            //string userName = AUsername;
            //string password = APassword;
            //string str3 = AFrom;
            //string host = ASmtpServer;
            //string displayName = AFromDisplayName;
            //try
            //{
            //    MailAddress from = new MailAddress(str3, displayName);
            //    MailAddress to = new MailAddress(ATo);
            //    MailMessage message = new MailMessage(from, to);
            //    message.Subject = ASubject;
            //    message.Body = AContext;
            //    message.IsBodyHtml = AIsBodyHtml;
            //    message.SubjectEncoding = Encoding.GetEncoding(ASubjectEncoding);
            //    message.BodyEncoding = Encoding.GetEncoding(ABodyEncoding);
            //    message.Priority = APriority;
            //    message.ReplyTo = new MailAddress(AReplyTo);
            //    if ((AAttachmentsFile != null) && (AAttachmentsFile.PostedFile.ContentLength > 0))
            //    {
            //        message.Attachments.Add(new Attachment(AAttachmentsFile.PostedFile.FileName));
            //    }
            //    SmtpClient client = new SmtpClient(host);
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = new NetworkCredential(userName, password);
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.Send(message);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
            try
            {
                #region Drop Mail
                
                //SmtpClient smtp = new SmtpClient(); //实例化一个SmtpClient

                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //将smtp的出站方式设为 Network

                //smtp.EnableSsl = false;//smtp服务器是否启用SSL加密

                //smtp.Host = "smtp.163.com"; //指定 smtp 服务器地址

                //smtp.Port = 25;             //指定 smtp 服务器的端口，默认是25，如果采用默认端口，可省去

                ////如果你的SMTP服务器不需要身份认证，则使用下面的方式，不过，目前基本没有不需要认证的了

                //smtp.UseDefaultCredentials = true;

                ////如果需要认证，则用下面的方式

                //smtp.Credentials = new NetworkCredential("邮箱帐号@163.com", "邮箱密码");

                //MailMessage mm = new MailMessage(); //实例化一个邮件类

                //mm.Priority = MailPriority.High; //邮件的优先级，分为 Low, Normal, High，通常用 Normal即可

                //mm.From = new MailAddress("邮箱帐号@163.com", "真有意思", Encoding.GetEncoding(936));

                ////收件方看到的邮件来源；

                ////第一个参数是发信人邮件地址

                ////第二参数是发信人显示的名称

                ////第三个参数是 第二个参数所使用的编码，如果指定不正确，则对方收到后显示乱码

                ////936是简体中文的codepage值

                ////注：上面的邮件来源，一定要和你登录邮箱的帐号一致，否则会认证失败

                //mm.ReplyTo = new MailAddress("test_box@gmail.com", "我的接收邮箱", Encoding.GetEncoding(936));

                ////ReplyTo 表示对方回复邮件时默认的接收地址，即：你用一个邮箱发信，但却用另一个来收信

                ////上面后两个参数的意义， 同 From 的意义

                //mm.CC.Add("a@163.com,b@163.com,c@163.com");

                ////邮件的抄送者，支持群发，多个邮件地址之间用 半角逗号 分开



                ////当然也可以用全地址，如下：

                //mm.CC.Add(new MailAddress("a@163.com", "抄送者A", Encoding.GetEncoding(936)));

                //mm.CC.Add(new MailAddress("b@163.com", "抄送者B", Encoding.GetEncoding(936)));

                //mm.CC.Add(new MailAddress("c@163.com", "抄送者C", Encoding.GetEncoding(936)));



                ////mm.Bcc.Add("d@163.com,e@163.com");

                ////邮件的密送者，支持群发，多个邮件地址之间用 半角逗号 分开



                ////当然也可以用全地址，如下：

                ////mm.CC.Add(new MailAddress("d@163.com", "密送者D", Encoding.GetEncoding(936)));

                ////mm.CC.Add(new MailAddress("e@163.com", "密送者E", Encoding.GetEncoding(936)));

                //mm.Sender = new MailAddress("xxx@xxx.com", "邮件发送者", Encoding.GetEncoding(936));

                ////可以任意设置，此信息包含在邮件头中，但并不会验证有效性，也不会显示给收件人

                ////说实话，我不知道有啥实际作用，大家可不理会，也可不写此项

                //mm.To.Add("g@163.com,h@163.com");

                ////邮件的接收者，支持群发，多个地址之间用 半角逗号 分开

                ////当然也可以用全地址添加

                //mm.To.Add(new MailAddress("g@163.com", "接收者g", Encoding.GetEncoding(936)));

                //mm.To.Add(new MailAddress("h@163.com", "接收者h", Encoding.GetEncoding(936)));

                //mm.Subject = "这是邮件标题"; //邮件标题

                //mm.SubjectEncoding = Encoding.GetEncoding(936);

                //// 这里非常重要，如果你的邮件标题包含中文，这里一定要指定，否则对方收到的极有可能是乱码。

                //// 936是简体中文的pagecode，如果是英文标题，这句可以忽略不用

                //mm.IsBodyHtml = true; //邮件正文是否是HTML格式

                //mm.BodyEncoding = Encoding.GetEncoding(936);

                ////邮件正文的编码， 设置不正确， 接收者会收到乱码

                //mm.Body = "<font color='red'>邮件测试，呵呵</font>";

                ////邮件正文

                ////mm.Attachments.Add( new Attachment( @"d:a.doc", System.Net.Mime.MediaTypeNames.Application.Rtf ) );

                ////添加附件，第二个参数，表示附件的文件类型，可以不用指定

                ////可以添加多个附件

                ////mm.Attachments.Add(new Attachment(@"d:b.doc"));

                //smtp.Send(mm); //发送邮件，如果不返回异常， 则大功告成了。
                
                #endregion
                //Mail.SendMail("wangqi_5203344@163.com", "CRMTree_Mail", strLine, null, "wangqi@shinfotech.cn", "shinfotech", "wangqi@shinfotech.cn", "mail.shinfotech.cn");
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("crm@shinfotech.cn", "TestingTesting");
                mm.To.Add("xianan@shinfotech.cn");
                mm.Subject = "监控出错";
                mm.Body = "message";
                SmtpClient sc = new SmtpClient("mail.shinfotech.cn");
                sc.Credentials = new NetworkCredential("crm@shinfotech.cn", "shinfotech");
                sc.Send(mm);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void Location_Click(object sender, RoutedEventArgs e)
        {
            //webBrowser1.Navigate(@"C:\Users\Administrator\Documents\Visual Studio 2013\Projects\ManullyModification\ManullyModification\GetLocation.html");

            //webBrowser1.ObjectForScripting = this;

            //string tag_lng = webBrowser1.Document.("mouselng").InnerText;
            //string tag_lat = webBrowser1.Document.GetElementById("mouselat").InnerText;

            //string query = "select * from CT_Car_Inventory left join CT_Car_Style on CI_CS_Code=CS_Code where CI_Code not in (select CI_Code from CT_Car_Inventory left join CT_Car_Style on CI_CS_Code=CS_Code" +
                   //" where CS_VIN_series= SUBSTRING(CI_VIN,4,5) and CS_VIN_Yr=SUBSTRING(CI_VIN,10,1)) and CI_VIN is not null and CI_Update_dt>'2015/01/25'";

           // DataTable dtTemp=ExecuteSQL.ExecuteQuery(query);

            //string FileName = "E:\\TEMP\\VinFailToDecode" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".xls";

            //CommonHelper.instance.ExportToExcel(FileName, dtTemp);

            //List<string> toList = new List<string>();
            //toList.Add("xianan@shinfotech.cn");
            //List<string> ccList = new List<string>();
            ////ccList.Add("fagahdel@gmail.com");
            //ccList.Add("shihong881214@163.com");
            //ccList.Add("xianan@shinfotech.cn");
            //ccList.Add("234163000@qq.com");
            //ccList.Add("fariborz@shunovo.com");
            //List<string> bccList = new List<string>();
            //bccList.Add("fagahdel@gmail.com");

            //CommonHelper.instance.SendMail("crm@shinfotech.cn", "shinfotech", toList, ccList, bccList, "Vin Code can't be decoded from System", "support@crmtree.com", "ThinkingTree", "mail.shinfotech.cn", "Please use the below query select CS_Code from CT_Car_Style where CS_VIN_series= SUBSTRING(@CI_VIN,4,5) and CS_VIN_Yr=SUBSTRING(@CI_VIN,10,1)", FileName);

            //CommonHelper.instance.generateEmailToItStuffs(dtTemp, "Mail for testing Alicloud Server", "This is mail just for testing");

            ArrayList al = new ArrayList();

            al.Add(125);
            al.Add("AutoGenerated");
            al.Add("卢顺杨");
            al.Add("2000/01/01");
            al.Add(21);
            al.Add("AutoGenerated");


            ExecuteSQL.SaveReturnIndentityKey("Add_Dealer_Empl_Process", al, true);

            //ExecuteSQL.TestingGetProcedure();
        }

        private void gd_FieldsMapping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
  

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //string path = tbTestExcel.Text;
            //string path = @"E:\temp\repairment\OTHER_125\new.xls";

            //ExcelHelper eh = new ExcelHelper(path);

            //DataTable dt = eh.ExportExcelToDataTable();

            //System.Windows.MessageBox.Show(EncryptDES("aA1", "Shunovo20150701") + ";" + EncryptDES("sa", "Shunovo20150701") + ";" + EncryptDES("crmtree_dbo", "Shunovo20150701"));

            //tbTestExcel1.Text = EncryptDES("crmtree_dbo", "Shunovo20150701") + ";" + EncryptDES("crmtree_dbo_prod", "Shunovo20150701") + ";";

            
        }

        private static byte[] Keys = { 0xEF, 0xAB, 0x56, 0x78, 0x90, 0x34, 0xCD, 0x12 };
        public string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }

    }

    public class OldWindow : System.Windows.Forms.IWin32Window
    {
        IntPtr _handle;
        public OldWindow(IntPtr handle)
        {
            _handle = handle;
        }
        #region IWin32Window Members
        IntPtr System.Windows.Forms.IWin32Window.Handle
        {
            get { return _handle; }
        }
        #endregion
    } 
}
