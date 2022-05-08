using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mp3Lyric.CrawlModel
{
    public class NovelModel
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
         [JsonProperty("synopsis")]
        public string Synopsis { get; set; }
        [JsonProperty("author_id")]
        public int? Author_id { get; set; }
        [JsonProperty("author_name")]
        public string Author_name { get; set; }
        [JsonProperty("vote_count")]
        public int? Vote_count { get; set; }
        [JsonProperty("review_score")]
        public string Review_score { get; set; }
        [JsonProperty("comment_count")]
        public int? Comment_count { get; set; }
        [JsonProperty("chapter_count")]
        public int? Chapter_count { get; set; }
        [JsonProperty("bookmark_count")]
        public int? Bookmark_count { get; set; }
        [JsonProperty("word_count")]
        public int? Word_count { get; set; }
        [JsonProperty("user_name")]
        public string User_name { get; set; }
        [JsonProperty("new_chap_at")]
        public string New_chap_at { get; set; }
        [JsonProperty("kind")]
        public int? Kind { get; set; }
        [JsonProperty("sex")]
        public int? Sex { get; set; }
        [JsonProperty("lastest_index")]
        public int? Latest_index { get; set; }
        [JsonProperty("genres")]
        public Dictionary<int,string> Genres { get; set; }
        //[JsonProperty("poster")]
        public PosterModel Poster { get; set; }

        public NovelModel() { }
        public NovelModel(JToken jtoken)
        {
            Id = (int?)jtoken?["id"]??0;
            Name = jtoken?["name"]?.ToString() ?? string.Empty;
            Slug = jtoken?["slug"]?.ToString() ?? string.Empty;
            Synopsis = jtoken?["synopsis"]?.ToString() ?? string.Empty;
            Author_id = (int?)jtoken?["author_id"] ?? 0;
            Author_name = jtoken?["author_name"]?.ToString() ?? string.Empty;
            Author_id = (int?)jtoken?["author_id"] ?? 0;
            Vote_count = (int?)jtoken?["vote_count"] ?? 0;
            Comment_count = (int?)jtoken?["comment_count"] ?? 0;
            Chapter_count = (int?)jtoken?["chapter_count"] ?? 0;
            Bookmark_count = (int?)jtoken?["bookmark_count"] ?? 0;
            Word_count = (int?)jtoken?["word_count"] ?? 0;
            Kind = (int?)jtoken?["kind"] ?? 0;
            Sex = (int?)jtoken?["sex"] ?? 0;
            Review_score = jtoken?["review_score"]?.ToString() ?? string.Empty;
            User_name = jtoken?["user_name"]?.ToString() ?? string.Empty;
            New_chap_at = jtoken?["new_chap_at"]?.ToString() ?? string.Empty;
            //New_chap_at = jtoken["new_chap_at"]?.ToObject<DateTimeOffset>();
            Latest_index = (int?)jtoken?["latest_index"] ?? 0;
            Poster = new PosterModel
            {
                _150 = jtoken?["poster"]?["150"]?.ToString() ?? string.Empty,
                _300 = jtoken?["poster"]?["300"]?.ToString() ?? string.Empty,
                _600 = jtoken?["poster"]?["600"]?.ToString() ?? string.Empty,
                Default = jtoken?["poster"]?["default"]?.ToString() ?? string.Empty
            };
            Genres = JsonConvert.DeserializeObject<Dictionary<int, string>>(jtoken?["genres"]?.ToString() ?? string.Empty) ?? new Dictionary<int, string>();

        }

    }
    public class PosterModel
    {
        [JsonProperty("150")]
        public string _150 { get; set; }
        [JsonProperty("300")]
        public string _300 { get; set; }
        [JsonProperty("600")]
        public string _600 { get; set; }
        [JsonProperty("default")]
        public string Default { get; set; }
    }
    public class PagingModel
    {
        [JsonProperty("_current")]
        public int? Current { get; set; }
        [JsonProperty("_next")]
        public int? Next { get; set; }
        [JsonProperty("_prev")]
        public int? Prev { get; set; }
        [JsonProperty("_last")]
        public int? Last { get; set; }
        [JsonProperty("_limit")]
        public int? Limit { get; set; }
        [JsonProperty("_total")]
        public int? Total { get; set; }
    }
    public class ChapterModel
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("index")]
        public int? Index { get; set; }

        [JsonProperty("published_at")]
        public DateTime? Published_At { get; set; }
    }
}

