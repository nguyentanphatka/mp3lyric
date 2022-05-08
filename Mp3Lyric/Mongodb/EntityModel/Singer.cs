namespace Mp3Lyric.Mongodb.EntityModel
{
    public class Singer : BaseEntity
    {
        public string SingerUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Slug { get; set; }
        public List<string> Genres { get; set; } = null!;
        public string Status { get; set; } = string.Empty;
        
        public string Image150 { get; set; } = string.Empty;
        public string Image300 { get; set; } = string.Empty;
        public string Image600 { get; set; } = string.Empty;
        public string ImageDefault { get; set; } = string.Empty;
        
        public int TotalViews { get; set; } = 0; //TODO
        public bool IsHotItem { get; set; } = true; //TODO
    }
}