using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using VM = MVC2015.Web.Model.SystemMaint.UserInfo;
using System.Linq.Expressions;
using System.Web.Mvc;
using MVC2015.Utility.Common;
using MVC2015.Web.Model.Account;
using MVC2015.Web.Model.Email;
using MVC2015.Web.BusinessLogic.Account;
using MVC2015.Utility;
using MVC2015.Web.Model.KeyValue;

namespace MVC2015.Web.BusinessLogic.SystemMaint
{
    public class UserInfo : IDisposable
    {
        //DB Data Context
        private readonly CTX.MVC2015DBContext Ctx;
        public UserInfo()
        {
            Ctx = new CTX.MVC2015DBContext();
        }
        public UserInfo(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }
        public IQueryable<VM.UserInfoItem> GetItems(VM.UserInfoSearch searchModel)
        {
            //Build search criteria lambda expression
            Expression<Func<MD.tbl_Common_User, Boolean>> expr = BuildSearchCriteria(searchModel);
            //if not set the sort way ,then the default set will be used.
            string sortBy = "UserId";
            string sortDirection = "DESC";
            if (!string.IsNullOrWhiteSpace(searchModel.SortBy))
            {
                sortBy = searchModel.SortBy;
                sortDirection = searchModel.SortDirection;
            }

            IQueryable<MD.tbl_Common_User> resultQueryable;
            resultQueryable = this.Ctx.tbl_Common_User;
            if (expr != null)
                resultQueryable = resultQueryable.Where(expr).SortWith(sortBy, sortDirection).Where(m => m.IsDeleted != true);
            else
                resultQueryable = resultQueryable.SortWith(sortBy, sortDirection).Where(m => m.IsDeleted != true);

            //filter tbl_Common_RoleUser then IsDeleted not be true
            IQueryable<MD.tbl_Common_RoleUser> roleUserQueryable;
            roleUserQueryable = this.Ctx.tbl_Common_RoleUser;
            roleUserQueryable = roleUserQueryable.Where(m => m.IsDeleted != true);

            var result = (from a in resultQueryable
                          join ru in roleUserQueryable on a.UserId equals ru.UserId into ruinner
                          from rui in ruinner.DefaultIfEmpty()
                          join r in Ctx.tbl_Common_Role on rui.RoleId equals r.RoleId into rinner
                          from ri in rinner.DefaultIfEmpty()
                          select new VM.UserInfoItem()
                          {
                              UserId = a.UserId,
                              LogonName = a.LogonName,
                              DomainAccount = a.DomainAccount,
                              UserName = a.UserName,
                              Password = a.Password,
                              Status = a.Status,
                              EmailAddress = a.EmailAddress,
                              CreatedDate = a.CreatedDate,
                              UpdatedDate = a.UpdatedDate,
                              RoleId = rui.RoleId,
                              RoleName = ri.Name
                              //GasStationName=a.tbl_Common_UserOfGasStation[0]
                          });
            return result;
        }


        private Expression<Func<MD.tbl_Common_User, bool>> BuildSearchCriteria(VM.UserInfoSearch searchModel)
        {
            if (searchModel == null)
            {
                throw new ArgumentNullException("searchModel is null.");
            }

            Expression<Func<MD.tbl_Common_User, bool>> expr = null;
            DynamicLambda<MD.tbl_Common_User> builder = new DynamicLambda<MD.tbl_Common_User>();
            if (searchModel.BeginDate.HasValue)
            {
                Expression<Func<MD.tbl_Common_User, bool>> temp = s => s.CreatedDate >= searchModel.BeginDate;
                expr = builder.BuildQueryAnd(expr, temp);
            }
            if (searchModel.EndDate.HasValue)
            {
                Expression<Func<MD.tbl_Common_User, bool>> temp = s => s.CreatedDate <= searchModel.EndDate;
                expr = builder.BuildQueryAnd(expr, temp);
            }
            if (searchModel.LogonName != null)
            {
                Expression<Func<MD.tbl_Common_User, Boolean>> temp = s => s.LogonName.Contains(searchModel.LogonName);
                expr = builder.BuildQueryAnd(expr, temp);
            }
            if (searchModel.UserName != null)
            {
                Expression<Func<MD.tbl_Common_User, Boolean>> temp = s => s.UserName.Contains(searchModel.UserName);
                expr = builder.BuildQueryAnd(expr, temp);
            }

            return expr;
        }

