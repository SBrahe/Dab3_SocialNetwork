using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dab_SocialNetwork.Models
{
    enum Feeling
    {
        Glad,
        Jævnt_Utilfreds,
        Sur,
        Ked_af_det,
        Festlig,
        Gammel
    }
    class Post
    {
        [BsonId]
        public string Id { get; set; }

        [BsonElement("Author of post")]
        public User Author { get; set; }

        [BsonElement("Public to guests")]
        public bool IsPublic { get; set; }
        
        [BsonElement("Post Feeling")]
        public Feeling PostFeeling { get; set; }

        [BsonElement("Post Text")]
        public string PostText { get; set; }

        [BsonElement("Circles for post")]
        public List<Circle> ShownCircles { get; set; }

        [BsonElement("Date and time of post")]
        public DateTime Created { get; set; }
        
        [BsonElement("Comments")]
        public List<Comment> Comment { get; set; }
    }
}
