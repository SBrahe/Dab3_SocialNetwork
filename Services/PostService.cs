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

        public List<Post> GetByAuthor(User user) =>
            _posts.Find(post=>post.Author== user).ToList();

        public List<Post> GetFromCircle(Circle circle) =>
            _posts.Find(post => post.ShownCircles.Contains(circle)).ToList();

        public List<Post> GetComments(User user) =>
            _posts.Find(post => post.Author == user).ToList();

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

        public void Empty() =>
            _posts.DeleteMany(post => post.Id !=null);
    }
}

