using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.DataProvider.MVC2015DB.Models.Mapping
{
    public class tbl_Common_UserOfCompanyMap : EntityTypeConfiguration<tbl_Common_UserOfCompany>
    {
        public tbl_Common_UserOfCompanyMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id });

        

            // Table & Column Mappings
            this.ToTable("tbl_Common_UserOfCompany");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CompanyId).HasColumnName("CompanyId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
        }
    }
}
