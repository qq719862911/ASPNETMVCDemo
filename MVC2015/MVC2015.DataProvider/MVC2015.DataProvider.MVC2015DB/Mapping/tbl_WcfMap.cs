using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_WcfMap : EntityTypeConfiguration<tbl_Wcf>
    {
        public tbl_WcfMap()
        {
            // Primary Key
            this.HasKey(t => t.WcfId);

            // Properties
            this.Property(t => t.WCFMethodName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbl_Wcf");
            this.Property(t => t.WcfId).HasColumnName("WcfId");
            this.Property(t => t.JobId).HasColumnName("JobId");
            this.Property(t => t.MCUId).HasColumnName("MCUId");
            this.Property(t => t.WCFMethodName).HasColumnName("WCFMethodName");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
