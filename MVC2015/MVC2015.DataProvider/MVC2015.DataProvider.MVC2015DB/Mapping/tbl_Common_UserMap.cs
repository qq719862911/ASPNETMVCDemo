using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Common_UserMap : EntityTypeConfiguration<tbl_Common_User>
    {
        public tbl_Common_UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.LogonName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.DomainAccount)
                .HasMaxLength(100);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Password)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(100);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("tbl_Common_User");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.LogonName).HasColumnName("LogonName");
            this.Property(t => t.DomainAccount).HasColumnName("DomainAccount");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
        }
    }
}
