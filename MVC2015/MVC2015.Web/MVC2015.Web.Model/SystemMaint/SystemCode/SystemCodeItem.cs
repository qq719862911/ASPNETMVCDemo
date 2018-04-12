using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.SystemCode
{
    public class SystemCodeItem
    {
        public int ID { get; set; }
        public string CodeType { get; set; }
        public string Code { get; set; }
        public int? Parent { get; set; }
        public string ResourceKey { get; set; }
        public int Value { get; set; }
    }
}
