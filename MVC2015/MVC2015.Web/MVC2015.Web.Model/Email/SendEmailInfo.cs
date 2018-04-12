using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.Email
{
    public class SendEmailInfo
    {
        public string mailFrom { get; set; }
        public string[] mailTo { get; set; }
        public string mailSubject { get; set; }
        public string mailBody { get; set; }
        public bool isBodyHtml { get; set; }
        public MailPriority priority { get; set; }
        public int? sendEmailFlag { get; set; }
        public int serviceOrderID { get; set; }
        public Nullable<DateTime> SendEmailDate { get; set; }
        public int companyId { get; set; }
        public bool isSendEmailtoIIS { get; set; }
        public int EquipmentPlanId { get; set; }
    }
}