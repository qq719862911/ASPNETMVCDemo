using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_SystemParameterMap : EntityTypeConfiguration<tbl_SystemParameter>
    {
        public tbl_SystemParameterMap()
        {
            // Primary Key
            this.HasKey(t => t.ParamName);

            // Properties
            this.Property(t => t.ParamName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Description)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("tbl_SystemParameter");
            this.Property(t => t.ParamName).HasColumnName("ParamName");
            this.Property(t => t.IntValue).HasColumnName("IntValue");
            this.Property(t => t.StringValue).HasColumnName("StringValue");
            this.Property(t => t.TimeValue).HasColumnName("TimeValue");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
