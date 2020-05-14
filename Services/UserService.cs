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

        public UserService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");
            _users = database.GetCollection<User>("Users");
        }

        //GETS
        public List<User> GetAllUsers() =>
            _users.Find(user => true).ToList();
        
        public List<User> GetUsersByCircleId(int circleId) =>
            _users.Find(user => user.Circles.Contains(circleId)).ToList();

        public User GetUserById(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User GetUserByName(string name) =>
            _users.Find<User>(user => user.Name == name).FirstOrDefault();


        //EDITS
        public User CreateUser(User user)
        {
            _users.InsertOne(user);
            return user;
        }
        public void UpdateUser(string name, User userIn) =>
            _users.ReplaceOne(user => user.Name == name, userIn);

        public void RemoveUser(User userIn) =>
            _users.DeleteOne(user => user.Name == userIn.Name);
        
        public void DeleteAllUsers() =>
            _users.DeleteMany(user => user.Name!=null);
    }
}

