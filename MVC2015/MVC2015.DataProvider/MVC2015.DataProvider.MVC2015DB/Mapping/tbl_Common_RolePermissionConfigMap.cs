using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Common_RolePermissionConfigMap : EntityTypeConfiguration<tbl_Common_RolePermissionConfig>
    {
        public tbl_Common_RolePermissionConfigMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbl_Common_RolePermissionConfig");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.IsDelete).HasColumnName("IsDelete");

            // Relationships
            this.HasRequired(t => t.tbl_Common_Module)
                .WithMany(t => t.tbl_Common_RolePermissionConfig)
                .HasForeignKey(d => d.ModuleId);
            this.HasRequired(t => t.tbl_Common_Role)
                .WithMany(t => t.tbl_Common_RolePermissionConfig)
                .HasForeignKey(d => d.RoleId);

        }
    }
}