        public VM.UserInfoItem GetItemById(int id)
        {
            VM.UserInfoItem item;
            item = (from a in Ctx.tbl_Common_User
                    join ru in Ctx.tbl_Common_RoleUser on a.UserId equals ru.UserId into ruinner
                    from rui in ruinner.DefaultIfEmpty()
                    join r in Ctx.tbl_Common_Role on rui.RoleId equals r.RoleId into rinner
                    from ri in rinner.DefaultIfEmpty()
                    join c in Ctx.tbl_Company on a.CompanyId equals c.ID into cinner
                    from ci in cinner.DefaultIfEmpty()
                    where a.UserId == id && a.IsDeleted != true
                    select new VM.UserInfoItem
                    {
                        UserId = a.UserId,
                        LogonName = a.LogonName,
                        DomainAccount = a.DomainAccount,
                        UserName = a.UserName,
                        Password = a.Password,
                        Status = a.Status,
                        StrStatus = a.Status.ToString(),
                        EmailAddress = a.EmailAddress,
                        CreatedDate = a.CreatedDate,
                        UpdatedDate = a.UpdatedDate,
                        RoleId = rui.RoleId,
                        RoleName = ri.Name,
                        CompanyName = ci.Name
                    }).FirstOrDefault();
            if (item != null)
            {
                //set Company
                item.CompanySelectList = (from c in Ctx.tbl_Company
                                          join uc in Ctx.tbl_Common_UserOfCompany on c.ID equals uc.CompanyId into ucinner
                                          from uci in ucinner.DefaultIfEmpty()
                                          where c.IsDeleted != true && uci.IsDeleted != true && uci.UserId == id
                                          select new SelectListItem
                                          {
                                              Text = c.Name,
                                              Value = c.ID.ToString()
                                          }).ToList();
                for (int i = 0; i < item.CompanySelectList.Count(); i++)
                {
                    if (i > 0)
                    {
                        item.UserCompanyName += ", " + item.CompanySelectList[i].Text;
                        item.UserCompanyValue += "," + item.CompanySelectList[i].Value;
                    }
                    else
                    {
                        item.UserCompanyName += item.CompanySelectList[i].Text;
                        item.UserCompanyValue += item.CompanySelectList[i].Value;
                    }
                }
            }
            return item;
        }

        public VM.UserInfoItem GetItemByIdForDelete(int id)
        {
            VM.UserInfoItem item;
            item = (from a in Ctx.tbl_Common_User
                    where a.UserId == id && a.IsDeleted == true
                    select new VM.UserInfoItem
                    {
                        UserId = a.UserId,
                        LogonName = a.LogonName,
                        DomainAccount = a.DomainAccount,
                        UserName = a.UserName,
                        Password = a.Password,
                        Status = a.Status,
                        StrStatus = a.Status.ToString(),
                        EmailAddress = a.EmailAddress,
                        CreatedDate = a.CreatedDate,
                        UpdatedDate = a.UpdatedDate
                    }).FirstOrDefault();
            return item;
        }

        public IQueryable<VM.UserInfoItem> GetItemByRoleId(int id)
        {
            var item = (from a in Ctx.tbl_Common_User
                        join ru in Ctx.tbl_Common_RoleUser on a.UserId equals ru.UserId into ruinner
                        from rui in ruinner.DefaultIfEmpty()
                        join r in Ctx.tbl_Common_Role on rui.RoleId equals r.RoleId into rinner
                        from ri in rinner.DefaultIfEmpty()
                        where ri.RoleId == id && a.IsDeleted != true && rui.IsDeleted != true
                        select new VM.UserInfoItem
                        {
                            UserId = a.UserId,
                            LogonName = a.LogonName,
                            DomainAccount = a.DomainAccount,
                            UserName = a.UserName,
                            Password = a.Password,
                            Status = a.Status,
                            StrStatus = a.Status.ToString(),
                            EmailAddress = a.EmailAddress,
                            CreatedDate = a.CreatedDate,
                            UpdatedDate = a.UpdatedDate,
                            RoleId = rui.RoleId,
                            RoleName = ri.Name,
                        }
                );

            return item;
        }

