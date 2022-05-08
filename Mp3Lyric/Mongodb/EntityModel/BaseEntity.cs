using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mp3Lyric.Mongodb.EntityModel
{
    public class BaseEntity
    {
        [BsonId]
        public ObjectId? Id { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
