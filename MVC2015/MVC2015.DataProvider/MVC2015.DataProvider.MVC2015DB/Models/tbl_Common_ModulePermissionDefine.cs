using System;
using System.Collections.Generic;

namespace MVC2015.DataProvider.MVC2015DB.Models
{
    public partial class tbl_Common_ModulePermissionDefine
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
        public virtual tbl_Common_Module tbl_Common_Module { get; set; }
    }
}