        public List<VM.UserInfoItem> GetItemByCompanyId(int id)
        {
            List<VM.UserInfoItem> item;
            item = (from u in Ctx.tbl_Common_User
                    join uc in Ctx.tbl_Common_UserOfCompany on u.UserId equals uc.UserId into ucinner
                    from uci in ucinner.DefaultIfEmpty()
                    join c in Ctx.tbl_Company on uci.CompanyId equals c.ID into cinner
                    from ci in cinner.DefaultIfEmpty()
                    join ru in Ctx.tbl_Common_RoleUser on u.UserId equals ru.UserId into ruinner
                    from rui in ruinner.DefaultIfEmpty()
                    join r in Ctx.tbl_Common_Role on rui.RoleId equals r.RoleId into rinner
                    from ri in rinner.DefaultIfEmpty()
                    where uci.CompanyId == id && u.IsDeleted != true && uci.IsDeleted != true
                    select new VM.UserInfoItem
                    {
                        UserId = u.UserId,
                        LogonName = u.LogonName,
                        UserName = u.UserName,
                        Status = u.Status,
                        StrStatus = u.Status.ToString(),
                        CompanyName = ci.Name,
                        RoleId = rui.RoleId,
                        RoleName = ri.Name
                    }
                ).ToList();

            return item;
        }

        public IQueryable<VM.UserInfoItem> GetAllItemById(VM.UserInfoSearch searchModel)
        {
            //Build search criteria lambda expression
            Expression<Func<MD.tbl_Common_User, Boolean>> expr = BuildSearchCriteria(searchModel);
            //if not set the sort way ,then the default set will be used.
            string sortBy = "UserId";
            string sortDirection = "DESC";
            if (!string.IsNullOrWhiteSpace(searchModel.SortBy))
            {
                sortBy = searchModel.SortBy;
                sortDirection = searchModel.SortDirection;
            }
            IQueryable<MD.tbl_Common_User> resultQueryable;
            resultQueryable = this.Ctx.tbl_Common_User;
            if (expr != null)
                resultQueryable = resultQueryable.Where(expr).SortWith(sortBy, sortDirection).Where(m => m.IsDeleted != true);
            else
                resultQueryable = resultQueryable.SortWith(sortBy, sortDirection).Where(m => m.IsDeleted != true);

            var item = (from a in resultQueryable
                        join ru in Ctx.tbl_Common_RoleUser on a.UserId equals ru.UserId into ruinner
                        from rui in ruinner.DefaultIfEmpty()
                        join r in Ctx.tbl_Common_Role on rui.RoleId equals r.RoleId into rinner
                        from ri in rinner.DefaultIfEmpty()
                        where a.IsDeleted != true
                        select new VM.UserInfoItem
                        {
                            UserId = a.UserId,
                            LogonName = a.LogonName,
                            UserName = a.UserName,
                            RoleId = rui.RoleId,
                            RoleName = ri.Name,
                            RoleIsDeleted = rui.IsDeleted,
                        }

                );


            return item;
        }

        public IQueryable<VM.UserInfoItem> GetAllUserItemForCompany(VM.UserInfoSearch searchModel)
        {
            //Build search criteria lambda expression
            Expression<Func<MD.tbl_Common_User, Boolean>> expr = BuildSearchCriteria(searchModel);
            //if not set the sort way ,then the default set will be used.
            string sortBy = "UserId";
            string sortDirection = "DESC";
            if (!string.IsNullOrWhiteSpace(searchModel.SortBy))
            {
                sortBy = searchModel.SortBy;
                sortDirection = searchModel.SortDirection;
            }
            IQueryable<MD.tbl_Common_User> resultQueryable;
            resultQueryable = this.Ctx.tbl_Common_User;
            if (expr != null)
                resultQueryable = resultQueryable.Where(expr).SortWith(sortBy, sortDirection).Where(m => m.IsDeleted != true);
            else
                resultQueryable = resultQueryable.SortWith(sortBy, sortDirection).Where(m => m.IsDeleted != true);

            var item = (from a in resultQueryable
                        join ru in Ctx.tbl_Common_RoleUser on a.UserId equals ru.UserId into ruinner
                        from rui in ruinner.DefaultIfEmpty()
                        join r in Ctx.tbl_Common_Role on rui.RoleId equals r.RoleId into rinner
                        from ri in rinner.DefaultIfEmpty()
                        where a.IsDeleted != true
                        select new VM.UserInfoItem
                        {
                            UserId = a.UserId,
                            LogonName = a.LogonName,
                            UserName = a.UserName,
                            RoleId = rui.RoleId,
                            RoleName = ri.Name
                        }

                );
            return item;
        }

