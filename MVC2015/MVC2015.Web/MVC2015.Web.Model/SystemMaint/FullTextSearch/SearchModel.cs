using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.FullTextSearch
{
    public class SearchModel
    {
        public SearchModel()
        { 
        }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }

    }
}
