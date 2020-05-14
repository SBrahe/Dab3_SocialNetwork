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
        public int Id { get; set; }

        [BsonElement("Name of the circle")]
        public string Name { get; set; }

        [BsonElement("Members of circle")]
        public List<string> Members { get; set; }
    }
}
