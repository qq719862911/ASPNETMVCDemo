using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_CompanyMap : EntityTypeConfiguration<tbl_Company>
    {
        public tbl_CompanyMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(300);

            this.Property(t => t.SendUrl)
                .HasMaxLength(200);

            this.Property(t => t.EmailAddress)
                .HasMaxLength(200);

            this.Property(t => t.DocumentMaker)
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("tbl_Company");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.ProcessingTime).HasColumnName("ProcessingTime");
            this.Property(t => t.SendTime).HasColumnName("SendTime");
            this.Property(t => t.SendUrl).HasColumnName("SendUrl");
            this.Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            this.Property(t => t.Rate).HasColumnName("Rate");
            this.Property(t => t.Fee).HasColumnName("Fee");
            this.Property(t => t.DocumentMaker).HasColumnName("DocumentMaker");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
