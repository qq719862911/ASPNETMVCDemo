using log4net;
using MVC2015.FullTextSearch;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC2015.Web.Site.Common
{
    public sealed class TimerJob:IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TimerJob));

        public void Execute(IJobExecutionContext context)
        {
            _logger.InfoFormat("LuceneSample.CreateIndex() Start at:"+DateTime.Now.ToString());
            LuceneSample.CreateIndex();
            _logger.InfoFormat("LuceneSample.CreateIndex() End at:" + DateTime.Now.ToString());
        }

    }
}