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
            _userService = new UserService();
            _postService = new PostService();
        }

        //-----------------------Empty Database------------------------//
        public void EmptyDatabase()
        {
            _userService.DeleteAllUsers();
            _postService.DeleteAllPosts();
        }

        //-----------------------User seeding------------------------//
        public void UserSeed()
        {
            _users = new List<User>()
            {
                new User
                {
                    Name = "Jodle Birge",
                    Gender = "Mand",
                    Age = 49,
                    Email = "JodleBirge@Toppen.dk",
                    MobileNum = "12345678",
                    Circles = new List<int>(),
                    BlockedUsers = new List<String>(),
                    FollowedUsers = new List<String>()
                },

                new User
                {
                    Name = "Ib Grønbech",
                    Gender = "Mand",
                    Age = 42,
                    Email = "Ibberen@NyTrailer.dk",
                    MobileNum = "87654321",
                    Circles = new List<int>(),
                    BlockedUsers = new List<String>(),
                    FollowedUsers = new List<String>()
                },

                new User
                {
                    Name = "Toke",
                    Gender = "Mand",
                    Age = 32,
                    Email = "Trælstype@Ditmer.dk",
                    MobileNum = "80000085",
                    Circles = new List<int>(),
                    BlockedUsers = new List<String>(),
                    FollowedUsers = new List<String>()
                },

                new User
                {
                    Name = "Finn Nørbygaard",
                    Gender = "Mand",
                    Age = 55,
                    Email = "Ruineret@Krisetid.dk",
                    MobileNum = "00000000",
                    Circles = new List<int>(),
                    BlockedUsers = new List<String>(),
                    FollowedUsers = new List<String>()
                },

                new User
                {
                    Name = "GrauballeManden",
                    Gender = "Mand",
                    Age = 740,
                    Email = "SikkeEnDag@Mosemail.dk",
                    MobileNum = "22445543",
                    Circles = new List<int>(),
                    BlockedUsers = new List<String>(),
                    FollowedUsers = new List<String>()
                },

                new User
                {
                    Name = "Marianne-Birgitte",
                    Gender = "Kvinde",
                    Age = 32,
                    Email = "Marianne-Birgitte@Marianne-Birgitte.dk",
                    MobileNum = "96700012",
                    Circles = new List<int>(),
                    BlockedUsers = new List<String>(),
                    FollowedUsers = new List<String>()
                },
            };

            foreach (var user in _users)
            {
                _userService.CreateUser(user);
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
                    Members = new List<String>()
                },
                new Circle
                {
                    Name = "Der var engang",
                    Members = new List<String>()
                },
                new Circle
                {
                    Name = "Trailerstuen",
                    Members = new List<String>()
                },

                new Circle
                {
                    Name = "UFO-Kaptajnerne"
                }
            };

            var circleuser1 = _userService.GetUserByName("Jodle Birge");
            circleuser1.Circles.Add(_circles[0].Id);
            _userService.UpdateUser("Jodle Birge", circleuser1);

            var circleuser2 = _userService.GetUserByName("Ib Grønbech");
            circleuser2.Circles.Add(_circles[0].Id);
            circleuser2.Circles.Add(_circles[1].Id);
            circleuser2.Circles.Add(_circles[2].Id);
            _userService.UpdateUser("Ib Grønbech", circleuser2);

            var circleuser3 = _userService.GetUserByName("Toke");
            circleuser3.Circles.Add(_circles[3].Id);
            _userService.UpdateUser("Toke", circleuser3);

            var circleuser4 = _userService.GetUserByName("Finn Nørbygaard");
            circleuser4.Circles.Add(_circles[0].Id);
            circleuser4.Circles.Add(_circles[2].Id);
            _userService.UpdateUser("Finn Nørbygaard", circleuser4);

            var circleuser5 = _userService.GetUserByName("GrauballeManden");
            circleuser5.Circles.Add(_circles[3].Id);
            _userService.UpdateUser("GrauballeManden", circleuser5);

            var circleuser6 = _userService.GetUserByName("Marianne-Birgitte");
            circleuser6.Circles.Add(_circles[1].Id);
            circleuser6.Circles.Add(_circles[3].Id);
            _userService.UpdateUser("Marianne-Birgitte", circleuser6);
        }

        //-----------------------FollowedUser seeding------------------------//
        public void FollowedUserSeed()
        {
            var jodlebirge = _userService.GetUserByName("Jodle Birge");
            jodlebirge.FollowedUsers.Add("Ib Grønbech");
            
            var ibgroenbech = _userService.GetUserByName("Ib Grønbech");
            ibgroenbech.FollowedUsers.Add("Jodle Birge");
            
            var finnnoerbygaard = _userService.GetUserByName("Ib Grønbech");
            finnnoerbygaard.FollowedUsers.Add("Jodle Birge");
            
            var mariannebirgitte = _userService.GetUserByName("Marianne-Birgitte");
            mariannebirgitte.FollowedUsers.Add("GrauballeManden");
        }

        //-----------------------BlockedUser seeding------------------------//
        
        public void BlockedUserSeed()
        {
            var jodlebirge = _userService.GetUserByName("Jodle Birge");
            jodlebirge.BlockedUsers.Add("Toke");
            
            var grauballemanden = _userService.GetUserByName("GrauballeManden");
            grauballemanden.BlockedUsers.Add("Finn Nørbygaard");

        }

        //----------------------Post and comment seeding------------------------//
        public void PostSeed()
        {
            _posts = new List<Post>()
            {
                new Post()
                {
                    Author = _users[0],
                    IsPublic = false,
                    PostType = PostType.Text,
                    PostText = "Peter lå i telt",
                    ShownCircles = new List<int> {_circles[0].Id},
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Author = _users[1],
                            Content = "Og Ole lå i campingsvogn",
                            DateAndTime = DateTime.Now
                        },

                        new Comment()
                        {
                            Author = _users[0],
                            Content = "Ole lå iii Ole lå iii",
                            DateAndTime = DateTime.Now
                        }
                    }
                },

                new Post()
                {
                    Author = _users[1],
                    IsPublic = false,
                    PostType = PostType.Text,
                    PostText = "Jeg har fået trailer og mor har fået mave på",
                    ShownCircles = new List<int> {_circles[0].Id},
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Author = _users[2],
                            Content =
                                "Din post har ikke færdighederne jeg forventer, men du har en rigtig fin personlighed",
                            DateAndTime = DateTime.Now
                        },
                    }
                },

                new Post()
                {
                    Author = _users[2],
                    IsPublic = true,
                    PostType = PostType.Feeling,
                    PostFeeling = Feeling.Jaevnt_Utilfreds,
                    ShownCircles = new List<int> {_circles[3].Id},
                    Created = DateTime.Now,
                },

                new Post()
                {
                    Author = _users[3],
                    IsPublic = false,
                    PostType = PostType.Text,
                    PostText = "Vi på vejen igen",
                    ShownCircles = new List<int> {_circles[2].Id},
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Author = _users[1],
                            Content = "*Smiler*",
                            DateAndTime = DateTime.Now
                        },
                    }
                },

                new Post()
                {
                    Author = _users[4],
                    IsPublic = false,
                    PostType = PostType.Feeling,
                    PostFeeling = Feeling.Gammel,
                    ShownCircles = new List<int> {_circles[3].Id},
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Author = _users[5],
                            Content =
                                "Dette minder mig om 1986, da Tjernobyl eksploderede, og myndighederne rådede os om," +
                                " at frugten på vores træer måske har fået radioaktiv forgiftning og ikke at spise dem det år. " +
                                "Mange var vrede over, at nogen skulle fortelle dem, at de ikke kunne spise deres egen frugt. Det er den samme kortsigtighed vi ser i år.",
                            DateAndTime = DateTime.Now
                        },
                    }
                },

                new Post()
                {
                    Author = _users[5],
                    IsPublic = true,
                    PostType = PostType.Text,
                    PostText =
                        "Jeg synes vi som samfund bør fordømme enhver kultur/samfund, hvor det praktiseres at voksne legalt kan gifte sig med børn.",
                    ShownCircles = new List<int> {_circles[1].Id},
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Author = _users[1],
                            Content = "Sikke en dag",
                            DateAndTime = DateTime.Now
                        },
                    }
                }
            };

            foreach (var post_ in _posts)
            {
                _postService.CreatePost(post_);
            }
        }
    }
}