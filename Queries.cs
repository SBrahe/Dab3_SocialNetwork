using System;
using System.Collections.Generic;
using System.Linq;
using Dab_SocialNetwork.Models;
using Dab_SocialNetwork.Services;

namespace Dab_SocialNetwork
{
    class Queries
    {
        private UserService userService = new UserService();
        private PostService postService = new PostService();

        public void ShowFeedForUser(User Subject)
        {
            List<Post> postsInSubjectFeed = new List<Post>();
            List<Circle> circlesFollowedBySubject = Subject.Circles;
            foreach (var x in circlesFollowedBySubject)
            {
                List<Post> postsInCircle = postService.GetPostsInCircle(x);
                postsInSubjectFeed.AddRange(postsInCircle);
            }

            List<User> usersFollowedBySubject = Subject.FollowedUsers;
            foreach (var x in usersFollowedBySubject)
            {
                List<Post> postsFromFollowedUser = postService.GetByAuthor(x);
                postsFromFollowedUser.AddRange(postsFromFollowedUser);
            }

            Console.WriteLine($"-------------{Subject.Name}'s Feed-------------");
            for (var x = 0; x < postsInSubjectFeed.Count; x++)
            {
                Console.WriteLine("****");
                Console.WriteLine($"Post #{x + 1}, content type: {postsInSubjectFeed[x].PostType}");
                if (postsInSubjectFeed[x].PostType == PostType.Text)
                {
                    Console.WriteLine($"'{postsInSubjectFeed[x].PostText}'");
                }
                else if (postsInSubjectFeed[x].PostType == PostType.Feeling)
                {
                    Console.WriteLine($"'{postsInSubjectFeed[x].PostFeeling}'");
                }

                Console.WriteLine($"Author: {postsInSubjectFeed[x].Author.Name}");
                Console.WriteLine($"Date of post: {postsInSubjectFeed[x].Created}");
                if (postsInSubjectFeed[x].Comments == null) continue;
                Console.WriteLine($"Comments:");
                foreach (var y in postsInSubjectFeed[x].Comments)
                {
                    Console.WriteLine($"'{y.Content}'");
                    Console.WriteLine($"Author: {y.Author.Name} ");
                    Console.WriteLine($"Date of comment: {y.DateAndTime} ");
                    Console.WriteLine("*");
                }
            }
        }

        public void ShowWallForFriend(User wallOwner, User viewer)
        {
            List<Post> postsOnWall = new List<Post>();
            List<Post> postsThatViewerHasAccessTo = new List<Post>();

            postsOnWall.AddRange(postService.GetByAuthor(wallOwner));
            foreach (var x in postsOnWall)
            {
                var viewingFriendHasAccess = false;
                List<Circle> postCircles = x.ShownCircles;
                if (x.ShownCircles == null) continue;
                foreach (var y in postCircles)
                {
                    if (y.Members == null) continue;
                    if (y.Members.Contains(userService.GetByName(viewer.Name)))
                    {
                        viewingFriendHasAccess = true;
                    }
                }

                if (x.IsPublic == true)
                {
                    viewingFriendHasAccess = true;
                }

                if (viewingFriendHasAccess == true)
                {
                    postsThatViewerHasAccessTo.Add(x);
                }
            }

            Console.WriteLine($"-------------{wallOwner.Name}'s Wall-------------");
            for (var x = 0; x < postsThatViewerHasAccessTo.Count; x++)
            {
                Console.WriteLine("****");
                Console.WriteLine($"Post #{x + 1}, content type: {postsThatViewerHasAccessTo[x].PostType}");
                if (postsThatViewerHasAccessTo[x].PostType == PostType.Text)
                {
                    Console.WriteLine($"'{postsThatViewerHasAccessTo[x].PostText}'");
                }
                else if (postsThatViewerHasAccessTo[x].PostType == PostType.Feeling)
                {
                    Console.WriteLine($"'{postsThatViewerHasAccessTo[x].PostFeeling}'");
                }

                Console.WriteLine($"Author: {postsThatViewerHasAccessTo[x].Author.Name}");
                Console.WriteLine($"Date of post: {postsThatViewerHasAccessTo[x].Created}");

                if (postsThatViewerHasAccessTo[x].Comments == null) continue;
                Console.WriteLine($"Comments:");
                foreach (var y in postsThatViewerHasAccessTo[x].Comments)
                {
                    Console.WriteLine($"'{y.Content}'");
                    Console.WriteLine($"Author: {y.Author.Name} ");
                    Console.WriteLine($"Date of comment: {y.DateAndTime} ");
                    Console.WriteLine("*");
                }
            }
        }

        public void ShowOwnWall(User wallOwner)
        {
            List<Post> postsOnWall = new List<Post>();

            postsOnWall = postService.GetByAuthor(wallOwner);
            
            Console.WriteLine($"-------------{wallOwner.Name}'s Wall-------------");
            for (var x = 0; x < postsOnWall.Count; x++)
            {
                Console.WriteLine("****");
                Console.WriteLine($"Post #{x + 1}, content type: {postsOnWall[x].PostType}");
                if (postsOnWall[x].PostType == PostType.Text)
                {
                    Console.WriteLine($"'{postsOnWall[x].PostText}'");
                }
                else if (postsOnWall[x].PostType == PostType.Feeling)
                {
                    Console.WriteLine($"'{postsOnWall[x].PostFeeling}'");
                }

                Console.WriteLine($"Author: {postsOnWall[x].Author.Name}");
                Console.WriteLine($"Date of post: {postsOnWall[x].Created}");
                
                if (postsOnWall[x].Comments == null) continue;
                Console.WriteLine($"Comments:");
                foreach (var y in postsOnWall[x].Comments)
                {
                    Console.WriteLine($"'{y.Content}'");
                    Console.WriteLine($"Author: {y.Author.Name} ");
                    Console.WriteLine($"Date of comment: {y.DateAndTime} ");
                    Console.WriteLine("*");
                }
            }

        }

        //////////////////////////////////////CREATIONS/////////////////////////////////////////////////////
        public void CreatePost(User author, string postText, bool isPublic)
        {
            var post = new Post
            {
                Author = author,
                PostText = postText,
                Created = DateTime.Now,
                IsPublic = isPublic,
                Comments = new List<Comment>()
            };

            postService.Create(post);
        }

        public void CreateComment(User author, Post post)
        {

            Console.Write("Enter Content of Comment ");
            string content = Console.ReadLine();

        var comment = new Comment
            {
                Author = author,
                Content = content,
                DateAndTime = DateTime.Now
            };

            post.Comments.Add(comment);
            postService.Update(post.Id, post);
        }
    }
}

