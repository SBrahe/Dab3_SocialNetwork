using System;
using System.Collections;

namespace Dab_SocialNetwork
{
    public class SocialNetworkFeeling
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
    }
}