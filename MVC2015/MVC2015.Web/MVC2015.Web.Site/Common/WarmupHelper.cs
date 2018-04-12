//// ***********************************************************************
//// Assembly         : Towngas.Airwave.Web.Common
//// Author           : xiexiaodong
//// Created          : 06-16-2014
//// Description      : Warm up pages automatically when IIS starts
//// ***********************************************************************
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Text;
//using System.Xml.Linq;
//using System.Configuration;
//using System.Web.Hosting;
//using System.Net;
//using System.IO;
//using System.Reflection;
//using System.Web.Security;
//using System.Threading.Tasks;

//using log4net;
//using Abot.Crawler;
//using Abot.Poco;
//using Abot.Core;
//using MVC2015.Web.Model.Common;
//using MVC2015.Web.BusinessLogic.Account;
//using MVC2015.Web.Site.Common;
//using MVC2015.Web.Site;
//using MVC2015.Web.Model.Account;
//using Microsoft.AspNet.Identity;

//namespace MVC2015.Web.Site.Common
//{
//    /// <summary>
//    /// warmup app, will execute before Application_Start();
//    /// use crawler to warmup every page;
//    /// support IIS 7.5 and above(got a plugin to install for IIS 7.5).
//    /// </summary>
//    public class ApplicationPreload : IProcessHostPreloadClient
//    {
//        public async void Preload(string[] parameters)
//        {
//            try
//            {
//                var id = new Guid();
//                System.Diagnostics.Debug.WriteLine("app preload");
//                CrawlConfiguration crawlConfig = new CrawlConfiguration();
//                crawlConfig.CrawlTimeoutSeconds = 300;
//                crawlConfig.MaxConcurrentThreads = 30;
//                //crawlConfig.MaxCrawlDepth = 3;
//                //crawlConfig.HttpRequestMaxAutoRedirects = 3;                   
//                //crawlConfig.MaxPagesToCrawl = 2000;
//                //crawlConfig.IsExternalPageCrawlingEnabled = false;
//                crawlConfig.IsHttpRequestAutoRedirectsEnabled = true;
//                //PoliteWebCrawler unlogin = new PoliteWebCrawler(crawlConfig, null, null, null, new MyPageRequester(crawlConfig), null, null, null, null);
//                PoliteWebCrawler crawlerSysAdmin = new PoliteWebCrawler(crawlConfig, null, null, null, new MyPageRequester(crawlConfig, id), null, null, null, null);
//                //PoliteWebCrawler crawlerSiteAdmin = new PoliteWebCrawler(crawlConfig, null, null, null, new MyPageRequester(crawlConfig), null, null, null, null);
//                //bind event
//                crawlerSysAdmin.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
//                crawlerSysAdmin.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
//                crawlerSysAdmin.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
//                crawlerSysAdmin.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

//                //crawlerSiteAdmin.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
//                //crawlerSiteAdmin.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
//                //crawlerSiteAdmin.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
//                //crawlerSiteAdmin.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;
//                //await System.Threading.Tasks.Task.Run(() => RunCrawler(unlogin, EnumRole.NotLogin));
//                await System.Threading.Tasks.Task.Run(() => RunCrawler(crawlerSysAdmin, EnumRole.SysAdmin));
//                //await System.Threading.Tasks.Task.Run(() => RunCrawler(crawlerSiteAdmin, EnumRole.SiteAdmin));
//            }
//            catch (Exception e)
//            {
//                System.Diagnostics.Debug.WriteLine("app error");
//                System.Diagnostics.Debug.WriteLine(e.Message);
//            }
//        }


//        /// <summary>
//        /// start to crawl when the Application is Started;
//        /// </summary>
//        /// <param name="crawler"></param>
//        /// <param name="crawlType">sysadmin / siteadmin</param>
//        private void RunCrawler(PoliteWebCrawler crawler, EnumRole role)
//        {
//            string strAppUrl = ConfigurationHelper.GetAppSetting(BasicParam.LocalUrl); //ConfigurationHelper.GetAppSetting("AppUrl");
//            Uri appUri = new Uri(strAppUrl);
//            //Login user = new Login();
//            //string strSiteAdminName = user.GetFirstSiteAdminName();
//            //string strCompany = user.GetFirstSiteAdminCompany(strSiteAdminName);
//            //string strDomainName = appUri.Host;
//            while (true)
//            {
//                System.Threading.Thread.Sleep(5000);
//                if (MvcApplication.IsApplicationStarted)
//                {
//                    break;
//                }
//            }
//            //user.Dispose();

