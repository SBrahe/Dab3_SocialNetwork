using System;
using System.Collections.Generic;
using System.Text;
using Dab_SocialNetwork.Models;
using MongoDB.Driver;

namespace Dab_SocialNetwork.Services
{
    class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Circle> _circles;

        public UserService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _users = database.GetCollection<User>("Users");
            _circles = database.GetCollection<Circle>("Circles");
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User GetById(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User GetByName(string name) =>
            _users.Find<User>(user => user.Name == name).FirstOrDefault();
        
        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string name, User userIn) =>
            _users.ReplaceOne(user => user.Name == name, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Name == userIn.Name);

        public void Remove(string name) =>
            _users.DeleteOne(user => user.Name== name);

        public void Empty() =>
            _users.DeleteMany(user => user.Name!=null);
    }
}

