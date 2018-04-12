using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.Account.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "SMM_UserInfo_UserName")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "SMM_UserInfo_Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string EncryptPassword { get; set; }
        [Required(ErrorMessage = "SMM_UserInfo_ValidationCode")]
        public string ValidationCode { get; set; }
        //[EmailEx(ErrorMessage = "SMM_UserInfo_FormatEmailAddress")]
        //[Required(ErrorMessage = "SMM_UserInfo_EmailAddress")]
        public string Email { get; set; }

    }
}
