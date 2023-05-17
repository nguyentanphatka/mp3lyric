using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using MongoDB.Bson;
using MongoDB.Driver;
using Mp3Lyric.Logging;
using Mp3Lyric.Mongodb.Common;
using Mp3Lyric.Mongodb.DbSetting;
using Mp3Lyric.Mongodb.EntityModel;
using Serilog;

namespace Mp3Lyric.Services
{
    public class CrawlServices : ICrawlServices
    {

        private readonly IDbContext _dbContext;
        public CrawlServices(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #region Common funtion
        [LogMethod]
        static HtmlNode GetRootNode(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            return doc.DocumentNode;
        }
        [LogMethod]
        static string GetHtml(HtmlNode node, string xpath)
        {
            var singleNode = node.SelectSingleNode(xpath);
            return singleNode?.InnerHtml ?? string.Empty;
        }
        [LogMethod]
        static string GetText(HtmlNode node)
        {
            return node?.InnerText ?? string.Empty;
        }
        [LogMethod]
        static string GetText(HtmlNode node, string xpath)
        {
            var _node = node.SelectSingleNode(xpath);
            return _node?.InnerText ?? string.Empty;
        }
        [LogMethod]
        static string? GetAttribute(HtmlNode node, string xpath, string attributeName)
        {
            var singleNode = node;
            if (!string.IsNullOrWhiteSpace(xpath))
            {
                singleNode = node.SelectSingleNode(xpath);
            }
            if (singleNode?.Attributes?.OfType<HtmlAttribute>().Any(att => att.Name == attributeName) == true)
                return singleNode.Attributes?[attributeName]?.Value;
            return null;
        }
        [LogMethod]
        static List<string> GetMultipleText(HtmlNode node, string xpath)
        {
            var resultList = new List<string>();
            var nodes = node.SelectNodes(xpath);
            foreach (var innerNode in nodes)
            {
                var inner = HttpUtility.HtmlDecode(innerNode.InnerText ?? string.Empty);
                if (!string.IsNullOrEmpty(inner))
                    resultList.Add(inner);
            }
            return resultList;
        }
        [LogMethod]
        static HtmlNodeCollection GetChildNodes(HtmlNode node, string xpath)
        {
            return node.SelectNodes(xpath);
        }

        #endregion
        [LogMethod]
        public async Task CrawlLyricAsync()
        {
            var headCrawlUrl =
                $@"https://www.lyrics.com/genre/Rock;Blues;Stage%20__%20Screen;Reggae;Spiritual;Non-Music;Pop;Latin;Jazz;Hip%20Hop;Folk,%20World,%20__%20Country;Funk%20--%20Soul;Electronic;Classical;Brass%20__%20Military;Children";
            var headRootNode = GetRootNode(headCrawlUrl);

            var totalPageText = GetChildNodes(headRootNode, "//div[@class='pager']/a")?.Last()?.InnerText;

            int.TryParse(totalPageText, out var totalPage);
            
            var index = await _dbContext.Songs.Find(FilterDefinition<Song>.Empty).Project(s=>s.Index).SortByDescending(song => song.Index).Limit(1).FirstOrDefaultAsync() ?? 0;
            var begin = 356;
            for (var i = begin; i < totalPage; i++)
            {
                var crawlUrl = headCrawlUrl + @$"/{i}";
                var rootNode = GetRootNode(crawlUrl);
                var listNodes = GetChildNodes(rootNode, "//div[@class='lyric-meta col-sm-6 col-xs-6']");


                var listWrites = new List<WriteModel<Song>>();
                foreach (var node in listNodes)
                {
                    var lyricName = GetText(node, "p[@class='lyric-meta-title']");
                    var lyricUrl = GetAttribute(node, "p[@class='lyric-meta-title']/a", "href");
                    var albumName = GetText(node, "p[@class='lyric-meta-album']");
                    var albumUrl = GetAttribute(node, "p[@class='lyric-meta-album']/a", "href");
                    var albumThumbUrl = GetAttribute(node, "div[@class='album-thumb']/a/img", "src");
                    var albumAuthorName = GetText(node, "p[@class='lyric-meta-album-artist']");
                    var albumAuthorUrl = GetAttribute(node, "p[@class='lyric-meta-album-artist']/a", "href");


                    var song = new Song()
                    {

                        Id = ObjectId.GenerateNewId(),
                        CrawlStatus = CrawlStatus.New,
                        Name = lyricName,
                        SongUrl = lyricUrl,
                        Index = index,
                        Created = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow
                    };
                    listWrites.Add(new InsertOneModel<Song>(song));
                    index++;
                }
                
                if (listWrites.Any())
                    await _dbContext.Songs.BulkWriteAsync(listWrites);

                
                Log.Information("Page +" + i + " done.");

            }
            
            Log.Information("DONE");
        }
    }
}
