using System.Collections.Generic;
using Dab_SocialNetwork.Models;
using MongoDB.Driver;

namespace Dab_SocialNetwork.Services
{
    class CircleService
    {
        private readonly IMongoCollection<Circle> _circles;

        public CircleService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");
            _circles = database.GetCollection<Circle>("Circles");
        }

        //GETS
        public List<Circle> GetAllCircles() =>
            _circles.Find(user => true).ToList();
        public Circle GetCircleById(int id) =>
            _circles.Find<Circle>(circle => circle.Id == id).FirstOrDefault();

        public Circle GetCircleByName(string circleName) =>
            _circles.Find<Circle>(circle => circle.Name == circleName).FirstOrDefault();
    }
}