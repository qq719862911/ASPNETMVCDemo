using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.Permission
{
    public class BrowserVersion
    {
        public string BrowserName { get; set; }
        public string Version { get; set; }
        public bool IsAllow { get; set; }
        public bool SpecifiedVersion { get; set; }
        
    }
}
