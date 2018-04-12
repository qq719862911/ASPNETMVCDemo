using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Wcf_StepNameMap : EntityTypeConfiguration<tbl_Wcf_StepName>
    {
        public tbl_Wcf_StepNameMap()
        {
            // Primary Key
            this.HasKey(t => t.StepCode);

            // Properties
            this.Property(t => t.StepCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StepName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.StepDescription)
                .IsRequired()
                .HasMaxLength(1000);

            // Table & Column Mappings
            this.ToTable("tbl_Wcf_StepName");
            this.Property(t => t.StepCode).HasColumnName("StepCode");
            this.Property(t => t.StepName).HasColumnName("StepName");
            this.Property(t => t.StepDescription).HasColumnName("StepDescription");
        }
    }
}
