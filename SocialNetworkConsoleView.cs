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
                Console.WriteLine($"You're logged in as {loggedInAs.Name}. What would you like to do?");
                Console.WriteLine("1: Show my feed");
                Console.WriteLine("2: Show a friend's wall");
                Console.WriteLine("3: Show own wall");
                Console.WriteLine("4: Create post");
                Console.WriteLine("5: Create comment");
                Console.WriteLine("6: Follow user");
                Console.WriteLine("7: Block user");

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
                    case '6':
                        System.Console.WriteLine("");
                        FollowUser(loggedInAs);
                        break;
                    case '7':
                        System.Console.WriteLine("");
                        BlockUser(loggedInAs);
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

            try
            {
                User userWhoseWallToShow = userService.GetByName(userWhoseWallToShowString);
                queries.ShowWallForFriend(userWhoseWallToShow, loggedInAs);
            }
            catch
            {
                Console.WriteLine("Invalid name");
                ShowOwnWall(loggedInAs);
            }
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
            Console.WriteLine("These are the last 5 posts. Which one do you want to comment on?");
            List<Post> posts = postService.Get();
            for (int x = 1; x <= 5; x++)
            {
                Console.WriteLine(x+": By "+posts[x].Author.Name+". Time: "+posts[x].Created);
            }
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            char consolekeypressed = consoleKeyInfo.KeyChar;
            double key = char.GetNumericValue(consolekeypressed);
            int keyasint = Convert.ToInt32(key);
            var post = posts[keyasint];

            queries.CreateComment(loggedInAs,post);
        }

        private void FollowUser(User loggedInAs)
        {

        }

        private void BlockUser(User loggedInAs)
        {

        }
    }
}