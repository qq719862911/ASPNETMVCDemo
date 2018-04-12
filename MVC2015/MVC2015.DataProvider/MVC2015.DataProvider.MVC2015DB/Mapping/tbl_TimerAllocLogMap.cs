using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_TimerAllocLogMap : EntityTypeConfiguration<tbl_TimerAllocLog>
    {
        public tbl_TimerAllocLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Guid)
                .IsRequired();

            this.Property(t => t.RunningTimerId)
                .IsRequired();

            this.Property(t => t.MachineName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("tbl_TimerAllocLog");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Guid).HasColumnName("Guid");
            this.Property(t => t.Datetime).HasColumnName("Datetime");
            this.Property(t => t.RunningTimerId).HasColumnName("RunningTimerId");
            this.Property(t => t.MachineName).HasColumnName("MachineName");
        }
    }
}
