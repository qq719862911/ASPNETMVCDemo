using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Wcf_StepMap : EntityTypeConfiguration<tbl_Wcf_Step>
    {
        public tbl_Wcf_StepMap()
        {
            // Primary Key
            this.HasKey(t => t.WcfStepId);

            // Properties
            this.Property(t => t.StepCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbl_Wcf_Step");
            this.Property(t => t.WcfStepId).HasColumnName("WcfStepId");
            this.Property(t => t.WcfId).HasColumnName("WcfId");
            this.Property(t => t.StepCode).HasColumnName("StepCode");
            this.Property(t => t.StepDuration).HasColumnName("StepDuration");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasRequired(t => t.tbl_Wcf)
                .WithMany(t => t.tbl_Wcf_Step)
                .HasForeignKey(d => d.WcfId);

        }
    }
}
