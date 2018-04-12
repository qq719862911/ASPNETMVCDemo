using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using VM = MVC2015.Web.Model;

namespace MVC2015.Web.BusinessLogic.SystemMaint
{
    public class Module : IDisposable
    {
        //DB Data Context
        private readonly CTX.MVC2015DBContext Ctx;

        public Module()
        {
            Ctx = new CTX.MVC2015DBContext();
        }

        public Module(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }

        public List<VM.SystemMaint.Module.ModuleItem> GetModuleItem()
        {
            List<VM.SystemMaint.Module.ModuleItem> moduleItems = (from item in Ctx.tbl_Common_Module
                                                                  select new VM.SystemMaint.Module.ModuleItem
                                                                  {
                                                                      Description = item.Description,
                                                                      MenuGroupOrder = item.MenuGroupOrder,
                                                                      DisplayOrder = item.DisplayOrder,
                                                                      IsMenu = item.IsMenu,
                                                                      ModuleId = item.ModuleId,
                                                                      ParentId = item.ParentId,
                                                                      ResourceKey = item.ResourceKey
                                                                  }).ToList();

            return moduleItems;


        }

        public List<VM.SystemMaint.Module.ModuleItem> GetHierarchyModuleItem(List<VM.SystemMaint.RoleInfo.RolePermission> rolePermissions = null)
        {
            List<VM.SystemMaint.Module.ModuleItem> moduleItems = GetModuleItem();

            List<VM.SystemMaint.Module.ModulePermission> modulePermissions = GetModulePermission();

            if (rolePermissions != null && rolePermissions.Count > 0)
            {
                rolePermissions.ForEach(p =>
                {
                    modulePermissions.Where(mp => mp.ModuleId == p.ModuleId).ToList().ForEach(item =>
                    {
                        if ((item.Value & p.Value) > 0)
                        {
                            item.IsSelected = true;
                        }
                    });
                });
            }

            moduleItems.ForEach(p =>
            {
                p.ModulePermissions = modulePermissions.Where(mp => mp.ModuleId == p.ModuleId).ToList();
            });

            return HierarchyModuleItem(moduleItems);
        }

        private List<VM.SystemMaint.Module.ModuleItem> HierarchyModuleItem(List<VM.SystemMaint.Module.ModuleItem> moduleItems, int? parentId = null)
        {
            List<VM.SystemMaint.Module.ModuleItem> HierarchyItems = null;

            if (moduleItems != null && moduleItems.Count > 0)
            {
                if (parentId.HasValue)
                {
                    HierarchyItems = moduleItems.Where(p => p.ParentId == parentId).OrderBy(p => p.DisplayOrder).ToList();
                }
                else
                {
                    HierarchyItems = moduleItems.Where(p => !moduleItems.Any(m => m.ModuleId == p.ParentId)).OrderBy(p => p.MenuGroupOrder).ToList();
                }

                HierarchyItems.ForEach(p =>
                {
                    p.ChildModuleItems = HierarchyModuleItem(moduleItems, p.ModuleId);
                });
            }

            return HierarchyItems;
        }

        private List<VM.SystemMaint.Module.ModulePermission> GetModulePermission()
        {
            List<VM.SystemMaint.Module.ModulePermission> modulePermissions = (from item in Ctx.tbl_Common_ModulePermissionDefine
                                                                              select new VM.SystemMaint.Module.ModulePermission
                                                                              {
                                                                                  Action = item.Action,
                                                                                  Area = item.Area,
                                                                                  Controller = item.Controller,
                                                                                  Description = item.Description,
                                                                                  Id = item.Id,
                                                                                  ModuleId = item.ModuleId,
                                                                                  ResourceKey = item.ResourceKey,
                                                                                  Value = item.Value,
                                                                                  ParentValue = item.ParentValue
                                                                              }).ToList();

            return modulePermissions;
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
