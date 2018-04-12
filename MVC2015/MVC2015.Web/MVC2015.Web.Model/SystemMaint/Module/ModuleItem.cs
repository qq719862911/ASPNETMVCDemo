using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.Module
{
    public class ModuleItem
    {
        public int ModuleId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ResourceKey { get; set; }
        public string Description { get; set; }
        public int MenuGroupOrder { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsMenu { get; set; }

        public List<ModuleItem> ChildModuleItems { get; set; }
        public List<ModulePermission> ModulePermissions { get; set; }
    }
}
