using System;
using System.Collections;
using Dab_SocialNetwork.Models;
using Dab_SocialNetwork.Services;

namespace Dab_SocialNetwork
{
    public class SocialNetworkConsoleView
    {
        private UserService userService;
        private Queries queries;
        
        public SocialNetworkConsoleView()
        {
            userService = new UserService();
            queries = new Queries();
        }

        public void LaunchSocialNetwork()
        {
            
            User loggedInAs = userService.GetByName("Jodle Birge");
            Console.WriteLine("Social Network launched");
            
            while (true)
            {
                System.Console.WriteLine("You're logged in as Jodle Birge. What would you like to do?");
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
            
        }

        private void ShowFriendWall(User loggedInAs)
        {
            
        }

        private void ShowOwnWall(User loggedInAs)
        {
            
        }
        
        private void CreatePost(User loggedInAs)
        {
            
        }
        
        private void CreateComment(User loggedInAs)
        {
            
        }
    }
}