using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Common_ModuleMap : EntityTypeConfiguration<tbl_Common_Module>
    {
        public tbl_Common_ModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ModuleId);

            // Properties
            this.Property(t => t.ModuleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ResourceKey)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("tbl_Common_Module");
            this.Property(t => t.ModuleId).HasColumnName("ModuleId");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.ResourceKey).HasColumnName("ResourceKey");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.MenuGroupOrder).HasColumnName("MenuGroupOrder");
            this.Property(t => t.DisplayOrder).HasColumnName("DisplayOrder");
            this.Property(t => t.IsMenu).HasColumnName("IsMenu");
        }
    }
}
