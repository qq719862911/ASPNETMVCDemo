using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC2015.Utility.Common;
using GSIMS.DataProvider;

namespace MVC2015.DataProvider.MVC2015DB.StoredProcedure
{
    [StoredProcedureName("usp_UpdateDataUnitAndGasType")]
    public class USP_UpdateDataUnitAndGasType : SPBase
    {
        [Parameter("OriginalGasType", TypeName = "nvarchar", Length = 50)]
        public string OriginalGasType { get; set; }
        [Parameter("CurrentGasType", TypeName = "nvarchar", Length = 50)]
        public string CurrentGasType { get; set; }
        [Parameter("OriginalUnit", TypeName = "nvarchar", Length = 50)]
        public string OriginalUnit { get; set; }
        [Parameter("CurrentUnit", TypeName = "nvarchar", Length = 50)]
        public string CurrentUnit { get; set; }

    }
}