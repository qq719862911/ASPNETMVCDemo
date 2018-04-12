using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Job_HistoryDataMap : EntityTypeConfiguration<tbl_Job_HistoryData>
    {
        public tbl_Job_HistoryDataMap()
        {
            // Primary Key
            this.HasKey(t => t.JobDataId);

            // Properties
            this.Property(t => t.DataTypeName)
                .IsRequired();

            this.Property(t => t.DataValue)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("tbl_Job_HistoryData");
            this.Property(t => t.JobDataId).HasColumnName("JobDataId");
            this.Property(t => t.JobHistoryId).HasColumnName("JobHistoryId");
            this.Property(t => t.DataTypeName).HasColumnName("DataTypeName");
            this.Property(t => t.DataValue).HasColumnName("DataValue");

            // Relationships
            this.HasRequired(t => t.tbl_Job_History)
                .WithMany(t => t.tbl_Job_HistoryData)
                .HasForeignKey(d => d.JobHistoryId);

        }
    }
}
