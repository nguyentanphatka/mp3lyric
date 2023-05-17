using Mp3Lyric.Mongodb.Common;

namespace Mp3Lyric.Mongodb.DbSetting
{
    public class DatabaseInfo
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

        public string SongCollectionName { get; set; } = Language.SongCollectionName;
        public string SingerCollectionName { get; set; } = Language.SingerCollectionName;
    }
}
