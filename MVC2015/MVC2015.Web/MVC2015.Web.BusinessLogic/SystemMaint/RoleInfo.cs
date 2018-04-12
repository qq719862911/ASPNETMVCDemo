using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MVC2015.Web.Model;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using VM = MVC2015.Web.Model.SystemMaint.RoleInfo;
using System.Linq.Expressions;
using MVC2015.Web.BusinessLogic.Account;
using MVC2015.Utility;
using MVC2015.Utility.Common;
using MVC2015.Web.Model.SystemMaint.Module;
namespace MVC2015.Web.BusinessLogic.SystemMaint
{
    public class RoleInfo : IDisposable
    {
        //DB Data Context
        private readonly CTX.MVC2015DBContext Ctx;
        public RoleInfo()
        {
            Ctx = new CTX.MVC2015DBContext();
        }
        public RoleInfo(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }
        public IQueryable<VM.RoleInfoItem> GetItems(VM.RoleInfoSearch searchModel)
        {
            //Build search criteria lambda expression
            Expression<Func<MD.tbl_Common_Role, Boolean>> expr = BuildSearchCriteria(searchModel);
            //if not set the sort way ,then the default set will be used.
            string sortBy = "RoleId";
            string sortDirection = "DESC";
            if (!string.IsNullOrWhiteSpace(searchModel.SortBy))
            {
                sortBy = searchModel.SortBy;
                sortDirection = searchModel.SortDirection;
            }

            IQueryable<MD.tbl_Common_Role> resultQueryable;
            resultQueryable = this.Ctx.tbl_Common_Role;
            if (expr != null)
                resultQueryable = resultQueryable.Where(expr).SortWith(sortBy, sortDirection).Where(m => m.IsDeleted != true);
            else
                resultQueryable = resultQueryable.SortWith(sortBy, sortDirection).Where(m => m.IsDeleted != true);


            var result = resultQueryable.Select(a => new VM.RoleInfoItem()
            {
                RoleId = a.RoleId,
                Name = a.Name,
                Description = a.Description,
                CreatedDate = a.CreatedDate
            });
            return result;
        }

        public IQueryable<VM.RoleInfoItem> GetAllItems()
        {
            IQueryable<MD.tbl_Common_Role> resultQueryable;
            resultQueryable = this.Ctx.tbl_Common_Role;
            resultQueryable = resultQueryable.Where(m => m.IsDeleted != true);


            var result = resultQueryable.Select(a => new VM.RoleInfoItem()
            {
                RoleId = a.RoleId,
                Name = a.Name,
                Description = a.Description,
                CreatedDate = a.CreatedDate
            });
            return result;

        }
        private Expression<Func<MD.tbl_Common_Role, bool>> BuildSearchCriteria(VM.RoleInfoSearch searchModel)
        {
            if (searchModel == null)
            {
                throw new ArgumentNullException("searchModel is null.");
            }

            Expression<Func<MD.tbl_Common_Role, bool>> expr = null;
            DynamicLambda<MD.tbl_Common_Role> builder = new DynamicLambda<MD.tbl_Common_Role>();
            if (searchModel.BeginDate.HasValue)
            {
                Expression<Func<MD.tbl_Common_Role, bool>> temp = s => s.CreatedDate >= searchModel.BeginDate;
                expr = builder.BuildQueryAnd(expr, temp);
            }
            if (searchModel.EndDate.HasValue)
            {
                Expression<Func<MD.tbl_Common_Role, bool>> temp = s => s.CreatedDate <= searchModel.EndDate;
                expr = builder.BuildQueryAnd(expr, temp);
            }

            return expr;
        }
        public VM.RoleInfoItem GetItemById(int id)
        {
            VM.RoleInfoItem item;
            item = (from a in Ctx.tbl_Common_Role.Where(t => t.RoleId == id && t.IsDeleted != true)
                    select new VM.RoleInfoItem
                    {
                        RoleId = a.RoleId,
                        Name = a.Name,
                        Description = a.Description,
                        CreatedDate = a.CreatedDate
                    }).FirstOrDefault();

            return item;
        }

        public VM.RoleInfoItem GetItemByIdForDelete(int id)
        {
            VM.RoleInfoItem item;
            item = (from a in Ctx.tbl_Common_Role.Where(t => t.RoleId == id && t.IsDeleted == true)
                    select new VM.RoleInfoItem
                    {
                        RoleId = a.RoleId,
                        Name = a.Name,
                        Description = a.Description,
                        CreatedDate = a.CreatedDate
                    }).FirstOrDefault();

            return item;
        }

        public List<VM.RolePermission> GetRolePermission(int roleId)
        {
            List<VM.RolePermission> items = null;

            items = (from item in Ctx.tbl_Common_RolePermissionConfig
                     where item.RoleId == roleId
                     select new VM.RolePermission
                     {
                         CreatedBy = item.CreatedBy,
                         CreatedDate = item.CreatedDate,
                         Id = item.Id,
                         ModuleId = item.ModuleId,
                         RoleId = item.RoleId,
                         UpdatedBy = item.UpdatedBy,
                         UpdatedDate = item.UpdatedDate,
                         Value = item.Value
                     }).ToList();

            return items;
        }

