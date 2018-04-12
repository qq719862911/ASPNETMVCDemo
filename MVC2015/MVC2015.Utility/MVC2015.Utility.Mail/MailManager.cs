//using Microsoft.VisualBasic;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Configuration;
//using System.Timers;
//using Config;
//using Base;
//using EventHandler;
//using System.Reflection;



///// <summary> 
///// Mail manager class, include the mail job, mail queue, time process. 
///// </summary> 
//internal class MailManager
//{
//    public static readonly MailManager Instance = new MailManager();
//    private MailManager()
//    {
//        //Get the Mail Section info from config file. 
//        _mailSection = (MailSection)ConfigurationManager.GetSection("mail");
//        //Get whether enable mail sending. 
//        _enable = _mailSection.Enable;
//        _enableSSL = _mailSection.EnableSSL;
//        //Initialize the job timer ------------ 
//        _timer = new Timer();
//        _timer.Interval = _mailSection.Job.SendInterval * 1000 * 60;
//        _timer.Enabled = true;
//        _timer.Elapsed += Timer_Elapsed;
//        //------------------------------------- 

//        //Initialize the Mail Queue ---------------- 
//        _queue = new MailQueue(_mailSection.Queue);
//        _queue.AddingMail += Queue_AddingMail;
//        _queue.AddedMail += Queue_AddedMail;
//        _queue.DeletingMail += Queue_DeletingMail;
//        _queue.DeletedMail += Queue_DeletedMail;
//        _queue.DequeueMail += Queue_DequeueMail;
//        _queue.ExceedQueueSize += Queue_ExceedQueueSize;
//        _queue.MailError += QueueAndJob_MailError;
//        //------------------------------------------ 

//        //Initialize the Mail Job ----------------- 
//        _job = new MailJob(_mailSection.Job, _queue, _enableSSL);
//        _job.SendingMail += Job_SendingMail;
//        _job.SendedMail += Job_SendedMail;
//        _job.MailError += QueueAndJob_MailError;
//        //------------------------------------------ 
//        //Initialize the Event Receiver ------- 
//        // aaron 2014/3/7
//        string eventHandler = _mailSection.EventReceiver.Type;
//        string dllPath = _mailSection.EventReceiver.DllPath;
//        if (!string.IsNullOrEmpty(dllPath) && !string.IsNullOrEmpty(eventHandler))
//        {
//            Assembly assembly = Assembly.LoadFile(_mailSection.EventReceiver.DllPath);
//            Type tp = assembly.GetType(eventHandler);
//            _mailEventReceiver = (MailEvent)Activator.CreateInstance(tp, false);
//        }
//        else if (!string.IsNullOrEmpty(eventHandler))
//        {
//            Type tp = Type.GetType(eventHandler);
//            _mailEventReceiver = (MailEvent)Activator.CreateInstance(tp, false);
//            //------------------------------------- 
//        }

//        //if (string.IsNullOrEmpty(_mailSection.EventReceiver) == false)
//        //{
//        //    Type tp = Type.GetType(_mailSection.EventReceiver);
//        //    _mailEventReceiver = (MailEvent)Activator.CreateInstance(tp, false);
//        //    //------------------------------------- 
//        //}
//    }



//    private MailSection _mailSection;
//    private MailQueue _queue;
//    private MailJob _job;
//    //Whether enable mail sending job. 
//    private bool _enable;
//    private bool _enableSSL;
//    private Timer _timer;

//    private MailEvent _mailEventReceiver;
//    public void Start()
//    {
//        if (_enable)
//        {
//            _timer.Start();
//        }
//    }
//    public void Stop()
//    {
//        _timer.Stop();
//    }

//    public bool AddMail(Email email, ref string msg)
//    {
//        bool check = false;
//        check = _queue.Enqueue(email, ref msg);
//        return check;
//    }

//    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
//    {
//        _timer.Enabled = false;
//        _job.Run();
//        _timer.Enabled = true;
//    }

//    #region "Event Receiver"
//    private void Job_SendedMail(object sender, MailSendedMailEventArgs e)
//    {
//        if (_mailEventReceiver != null)
//        {
//            _mailEventReceiver.OnSendedMail(sender, e);
//        }
//    }

//    private void Job_SendingMail(object sender, MailSendingMailEventArgs e)
//    {
//        if (_mailEventReceiver != null)
//        {
//            _mailEventReceiver.OnSendingMail(sender, e);
//        }
//    }

//    private void QueueAndJob_MailError(object sender, MailErrorEventArgs e)
//    {
//        if (_mailEventReceiver != null)
//        {
//            _mailEventReceiver.OnMailError(sender, e);
//        }
//    }

//    private void Queue_ExceedQueueSize(object sender, MailExceedQueueSizeEventArgs e)
//    {
//        if (_mailEventReceiver != null)
//        {
//            _mailEventReceiver.OnExceedQueueSize(sender, e);
//        }
//    }

//    private void Queue_DequeueMail(object sender, MailDequeueMailEventArgs e)
//    {
//        if (_mailEventReceiver != null)
//        {
//            _mailEventReceiver.OnDequeueMail(sender, e);
//        }
//    }

//    private void Queue_DeletedMail(object sender, MailDeletedMailEventArgs e)
//    {
//        if (_mailEventReceiver != null)
//        {
//            _mailEventReceiver.OnDeletedMail(sender, e);
//        }
//    }

//    private void Queue_DeletingMail(object sender, MailDeletingMailEventArgs e)
//    {
//        if (_mailEventReceiver != null)
//        {
//            _mailEventReceiver.OnDeletingMail(sender, e);
//        }
//    }

//    private void Queue_AddedMail(object sender, MailAddedMailEventArgs e)
//    {
//        if (_mailEventReceiver != null)
//        {
//            _mailEventReceiver.OnAddedMail(sender, e);
//        }
//    }

//    private void Queue_AddingMail(object sender, MailAddingMailEventArgs e)
//    {
//        if (_mailEventReceiver != null)
//        {
//            _mailEventReceiver.OnAddingMail(sender, e);
//        }
//    }
//    #endregion

//}


