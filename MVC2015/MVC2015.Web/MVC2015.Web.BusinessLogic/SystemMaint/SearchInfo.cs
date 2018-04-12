using MVC2015.Web.Model.SystemMaint.FullTextSearch;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using VM = MVC2015.Web.Model.SystemMaint.FullTextSearch;

namespace MVC2015.Web.BusinessLogic.SystemMaint
{
    public class SearchInfo : IDisposable
    {
        private readonly CTX.MVC2015DBContext Ctx;
        public SearchInfo()
        {
            Ctx = new CTX.MVC2015DBContext();
        }
        public SearchInfo(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }
        public IQueryable<VM.SearchModel> GetAll()
        {
            var result = from fullSearch in Ctx.tbl_Search_SearchInfo.Where(s=>1==1)
                         select new VM.SearchModel()
                         {
                             ID = fullSearch.ID,
                             Title = fullSearch.Title,
                             Contents = fullSearch.Contents,
                             CreateDate = fullSearch.CreateDate,
                             CreateBy = fullSearch.CreateBy,
                             ModifyBy = fullSearch.ModifyBy,
                             ModifyDate = fullSearch.ModifyDate
                         };
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
