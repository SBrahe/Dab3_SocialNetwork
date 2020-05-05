using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dab_SocialNetwork.Models
{
    class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }



        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Gender")]
        public string Gender { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("MobileNumber")]
        public string MobileNum { get; set; }

        [BsonElement("Circles")]
        public List<Circle> Circles { get; set; }

        [BsonElement("Blocked users")]
        public List<User> BlockedUsers { get; set; }
        
        [BsonElement("Followed users")]
        public List<User> FollowedUsers { get; set; }
    }
}
