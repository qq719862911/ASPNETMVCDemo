using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;

namespace FullTextSearch
{

    public class SearchBll
    {
        private readonly CTX.MVC2015DBContext Ctx;
        public SearchBll()
        {
            Ctx = new CTX.MVC2015DBContext();
        }
        public SearchBll(CTX.MVC2015DBContext context)
        {
            Ctx = context;
        }

        public List<SearchInfo> GetAllPost()
        {
            //var result = from company in Ctx.tbl_Search_SearchInfo.Where(s => s.IsDeleted != true).DefaultIfEmpty();

            List<SearchInfo> list = new List<SearchInfo>();
            SearchInfo info = new SearchInfo();
            info.ID = 1;
            info.Title = "测试Test";
            info.Content = "信念的敬意1。 王健林，61岁，四川人，大连万斯发布2015全球富豪榜，王健林成为 中国内地首富。根据彭博华人富豪榜和亚洲富豪榜截至5月1日的数据，王健林以381亿美元的身家超过李嘉诚，成为亚洲首富";

            SearchInfo info2 = new SearchInfo();
            info2.ID = 2;
            info2.Title = "测试Test2";
            info2.Content = "信念的敬意2。 王健林，61岁，林卫华四川人，大连万斯发布2015全球富豪榜，王健林成为 中国内地首富。根据彭博华人富豪榜和亚洲富豪榜截至5月1日的数据，王健林以381亿美元的身家超过李嘉诚，成为亚洲首富";

            SearchInfo info3 = new SearchInfo();
            info3.ID = 3;
            info3.Title = "测试Test3";
            info3.Content = "信念的敬意3。 王健林，61岁，林卫华四川人，大连万斯发布2015全球富豪榜，王健林成为 中国内地首富。根据彭博华人富豪榜和亚洲富豪榜截至5月1日的数据，王健林以381亿美元的身家超过李嘉诚，成为亚洲首富";

            list.Add(info);
            list.Add(info2);
            list.Add(info3);
            return list;
        }
    }
}
