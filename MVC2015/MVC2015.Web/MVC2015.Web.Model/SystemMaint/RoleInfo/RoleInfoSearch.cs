using MVC2015.Web.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.RoleInfo
{
    public class RoleInfoSearch : BaseSearchModel
    {
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
