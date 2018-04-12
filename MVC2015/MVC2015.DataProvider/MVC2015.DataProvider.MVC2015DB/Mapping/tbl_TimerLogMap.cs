using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_TimerLogMap : EntityTypeConfiguration<tbl_TimerLog>
    {
        public tbl_TimerLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TimerName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TimerValue)
                .IsRequired();

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbl_TimerLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TimerName).HasColumnName("TimerName");
            this.Property(t => t.TimerValue).HasColumnName("TimerValue");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
