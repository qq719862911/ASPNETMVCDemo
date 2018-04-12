//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//public sealed class MailHelper
//{


//    private MailHelper()
//    {
//    }
//    public static void StartMailJob()
//    {
//        MailManager.Instance.Start();
//    }

//    public static void StopMailJob()
//    {
//        MailManager.Instance.Stop();
//    }

//    public static bool SendMail(string mailFrom, string[] mailTo, string[] mailCc, string[] mailBcc, string mailSubject, string mailBody, bool isBodyHtml, MailPriority priority, string[] attachment, ref string msg, out Email outEmail,int companyId,bool isSendEmailtoIIS)
//    {

//        Email mail = new Email();
//        mail.Priority = priority;
//        if (!string.IsNullOrEmpty(mailFrom))
//        {
//            mail.From = new MailAddress(mailFrom);
//        }

//        if (mailTo != null)
//        {
//            foreach (string to in mailTo)
//            {
//                if (to != null && to.Trim().Length > 0)
//                {
//                    mail.To.Add(to.Trim());
//                }
//            }
//        }


//        if (mailCc != null)
//        {
//            foreach (string cc in mailCc)
//            {
//                if (cc != null && cc.Trim().Length > 0)
//                {
//                    mail.CC.Add(cc.Trim());
//                }
//            }
//        }

//        if (mailBcc != null)
//        {
//            foreach (string bcc in mailBcc)
//            {
//                if (bcc != null && bcc.Trim().Length > 0)
//                {
//                    mail.Bcc.Add(bcc);
//                }
//            }
//        }
//        mail.Subject = mailSubject;
//        mail.Body = mailBody;
//        mail.IsBodyHtml = isBodyHtml;
//        mail.CompanyId = companyId;
//        mail.IsSendEmailToIIS = isSendEmailtoIIS;
//        if (attachment != null)
//        {
//            foreach (string attach in attachment)
//            {
//                if (attach != null && attach.Trim().Length > 0)
//                {
//                    mail.Attachment.Add(attach.Trim());
//                }
//            }
//        }

//        outEmail = mail;
//        return MailManager.Instance.AddMail(mail, ref msg);
//    }

//    /// <summary>
//    /// Send Email
//    /// </summary>
//    /// <param name="mailTo">Email address, which the email will send to</param>
//    /// <param name="mailSubject">The email subject</param>
//    /// <param name="mailBody">The email body</param>
//    /// <param name="isBodyHtml">whether show the email body in Html format,default is True</param>
//    /// <param name="priority">The email priority,default is Normal</param>
//    public static bool SendMail(string[] mailTo, string mailSubject, string mailBody, int companyId, bool isSendEmailtoIIS, bool isBodyHtml = true, MailPriority priority = MailPriority.Normal)
//    {
//        var msg = string.Empty;
//        Email email;
//        return SendMail(string.Empty, mailTo, null, null, mailSubject, mailBody, isBodyHtml, priority, null, ref msg, out email,companyId,isSendEmailtoIIS);

//    }



//    ///<summary>
//    /// Send Email
//    /// </summary>
//    /// <param name="mailTo">Email address,which the email will send to</param>
//    /// <param name="mailCc">Email address,which the email will CC to</param>
//    /// <param name="mailSubject">The email subject</param>
//    /// <param name="mailBody">The email body</param>
//    /// <param name="isBodyHtml">whether show the email body in Html format,default is True</param>
//    /// <param name="priority">The email priority,default is Normal</param>
//    public static bool SendMail(string[] mailTo, string[] mailCc, string mailSubject, string mailBody, int companyId, bool isSendEmailtoIIS, bool isBodyHtml = true, MailPriority priority = MailPriority.Normal)
//    {
//        var msg = string.Empty;
//        Email email;
//        return SendMail(string.Empty, mailTo, mailCc, null, mailSubject, mailBody, isBodyHtml, priority, null, ref msg, out email,companyId,isSendEmailtoIIS);

//    }

