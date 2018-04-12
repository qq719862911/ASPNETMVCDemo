using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using PanGu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CTX = MVC2015.DataProvider.MVC2015DB.Contexts;
using MD = MVC2015.DataProvider.MVC2015DB.Models;
using MVC2015.Web.BusinessLogic.SystemMaint;
using BL=MVC2015.Web.BusinessLogic.SystemMaint;
using VM = MVC2015.Web.Model.SystemMaint.FullTextSearch;


namespace MVC2015.FullTextSearch
{
    public class LuceneSample
    {
        public static void CreateIndex()
        {
            BL.SearchInfo bl = new BL.SearchInfo();

            Stopwatch sw = new Stopwatch();//加入时间统计
            //获取 数据列表
            var searchDataList = bl.GetAll();
            IList<VM.SearchModel> infos = searchDataList.ToList<VM.SearchModel>();

            sw.Start();
            //判断目录directory是否是一个索引目录。  
            bool isUpdate = IndexReader.IndexExists(_directory());
            IndexWriter writer = new IndexWriter(_directory(), new PanGuAnalyzer(), true, IndexWriter.MaxFieldLength.LIMITED);

            WebClient wc = new WebClient();
            //编码，防止乱码  
            wc.Encoding = Encoding.UTF8;

            foreach (VM.SearchModel item in infos)
            {
                //为避免重复索引，先输出ID的记录，在重新添加  
                writer.DeleteDocuments(new Term("ID", item.ID.ToString()));

                Document doc = new Document();
                Field id = new Field("ID", item.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
                Field title = new Field("Title", item.Title.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
                Field content = new Field("Content", item.Contents.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
                doc.Add(id);
                doc.Add(title);
                doc.Add(content);
                writer.AddDocument(doc);
            }
            writer.Optimize();
            writer.Commit();
            sw.Stop();
        }

        public static IEnumerable<VM.SearchModel> Search(string keyWord)
        {
            try
            {
                List<VM.SearchModel> result = new List<VM.SearchModel>();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                /*加入时间统计*/
                Stopwatch sw = new Stopwatch();
                sw.Start();

                /*创建 Lucene.net 搜索实例*/
                string IndexDic = GetMapPath("~/SearchData/IndexData");
                string PanGuXmlPath = GetMapPath("~/SearchData/PanGu/PanGu.xml");
                PanGu.Segment.Init(PanGuXmlPath);

                FSDirectory directory = FSDirectory.Open(new DirectoryInfo(IndexDic), new NoLockFactory());
                IndexReader reader = IndexReader.Open(directory, true);
                IndexSearcher searcher = new IndexSearcher(reader);

                /*为搜索实例 加入搜索分词规则  来源 盘古分词*/
                keyWord = GetKeyWordsSplitBySpace(keyWord, new PanGuTokenizer());
                PhraseQuery query = new PhraseQuery();
                if (!string.IsNullOrEmpty(keyWord))
                {
                    dic.Add("Content", keyWord);
                    foreach (string word in keyWord.Split('/'))
                    {
                        if (word == "")
                            continue;
                        query.Add(new Term("Content", word)); 
                    }
                }
                query.Slop=100;
                TopScoreDocCollector collector = TopScoreDocCollector.Create(1024, true);//最大1024条记录
                searcher.Search(query, null, collector);
                int totalCount = collector.TotalHits;//返回总条数  
                ScoreDoc[] docs = collector.TopDocs(0, 10).ScoreDocs;//分页,下标应该从0开始吧，0是第一条记录  

                ///*指定排序方式  按 PostScore 字段来排序*/
                //List<SortField> sorts = new List<SortField>();
                //SortField sf = new SortField("ID", SortField.INT, true);
                //sorts.Add(sf);
                //Sort sort = new Sort(sorts.ToArray());
                //TopFieldDocs docs = searcher.Search(bq, null, searcher.MaxDoc, sort);
                //int allCount = docs.TotalHits;
                ///*获取匹配的前10条*/
                //ScoreDoc[] hits = TopDocs(0, 10, docs);
                foreach (ScoreDoc sd in docs)//遍历搜索到的结果
                {
                    try
                    {
                        Document doc = searcher.Doc(sd.Doc);
                        var model = new VM.SearchModel();
                        model.ID = int.Parse(doc.Get("ID"));
                        model.Title = doc.Get("Title");
                        model.Contents = doc.Get("Content");

                        //SetHighlighter(null, model);
                        result.Add(SetHighlighter(dic, model));
                    }
                    catch
                    {

                    }
                }
                searcher.Close();
                searcher.Dispose();
                sw.Stop();

                return result;
            }
            catch (Exception)
            {

                return null;
            }

        }

        #region Base function
        private static FSDirectory _directory()
        {
            string _luceneDir = GetMapPath("~/SearchData/IndexData");
            FSDirectory _directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
            if (IndexWriter.IsLocked(_directoryTemp)) IndexWriter.Unlock(_directoryTemp);
            var lockFilePath = Path.Combine(_luceneDir, "write.lock");
            if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
            return _directoryTemp;

        }

        private static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用  
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\") || strPath.StartsWith("~"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        private static string GetKeyWordsSplitBySpace(string keywords, PanGuTokenizer ktTokenizer)
        {
            StringBuilder result = new StringBuilder();
            /*执行分词操作 一个关键字可以拆分为多个次和单个字*/
            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);
 
            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }
 
                result.AppendFormat("{0}/", word.Word);
            }
 
            return result.ToString().Trim();
        }

        private static VM.SearchModel SetHighlighter(Dictionary<string, string> dicKeywords, VM.SearchModel model)
        {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
            PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
            highlighter.FragmentSize = 100;
            string strContent = string.Empty;
            dicKeywords.TryGetValue("Content", out strContent);
            if (!string.IsNullOrEmpty(strContent))
            {
                model.Contents = highlighter.GetBestFragment(strContent,model.Contents)+"...";
                //model.Content = string.Join("...", (highlighter.GetFragments(strContent, model.Content,3).ToList()));
            }

            return model;
        }
        private static ScoreDoc[] TopDocs(int start, int limit, TopFieldDocs docs)
        {
            int endIndex = 0;
            int hc = docs.TotalHits;
            if (hc - start > limit)
            {
                endIndex = start + limit;
            }
            else
            {
                endIndex = hc;
            }

            List<ScoreDoc> dl = new List<ScoreDoc>();
            var da = docs.ScoreDocs;
            for (int i = start; i < endIndex; i++)
            {
                dl.Add(da[i]);
            }
            return dl.ToArray();
        }
        #endregion
    }
}
