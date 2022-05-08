using MongoDB.Bson;
using Mp3Lyric.Mongodb.Common;

namespace Mp3Lyric.Mongodb.EntityModel
{
    public class Song : BaseEntity
    {
        public ObjectId? SingerId { get; set; }
        public string SongUrl { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public string DownloadUrl { get; set; } = string.Empty;
        
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Sumary { get; set; } = string.Empty; //markdown
        public string CrawlStatus { get; set; } = Common.CrawlStatus.New;
        public string Content { get; set; } = string.Empty; // markdown
        public DateTimeOffset? PublishedAt { get; set; } = null!; // markdown
        public int? Index { get; set; }
        public int WordCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;

        public List<string> Composed { get; set; } = new List<string>();
        public List<string> Producer { get; set; } = new List<string>();
        public string Album { get; set; } = string.Empty;
        public List<string> Genres { get; set; } = new List<string>();
    }
}