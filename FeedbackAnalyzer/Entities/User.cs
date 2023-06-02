using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Prediction.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public List<UserDetail> UserDetails { get; set; }
    }

    public class UserDetail
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
