namespace Mp3Lyric.Mongodb.DbSetting
{
    public class DatabaseInfo
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

        public string SongCollectionName { get; set; } = null!;
        public string SingerCollectionName { get; set; } = null!;
    }
}
