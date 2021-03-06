using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Common_UserOfGasStationMap : EntityTypeConfiguration<tbl_Common_UserOfGasStation>
    {
        public tbl_Common_UserOfGasStationMap()
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
            this.ToTable("tbl_Common_UserOfGasStation");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GasStationId).HasColumnName("GasStationId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");

            // Relationships
            this.HasRequired(t => t.tbl_Common_User)
                .WithMany(t => t.tbl_Common_UserOfGasStation)
                .HasForeignKey(d => d.UserId);

        }
    }
}
