using System;
using System.Collections;
using System.Collections.Generic;
using Dab_SocialNetwork.Models;
using Dab_SocialNetwork.Services;

namespace Dab_SocialNetwork
{
    public class SocialNetworkConsoleView
    {
        private UserService userService;
        private PostService postService;
        private Queries queries;
        
        public SocialNetworkConsoleView()
        {
            userService = new UserService();
            postService = new PostService();
            queries = new Queries();
        }

        public void LaunchSocialNetwork()
        {
            User loggedInAs = userService.GetByName("Jodle Birge");
            Console.WriteLine("Social Network launched");
            
            while (true)
            {
                System.Console.WriteLine($"You're logged in as {loggedInAs.Name}. What would you like to do?");
                System.Console.WriteLine("1: Show my feed");
                System.Console.WriteLine("2: Show a friend's wall");
                System.Console.WriteLine("3: Show own wall");
                System.Console.WriteLine("4: Create post");
                System.Console.WriteLine("5: Create comment");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                switch (consoleKeyInfo.KeyChar)
                {
                    case '1':
                        Console.WriteLine("");
                        ShowMyFeed(loggedInAs);
                        break;
                    case '2':
                        System.Console.WriteLine("");
                        ShowFriendWall(loggedInAs);
                        break;
                    case '3':
                        System.Console.WriteLine("");
                        ShowOwnWall(loggedInAs);
                        break;
                    case '4':
                        System.Console.WriteLine("");
                        CreatePost(loggedInAs);
                        break;
                    case '5':
                        System.Console.WriteLine("");
                        CreateComment(loggedInAs);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ShowMyFeed(User loggedInAs)
        {
            queries.ShowFeedForUser(loggedInAs);
        }

        private void ShowFriendWall(User loggedInAs)
        {
            System.Console.WriteLine("Whose wall would you like to see?");
            var userWhoseWallToShowString = Console.ReadLine();
            
            User userWhoseWallToShow = userService.GetByName(userWhoseWallToShowString);
            queries.ShowWallForFriend(userWhoseWallToShow,loggedInAs);
        }

        private void ShowOwnWall(User loggedInAs)
        {
            queries.ShowOwnWall(loggedInAs);
        }
        
        private void CreatePost(User loggedInAs)
        {
            var isPublic=true;
            Console.WriteLine("Public Post? (y/n)");
            var publicprivate = Console.ReadLine();
            if (publicprivate == "y")
            {
                isPublic = true;
            }
            else if (publicprivate == "n")
            {
                isPublic = false;
            }
            
            Console.WriteLine("Enter Content of Post");
            var content = Console.ReadLine();

            Console.WriteLine();
            Post post = new Post
            {
                Author = loggedInAs,
                PostText = content,
                IsPublic = isPublic,
                Created = DateTime.Now,
                Comments = new List<Comment>()
            };
            queries.CreatePost(loggedInAs,content,isPublic);
        }
        
        private void CreateComment(User loggedInAs)
        {
            Console.Write("Input id of post you want to comment");
            string id = Console.ReadLine();

            Console.Write("Enter Content of Comment ");
            string content = Console.ReadLine();

            Comment comment = new Comment
            {
                Author = loggedInAs,
                Content = content,
                DateAndTime = DateTime.Now
            };

            Post post = postService.GetById(id);
            post.Comments.Add(comment);
            postService.Update(post.Id, post);
        }
    }
}