using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.Permission
{
    public class SiteMenu
    {
        public SiteMenu()
        {
            ChildrenMenu = new List<SiteMenu>();
        }
        public int MenuId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ResourceKey { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }
        public int DisplayOrder { get; set; }
        public virtual List<SiteMenu> ChildrenMenu { get; set; }
        public virtual SiteMenu ParentMenu { get; set; }
        public bool IsAvailable { get; set; }
    }
}
