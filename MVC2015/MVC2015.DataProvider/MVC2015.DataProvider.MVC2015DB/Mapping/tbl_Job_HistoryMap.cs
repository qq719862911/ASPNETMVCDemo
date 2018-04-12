using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Job_HistoryMap : EntityTypeConfiguration<tbl_Job_History>
    {
        public tbl_Job_HistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.JobHistoryId);

            // Properties
            // Table & Column Mappings
            this.ToTable("tbl_Job_History");
            this.Property(t => t.JobHistoryId).HasColumnName("JobHistoryId");
            this.Property(t => t.JobId).HasColumnName("JobId");
            this.Property(t => t.StatusValue).HasColumnName("StatusValue");
            this.Property(t => t.StatusStartDate).HasColumnName("StatusStartDate");
            this.Property(t => t.StatusEndDate).HasColumnName("StatusEndDate");

            // Relationships
            this.HasRequired(t => t.tbl_Job_Master)
                .WithMany(t => t.tbl_Job_History)
                .HasForeignKey(d => d.JobId);

        }
    }
}
