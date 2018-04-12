using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using System.Data.Entity.SqlServer;
using MVC2015.Web.Model.Account.Login;
using MVC2015.Utility;
using MVC2015.Web.Model.Account;
using MVC2015.Web.Model.Permission;
using MVC2015.Web.Model.SystemMaint.Company;
using MVC2015.Web.Model.Email;

namespace MVC2015.Web.BusinessLogic.Account
{
    public class Login : IDisposable
    {
        private readonly CTX.MVC2015DBContext Ctx;

        public Login()
        {
            Ctx = new CTX.MVC2015DBContext();
        }

        public Login(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }

        public bool ValidUserPassword(string logonName, string password)
        {
            LoginModel model = (from a in Ctx.tbl_Common_User.Where(t => t.LogonName == logonName && t.IsDeleted != true && t.Status == 1)
                                select new LoginModel
                                {
                                    UserName = a.UserName,
                                    Password = a.Password
                                }).FirstOrDefault();
            if (model != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ResetPassword(string name, string emailTo)
        {

            MD.tbl_Common_User date = Ctx.tbl_Common_User.First(u => u.LogonName == name);

            IPasswordPolicy Password = new RandomPassword();
            string orginalPassword = Password.GeneratePassword();
            date.Password = HashEncrypt.SHA512Encrypt(orginalPassword);
            date.UpdatedBy = null;
            date.UpdatedDate = null;
            Ctx.SaveChanges();
            //发送邮件 
            SendEmail(date, orginalPassword, "User_ResetPassword", emailTo);


            return true;
        }

        private void SendEmail(MD.tbl_Common_User model, string password, string emailTemplateName, string emailTo)
        {
            bool isSendEmailtoIIS = false;
            var sendEmailInfo = new SendEmailInfo();
            var email = Ctx.tbl_EmailTemplate.FirstOrDefault(t => t.Name == emailTemplateName);
            string[] emailSendTo = { emailTo };

            string url = string.Format("{0}/", EmailTemplateParam.GetHostName());

            sendEmailInfo.mailSubject = email.EmailSubject.Replace(EmailTemplateParam.SystemName, EmailTemplateParam.SystemNameBasic);
            sendEmailInfo.mailBody = email.EmailContent.Replace(EmailTemplateParam.UserDisplayName, model.UserName)
                .Replace(EmailTemplateParam.URL, url)
                .Replace(EmailTemplateParam.UserName, model.LogonName)
                .Replace(EmailTemplateParam.Password, password)
                .Replace("[DateTime]", DateTime.Now.ToString("yyyy年MM月dd日"));

            //MailHelper.SendMail(emailSendTo, sendEmailInfo.mailSubject, sendEmailInfo.mailBody, 0, isSendEmailtoIIS, true);

        }

        public string ValiableEmail(string email, string loginName)
        {
            try
            {
                var user = Ctx.tbl_Common_User.First(a => a.LogonName == loginName && a.Status == 1);
                if (user.EmailAddress.Equals(email))
                {
                    return "email";
                }
                else
                {
                    return "true";
                }
            }
            catch (Exception)
            {
                return "error";
            }

        }

        public IdentityModel GetLoginByName(string userName)
        {
            var user = Ctx.tbl_Common_User.AsNoTracking().Where(u => u.LogonName == userName && u.IsDeleted != true).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            try
            {
                var results = (from login in Ctx.tbl_Common_User
                               where login.LogonName == userName && login.Status == 1 && login.IsDeleted != true
                               select new IdentityModel()
                               {
                                   Id = login.UserId.ToString(),
                                   LogonName = login.LogonName,
                                   UserName = login.UserName,
                                   PasswordHash = login.Password,
                                   DomainAccount = login.DomainAccount,
                                   EmailAddress = login.EmailAddress
                               }).FirstOrDefault();
                var roleUsers = user.tbl_Common_RoleUser.FirstOrDefault();
                int roleIds = roleUsers == null ? 0 : roleUsers.RoleId.Value;
                var role = Ctx.tbl_Common_Role.Where(r => r.RoleId == roleIds).FirstOrDefault();

                results.Id = user.UserId.ToString();
                results.RoleId = roleIds;
                results.RoleName = role == null ? "" : role.Name;
                results.RoleSelectList =
                    (from m in Ctx.tbl_Common_ModulePermissionDefine
                     join pi in Ctx.tbl_Common_RolePermissionConfig.DefaultIfEmpty() on m.ModuleId equals pi.ModuleId into inner
                     from p in inner.DefaultIfEmpty()
                     where (p.RoleId == roleIds || p.RoleId == null)
                     select new RolePermission
                     {
                         Action = string.IsNullOrEmpty(m.Action) ? "index" : m.Action,
                         Area = m.Area ?? string.Empty,
                         Controller = m.Controller ?? string.Empty,
                         ModuleId = m.ModuleId,
                         IsAvailable = (m.Value & p.Value) == m.Value ? true : false
                     }).ToList();

                MD.tbl_Common_UserOfGasStation userGas = Ctx.tbl_Common_UserOfGasStation.Where(m => m.UserId == user.UserId).FirstOrDefault();
                //if (gas != null)
                //    results.CompanyId = gas.Company;
                return results;
            }
            catch (Exception)
            {
                return null;
            }


        }

        public IdentityModel GetLoginById(string userId)
        {
            var result = (from login in Ctx.tbl_Common_User
                          where login.UserId.ToString() == userId && login.Status == 1 && login.IsDeleted != true
                          select new IdentityModel()
                          {
                              Id = login.UserId.ToString(),
                              UserName = login.UserName,
                              PasswordHash = login.Password,
                              DomainAccount = login.DomainAccount,
                              EmailAddress = login.EmailAddress
                          }).FirstOrDefault();
            return result;
        }

        public List<CompanyIdModel> GetCompanyId(string userId)
        {
            int UserId = Convert.ToInt32(userId);
            var result = (from companyId in Ctx.tbl_Common_UserOfCompany
                          where companyId.UserId == UserId && companyId.IsDeleted != true
                          select new CompanyIdModel()
                          {
                              CompanyId = companyId.CompanyId
                          }).ToList();
            return result;
        }

        public void AddValidSuccessHistory(string logonName, string cryptPwd, string ipAddress)
        {
            MD.tbl_UserLogonHistory logonHistory = new MD.tbl_UserLogonHistory();
            logonHistory.IPAddress = ipAddress;
            logonHistory.LogonName = logonName;
            logonHistory.LogonTimes = 1;
            logonHistory.LogonDate = DateTime.Now;
            Ctx.tbl_UserLogonHistory.Add(logonHistory);
            Ctx.SaveChanges();
        }

        public int GetLastHourLoginFailTimes(string ipAddress)
        {

            var lastHistory = Ctx.tbl_UserLogonHistory.Where(
                m => m.IPAddress == ipAddress).OrderByDescending(m => m.LogonDate)
                .FirstOrDefault();
            if (lastHistory != null && lastHistory.Password == null)
            {
                return 0;
            }
            else
            {
                var lastSuccess = Ctx.tbl_UserLogonHistory.Where(
                m => m.IPAddress == ipAddress && m.Password == null).OrderByDescending(m => m.LogonDate)
                .FirstOrDefault();
                var lastSuccessId = lastSuccess == null ? 0 : lastSuccess.ID;

                var failTimes = Ctx.tbl_UserLogonHistory.Where(
                     m => m.Password != null && m.ID >= lastSuccessId && SqlFunctions.DateAdd("hour", 1, m.LogonDate) >= DateTime.Now && m.IPAddress == ipAddress)
                     .Sum(md => md.LogonTimes);
                return failTimes ?? 0;
            }
        }

        public void AddValidFaileHistory(string logonName, string cryptPwd, string ipAddress)
        {
            var lastSuccess = Ctx.tbl_UserLogonHistory.Where(m => m.IPAddress == ipAddress && m.Password == null).OrderByDescending(m => m.LogonDate).FirstOrDefault();

            MD.tbl_UserLogonHistory logonHistory = Ctx.tbl_UserLogonHistory.Where(md => md.LogonName == logonName && md.IPAddress == ipAddress
                && md.Password != null
                && SqlFunctions.DateAdd("mi", 20, md.LogonDate) > DateTime.Now).OrderByDescending(md => md.LogonDate).FirstOrDefault();
            if (logonHistory == null || (lastSuccess != null && lastSuccess.ID > logonHistory.ID)) // 登录成功过，应忽略20分钟的条件，新增一条记录
            {
                logonHistory = new MD.tbl_UserLogonHistory();
                logonHistory.IPAddress = ipAddress;
                logonHistory.LogonName = logonName;
                logonHistory.Password = cryptPwd;
                logonHistory.LogonTimes = 1;
                logonHistory.LogonDate = DateTime.Now;
                Ctx.tbl_UserLogonHistory.Add(logonHistory);
            }
            else
            {
                logonHistory.Password = cryptPwd;
                logonHistory.LogonTimes = logonHistory.LogonTimes + 1;
                logonHistory.LogonDate = DateTime.Now;
            }
            Ctx.SaveChanges();
        }
        public void Dispose()
        {
            if (Ctx != null)
            {
                Ctx.Dispose();
            }
        }
    }
}
