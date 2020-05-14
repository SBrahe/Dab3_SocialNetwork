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
            var feeling=Feeling.Jævnt_Utilfreds;
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

            Console.WriteLine("Write text (t) or set a feeling (f)");
            var choice = Console.ReadLine();
            if (choice == "t")
            {
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
                queries.CreatePost(loggedInAs, content, isPublic);
            }
            else if (choice == "f")
            {
                Console.WriteLine("Set a feeling: \n 1: Festlig \n 2: Gammel \n 3: Glad \n 4: Jævnt utilfreds \n 5: Ked af Det \n 6: Sur");
                var id =Console.ReadLine();
                switch (id)
                {
                    case "1":
                        Console.WriteLine("");
                        feeling = Feeling.Festlig;
                        break;
                    case "2":
                        System.Console.WriteLine("");
                        feeling = Feeling.Gammel;
                        break;
                    case "3":
                        System.Console.WriteLine("");
                        feeling = Feeling.Glad;
                        break;
                    case "4":
                        System.Console.WriteLine("");
                        feeling = Feeling.Jævnt_Utilfreds;
                        break;
                    case "5":
                        System.Console.WriteLine("");
                        feeling = Feeling.Ked_af_det;
                        break;
                    case "6":
                        System.Console.WriteLine("");
                        feeling = Feeling.Sur;
                        break;
                }

                
                Post post = new Post
                {
                    Author = loggedInAs,
                    PostFeeling = feeling,
                    IsPublic = isPublic,
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                };
                queries.CreatePost(loggedInAs,feeling.ToString(), isPublic);
            }
            else
            {
                Console.WriteLine("invalid input");
                CreatePost(loggedInAs);
            }
            
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
            Console.WriteLine("Enter valid User to follow: ");
            var usertoFollow = Console.ReadLine();

            try
            {
                userService.GetUserByName(usertoFollow).FollowedUsers.Add(usertoFollow);

            }
            catch
            {
                Console.WriteLine("invalid user");
                FollowUser(loggedInAs);
            }
        }

        private void BlockUser(User loggedInAs)
        {
            Console.WriteLine("Enter valid User to block: ");
            var usertoBlock = Console.ReadLine();

            try
            {
                userService.GetUserByName(usertoBlock).BlockedUsers.Add(usertoBlock);
                
            }
            catch
            {
               Console.WriteLine("invalid user");
               BlockUser(loggedInAs);
            }
            
        }
    }
}