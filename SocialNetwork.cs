using System;
using System.Collections;

namespace Dab_SocialNetwork
{
    public class SocialNetworkFunctions
    {
        public void LaunchSocialNetwork()
        {
            Console.WriteLine("Social Network launched");
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            while (true)
            {
                System.Console.WriteLine("You're logged in as XXXXX. What would you like to do?");
                System.Console.WriteLine("1: Show my feed");
                System.Console.WriteLine("2: Show a friend's wall");
                System.Console.WriteLine("3: Show own wall");
                System.Console.WriteLine("4: Create post");
                System.Console.WriteLine("5: Create comment");

                consoleKeyInfo = Console.ReadKey();
                switch (consoleKeyInfo.KeyChar)
                {
                    case '1':
                        Console.WriteLine("");
                        ShowMyFeed();
                        break;
                    case '2':
                        System.Console.WriteLine("");
                        ShowFriendWall();
                        break;
                    case '3':
                        System.Console.WriteLine("");
                        ShowOwnWall();
                        break;
                    case '4':
                        System.Console.WriteLine("");
                        CreatePost();
                        break;
                    case '5':
                        System.Console.WriteLine("");
                        CreateComment();
                        break;
                    case '6':
                        System.Console.WriteLine("");
                        CreateComment();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ShowMyFeed()
        {
            
        }

        private void ShowFriendWall()
        {
            
        }

        private void ShowOwnWall()
        {
            
        }
        
        private void CreatePost()
        {
            
        }
        
        private void CreateComment()
        {
            
        }
    }
}