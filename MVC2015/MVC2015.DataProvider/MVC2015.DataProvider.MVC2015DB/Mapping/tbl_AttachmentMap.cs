using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_AttachmentMap : EntityTypeConfiguration<tbl_Attachment>
    {
        public tbl_AttachmentMap()
        {
            // Primary Key
            this.HasKey(t => t.AttachmentId);

            // Properties
            this.Property(t => t.FileName)
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbl_Attachment");
            this.Property(t => t.AttachmentId).HasColumnName("AttachmentId");
            this.Property(t => t.AttachmentGuid).HasColumnName("AttachmentGuid");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.FileContent).HasColumnName("FileContent");
            this.Property(t => t.FileSize).HasColumnName("FileSize");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
