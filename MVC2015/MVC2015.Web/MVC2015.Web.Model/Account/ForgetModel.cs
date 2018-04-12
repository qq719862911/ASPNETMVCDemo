using MVC2015.Web.Model.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.Account
{
    public class ForgetModel
    {
        [Required(ErrorMessage = "SMM_UserInfo_UserName")]
        public string UserName { get; set; }

        //[EmailAddress(ErrorMessage = "邮件格式不正确")]
        [EmailEx(ErrorMessage = "SMM_UserInfo_FormatEmailAddress")]
        [Required(ErrorMessage = "SMM_UserInfo_EmailAddress")]
        public string Email { get; set; }
    }
}
