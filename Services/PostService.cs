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

        //GETS
        public List<Post> GetAllPosts() =>
            _posts.Find(post => true).ToList();

        public Post GetPostById(string id) =>
            _posts.Find<Post>(post => post.Id == id).FirstOrDefault();

        public List<Post> GetPostByAuthor(User user) =>
            _posts.Find(post => post.Author.Id == user.Id).ToList();

        public List<Comment> GetPostComments(Post post)
        {
            List<Comment> comments = new List<Comment>();
            foreach (var comment_ in post.Comments)
            {
                comments.Add(comment_);
            }
            return comments;
        }

        public List<Post> GetPostsByCircleId(int circleId) =>
            _posts.Find(post => post.ShownCircles.Contains(circleId)).ToList();
        
        //EDITS
        public Post CreatePost(Post post)
        {
            _posts.InsertOne(post);
            return post;
        }
        public void RemovePost(Post postIn) =>
            _posts.DeleteOne(post => post.Id == postIn.Id);

        public void DeleteAllPosts() =>
            _posts.DeleteMany(post => post.Id != null);
    }
}