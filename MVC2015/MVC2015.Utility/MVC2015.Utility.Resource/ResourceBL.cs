using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using System.Linq.Expressions;
using MVC2015.Utility.Common;

namespace MVC2015.Utility.Resource
{
    public class ResourceBL : IDisposable
    {
        //DB Data Context
        private readonly CTX.MVC2015DBContext Ctx;

        public ResourceBL()
        {
            Ctx = new CTX.MVC2015DBContext();
        }

        public ResourceBL(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }

        public IQueryable<ResourceModel> GetItems()
        {
            IQueryable<MD.tbl_Common_Resource> resultQueryable;
            resultQueryable = this.Ctx.tbl_Common_Resource;
            var result = resultQueryable.Select(a => new ResourceModel()
            {
                ResourceKey = a.ResourceKey,
                ResourceValueENUS = a.ResourceValueENUS,
                ResourceValueZHCN = a.ResourceValueZHCN,
                ResourceValueZHHK = a.ResourceValueZHHK
            });
            return result;
        }

        public void Dispose()
        {
            if (Ctx != null)
            {
                Ctx.Dispose();
            }
        }
    }
}