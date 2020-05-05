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
            dataSeeder.SeedDatabase();
            
            SocialNetworkConsoleView socialnetwork = new SocialNetworkConsoleView();
            socialnetwork.LaunchSocialNetwork();
        }
        
    }
}
