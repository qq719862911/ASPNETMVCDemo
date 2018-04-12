using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_UserLogonHistoryMap : EntityTypeConfiguration<tbl_UserLogonHistory>
    {
        public tbl_UserLogonHistoryMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.IPAddress)
                .HasMaxLength(50);

            this.Property(t => t.LogonName)
                .HasMaxLength(200);

            this.Property(t => t.Password)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("tbl_UserLogonHistory");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
            this.Property(t => t.LogonName).HasColumnName("LogonName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.LogonDate).HasColumnName("LogonDate");
            this.Property(t => t.LogonTimes).HasColumnName("LogonTimes");
        }
    }
}
