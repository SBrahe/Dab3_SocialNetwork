using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Dab_SocialNetwork.Models
{
    class Comment
    {
        [BsonElement("Author of comment")]
        public User Author { get; set; }

        [BsonElement("Content of comment")]
        public string Content { get; set; }

        [BsonElement("Date and time of comment")]
        public DateTime DateAndTime { get; set; }
    }
}
