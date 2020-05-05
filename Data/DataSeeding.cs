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
        private List<Circle> _circles;
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
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
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
                    FollowedUsers = new List<User>()
                },

                new User
                {
                    Name = "Toke",
                    Gender="Mand",
                    Age= 32,
                    Email = "Trælstype@Ditmer.dk",
                    MobileNum = "80000085",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
                },

                new User
                {
                    Name = "Finn Nørbygaard",
                    Gender="Mand",
                    Age= 55,
                    Email = "Ruineret@Krisetid.dk",
                    MobileNum = "00000000",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
                },

                new User
                {
                    Name = "GrauballeManden",
                    Gender="Mand",
                    Age= 740,
                    Email = "SikkeEnDag@Mosemail.dk",
                    MobileNum = "22445543",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
                },

                new User
                {
                    Name = "Marianne-Birgitte",
                    Gender="Kvinde",
                    Age= 32,
                    Email = "Marianne-Birgitte@Marianne-Birgitte.dk",
                    MobileNum = "96700012",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
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
            _circles = new List<Circle>()
            {
                new Circle
                {
                    Name = "Det musik for mig",
                    Members = new List<User>()
                },
                new Circle
                {
                    Name = "Der var engang",
                    Members = new List<User>()
                },
                new Circle
                {
                    Name = "Trailerstuen",
                    Members = new List<User>()
                },

                new Circle
                {
                    Name="UFO-Kaptajnerne"
                }
            };

            var circleuser1 = _userService.GetByName("Jodle Birge");
            circleuser1.Circles.Add(_circles[0]);
            _userService.Update("Morten", circleuser1);

            var circleuser2 = _userService.GetByName("Ib Grønbech");
            circleuser2.Circles.Add(_circles[0]);
            circleuser2.Circles.Add(_circles[1]);
            circleuser2.Circles.Add(_circles[2]);

            _userService.Update("Morten", circleuser2);

            var circleuser3 = _userService.GetByName("Toke");
            circleuser3.Circles.Add(_circles[3]);
            _userService.Update("Morten", circleuser3);

            var circleuser4 = _userService.GetByName("Finn Nørbygaard");
            circleuser4.Circles.Add(_circles[0]);
            circleuser4.Circles.Add(_circles[2]);
            _userService.Update("Morten", circleuser4);

            var circleuser5 = _userService.GetByName("GrauballeManden");
            circleuser5.Circles.Add(_circles[3]);
            _userService.Update("Morten", circleuser5);

            var circleuser6 = _userService.GetByName("Marianne-Birgitte");
            circleuser6.Circles.Add(_circles[1]);
            circleuser6.Circles.Add(_circles[3]);
            _userService.Update("Morten", circleuser6);

        }

        //-----------------------FollowedUser seeding------------------------//
        public void FollowedUserSeed()
        {

        }

        //-----------------------BlockedUser seeding------------------------//

        public void BlockedUserSeed()
        {

        }

        //----------------------Post seeding------------------------//
        public void PostSeed()
        {

        }

        //-----------------------Comment seeding------------------------//

        public void CommentSeed()
        {

        }




    }
}
