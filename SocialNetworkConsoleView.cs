using System;
using System.Collections;
using System.Collections.Generic;
using Dab_SocialNetwork.Models;
using Dab_SocialNetwork.Services;

//jodle birge har blokeret toke
//jodle birge er med i cirklen "Det musik for mig"
//jodle birge følger Ib Grønbech

namespace Dab_SocialNetwork
{
    public class SocialNetworkConsoleView
    {
        private UserService userService;
        private PostService postService;
        private Queries queries;
        private User _loggedInAs;

        public SocialNetworkConsoleView()
        {
            userService = new UserService();
            postService = new PostService();
            queries = new Queries();
        }

        public void LaunchSocialNetwork()
        {
            _loggedInAs = userService.GetUserByName("Jodle Birge");
            Console.WriteLine("Social Network launched");

            while (true)
            {
                Console.WriteLine($"You're logged in as {_loggedInAs.Name}. What would you like to do?");
                Console.WriteLine("1: Show my feed");
                Console.WriteLine("2: Show a friend's wall");
                Console.WriteLine("3: Show own wall");
                Console.WriteLine("4: Create post");
                Console.WriteLine("5: Create comment");
                Console.WriteLine("6: Follow user");
                Console.WriteLine("7: Block user");
                Console.WriteLine("8: Log out");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                switch (consoleKeyInfo.KeyChar)
                {
                    case '1':
                        Console.WriteLine("");
                        ShowMyFeed(_loggedInAs);
                        break;
                    case '2':
                        System.Console.WriteLine("");
                        ShowFriendWall(_loggedInAs);
                        break;
                    case '3':
                        System.Console.WriteLine("");
                        ShowOwnWall(_loggedInAs);
                        break;
                    case '4':
                        System.Console.WriteLine("");
                        CreatePost(_loggedInAs);
                        break;
                    case '5':
                        System.Console.WriteLine("");
                        CreateComment(_loggedInAs);
                        break;
                    case '6':
                        System.Console.WriteLine("");
                        FollowUser(_loggedInAs);
                        break;
                    case '7':
                        System.Console.WriteLine("");
                        BlockUser(_loggedInAs);
                        break;
                    case '8':
                        System.Console.WriteLine("");
                        Logout();
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
                User userWhoseWallToShow = userService.GetUserByName(userWhoseWallToShowString);
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
            var feeling = Feeling.Jaevnt_Utilfreds;
            var isPublic = true;
            Console.WriteLine("Public Post? (y/n)");
            ConsoleKeyInfo publicprivate = Console.ReadKey();
            if (publicprivate.KeyChar == 'y')
            {
                isPublic = true;
                Console.WriteLine("");
            }
            else if (publicprivate.KeyChar == 'n')
            {
                isPublic = false;
                Console.WriteLine("");
            }

            Console.WriteLine("Write text (t) or set a feeling (f)");
            ConsoleKeyInfo postTypeChoice = Console.ReadKey();
            if (postTypeChoice.KeyChar == 't')
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
            else if (postTypeChoice.KeyChar == 'f')
            {
                Console.WriteLine("");
                Console.WriteLine(
                    "Select a feeling: \n 1: Festlig \n 2: Gammel \n 3: Glad \n 4: Jaevnt utilfreds \n 5: Ked af Det \n 6: Sur");
                ConsoleKeyInfo feelingChoice = Console.ReadKey();
                switch (feelingChoice.KeyChar)
                {
                    case '1':
                        Console.WriteLine("");
                        feeling = Feeling.Festlig;
                        Console.WriteLine("Post created.");
                        break;
                    case '2':
                        System.Console.WriteLine("");
                        feeling = Feeling.Gammel;
                        Console.WriteLine("Post created.");
                        break;
                    case '3':
                        System.Console.WriteLine("");
                        feeling = Feeling.Glad;
                        Console.WriteLine("Post created.");
                        break;
                    case '4':
                        System.Console.WriteLine("");
                        feeling = Feeling.Jaevnt_Utilfreds;
                        Console.WriteLine("Post created.");
                        break;
                    case '5':
                        System.Console.WriteLine("");
                        feeling = Feeling.Ked_af_det;
                        Console.WriteLine("Post created.");
                        break;
                    case '6':
                        System.Console.WriteLine("");
                        feeling = Feeling.Sur;
                        Console.WriteLine("Post created.");
                        break;
                    default:
                        System.Console.WriteLine("Invalid input. Try again.");
                        CreatePost(_loggedInAs);
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
                queries.CreatePost(loggedInAs, feeling.ToString(), isPublic);
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
            List<Post> posts = postService.GetAllPosts();
            for (int x = 1; x <= 5; x++)
            {
                Console.WriteLine(x + ": By " + posts[x].Author.Name + ". Time: " + posts[x].Created);
            }

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            char consolekeypressed = consoleKeyInfo.KeyChar;
            double key = char.GetNumericValue(consolekeypressed);
            int keyasint = Convert.ToInt32(key);
            var post = posts[keyasint];
            Console.WriteLine("");
            queries.CreateComment(loggedInAs, post);
        }

        private void FollowUser(User loggedInAs)
        {
            Console.WriteLine("Enter User to follow: ");
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
            Console.WriteLine("Enter User to block: ");
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

        private void Logout()
        {
            Console.WriteLine("You are logged out. Log in (l) or quit (q) ");
            var action = Console.ReadKey();
            switch (action.KeyChar)
            {
                case 'l':
                    Console.WriteLine("");
                    Console.WriteLine("Enter User to log in as: ");
                    _loggedInAs = userService.GetUserByName(Console.ReadLine());
                    break;
                case 'q':
                    System.Environment.Exit(1);
                    break;
                default:
                    System.Environment.Exit(1);
                    break;
            }
        }
    }
}