using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace  MVC2015.DataProvider.MVC2015DB
{
    public partial class BaseContext : DbContext
    {
        public BaseContext()
            : base("Name=MVC2015DB")
        {

        }

        public BaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }
}