//    ///<summary>
//    /// Send Email
//    /// </summary>
//    /// <param name="mailTo">Email address,which the email will send to</param>
//    /// <param name="mailCc">Email address,which the email will Cc to</param>
//    /// <param name="mailBcc">Email address,which the email will Bcc to</param>
//    /// <param name="mailSubject">The email subject</param>
//    /// <param name="mailBody">The email body</param>
//    /// <param name="isBodyHtml">whether show the email body in Html format,default is True</param>
//    /// <param name="priority">The email priority,default is Normal</param>
//    public static bool SendMail(string[] mailTo, string[] mailCc, string[] mailBcc, string mailSubject, string mailBody, int companyId, bool isSendEmailtoIIS, bool isBodyHtml = true, MailPriority priority = MailPriority.Normal)
//    {
//        var msg = string.Empty;
//        Email email;
//        return SendMail(string.Empty, mailTo, mailCc, mailBcc, mailSubject, mailBody, isBodyHtml, priority, null, ref msg, out email,companyId,isSendEmailtoIIS);

//    }

//    ///<summary>
//    /// Send Email
//    /// </summary>
//    /// <param name="mailFrom">Email address,the email address of email sender</param>
//    /// <param name="mailTo">Email address,which the email will send to</param>
//    /// <param name="mailSubject">The email subject</param>
//    /// <param name="mailBody">The email body</param>
//    /// <param name="isBodyHtml">whether show the email body in Html format,default is True</param>
//    /// <param name="priority">The email priority,default is Normal</param>
//    public static bool SendMail(string mailFrom, string[] mailTo, string mailSubject, string mailBody, int companyId, bool isSendEmailtoIIS, bool isBodyHtml = true, MailPriority priority = MailPriority.Normal)
//    {
//        var msg = string.Empty;
//        Email email;
//        return SendMail(mailFrom, mailTo, null, null, mailSubject, mailBody, isBodyHtml, priority, null, ref msg, out email,companyId,isSendEmailtoIIS);

//    }

//    ///<summary>
//    /// Send Email
//    /// </summary>
//    /// <param name="mailFrom">Email address,the email address of email sender</param>
//    /// <param name="mailTo">Email address,which the email will send to</param>
//    /// <param name="mailCc">Email address,which the email will Cc to</param>
//    /// <param name="mailSubject">The email subject</param>
//    /// <param name="mailBody">The email body</param>
//    /// <param name="isBodyHtml">whether show the email body in Html format,default is True</param>
//    /// <param name="priority">The email priority,default is Normal</param>
//    public static bool SendMail(string mailFrom, string[] mailTo, string[] mailCc, string mailSubject, string mailBody, int companyId, bool isSendEmailtoIIS, bool isBodyHtml = true, MailPriority priority = MailPriority.Normal)
//    {
//        var msg = string.Empty;
//        Email email;
//        return SendMail(mailFrom, mailTo, mailCc, null, mailSubject, mailBody, isBodyHtml, priority, null, ref msg, out email,companyId,isSendEmailtoIIS);

//    }

//    ///<summary>
//    /// Send Email
//    /// </summary>
//    /// <param name="mailFrom">Email address,the email address of email sender</param>
//    /// <param name="mailTo">Email address,which the email will send to</param>
//    /// <param name="mailCc">Email address,which the email will Cc to</param>
//    /// <param name="mailBcc">Email address,which the email will Bcc to</param>
//    /// <param name="mailSubject">The email subject</param>
//    /// <param name="mailBody">The email body</param>
//    /// <param name="isBodyHtml">whether show the email body in Html format,default is True</param>
//    /// <param name="priority">The email priority,default is Normal</param>
//    public static bool SendMail(string mailFrom, string[] mailTo, string[] mailCc, string[] mailBcc, string mailSubject, string mailBody, int companyId, bool isSendEmailtoIIS, bool isBodyHtml = true, MailPriority priority = MailPriority.Normal)
//    {
//        var msg = string.Empty;
//        Email email;
//        return SendMail(mailFrom, mailTo, mailCc, mailBcc, mailSubject, mailBody, isBodyHtml, priority, null, ref msg, out email,companyId,isSendEmailtoIIS);

//    }
//}
