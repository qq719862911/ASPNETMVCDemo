using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Utility.Common.Web
{
    /// <summary>
    /// Get seetings from ConfigurationManager
    /// </summary>
    public static class ConfigurationHelper
    {
        public static string GetAppSetting(string name)
        {
            string strSetting = string.Empty;
            if (ConfigurationManager.AppSettings.AllKeys.Contains(name))
            {
                strSetting = ConfigurationManager.AppSettings[name];
            }
            return strSetting;
        }

        public static string GetConnectionString(string name)
        {
            string strConnectionString = string.Empty;
            try
            {
                strConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[name].ConnectionString;
            }
            catch
            {

            }
            return strConnectionString;
        }
    }

}
