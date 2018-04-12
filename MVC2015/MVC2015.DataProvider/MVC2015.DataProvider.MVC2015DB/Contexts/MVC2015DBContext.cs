using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MVC2015.DataProvider.MVC2015DB.Models.Mapping;
using MVC2015.DataProvider.MVC2015DB.Models;

namespace MVC2015.DataProvider.MVC2015DB.Contexts
{
    public partial class MVC2015DBContext : BaseContext
    {
        static MVC2015DBContext()
        {
            Database.SetInitializer<MVC2015DBContext>(null);
        }

        //public MVC2015DBContext()
        //    : base("Name=MVC2015DBContext")
        //{
        //}

        public DbSet<Common_Mail_BadMail> Common_Mail_BadMail { get; set; }
        public DbSet<Common_Mail_MailQueue> Common_Mail_MailQueue { get; set; }
        public DbSet<ELMAH_Error> ELMAH_Error { get; set; }
        public DbSet<tbl_Attachment> tbl_Attachment { get; set; }
        public DbSet<tbl_AuditLog> tbl_AuditLog { get; set; }
        public DbSet<tbl_Common_CustomerField> tbl_Common_CustomerField { get; set; }
        public DbSet<tbl_Common_Module> tbl_Common_Module { get; set; }
        public DbSet<tbl_Common_ModulePermissionDefine> tbl_Common_ModulePermissionDefine { get; set; }
        public DbSet<tbl_Common_Resource> tbl_Common_Resource { get; set; }
        public DbSet<tbl_Common_Role> tbl_Common_Role { get; set; }
        public DbSet<tbl_Common_RolePermissionConfig> tbl_Common_RolePermissionConfig { get; set; }
        public DbSet<tbl_Common_RoleUser> tbl_Common_RoleUser { get; set; }
        public DbSet<tbl_Common_User> tbl_Common_User { get; set; }
        public DbSet<tbl_Common_UserOfCompany> tbl_Common_UserOfCompany { get; set; }
        public DbSet<tbl_Common_UserOfGasStation> tbl_Common_UserOfGasStation { get; set; }
        public DbSet<tbl_Company> tbl_Company { get; set; }
        public DbSet<tbl_EmailTemplate> tbl_EmailTemplate { get; set; }
        public DbSet<tbl_Job_History> tbl_Job_History { get; set; }
        public DbSet<tbl_Job_HistoryData> tbl_Job_HistoryData { get; set; }
        public DbSet<tbl_Job_Master> tbl_Job_Master { get; set; }
        public DbSet<tbl_Job_StatusType> tbl_Job_StatusType { get; set; }
        public DbSet<tbl_Search_SearchInfo> tbl_Search_SearchInfo { get; set; }
        public DbSet<tbl_SystemCode> tbl_SystemCode { get; set; }
        public DbSet<tbl_SystemParameter> tbl_SystemParameter { get; set; }
        public DbSet<tbl_TimerAllocLog> tbl_TimerAllocLog { get; set; }
        public DbSet<tbl_TimerLog> tbl_TimerLog { get; set; }
        public DbSet<tbl_UserLogonHistory> tbl_UserLogonHistory { get; set; }
        public DbSet<tbl_Wcf> tbl_Wcf { get; set; }
        public DbSet<tbl_Wcf_Step> tbl_Wcf_Step { get; set; }
        public DbSet<tbl_Wcf_StepData> tbl_Wcf_StepData { get; set; }
        public DbSet<tbl_Wcf_StepException> tbl_Wcf_StepException { get; set; }
        public DbSet<tbl_Wcf_StepName> tbl_Wcf_StepName { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Common_Mail_BadMailMap());
            modelBuilder.Configurations.Add(new Common_Mail_MailQueueMap());
            modelBuilder.Configurations.Add(new ELMAH_ErrorMap());
            modelBuilder.Configurations.Add(new tbl_AttachmentMap());
            modelBuilder.Configurations.Add(new tbl_AuditLogMap());
            modelBuilder.Configurations.Add(new tbl_Common_CustomerFieldMap());
            modelBuilder.Configurations.Add(new tbl_Common_ModuleMap());
            modelBuilder.Configurations.Add(new tbl_Common_ModulePermissionDefineMap());
            modelBuilder.Configurations.Add(new tbl_Common_ResourceMap());
            modelBuilder.Configurations.Add(new tbl_Common_RoleMap());
            modelBuilder.Configurations.Add(new tbl_Common_RolePermissionConfigMap());
            modelBuilder.Configurations.Add(new tbl_Common_RoleUserMap());
            modelBuilder.Configurations.Add(new tbl_Common_UserMap());
            modelBuilder.Configurations.Add(new tbl_Common_UserOfCompanyMap());
            modelBuilder.Configurations.Add(new tbl_Common_UserOfGasStationMap());
            modelBuilder.Configurations.Add(new tbl_CompanyMap());
            modelBuilder.Configurations.Add(new tbl_EmailTemplateMap());
            modelBuilder.Configurations.Add(new tbl_Job_HistoryMap());
            modelBuilder.Configurations.Add(new tbl_Job_HistoryDataMap());
            modelBuilder.Configurations.Add(new tbl_Job_MasterMap());
            modelBuilder.Configurations.Add(new tbl_Job_StatusTypeMap());
            modelBuilder.Configurations.Add(new tbl_Search_SearchInfoMap());
            modelBuilder.Configurations.Add(new tbl_SystemCodeMap());
            modelBuilder.Configurations.Add(new tbl_SystemParameterMap());
            modelBuilder.Configurations.Add(new tbl_TimerAllocLogMap());
            modelBuilder.Configurations.Add(new tbl_TimerLogMap());
            modelBuilder.Configurations.Add(new tbl_UserLogonHistoryMap());
            modelBuilder.Configurations.Add(new tbl_WcfMap());
            modelBuilder.Configurations.Add(new tbl_Wcf_StepMap());
            modelBuilder.Configurations.Add(new tbl_Wcf_StepDataMap());
            modelBuilder.Configurations.Add(new tbl_Wcf_StepExceptionMap());
            modelBuilder.Configurations.Add(new tbl_Wcf_StepNameMap());
        }
    }
}
