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
                Console.WriteLine($"Post #{x+1}");
                Console.WriteLine($"'{postsInSubjectFeed[x].PostText}'");
                Console.WriteLine($"Author: {postsInSubjectFeed[x].Author.Name}");
                Console.WriteLine($"Date of post: {postsInSubjectFeed[x].Created}");

                Console.WriteLine($"Comments:");
                List<Comment> commentsOnPost = postService.GetComments(postsInSubjectFeed[x]);
                foreach (var y in postsInSubjectFeed[x].Comment)
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

            postsOnWall = postService.GetByAuthor(wallOwner);
            foreach (var x in postsOnWall)
            {
                var viewingFriendHasAccess = false;
                List<Circle> postCircles = x.ShownCircles;
                foreach (var y in postCircles)
                {
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
                Console.WriteLine($"Post #{x+1}");
                Console.WriteLine($"'{postsThatViewerHasAccessTo[x].PostText}'");
                Console.WriteLine($"Author: {postsThatViewerHasAccessTo[x].Author.Name}");
                Console.WriteLine($"Date of post: {postsThatViewerHasAccessTo[x].Created}");

                Console.WriteLine($"Comments:");
                List<Comment> commentsOnPost = postService.GetComments(postsThatViewerHasAccessTo[x]);
                foreach (var y in postsThatViewerHasAccessTo[x].Comment)
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
            List<Post> postsOnSubjectWall = new List<Post>();

            postsOnSubjectWall = postService.GetByAuthor(wallOwner);
            foreach (var x in postsOnSubjectWall)
            {
               
            }

            Console.WriteLine("WALL");
            for (var x = 0; x < postsOnSubjectWall.Count; x++)
            {
                Console.WriteLine($"*******");
                Console.WriteLine($"{postsOnSubjectWall[x].PostText}\n");
                Console.WriteLine($"{postsOnSubjectWall[x].Author.Name}\n");
                Console.WriteLine($"Date of post: {postsOnSubjectWall[x].Created}\n");
                List<Comment> commentsOnPost = postService.GetComments(postsOnSubjectWall[x]);
                if (commentsOnPost.Any())
                {
                    Console.WriteLine($"Comments:");
                    foreach (var y in postsOnSubjectWall[x].Comment)
                    {
                        Console.WriteLine($"Comment: {y.Content}\n");
                        Console.WriteLine($"Author: {y.Author.Name} ");
                        Console.WriteLine($"Date of comment: {y.DateAndTime} ");
                    }
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
                Comment = new List<Comment>()
            };

            postService.Create(post);
        }

        public void CreateComment(User author, Post post, string content)
        {
            var comment = new Comment
            {
                Author = author,
                Content = content,
                DateAndTime = DateTime.Now
            };

            post.Comment.Add(comment);
            postService.Update(post.Id, post);
        }
    }
}

