using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class Common_Mail_BadMail
    {
        public System.Guid MailID { get; set; }
        public int MailPriority { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string MailCc { get; set; }
        public string MailBcc { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public bool IsBodyHtml { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Attachment { get; set; }
    }
}
