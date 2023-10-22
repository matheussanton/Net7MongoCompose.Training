using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace src.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string UserName { get; set; } = null!;
        public int Age { get; set; }
    }
}
