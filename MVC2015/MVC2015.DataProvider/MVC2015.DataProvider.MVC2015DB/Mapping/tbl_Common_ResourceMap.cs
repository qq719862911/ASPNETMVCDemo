using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Common_ResourceMap : EntityTypeConfiguration<tbl_Common_Resource>
    {
        public tbl_Common_ResourceMap()
        {
            // Primary Key
            this.HasKey(t => t.ResourceKey);

            // Properties
            this.Property(t => t.ResourceKey)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.ResourceValueZHCN)
                .HasMaxLength(1000);

            this.Property(t => t.ResourceValueZHHK)
                .HasMaxLength(1000);

            this.Property(t => t.ResourceValueENUS)
                .HasMaxLength(1000);

            this.Property(t => t.CreatedBy)
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("tbl_Common_Resource");
            this.Property(t => t.ResourceKey).HasColumnName("ResourceKey");
            this.Property(t => t.ResourceValueZHCN).HasColumnName("ResourceValueZHCN");
            this.Property(t => t.ResourceValueZHHK).HasColumnName("ResourceValueZHHK");
            this.Property(t => t.ResourceValueENUS).HasColumnName("ResourceValueENUS");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
