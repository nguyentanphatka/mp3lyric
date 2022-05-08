using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mp3Lyric.Mongodb.EntityModel;

namespace Mp3Lyric.Mongodb.DbSetting
{
    public interface IDbContext
    {
        public IMongoCollection<Singer> Singers { get; set; }
        public IMongoCollection<Song> Songs { get; set; }
    }

    public class DbContext : IDbContext
    {
        public MongoClient MongoClient { get; set; }
        public IMongoCollection<Singer> Singers { get; set; }
        public IMongoCollection<Song> Songs { get; set; }

        public DbContext(IOptions<DatabaseInfo> mongoDbConfigOptions)
        {
            var mongoUrl = MongoUrl.Create(mongoDbConfigOptions.Value.ConnectionString);
            MongoClient = new MongoClient(mongoUrl);
            var db = MongoClient.GetDatabase(mongoDbConfigOptions.Value.DatabaseName);

            Singers = db.GetCollection<Singer>(mongoDbConfigOptions.Value.SingerCollectionName);
            Songs = db.GetCollection<Song>(mongoDbConfigOptions.Value.SongCollectionName);
            
            var indexKeysDefinition = Builders<Singer>.IndexKeys.Ascending(novel => novel.Status);
            Singers.Indexes.CreateOneAsync(new CreateIndexModel<Singer>(indexKeysDefinition), cancellationToken: CancellationToken.None);
            
            var indexKeysChapterDefinition = Builders<Song>.IndexKeys.Ascending(chap => chap.SingerId);
            Songs.Indexes.CreateOneAsync(new CreateIndexModel<Song>(indexKeysChapterDefinition), cancellationToken: CancellationToken.None);
        }
    }
}
