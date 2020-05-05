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
            List<Circle> circlesFollowedBySubject = userService.GetCirclesByUser(Subject);
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

            Console.WriteLine("FEED");
            for (var x = 0; x < postsInSubjectFeed.Count; x++)
            {
                Console.WriteLine($"*******");
                Console.WriteLine($"{postsInSubjectFeed[x].PostText}\n");
                Console.WriteLine($"{postsInSubjectFeed[x].Author.Name}\n");
                Console.WriteLine($"Date of post: {postsInSubjectFeed[x].Created}\n");

                Console.WriteLine($"Comments:");
                List<Comment> commentsOnPost = postService.GetComments(postsInSubjectFeed[x]);
                foreach (var y in postsInSubjectFeed[x].Comment)
                {
                    Console.WriteLine($"Comment: {y.Content}\n");
                    Console.WriteLine($"Author: {y.Author.Name} ");
                    Console.WriteLine($"Date of comment: {y.DateAndTime} ");
                }
            }
        }

        public void ShowWallForFriend(User wallOwner, User viewer)
        {
            List<Post> postsOnSubjectWall = new List<Post>();
            List<Post> postsThatViewingFriendHasAccessTo = new List<Post>();

            postsOnSubjectWall = postService.GetByAuthor(wallOwner);
            foreach (var x in postsOnSubjectWall)
            {
                var viewingFriendHasAccess = false;
                List<Circle> postCircles = x.ShownCircles;
                foreach (var y in postCircles)
                {
                    if (y.Members.Contains(userService.GetByName(viewer.Name)))
                    {
                        viewingFriendHasAccess = true;
                    }

                    if (x.IsPublic == true)
                    {
                        viewingFriendHasAccess = true;
                    }

                    if ((!postsThatViewingFriendHasAccessTo.Contains(x)) & (viewingFriendHasAccess == true))
                    {
                        postsThatViewingFriendHasAccessTo.Add(x);
                    }
                }
            }

            Console.WriteLine("WALL");
            for (var x = 0; x < postsThatViewingFriendHasAccessTo.Count; x++)
            {
                Console.WriteLine($"*******");
                Console.WriteLine($"{postsThatViewingFriendHasAccessTo[x].PostText}\n");
                Console.WriteLine($"{postsThatViewingFriendHasAccessTo[x].Author.Name}\n");
                Console.WriteLine($"Date of post: {postsThatViewingFriendHasAccessTo[x].Created}\n");
                List<Comment> commentsOnPost = postService.GetComments(postsThatViewingFriendHasAccessTo[x]);
                if (commentsOnPost.Any())
                {
                    Console.WriteLine($"Comments:");
                    foreach (var y in postsThatViewingFriendHasAccessTo[x].Comment)
                    {
                        Console.WriteLine($"Comment: {y.Content}\n");
                        Console.WriteLine($"Author: {y.Author.Name} ");
                        Console.WriteLine($"Date of comment: {y.DateAndTime} ");
                    }
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
    }
}