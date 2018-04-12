using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2015.Web.Site.Common
{
    public class ServerGUID
    {
        public static Guid ServerGuid;
        private static object locker = new object();
        public ServerGUID()
        {
            lock (locker)
            {
                Guid serverGUID = Guid.NewGuid();
                ServerGuid = serverGUID;
            }
        }
    }
}