        //public List<SelectListItem> GetAllGasStationSelectList()
        //{
        //    var allGasStation = (from gasStation in Ctx.tbl_Gs_GasStationInfo
        //                         where gasStation.IsDelete != true
        //                         select new SelectListItem
        //                         {
        //                             Text = gasStation.Name,
        //                             Value = gasStation.GasStationId.ToString(),
        //                             Selected = true
        //                         });
        //    return allGasStation.ToList();

        //}

        public bool Create(VM.UserInfoItem model)
        {
            MD.tbl_Common_User date = new MD.tbl_Common_User();
            date.UserName = model.UserName;
            date.LogonName = model.LogonName;
            //model.StatusList.First().
            if (model.StrStatus.ToString() == UserStatus.Normal.strValue)
            {
                date.Status = UserStatus.Normal.intValue;
            }
            else
            {
                date.Status = UserStatus.Disable.intValue;
            }

            date.DomainAccount = model.DomainAccount;
            date.EmailAddress = model.EmailAddress;
            date.CreatedDate = DateTime.Now;
            date.CreatedBy = model.CreatedBy;
            date.UpdatedDate = DateTime.Now;
            date.UpdatedBy = model.UpdatedBy;

            //生成密码
            //IPasswordPolicy Password = new RandomPassword();
            //string orginalPassword = Password.GeneratePassword();
            //date.Password = HashEncrypt.SHA512Encrypt(orginalPassword);
            date.Password = model.Password;

            Ctx.tbl_Common_User.Add(date);

            //生成角色关系
            MD.tbl_Common_RoleUser roleUser = new MD.tbl_Common_RoleUser();
            roleUser.RoleId = model.RoleId.Value;
            roleUser.UserId = date.UserId;
            roleUser.CreatedDate = DateTime.Now;
            roleUser.CreatedBy = model.CreatedBy;
            //roleUser.UpdatedDate = DateTime.Now;
            //roleUser.UpdatedBy = model.UpdatedBy;

            Ctx.tbl_Common_RoleUser.Add(roleUser);
            Ctx.SaveChanges();

            //生成公司关系
            updateUserOfCompany(roleUser.UserId, model.UserCompanyValue, model.CreatedBy);

            //生成 气站关系 
            // updateUserOfGasStation(roleUser.UserId, model.UserGasStationValue, model.CreatedBy);
            //发送邮件 
            //if(date.Status==1)
            //    SendEmail(date, orginalPassword, "User_New");
            return true;
        }

