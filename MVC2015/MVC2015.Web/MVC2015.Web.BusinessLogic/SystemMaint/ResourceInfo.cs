using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using VM = MVC2015.Web.Model.SystemMaint.ResourceInfo;
using MVC2015.Utility.Resource;
using System.Linq.Expressions;
using MVC2015.Utility.Common;
using SP = MVC2015.DataProvider.MVC2015DB.StoredProcedure;

namespace MVC2015.Web.BusinessLogic.SystemMaint
{
    public class ResourceInfo : IDisposable
    {
        private readonly CTX.MVC2015DBContext Ctx;

        public ResourceInfo()
        {
            Ctx = new CTX.MVC2015DBContext();
        }

        public ResourceInfo(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }

        public IQueryable<VM.ResourceInfoItem> GetList()
        {
            var result = from resource in Ctx.tbl_Common_Resource.Where(r => r.IsDeleted != true)
                         select new VM.ResourceInfoItem()
                         {
                             ResourceKey = resource.ResourceKey,
                             ResourceValueZHCN = resource.ResourceValueZHCN,
                             ResourceValueZHHK = resource.ResourceValueZHHK,
                             ResourceValueENUS = resource.ResourceValueENUS
                         };
            return result;
        }

        public bool Create(VM.ResourceInfoItem model)
        {
            var temp = (from ri in Ctx.tbl_Common_Resource.Where(r => r.ResourceKey == model.ResourceKey) select ri).ToList();

            if (temp.Count() == 0)
            {
                MD.tbl_Common_Resource data = new MD.tbl_Common_Resource();

                data.ResourceKey = model.ResourceKey;
                data.ResourceValueZHCN = model.ResourceValueZHCN;
                data.ResourceValueZHHK = model.ResourceValueZHHK;
                data.ResourceValueENUS = model.ResourceValueENUS;
                data.IsDeleted = null;
                data.CreatedBy = model.CreatedBy;
                data.CreatedDate = DateTime.Now;

                Ctx.tbl_Common_Resource.Add(data);
                Ctx.SaveChanges();
                ResourceHelper.UpdateCommonResourceList();
                return true;
            }
            else if (temp.First().IsDeleted == true)
            {
                temp.First().ResourceValueENUS = model.ResourceValueENUS;
                temp.First().ResourceValueZHCN = model.ResourceValueZHCN;
                temp.First().ResourceValueZHHK = model.ResourceValueZHHK;
                temp.First().IsDeleted = null;
                temp.First().CreatedBy = model.CreatedBy;
                temp.First().CreatedDate = DateTime.Now;

                Ctx.SaveChanges();
                ResourceHelper.UpdateCommonResourceList();
                return true;
            }
            else
                return false;
        }

        public VM.ResourceInfoItem GetResourceItemById(string id)
        {
            var data = from ri in Ctx.tbl_Common_Resource.Where(r => r.ResourceKey == id && r.IsDeleted != true)
                       select new VM.ResourceInfoItem()
                       {
                           ResourceKey = ri.ResourceKey,
                           ResourceValueENUS = ri.ResourceValueENUS,
                           ResourceValueZHCN = ri.ResourceValueZHCN,
                           ResourceValueZHHK = ri.ResourceValueZHHK
                       };
            return data.FirstOrDefault();
        }

        public bool Update(VM.ResourceInfoItem model)
        {
            var data = Ctx.tbl_Common_Resource.First(r => r.ResourceKey == model.ResourceKey_Old);
            
            //data.ResourceKey = model.ResourceKey;
            data.ResourceValueENUS = model.ResourceValueENUS;
            data.ResourceValueZHCN = model.ResourceValueZHCN;
            data.ResourceValueZHHK = model.ResourceValueZHHK;
            //data.IsDeleted = model.IsDeleted;
            data.UpdatedBy = model.UpdatedBy;
            data.UpdatedDate = DateTime.Now;
            var result = Ctx.SaveChanges();
            if (result > 0)
            {
                ResourceHelper.UpdateCommonResourceList();
                return true;
            }
            return false;
        }

        public bool UpdateResource(VM.ResourceInfoItem model)
        {
            var data = Ctx.tbl_Common_Resource.First(r => r.ResourceKey == model.ResourceKey_Old);
            //data.ResourceKey = model.ResourceKey;
            data.ResourceValueENUS = model.ResourceValueENUS;
            data.ResourceValueZHCN = model.ResourceValueZHCN;
            data.ResourceValueZHHK = model.ResourceValueZHHK;
            //data.IsDeleted = model.IsDeleted;
            data.UpdatedBy = model.UpdatedBy;
            data.UpdatedDate = DateTime.Now;
            var result = Ctx.SaveChanges();
            if (result > 0)
            {
                ResourceHelper.UpdateCommonResourceList();
                return true;
            }
            return false;
        }

        public bool Delete(string id, string updatedBy)
        {
            var data = Ctx.tbl_Common_Resource.First(r => r.ResourceKey == id);
            data.IsDeleted = true;
            data.UpdatedBy = updatedBy;
            data.UpdatedDate = DateTime.Now;
            Ctx.SaveChanges();
            ResourceHelper.UpdateCommonResourceList();
            return true;
        }

        public List<VM.ResourceInfoItem> GetResourceInfoItem(VM.ResourceInfoSearch searchModel)
        {
            //Build search criteria lambda expression
            Expression<Func<MD.tbl_Common_Resource, Boolean>> expr = BuildSearchCriteria(searchModel);
            //if not set the sort way ,then the default set will be used.
            string sortBy = "ResourceKey";
            string sortDirection = "DESC";
            if (!string.IsNullOrWhiteSpace(searchModel.SortBy))
            {
                sortBy = searchModel.SortBy;
                sortDirection = searchModel.SortDirection;
            }

            IQueryable<MD.tbl_Common_Resource> resultQueryable;
            resultQueryable = this.Ctx.tbl_Common_Resource;
            if (expr != null)
                resultQueryable = resultQueryable.Where(expr).SortWith(sortBy, sortDirection);
            else
                resultQueryable = resultQueryable.SortWith(sortBy, sortDirection);

            var result = (from r in resultQueryable
                          select new VM.ResourceInfoItem()
                          {
                              ResourceKey = r.ResourceKey,
                              ResourceValueZHCN = r.ResourceValueZHCN,
                              ResourceValueENUS = r.ResourceValueENUS,
                              ResourceValueZHHK = r.ResourceValueZHHK
                          }).ToList();
            return result;
        }

        private Expression<Func<MD.tbl_Common_Resource, bool>> BuildSearchCriteria(VM.ResourceInfoSearch searchModel)
        {
            if (searchModel == null)
            {
                throw new ArgumentNullException("searchModel is null.");
            }

            Expression<Func<MD.tbl_Common_Resource, bool>> expr = null;
            DynamicLambda<MD.tbl_Common_Resource> builder = new DynamicLambda<MD.tbl_Common_Resource>();

            if (searchModel.ResourceKey != null)
            {
                Expression<Func<MD.tbl_Common_Resource, bool>> temp = s => s.ResourceKey.Contains(searchModel.ResourceKey);
                expr = builder.BuildQueryAnd(expr, temp);
            }
            return expr;
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
