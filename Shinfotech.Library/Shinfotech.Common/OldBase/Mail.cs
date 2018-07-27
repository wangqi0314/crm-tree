using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.WebControls;
namespace ShInfoTech.Common
{
    public class Mail
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="ATo">收件人地址</param>
        /// <param name="ASubject">主题</param>
        /// <param name="AContext">邮件内容</param>
        /// <param name="AAttachmentsFile">附件</param>
        /// <param name="AUsername">发件人地址</param>
        /// <param name="APassword">发件人地址密码</param>
        /// <param name="AFrom">发件人地址</param>
        /// <param name="ASmtpServer">host</param>
        /// <returns></returns>
        public static bool SendMail(string ATo, string ASubject, string AContext, FileUpload AAttachmentsFile, string AUsername, string APassword, string AFrom, string ASmtpServer)
        {
            string userName = AUsername;
            string password = APassword;
            string str3 = AFrom;
            string host = ASmtpServer;
            try
            {
                MailAddress from = new MailAddress(str3);
                MailAddress to = new MailAddress(ATo);
                MailMessage message = new MailMessage(from, to);
                message.Subject = ASubject;
                message.Body = AContext;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.GetEncoding("UTF-8");
                message.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                message.Priority = MailPriority.Normal;
                if ((AAttachmentsFile != null) && (AAttachmentsFile.PostedFile.ContentLength > 0))
                {
                    message.Attachments.Add(new Attachment(AAttachmentsFile.PostedFile.FileName));
                }
                SmtpClient client = new SmtpClient(host);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SendMail(string ATo, string ASubject, string AContext, FileUpload AAttachmentsFile, string AUsername, string APassword, string AFrom, string AFromDisplayName, string ASmtpServer)
        {
            string userName = AUsername;
            string password = APassword;
            string str3 = AFrom;
            string host = ASmtpServer;
            string displayName = AFromDisplayName;
            try
            {
                MailAddress from = new MailAddress(str3, displayName);
                MailAddress to = new MailAddress(ATo);
                MailMessage message = new MailMessage(from, to);
                message.Subject = ASubject;
                message.Body = AContext;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.GetEncoding("UTF-8");
                message.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                message.Priority = MailPriority.Normal;
                if ((AAttachmentsFile != null) && (AAttachmentsFile.PostedFile.ContentLength > 0))
                {
                    message.Attachments.Add(new Attachment(AAttachmentsFile.PostedFile.FileName));
                }
                SmtpClient client = new SmtpClient(host);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SendMail(string ATo, string ASubject, string AContext, FileUpload AAttachmentsFile, string AUsername, string APassword, string AFrom, string AFromDisplayName, string ASmtpServer, MailPriority APriority, string AReplyTo)
        {
            string userName = AUsername;
            string password = APassword;
            string str3 = AFrom;
            string host = ASmtpServer;
            string displayName = AFromDisplayName;
            try
            {
                MailAddress from = new MailAddress(str3, displayName);
                MailAddress to = new MailAddress(ATo);
                MailMessage message = new MailMessage(from, to);
                message.Subject = ASubject;
                message.Body = AContext;
                message.IsBodyHtml = true;
                message.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                message.BodyEncoding = Encoding.GetEncoding("UTF-8");
                message.Priority = APriority;
                message.ReplyTo = new MailAddress(AReplyTo);
                if ((AAttachmentsFile != null) && (AAttachmentsFile.PostedFile.ContentLength > 0))
                {
                    message.Attachments.Add(new Attachment(AAttachmentsFile.PostedFile.FileName));
                }
                SmtpClient client = new SmtpClient(host);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SendMail(string ATo, string ASubject, string AContext, FileUpload AAttachmentsFile, string AUsername, string APassword, string AFrom, string AFromDisplayName, string ASmtpServer, MailPriority APriority, string AReplyTo, bool AIsBodyHtml, string ASubjectEncoding, string ABodyEncoding)
        {
            string userName = AUsername;
            string password = APassword;
            string str3 = AFrom;
            string host = ASmtpServer;
            string displayName = AFromDisplayName;
            try
            {
                MailAddress from = new MailAddress(str3, displayName);
                MailAddress to = new MailAddress(ATo);
                MailMessage message = new MailMessage(from, to);
                message.Subject = ASubject;
                message.Body = AContext;
                message.IsBodyHtml = AIsBodyHtml;
                message.SubjectEncoding = Encoding.GetEncoding(ASubjectEncoding);
                message.BodyEncoding = Encoding.GetEncoding(ABodyEncoding);
                message.Priority = APriority;
                message.ReplyTo = new MailAddress(AReplyTo);
                if ((AAttachmentsFile != null) && (AAttachmentsFile.PostedFile.ContentLength > 0))
                {
                    message.Attachments.Add(new Attachment(AAttachmentsFile.PostedFile.FileName));
                }
                SmtpClient client = new SmtpClient(host);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
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
            catch 
            {
                return false;
            }
        }
    }
}
