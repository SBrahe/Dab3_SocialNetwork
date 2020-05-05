using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Dab_SocialNetwork.Models
{
    class Circle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        [BsonElement("Members of circle")]
        public List<User> Members { get; set; }
    }
}
