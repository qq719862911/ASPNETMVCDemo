using MVC2015.FullTextSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM = MVC2015.Web.Model.SystemMaint.FullTextSearch;

namespace MVC2015.Web.Site.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string keyWords)
        {
            if (keyWords == null)
                keyWords = Request.Form["tbWord"];
            IEnumerable<VM.SearchModel> list = LuceneSample.Search(keyWords);
            return View();
        }
        public ActionResult CreateIndex()
        {
            LuceneSample.CreateIndex();
            return View("Index");
        }
	}
}