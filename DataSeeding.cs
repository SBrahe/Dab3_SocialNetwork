using System;
using System.Collections.Generic;
using System.Text;
using Dab_SocialNetwork.Models;
using Dab_SocialNetwork.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dab_SocialNetwork
{
    class DataSeeding
    {
        private List<User> _users;
        private List<Post> _posts;
        private readonly UserService _userService;
        private readonly PostService _postService;


        public DataSeeding()
        {
            _userService=new UserService();
            _postService=new PostService();
        }

        //-----------------------Empty Database------------------------//
        public void SeedDatabase()
        {
            
            _userService.Empty();
            _postService.Empty();

        }

        //-----------------------User seeding------------------------//
        public void UserSeed()
        {
            _users = new List<User>()
            {
                new User
                {
                    Name = "Jodle Birge",
                    Gender="Mand",
                    Age= 49,
                    Email = "JodleBirge@Toppen.dk",
                    MobileNum = "12345678",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>()
                },

                new User
                {
                    Name = "Ib Grønbech",
                    Gender="Mand",
                    Age= 42,
                    Email = "Ibberen@NyTrailer.dk",
                    MobileNum = "87654321",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>()
                },

                new User
                {
                    Name = "Toke",
                    Gender="Mand",
                    Age= 32,
                    Email = "Trælstype@Ditmer.dk",
                    MobileNum = "80000085",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>()
                },

                new User
                {
                    Name = "Finn Nørbygaard",
                    Gender="Mand",
                    Age= 55,
                    Email = "Ruineret@Krisetid.dk",
                    MobileNum = "00000000",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>()
                },

                new User
                {
                    Name = "GrauballeManden",
                    Gender="Mand",
                    Age= 740,
                    Email = "SikkeEnDag@Mosemail.dk",
                    MobileNum = "22445543",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>()
                },

                new User
                {
                    Name = "Klara-Birgitte",
                    Gender="Kvinde",
                    Age= 32,
                    Email = "Klara-Birgitte@Klara-Birgitte.dk",
                    MobileNum = "96700012",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>()
                },
            };

            foreach (var user in _users)
            {
                _userService.Create(user);
            }

        }

        //-----------------------Circle seeding------------------------//

        public void CircleSeed()
        {
            
            
        }

        public void BlockedUserSeed()
        {

        }

        public void Post()
        {

        }





        
    }
}
