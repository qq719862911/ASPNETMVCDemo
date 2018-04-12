using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Wcf_StepDataMap : EntityTypeConfiguration<tbl_Wcf_StepData>
    {
        public tbl_Wcf_StepDataMap()
        {
            // Primary Key
            this.HasKey(t => t.WcfStepDataId);

            // Properties
            this.Property(t => t.DataTypeName)
                .IsRequired();

            this.Property(t => t.DataValue)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("tbl_Wcf_StepData");
            this.Property(t => t.WcfStepDataId).HasColumnName("WcfStepDataId");
            this.Property(t => t.WcfStepId).HasColumnName("WcfStepId");
            this.Property(t => t.DataTypeName).HasColumnName("DataTypeName");
            this.Property(t => t.DataValue).HasColumnName("DataValue");

            // Relationships
            this.HasRequired(t => t.tbl_Wcf_Step)
                .WithMany(t => t.tbl_Wcf_StepData)
                .HasForeignKey(d => d.WcfStepId);

        }
    }
}
