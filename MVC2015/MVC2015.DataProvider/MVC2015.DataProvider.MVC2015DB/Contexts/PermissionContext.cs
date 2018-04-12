using MVC2015.DataProvider.MVC2015DB.Models;
using MVC2015.DataProvider.MVC2015DB.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.DataProvider.MVC2015DB.Contexts
{
    public class PermissionContext : BaseContext
    {
        public DbSet<tbl_Common_User> User { get; set; }
        public DbSet<tbl_Common_UserOfGasStation> UserOfGasStation { get; set; }
        public DbSet<tbl_Common_RoleUser> RoleUser { get; set; }
        public DbSet<tbl_Common_Role> Role { get; set; }
        public DbSet<tbl_Common_RolePermissionConfig> RolePermissionConfig { get; set; }
        public DbSet<tbl_Common_Module> Module { get; set; }
        public DbSet<tbl_Common_ModulePermissionDefine> ModulePermissionDefine { get; set; }
        public DbSet<tbl_Common_UserOfCompany> UserOfCompany { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new tbl_Common_UserMap());
            modelBuilder.Configurations.Add(new tbl_Common_UserOfGasStationMap());
            modelBuilder.Configurations.Add(new tbl_Common_RoleUserMap());
            modelBuilder.Configurations.Add(new tbl_Common_RoleMap());
            modelBuilder.Configurations.Add(new tbl_Common_RolePermissionConfigMap());
            modelBuilder.Configurations.Add(new tbl_Common_ModuleMap());
            modelBuilder.Configurations.Add(new tbl_Common_ModulePermissionDefineMap());
            modelBuilder.Configurations.Add(new tbl_Common_UserOfCompanyMap());
        }
    }
}
