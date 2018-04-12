using MVC2015.Utility.Resource;
using MVC2015.Web.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Web.Model.KeyValue
{
    public class UserStatus
    {
        public static readonly TextValue Normal = new TextValue { Text = ResourceHelper.GetValue("SM_UserInfo_UserStatus_Normal"), strValue = "1", intValue = 1 };
        public static readonly TextValue Disable = new TextValue { Text = ResourceHelper.GetValue("SM_UserInfo_UserStatus_Forbidden"), strValue = "0", intValue = 0 };

        public static TextValue GetModel(int Id)
        {
            switch (Id)
            {
                case 1:
                    return Normal;
                case 0:
                    return Disable;
                default:
                    throw new ArgumentException("application form's Id is invaild.");

            }
        }

        public static List<TextValue> GetList()
        {
            List<TextValue> selectList = new List<TextValue>();
            selectList.Add(new TextValue() { Text = "" });
            selectList.Add(new TextValue() { Text = UserStatus.Normal.Text, strValue = UserStatus.Normal.strValue });
            selectList.Add(new TextValue() { Text = UserStatus.Disable.Text, strValue = UserStatus.Disable.strValue });
            return selectList;
        }
    }
}
