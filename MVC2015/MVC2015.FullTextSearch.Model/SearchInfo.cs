using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch
{
    public class SearchInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CreteBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
