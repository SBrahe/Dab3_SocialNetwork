using System;
using System.Collections.Generic;
using System.Text;
using Dab_SocialNetwork.Models;
using MongoDB.Driver;

namespace Dab_SocialNetwork.Services
{
    class PostService
    {
        private readonly IMongoCollection<Post> _posts;

        public PostService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _posts = database.GetCollection<Post>("Posts");
        }

        public List<Post> Get() =>
            _posts.Find(post => true).ToList();

        public Post Get(string id) =>
            _posts.Find<Post>(post => post.Id == id).FirstOrDefault();

        public Post Create(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }

        public void Update(string id, Post postIn) =>
            _posts.ReplaceOne(post => post.Id == id, postIn);

        public void Remove(Post postIn) =>
            _posts.DeleteOne(post => post.Id == postIn.Id);

        public void Remove(string id) =>
            _posts.DeleteOne(post => post.Id == id);
    }
}

