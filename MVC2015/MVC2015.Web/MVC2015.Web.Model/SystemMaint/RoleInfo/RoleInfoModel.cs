using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.RoleInfo
{
    public class RoleInfoModel
    {
        public RoleInfoSearch Search { get; set; }

        public IQueryable<RoleInfoItem> List { get; set; }
    }
}
