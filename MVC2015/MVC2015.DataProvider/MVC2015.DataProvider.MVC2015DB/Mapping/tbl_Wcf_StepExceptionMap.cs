using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Wcf_StepExceptionMap : EntityTypeConfiguration<tbl_Wcf_StepException>
    {
        public tbl_Wcf_StepExceptionMap()
        {
            // Primary Key
            this.HasKey(t => t.WcfStepExceptionId);

            // Properties
            this.Property(t => t.ExceptionName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ExceptionDetail)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("tbl_Wcf_StepException");
            this.Property(t => t.WcfStepExceptionId).HasColumnName("WcfStepExceptionId");
            this.Property(t => t.WcfStepId).HasColumnName("WcfStepId");
            this.Property(t => t.ExceptionName).HasColumnName("ExceptionName");
            this.Property(t => t.ExceptionDetail).HasColumnName("ExceptionDetail");

            // Relationships
            this.HasRequired(t => t.tbl_Wcf_Step)
                .WithMany(t => t.tbl_Wcf_StepException)
                .HasForeignKey(d => d.WcfStepId);

        }
    }
}