        private void updateUserOfCompany(int userId, string userCompanyValue, string updateBy)
        {
            var delItem = Ctx.tbl_Common_UserOfCompany.Where(m => m.UserId == userId);
            if (delItem.Count() > 0)
            {
                foreach (var item in delItem)
                {
                    item.UpdatedBy = updateBy;
                    item.UpdatedDate = DateTime.Now;
                    item.IsDeleted = true;
                }
            }
            if (userCompanyValue == null)
            {
                Ctx.SaveChanges();
                return;
            }

            var CompanyIdList = userCompanyValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var companyId in CompanyIdList)
            {
                int cpid = Int32.Parse(companyId);
                var item = Ctx.tbl_Common_UserOfCompany.Where(m => m.CompanyId == cpid && m.UserId == userId).DefaultIfEmpty();
                if (item.Count() == 0 || item.First() == null)
                {
                    if (companyId != "")
                    {
                        MD.tbl_Common_UserOfCompany newModel1 = new MD.tbl_Common_UserOfCompany
                        {
                            UserId = userId,
                            CompanyId = int.Parse(companyId),
                            CreatedBy = updateBy,
                            CreatedDate = DateTime.Now
                        };
                        Ctx.tbl_Common_UserOfCompany.Add(newModel1);
                    }
                }

                else
                    item.First().IsDeleted = false;
            }
            try
            {
                Ctx.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
            {
                string msg = ex.Message;
                throw;
            }

        }
        public bool UpdateUserCompany(string allSelectList, int currUserId, string logonName)
        {

            var delItem = Ctx.tbl_Common_UserOfCompany.Where(m => m.UserId == currUserId);
            if (delItem.Count() > 0)
            {
                foreach (var item in delItem)
                {
                    item.UpdatedBy = logonName;
                    item.UpdatedDate = DateTime.Now;
                    item.IsDeleted = true;
                }
            }
            if (allSelectList == null)
            {
                Ctx.SaveChanges();
                return true;
            }
            var UserIdList = allSelectList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var UserId in UserIdList)
            {
                int uid = Int32.Parse(UserId);
                var item = Ctx.tbl_Common_UserOfCompany.Where(m => m.CompanyId == uid && m.UserId == currUserId).DefaultIfEmpty();
                if (item.Count() == 0 || item.First() == null)
                {
                    if (UserId != "")
                        Ctx.tbl_Common_UserOfCompany.Add(new MD.tbl_Common_UserOfCompany
                        {
                            CompanyId = int.Parse(UserId),
                            UserId = currUserId,
                            CreatedBy = logonName,
                            CreatedDate = DateTime.Now
                        });
                }
                else
                    item.First().IsDeleted = false;
            }
            Ctx.SaveChanges();

            return true;
        }
        private void updateUserOfGasStation(int userId, string userGasStationValue, string updateBy)
        {
            var delItme = Ctx.tbl_Common_UserOfGasStation.Where(m => m.UserId == userId);
            if (delItme.Count() > 0)
            {
                foreach (var itme in delItme)
                {
                    itme.UpdatedBy = updateBy;
                    itme.UpdatedDate = DateTime.Now;
                    itme.IsDeleted = true;
                }
            }
            if (userGasStationValue == null)
            {

                Ctx.SaveChanges();
                return;
            }
            var GasStationIdList = userGasStationValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var gasStationId in GasStationIdList)
            {
                int gsid = Int32.Parse(gasStationId);
                var itme = Ctx.tbl_Common_UserOfGasStation.Where(m => m.GasStationId == gsid && m.UserId == userId).DefaultIfEmpty();
                if (itme.Count() == 0 || itme.First() == null)
                {
                    if (gasStationId != "")
                        Ctx.tbl_Common_UserOfGasStation.Add(new MD.tbl_Common_UserOfGasStation
                        {
                            UserId = userId,
                            GasStationId = int.Parse(gasStationId),
                            CreatedBy = updateBy,
                            CreatedDate = DateTime.Now

                        });
                }
                else
                    itme.First().IsDeleted = false;

            }

