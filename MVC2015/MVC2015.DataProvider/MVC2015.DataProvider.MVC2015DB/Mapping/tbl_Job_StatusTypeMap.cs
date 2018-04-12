using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Job_StatusTypeMap : EntityTypeConfiguration<tbl_Job_StatusType>
    {
        public tbl_Job_StatusTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.JobStatusTypeCode);

            // Properties
            this.Property(t => t.JobStatusTypeCode)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.JobStatusTypeName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.JobStatusTypeDescription)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("tbl_Job_StatusType");
            this.Property(t => t.JobStatusTypeCode).HasColumnName("JobStatusTypeCode");
            this.Property(t => t.JobStatusTypeName).HasColumnName("JobStatusTypeName");
            this.Property(t => t.JobStatusTypeDescription).HasColumnName("JobStatusTypeDescription");
        }
    }
}
