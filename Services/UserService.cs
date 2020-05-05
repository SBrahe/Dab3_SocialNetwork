﻿using System;
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

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User GetByName(string name) =>
            _users.Find<User>(user => user.Name == name).FirstOrDefault();

        public Circle Get(User user, int id) =>
            user.Circles.Find(circle => circle.Id == id);

        public Circle Get(User user, string name) =>
            user.Circles.Find(circle => circle.Name == name);

        public List<Circle> GetCirclesByUser(User user)
        {
            var users = Get();
            var circles = new List<Circle>();

            foreach (var user_ in users)
            {
                var hasUser = user_.Circles.Find(x => x.Members.Contains(user));
                if (hasUser != null)
                {
                    circles.Add(hasUser);
                }
            }

            return circles;
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);

        public void Empty() =>
            _users.DeleteMany(user => user.Id !=null);
    }
}

