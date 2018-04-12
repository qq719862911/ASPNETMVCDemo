using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.SystemMaint.UserInfo
{
    public class UserInfoModel
    {
        public UserInfoSearch Search { get; set; }

        public IQueryable<UserInfoItem> List { get; set; }
    }
}
