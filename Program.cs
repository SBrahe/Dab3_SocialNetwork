using System;
using System.Runtime.CompilerServices;
using Dab_SocialNetwork.Services;

namespace Dab_SocialNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSeeding dataSeeder = new DataSeeding();
            dataSeeder.EmptyDatabase();
            dataSeeder.UserSeed();
            dataSeeder.FollowedUserSeed();
            dataSeeder.BlockedUserSeed();
            dataSeeder.CircleSeed();
            dataSeeder.PostSeed();
            dataSeeder.CommentSeed();
            
            SocialNetworkConsoleView socialnetwork = new SocialNetworkConsoleView();
            socialnetwork.LaunchSocialNetwork();
        }
        
    }
}