        public bool Create(VM.RoleInfoItem model, List<MVC2015.Web.Model.SystemMaint.Module.ModulePermission> ModulePermissions)
        {
            MD.tbl_Common_Role mdRoleModel = new MD.tbl_Common_Role();
            mdRoleModel.Name = model.Name;
            mdRoleModel.Description = model.Description;
            mdRoleModel.CreatedBy = model.CreatedBy;
            mdRoleModel.CreatedDate = DateTime.Now;
            Ctx.tbl_Common_Role.Add(mdRoleModel);
            Ctx.SaveChanges();
            //添加角色权限列表 
            CreatePermission(ConvertToRolePermission(mdRoleModel.RoleId, model.CreatedBy, ModulePermissions));
            Ctx.SaveChanges();

            return true;
        }

        public List<VM.RolePermission> ConvertToRolePermission(int roleId, string currentUser, List<MVC2015.Web.Model.SystemMaint.Module.ModulePermission> ModulePermissions)
        {
            List<VM.RolePermission> rolePermissions = null;
            if (ModulePermissions != null)
            {
                rolePermissions = (from item in ModulePermissions
                                   group item by item.ModuleId into role
                                   select new VM.RolePermission
                                   {
                                       RoleId = roleId,
                                       ModuleId = role.Key,
                                       Value = role.Where(p => p.IsSelected).Sum(p => p.Value),
                                       CreatedBy = currentUser,
                                       CreatedDate = DateTime.Now,
                                       UpdatedBy = currentUser,
                                       UpdatedDate = DateTime.Now
                                   }).ToList();

            }

            return rolePermissions;
        }

        public bool Update(VM.RoleInfoItem model, List<ModulePermission> ModulePermissions)
        {
            MD.tbl_Common_Role mdRoleModel = Ctx.tbl_Common_Role.First(u => u.RoleId == model.RoleId);
            mdRoleModel.Name = model.Name;
            mdRoleModel.Description = model.Description;
            mdRoleModel.UpdatedBy = model.UpdatedBy;
            mdRoleModel.UpdatedDate = DateTime.Now;

            UpdatePermission(ConvertToRolePermission(mdRoleModel.RoleId, model.UpdatedBy, ModulePermissions));
            Ctx.SaveChanges();

            return true;
        }
        public bool Delete(int id, string userName)
        {

            MD.tbl_Common_Role date = Ctx.tbl_Common_Role.First(u => u.RoleId == id);
            date.IsDeleted = true;
            date.UpdatedBy = userName;
            date.UpdatedDate = DateTime.Now;
            //删除 角色与权限关系 未做
            foreach (MD.tbl_Common_RolePermissionConfig item in Ctx.tbl_Common_RolePermissionConfig.Where(p => p.RoleId == id))
            {
                item.Value = 0;
            }
            Ctx.SaveChanges();
            return true;
        }

        //public bool DeleteUserList(int id)
        //{
        //    MD.tbl_Common_RoleUser date = Ctx.tbl_Common_RoleUser.First(u => u.UserId == id);
        //    date.IsDeleted = true;
        //    Ctx.SaveChanges();
        //    return true;
        //}

        public void UpdatePermission(List<VM.RolePermission> rolePermissions)
        {
            if (rolePermissions != null)
            {
                MD.tbl_Common_RolePermissionConfig mdRolePermission = null;
                rolePermissions.ForEach(p =>
                {
                    mdRolePermission = Ctx.tbl_Common_RolePermissionConfig.FirstOrDefault(rp => rp.RoleId == p.RoleId && rp.ModuleId == p.ModuleId);
                    if (mdRolePermission != null)
                    {
                        mdRolePermission.Value = p.Value;
                        mdRolePermission.UpdatedBy = p.UpdatedBy;
                        mdRolePermission.UpdatedDate = p.UpdatedDate;
                    }
                    else
                    {
                        mdRolePermission = ConvertToMDRolePermission(p);
                        Ctx.tbl_Common_RolePermissionConfig.Add(mdRolePermission);
                    }
                });
            }
        }

        public void CreatePermission(List<VM.RolePermission> rolePermissions)
        {
            if (rolePermissions != null)
            {
                MD.tbl_Common_RolePermissionConfig mdRolePermission = null;
                rolePermissions.ForEach(p =>
                {
                    mdRolePermission = ConvertToMDRolePermission(p);
                    Ctx.tbl_Common_RolePermissionConfig.Add(mdRolePermission);

                });
            }
        }

        private MD.tbl_Common_RolePermissionConfig ConvertToMDRolePermission(VM.RolePermission rolePermission)
        {
            MD.tbl_Common_RolePermissionConfig mdRolePermission = null;
            if (rolePermission != null)
            {
                mdRolePermission = new MD.tbl_Common_RolePermissionConfig
                {
                    ModuleId = rolePermission.ModuleId,
                    RoleId = rolePermission.RoleId,
                    Value = rolePermission.Value,
                    CreatedBy = rolePermission.CreatedBy,
                    CreatedDate = rolePermission.CreatedDate
                };
            }

            return mdRolePermission;
        }


        public void Dispose()
        {
            if (Ctx != null)
            {
                Ctx.Dispose();
            }
        }




        public bool ValidateName(int Id, string name)
        {
            return Ctx.tbl_Common_Role.Any(m => m.RoleId != Id && m.Name == name.Trim() && m.IsDeleted != true);
        }

        public bool ValidateModulePermissions(List<ModulePermission> ModulePermissions)
        {
            foreach (var Model in ModulePermissions)
            {
                if (Model.IsSelected)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ValidateDelete(int id)
        {
            int countRoleId = this.Ctx.tbl_Common_RoleUser.Where(m => m.IsDeleted != true && m.RoleId == id).Count();
            if (countRoleId > 0)
                return true;
            else
                return false;
        }
    }
}
