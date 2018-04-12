using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class Common_Mail_BadMailMap : EntityTypeConfiguration<Common_Mail_BadMail>
    {
        public Common_Mail_BadMailMap()
        {
            // Primary Key
            this.HasKey(t => t.MailID);

            // Properties
            this.Property(t => t.MailFrom)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.MailTo)
                .IsRequired()
                .HasMaxLength(2000);

            this.Property(t => t.MailSubject)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("Common_Mail_BadMail");
            this.Property(t => t.MailID).HasColumnName("MailID");
            this.Property(t => t.MailPriority).HasColumnName("MailPriority");
            this.Property(t => t.MailFrom).HasColumnName("MailFrom");
            this.Property(t => t.MailTo).HasColumnName("MailTo");
            this.Property(t => t.MailCc).HasColumnName("MailCc");
            this.Property(t => t.MailBcc).HasColumnName("MailBcc");
            this.Property(t => t.MailSubject).HasColumnName("MailSubject");
            this.Property(t => t.MailBody).HasColumnName("MailBody");
            this.Property(t => t.IsBodyHtml).HasColumnName("IsBodyHtml");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.Attachment).HasColumnName("Attachment");
        }
    }
}
