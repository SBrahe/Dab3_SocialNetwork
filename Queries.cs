using System;
using System.Collections.Generic;
using Dab_SocialNetwork.Models;
using Dab_SocialNetwork.Services;

namespace Dab_SocialNetwork
{
    class Queries
    {
        private UserService userService = new UserService();
        private PostService postService = new PostService();

        void ShowFeedForUser(User Subject)
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
                Console.WriteLine($"{postsInSubjectFeed[x].Content}\n");
                Console.WriteLine($"Date of post:  {postsInSubjectFeed[x].Created}\n");

                Console.WriteLine($"Comments:");
                List<Comment> commentsOnPost = postService.GetComments(postsInSubjectFeed[x]);
                foreach (var y in postsInSubjectFeed[x].Comment)
                {
                    Console.WriteLine($"  Comment: {y.Content}\n");
                    Console.WriteLine($"  Date:    {y.DateAndTime} ");
                    Console.WriteLine($"  Author:  {y.Author.Name} ");
                }
            }

        }
    }
}