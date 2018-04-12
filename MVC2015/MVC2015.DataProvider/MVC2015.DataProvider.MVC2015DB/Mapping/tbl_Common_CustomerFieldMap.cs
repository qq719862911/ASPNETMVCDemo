using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Common_CustomerFieldMap : EntityTypeConfiguration<tbl_Common_CustomerField>
    {
        public tbl_Common_CustomerFieldMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerFieldId);

            // Properties
            this.Property(t => t.Area)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Controllers)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.GridView)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("tbl_Common_CustomerField");
            this.Property(t => t.CustomerFieldId).HasColumnName("CustomerFieldId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.Controllers).HasColumnName("Controllers");
            this.Property(t => t.GridView).HasColumnName("GridView");
            this.Property(t => t.FieldString).HasColumnName("FieldString");

            // Relationships
            this.HasRequired(t => t.tbl_Common_User)
                .WithMany(t => t.tbl_Common_CustomerField)
                .HasForeignKey(d => d.UserId);

        }
    }
}
