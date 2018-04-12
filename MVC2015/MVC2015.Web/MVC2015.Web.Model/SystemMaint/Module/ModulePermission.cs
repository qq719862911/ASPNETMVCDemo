using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.Module
{
    public class ModulePermission
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string Description { get; set; }
        public string ResourceKey { get; set; }
        public int Value { get; set; }
        public int ParentValue { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public bool IsSelected { get; set; }
    }
}