            Ctx.SaveChanges();
        }

        public bool UserUpdate(VM.UserInfoItem model)
        {
            MD.tbl_Common_User date = Ctx.tbl_Common_User.First(u => u.UserId == model.UserId);
            date.UserName = model.UserName;
            date.EmailAddress = model.EmailAddress;
            date.UpdatedBy = model.UpdatedBy;
            date.UpdatedDate = DateTime.Now;

            Ctx.SaveChanges();

            return true;
        }
        public List<SelectListItem> GetAllItems()
        {
            IQueryable<MD.tbl_Common_Role> resultQueryable;
            resultQueryable = this.Ctx.tbl_Common_Role;
            resultQueryable = resultQueryable.Where(m => m.IsDeleted != true);
            var result = resultQueryable.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.RoleId.ToString()
            });
            return result.ToList();

        }
        public bool Update(VM.UserInfoItem model)
        {
            MD.tbl_Common_User date = Ctx.tbl_Common_User.First(u => u.UserId == model.UserId);
            date.UserName = model.UserName;
            date.LogonName = model.LogonName;
            if (model.StrStatus.ToString() == UserStatus.Normal.strValue)
            {
                date.Status = UserStatus.Normal.intValue;
            }
            else
            {
                date.Status = UserStatus.Disable.intValue;
            }
            date.DomainAccount = model.DomainAccount;
            date.EmailAddress = model.EmailAddress;
            date.UpdatedBy = model.UpdatedBy;
            date.UpdatedDate = DateTime.Now;
            //Ctx.SaveChanges();
            //修改公司关系
            updateUserOfCompany(date.UserId, model.UserCompanyValue, model.UpdatedBy);
            //UpdateUserCompany(model.UserCompanyValue, date.UserId, model.UpdatedBy);

            Ctx.SaveChanges();

            //修改 角色


            if (Ctx.tbl_Common_RoleUser.Where(m => m.UserId == model.UserId).Count() == 0)
            {
                MD.tbl_Common_RoleUser roleUser = new MD.tbl_Common_RoleUser();
                roleUser.RoleId = model.RoleId.Value;
                roleUser.UserId = date.UserId;
                roleUser.CreatedDate = DateTime.Now;
                roleUser.CreatedBy = model.UpdatedBy;
                //roleUser.UpdatedDate = DateTime.Now;
                //roleUser.UpdatedBy = model.UpdatedBy;

                Ctx.tbl_Common_RoleUser.Add(roleUser);
                Ctx.SaveChanges();
            }
            else
            {
                MD.tbl_Common_RoleUser roleUser = Ctx.tbl_Common_RoleUser.First(m => m.UserId == model.UserId);
                roleUser.RoleId = model.RoleId.Value;
                roleUser.IsDeleted = false;
                roleUser.UpdatedDate = DateTime.Now;
                roleUser.UpdatedBy = model.UpdatedBy;
                Ctx.SaveChanges();
            }

            updateUserOfGasStation(date.UserId, model.UserGasStationValue, model.UpdatedBy);
            return true;
        }
        public bool ResetPassword(int id, string userName)
        {

            MD.tbl_Common_User date = Ctx.tbl_Common_User.First(u => u.UserId == id);
            if (date.Status == 1)
            {
                IPasswordPolicy Password = new RandomPassword();
                string orginalPassword = Password.GeneratePassword();
                date.Password = HashEncrypt.SHA512Encrypt(orginalPassword);
                date.UpdatedBy = userName;
                date.UpdatedDate = DateTime.Now;
                Ctx.SaveChanges();
                //发送邮件 
                //SendEmail(date, orginalPassword, "User_ResetPassword");
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdatePassword(IdentityModel model)
        {

            MD.tbl_Common_User date = Ctx.tbl_Common_User.Where(c => c.UserId.ToString() == model.Id).FirstOrDefault();

            date.Password = model.PasswordHash;
            date.UpdatedBy = model.UpdateBy;
            date.UpdatedDate = DateTime.Now;
            Ctx.SaveChanges();


            return true;
        }
        public bool UpdatePasswordDb(VM.UserInfoItem model)
        {

            MD.tbl_Common_User date = Ctx.tbl_Common_User.Where(c => c.UserId == model.UserId).FirstOrDefault();

            date.Password = model.Password;
            date.UpdatedBy = model.UpdatedBy;
            date.UpdatedDate = DateTime.Now;
            Ctx.SaveChanges();


            return true;
        }
        public bool UpdateUserRole(string allSelectList, int currRoleId, string deleteUserIds, string logonName)
        {
            if (allSelectList != null)
            {
                string[] selectId = allSelectList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < selectId.Length; i++)
                {
                    if (selectId[i] != "")
                    {
                        int num = Convert.ToInt32(selectId[i].ToString().Trim());
                        if (Ctx.tbl_Common_RoleUser.Where(m => m.UserId == num).Count() == 0)
                        {
                            MD.tbl_Common_RoleUser roleUser = new MD.tbl_Common_RoleUser();
                            roleUser.UserId = num;
                            roleUser.RoleId = currRoleId;
                            roleUser.CreatedDate = DateTime.Now;
                            roleUser.CreatedBy = logonName;

                            Ctx.tbl_Common_RoleUser.Add(roleUser);
                            Ctx.SaveChanges();
                        }
                        else
                        {
                            MD.tbl_Common_RoleUser date = Ctx.tbl_Common_RoleUser.First(u => u.UserId == num);
                            date.RoleId = currRoleId;
                            date.IsDeleted = false;
                            date.UpdatedBy = logonName;
                            date.UpdatedDate = DateTime.Now;
                            Ctx.SaveChanges();
                        }
                    }
                }
            }

            if (deleteUserIds != null)
            {
                string[] delectId = deleteUserIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < delectId.Length; i++)
                {
                    if (delectId[i] != "")
                    {
                        int num = Convert.ToInt32(delectId[i].ToString().Trim());
                        MD.tbl_Common_RoleUser date = Ctx.tbl_Common_RoleUser.First(u => u.UserId == num);
                        date.RoleId = null;
                        date.IsDeleted = true;
                        date.UpdatedBy = logonName;
                        date.UpdatedDate = DateTime.Now;
                        Ctx.SaveChanges();
                    }
                }
            }

            return true;
        }


        public bool Delete(int id, string userName)
        {

            MD.tbl_Common_User date = Ctx.tbl_Common_User.First(u => u.UserId == id);
            date.IsDeleted = true;
            date.UpdatedBy = userName;
            date.UpdatedDate = DateTime.Now;
            Ctx.SaveChanges();

            MD.tbl_Common_RoleUser roleUser = Ctx.tbl_Common_RoleUser.First(u => u.UserId == id);
            roleUser.IsDeleted = true;
            roleUser.UpdatedBy = userName;
            roleUser.UpdatedDate = DateTime.Now;
            Ctx.SaveChanges();

            var userOfGasItem = Ctx.tbl_Common_UserOfGasStation.Where(u => u.IsDeleted != true && u.UserId == id);
            foreach (var userOfGas in userOfGasItem)
            {
                userOfGas.IsDeleted = true;
                userOfGas.UpdatedBy = userName;
                userOfGas.UpdatedDate = DateTime.Now;
            }
            Ctx.SaveChanges();

            var userOfCompanyItem = Ctx.tbl_Common_UserOfCompany.Where(u => u.IsDeleted != true && u.UserId == id);
            foreach (var userOfCompany in userOfCompanyItem)
            {
                userOfCompany.IsDeleted = true;
                userOfCompany.UpdatedBy = userName;
                userOfCompany.UpdatedDate = DateTime.Now;
            }
            Ctx.SaveChanges();

            return true;
        }
        public void Dispose()
        {
            if (Ctx != null)
            {
                Ctx.Dispose();
            }
        }


        public VM.UserInfoItem GetItemByName(string userName)
        {
            VM.UserInfoItem item;
            item = (from a in Ctx.tbl_Common_User
                    join ru in Ctx.tbl_Common_RoleUser on a.UserId equals ru.UserId into ruinner
                    from rui in ruinner.DefaultIfEmpty()
                    join r in Ctx.tbl_Common_Role on rui.RoleId equals r.RoleId into rinner
                    from ri in rinner.DefaultIfEmpty()
                    where a.LogonName == userName
                    select new VM.UserInfoItem
                    {
                        UserId = a.UserId,
                        LogonName = a.LogonName,
                        DomainAccount = a.DomainAccount,
                        UserName = a.UserName,
                        Password = a.Password,
                        Status = a.Status,
                        StrStatus = a.Status.ToString(),
                        EmailAddress = a.EmailAddress,
                        CreatedDate = a.CreatedDate,
                        UpdatedDate = a.UpdatedDate,
                        RoleId = rui.RoleId,
                        RoleName = ri.Name,
                    }).FirstOrDefault();

            //item.GasStationSelectList = (from g in Ctx.tbl_Gs_GasStationInfo
            //                             join ug in Ctx.tbl_Common_UserOfGasStation on g.GasStationId equals ug.GasStationId
            //                             where item.UserId == ug.UserId && g.IsDelete != true && ug.IsDeleted != true
            //                             select new SelectListItem
            //                             {
            //                                 Text = g.Name,
            //                                 Value = g.GasStationId.ToString()
            //                             }).ToList();
            for (int i = 0; i < item.GasStationSelectList.Count(); i++)
            {
                if (i > 0)
                {
                    item.UserGasStationName += "," + item.GasStationSelectList[i].Text;
                    item.UserGasStationValue += "," + item.GasStationSelectList[i].Value;
                }
                else
                {
                    item.UserGasStationName += item.GasStationSelectList[i].Text;
                    item.UserGasStationValue += item.GasStationSelectList[i].Value;
                }
            }
            return item;
        }

        #region ======Send Email======


        public void SendEmail(VM.UserInfoItem model, string password, string emailTemplateName)
        {
            bool isSendEmailtoIIS = false;
            var sendEmailInfo = new SendEmailInfo();
            var email = Ctx.tbl_EmailTemplate.FirstOrDefault(t => t.Name == emailTemplateName);
            string[] emailSendTo = { model.EmailAddress };

            string url = string.Format("{0}/", EmailTemplateParam.GetHostName());

            sendEmailInfo.mailSubject = email.EmailSubject.Replace(EmailTemplateParam.SystemName, EmailTemplateParam.SystemNameBasic);
            sendEmailInfo.mailBody = email.EmailContent.Replace(EmailTemplateParam.UserDisplayName, model.LogonName)
                .Replace(EmailTemplateParam.URL, url)
                .Replace(EmailTemplateParam.UserName, model.LogonName)
                .Replace(EmailTemplateParam.Password, password)
                .Replace("[DateTime]", DateTime.Now.ToString("yyyy年MM月dd日"));

            //MailHelper.SendMail(emailSendTo, sendEmailInfo.mailSubject, sendEmailInfo.mailBody, 0, isSendEmailtoIIS, true);

        }


        #endregion

        //获取气站列表
        //public VM.UserInfoItem GetItemByCompany(VM.UserInfoItem item, string value)
        //{
        //    //
        //    var CompanyIdList = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    List<SelectListItem> GasStation = new List<SelectListItem>();
        //    item.GasStationSelectList = new List<SelectListItem>();
        //    item.UserGasStationName = null;
        //    item.UserGasStationValue = null;
        //    foreach (string CompanyId in CompanyIdList)
        //    {
        //        int companyId = Int32.Parse(CompanyId);
        //        var gasStation = (from g in Ctx.tbl_Gs_GasStationInfo
        //                          join ug in Ctx.tbl_Common_UserOfGasStation on g.GasStationId equals ug.GasStationId
        //                          where item.UserId == ug.UserId && g.IsDelete != true && ug.IsDeleted != true && g.Company == companyId
        //                          select new SelectListItem
        //                          {
        //                              Text = g.Name,
        //                              Value = g.GasStationId.ToString()
        //                          }).ToList();
        //        foreach (SelectListItem g in gasStation)
        //        {
        //            item.GasStationSelectList.Add(new SelectListItem
        //            {
        //                Text = g.Text,
        //                Value = g.Value
        //            });
        //        }
        //    }



        //    //item.GasStationSelectList = (from g in Ctx.tbl_Gs_GasStationInfo
        //    //                             join ug in Ctx.tbl_Common_UserOfGasStation on g.GasStationId equals ug.GasStationId
        //    //                             where item.UserId == ug.UserId && g.IsDelete != true && ug.IsDeleted != true
        //    //                             select new SelectListItem
        //    //                             {
        //    //                                 Text = g.Name,
        //    //                                 Value = g.GasStationId.ToString()
        //    //                             }).ToList();


        //    for (int i = 0; i < item.GasStationSelectList.Count(); i++)
        //    {
        //        if (i > 0)
        //        {
        //            item.UserGasStationName += "," + item.GasStationSelectList[i].Text;
        //            item.UserGasStationValue += "," + item.GasStationSelectList[i].Value;
        //        }
        //        else
        //        {
        //            item.UserGasStationName += item.GasStationSelectList[i].Text;
        //            item.UserGasStationValue += item.GasStationSelectList[i].Value;
        //        }
        //    }
        //    return item;
        //}

        public bool ValidateName(int Id, string name)
        {
            return Ctx.tbl_Common_User.Any(m => m.UserId != Id && m.LogonName == name.Trim() && m.IsDeleted != true);
        }
    }

}
