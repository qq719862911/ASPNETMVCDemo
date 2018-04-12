using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_AuditLogMap : EntityTypeConfiguration<tbl_AuditLog>
    {
        public tbl_AuditLogMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditId);

            // Properties
            this.Property(t => t.ModuleName)
                .HasMaxLength(500);

            this.Property(t => t.OpereationType)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Operator)
                .HasMaxLength(100);

            this.Property(t => t.EntryID)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("tbl_AuditLog");
            this.Property(t => t.AuditId).HasColumnName("AuditId");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
            this.Property(t => t.OpereationType).HasColumnName("OpereationType");
            this.Property(t => t.OldData).HasColumnName("OldData");
            this.Property(t => t.NewData).HasColumnName("NewData");
            this.Property(t => t.Operator).HasColumnName("Operator");
            this.Property(t => t.OperationDate).HasColumnName("OperationDate");
            this.Property(t => t.EntryID).HasColumnName("EntryID");
        }
    }
}
