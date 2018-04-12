using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MVC2015.Web.Site.Common
{
    public sealed class QuartzHepler
    {
        public static void Start()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<TimerJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            //5 minutes between 8am and 23pm, every day
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                .WithIdentity("trigger1", "group1")
                                .WithCronSchedule("0 0/5 8-23 * * ?")
                                .Build();

            sched.ScheduleJob(job, trigger); 
        }
    }
}