using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Job_MasterMap : EntityTypeConfiguration<tbl_Job_Master>
    {
        public tbl_Job_MasterMap()
        {
            // Primary Key
            this.HasKey(t => t.JobId);

            // Properties
            this.Property(t => t.Operator)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("tbl_Job_Master");
            this.Property(t => t.JobId).HasColumnName("JobId");
            this.Property(t => t.MCUId).HasColumnName("MCUId");
            this.Property(t => t.JobTypeId).HasColumnName("JobTypeId");
            this.Property(t => t.Operator).HasColumnName("Operator");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.StatusDate).HasColumnName("StatusDate");
            this.Property(t => t.IsEXSPutACK).HasColumnName("IsEXSPutACK");
            this.Property(t => t.RetryCount).HasColumnName("RetryCount");
            this.Property(t => t.Automatic).HasColumnName("Automatic");
        }
    }
}
