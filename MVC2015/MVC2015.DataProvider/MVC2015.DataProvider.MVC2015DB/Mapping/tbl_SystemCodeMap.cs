using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_SystemCodeMap : EntityTypeConfiguration<tbl_SystemCode>
    {
        public tbl_SystemCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.CodeType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .IsRequired();

            this.Property(t => t.ResourceKey)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("tbl_SystemCode");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.CodeType).HasColumnName("CodeType");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.ResourceKey).HasColumnName("ResourceKey");
            this.Property(t => t.Parent).HasColumnName("Parent");
            this.Property(t => t.Value).HasColumnName("Value");
        }
    }
}
