using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MVC2015.Web.Model.Account;
using MVC2015.Web.Model.Common;
using MVC2015.Web.Model.SystemMaint.UserInfo;
using MVC2015.Web.BusinessLogic.Account;
using MVC2015.Web.BusinessLogic.SystemMaint;
using MVC2015.Web.Site.Models;
using Microsoft.AspNet.Identity;

namespace MVC2015.Web.Site.Common
{
    public class UserStore : IUserStore<IdentityModel>, IUserPasswordStore<IdentityModel>
    {
        public string LogonName = null;
        Login BL = new Login();
        public UserStore()
        {
            LogonName = null;
        }
        public UserStore(string logonName)
        {
            LogonName = logonName;
        }

        public async Task DeleteAsync(IdentityModel user)
        {
            throw new NotImplementedException();
        }

        // 创建用户
        public async Task CreateAsync(IdentityModel user)
        {
            UserInfo empBL = new UserInfo();
            UserInfoItem model = new UserInfoItem();
            model.UserName = user.LogonName;
            model.LogonName = user.UserName;
            model.Password = user.PasswordHash;
            model.StrStatus = user.StrStatus;
            model.RoleId = user.RoleId;
            model.UserCompanyValue = user.UserCompangValue;
            model.UserGasStationValue = user.UserGasStationValue;
            model.DomainAccount = user.DomainAccount;
            model.EmailAddress = user.EmailAddress;
            model.UserGasStationName = user.UserGasStationName;
            model.CreatedBy = user.CreateBy;
            await Task.Run(() => empBL.Create(model));
        }

        // 根据用户名找用户
        public async Task<IdentityModel> FindByNameAsync(string logonName)
        {
            IdentityModel result = null;

            result = await Task.Run(() => BL.GetLoginByName(logonName));
            return result;
        }

        public async Task<IdentityModel> FindByIdAsync(string userId)
        {
            IdentityModel result = null;
            result = await Task.Run(() => BL.GetLoginById(userId));
            return result;
        }



        public async Task UpdateAsync(IdentityModel user)
        {
            UserInfo empBL = new UserInfo();
            user.UpdateBy = LogonName;
            await Task.Run(() => empBL.UpdatePassword(user));
        }

        public void Dispose()
        {
        }

        public async Task<string> GetPasswordHashAsync(IdentityModel user)
        {
            return await Task.Run(() =>
            {
                return user.PasswordHash;
            });
        }

        public async Task<bool> HasPasswordAsync(IdentityModel user)
        {
            return await Task.Run(() =>
            {
                return !string.IsNullOrWhiteSpace(user.PasswordHash);
            });
        }

        public async Task SetPasswordHashAsync(IdentityModel user, string passwordHash)
        {
            await Task.Run(() =>
            {
                user.PasswordHash = passwordHash;
            });
        }
    }
}