//            if (role == EnumRole.SysAdmin)
//            {
//                System.Diagnostics.Debug.WriteLine("Start the 1st crawl.");
//                //sysadmin
//                //SetCookie("sysadmin", strDomainName);
//                CrawlResult result = crawler.Crawl(appUri);
//                if (result.ErrorOccurred)
//                    System.Diagnostics.Debug.WriteLine("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
//                else
//                    System.Diagnostics.Debug.WriteLine("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);
//            }
//            //else if (role == EnumRole.NotLogin)
//            //{
//            //    System.Diagnostics.Debug.WriteLine("Start the 2nd crawl.");
//            //    //
//            //    Uri appUris = new Uri(appUri + "Account/Login/Index");
//            //    SetCookie("notLogin", strDomainName);
//            //    CrawlResult result = crawler.Crawl(appUris);
//            //    if (result.ErrorOccurred)
//            //        System.Diagnostics.Debug.WriteLine("Crawl of {0} completed with error null: {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
//            //    else
//            //        System.Diagnostics.Debug.WriteLine("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);
//            //}
//            //else if (role == EnumRole.SiteAdmin)
//            //{
//            //    System.Diagnostics.Debug.WriteLine("Start the 2nd crawl.");
//            //    //siteadmin
//            //    SetCookie(strSiteAdminName, strDomainName, strCompany);
//            //    CrawlResult result = crawler.Crawl(appUri);
//            //    if (result.ErrorOccurred)
//            //        System.Diagnostics.Debug.WriteLine("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
//            //    else
//            //        System.Diagnostics.Debug.WriteLine("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);
//            //}
//        }

//        void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
//        {
//            PageToCrawl pageToCrawl = e.PageToCrawl;
//            System.Diagnostics.Debug.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
//        }

//        void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
//        {
//            CrawledPage crawledPage = e.CrawledPage;

//            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
//                System.Diagnostics.Debug.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
//            else
//                System.Diagnostics.Debug.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

//            if (string.IsNullOrEmpty(crawledPage.Content.Text))
//                System.Diagnostics.Debug.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);
//        }

//        void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
//        {
//            CrawledPage crawledPage = e.CrawledPage;
//            System.Diagnostics.Debug.WriteLine("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
//        }

//        void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
//        {
//            PageToCrawl pageToCrawl = e.PageToCrawl;
//            System.Diagnostics.Debug.WriteLine("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
//        }
//    }

//    public class MyPageRequester : PageRequester
//    {
//        //static ILog _logger = LogManager.GetLogger(typeof(MyPageRequester).FullName);
//        public static CookieCollection cookie;
//        public static Guid ID;
//        public MyPageRequester(CrawlConfiguration config, Guid id)
//            : base(config, null)
//        {
//            ID = id;
//        }

//        public override CrawledPage MakeRequest(Uri uri, Func<CrawledPage, CrawlDecision> shouldDownloadContent)
//        {
//            if (uri == null)
//                throw new ArgumentNullException("uri");

//            CrawledPage crawledPage = new CrawledPage(uri);

//            HttpWebRequest request = null;
//            HttpWebResponse response = null;
//            try
//            {
//                request = BuildRequestObject(uri);
//                request.CookieContainer = new System.Net.CookieContainer();
//                if (cookie != null)
//                {
//                    request.CookieContainer.Add(cookie);
//                }
//                request.Headers.Add("GUID", ID.ToString());
//                response = (HttpWebResponse)request.GetResponse();
//                if (cookie == null)
//                {
//                    //cookie = new Cookie(response.Cookies[0].Name,
//                    //    response.Cookies[0].Value, response.Cookies[0].Path, response.Cookies[0].Domain);
//                    cookie = response.Cookies;
//                    //cookie.Expires.AddMinutes(20);
//                }
//            }
//            catch (WebException e)
//            {
//                crawledPage.WebException = e;

//                if (e.Response != null)
//                    response = (HttpWebResponse)e.Response;

//                //_logger.DebugFormat("Error occurred requesting url [{0}]", uri.AbsoluteUri);
//                //_logger.Debug(e);
//            }
//            catch (Exception e)
//            {
//                //_logger.DebugFormat("Error occurred requesting url [{0}]", uri.AbsoluteUri);
//                //_logger.Debug(e);
//            }
//            finally
//            {
//                crawledPage.HttpWebRequest = request;

//                if (response != null)
//                {
//                    crawledPage.HttpWebResponse = response;
//                    CrawlDecision shouldDownloadContentDecision = shouldDownloadContent(crawledPage);
//                    if (shouldDownloadContentDecision.Allow)
//                        crawledPage.Content = _extractor.GetContent(response);
//                    //else
//                    //    _logger.DebugFormat("Links on page [{0}] not crawled, [{1}]", crawledPage.Uri.AbsoluteUri, shouldDownloadContentDecision.Reason);

//                    response.Close();//Should already be closed by _extractor but just being safe
//                }
//            }

//            return crawledPage;
//        }
//    }
//}