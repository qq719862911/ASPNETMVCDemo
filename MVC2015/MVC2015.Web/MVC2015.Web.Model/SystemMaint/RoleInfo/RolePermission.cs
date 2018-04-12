using MVC2015.Web.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.RoleInfo
{
    public class RolePermission : BaseModel
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int Value { get; set; }
        public int RoleId { get; set; }
    }
}
