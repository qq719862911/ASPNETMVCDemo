using MVC2015.Web.Model.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.UserInfo
{
    public class UserInfoUpdatePassword
    {
        public int UserId { get; set; }
        /// <summary>
        /// 原密码
        /// </summary>
        [Display(Name = "SM_UserInfo_OldPassword")]
        [Required(ErrorMessage = "SMM_UserInfo_OldPasswordIsNoEmpty")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "SMM_UserInfo_PasswordLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "SM_UserInfo_NewPassword", Description = "SMM_UserInfo_PasswordLength")]
        [Required(ErrorMessage = "SMM_UserInfo_NewPasswordIsNoEmpty")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "SMM_UserInfo_PasswordLength")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        /// 
        [Display(Name = "SM_UserInfo_ConfirmPassword", Description = "SMM_UserInfo_InputPasswordAgain")]
        //[CompareEx("NewPassword", ErrorMessage = "SMM_UserInfo_TwicePasswordDifferent")]
       // [CompareEx("NewPassword", ErrorMessage = "SMM_UserInfo_ConfirmPassword_Compare")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
