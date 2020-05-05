using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dab_SocialNetwork.Models
{
    class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Author of post")]
        public User Author { get; set; }

        [BsonElement("Public to guests")]
        public bool IsPublic { get; set; }
        
        [BsonElement("Post type")]
        public string PostType { get; set; }

        [BsonElement("Content of post")]
        public string Content { get; set; }

        [BsonElement("Circles for post")]
        public List<Circle> ShownCircles { get; set; }

        [BsonElement("Date and time of post")]
        public DateTime Created { get; set; }
        
        [BsonElement("Comments")]
        public List<Comment> Comment { get; set; }
    }
}
