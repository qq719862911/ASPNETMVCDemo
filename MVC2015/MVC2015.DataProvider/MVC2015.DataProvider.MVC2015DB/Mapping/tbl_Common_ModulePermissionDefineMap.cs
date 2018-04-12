using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Common_ModulePermissionDefineMap : EntityTypeConfiguration<tbl_Common_ModulePermissionDefine>
    {
        public tbl_Common_ModulePermissionDefineMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.ResourceKey)
                .HasMaxLength(100);

            this.Property(t => t.Area)
                .HasMaxLength(50);

            this.Property(t => t.Controller)
                .HasMaxLength(50);

            this.Property(t => t.Action)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbl_Common_ModulePermissionDefine");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ResourceKey).HasColumnName("ResourceKey");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.ParentValue).HasColumnName("ParentValue");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.Controller).HasColumnName("Controller");
            this.Property(t => t.Action).HasColumnName("Action");

            // Relationships
            this.HasRequired(t => t.tbl_Common_Module)
                .WithMany(t => t.tbl_Common_ModulePermissionDefine)
                .HasForeignKey(d => d.ModuleId);

        }
    }
}
