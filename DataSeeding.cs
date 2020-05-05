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
                    Gender = "Mand",
                    Age = 49,
                    Email = "JodleBirge@Toppen.dk",
                    MobileNum = "12345678",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
                },

                new User
                {
                    Name = "Ib Grønbech",
                    Gender = "Mand",
                    Age = 42,
                    Email = "Ibberen@NyTrailer.dk",
                    MobileNum = "87654321",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
                },

                new User
                {
                    Name = "Toke",
                    Gender = "Mand",
                    Age = 32,
                    Email = "Trælstype@Ditmer.dk",
                    MobileNum = "80000085",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
                },

                new User
                {
                    Name = "Finn Nørbygaard",
                    Gender = "Mand",
                    Age = 55,
                    Email = "Ruineret@Krisetid.dk",
                    MobileNum = "00000000",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
                },

                new User
                {
                    Name = "GrauballeManden",
                    Gender = "Mand",
                    Age = 740,
                    Email = "SikkeEnDag@Mosemail.dk",
                    MobileNum = "22445543",
                    Circles = new List<Circle>(),
                    BlockedUsers = new List<User>(),
                    FollowedUsers = new List<User>()
                },

                new User
                {
                    Name = "Marianne-Birgitte",
                    Gender = "Kvinde",
                    Age = 32,
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
                    Name = "UFO-Kaptajnerne"
                }
            };

            var circleuser1 = _userService.GetByName("Jodle Birge");
            circleuser1.Circles.Add(_circles[0]);
            _userService.Update("Jodle Birge", circleuser1);

            var circleuser2 = _userService.GetByName("Ib Grønbech");
            circleuser2.Circles.Add(_circles[0]);
            circleuser2.Circles.Add(_circles[1]);
            circleuser2.Circles.Add(_circles[2]);
            _userService.Update("Ib Grønbech", circleuser2);

            var circleuser3 = _userService.GetByName("Toke");
            circleuser3.Circles.Add(_circles[3]);
            _userService.Update("Toke", circleuser3);

            var circleuser4 = _userService.GetByName("Finn Nørbygaard");
            circleuser4.Circles.Add(_circles[0]);
            circleuser4.Circles.Add(_circles[2]);
            _userService.Update("Finn Nørbygaard", circleuser4);

            var circleuser5 = _userService.GetByName("GrauballeManden");
            circleuser5.Circles.Add(_circles[3]);
            _userService.Update("GrauballeManden", circleuser5);

            var circleuser6 = _userService.GetByName("Marianne-Birgitte");
            circleuser6.Circles.Add(_circles[1]);
            circleuser6.Circles.Add(_circles[3]);
            _userService.Update("Marianne-Birgitte", circleuser6);
        }

        //-----------------------FollowedUser seeding------------------------//
        public void FollowedUserSeed()
        {
            var followuser1 = _userService.GetByName("Jodle Birge");
            followuser1.FollowedUsers = new List<User>()
            {
                _userService.GetByName("Ib Grønbech")
            };

            _userService.Update("JodleBirge", followuser1);

            var followuser2 = _userService.GetByName("Ib Grønbech");
            followuser2.FollowedUsers = new List<User>()
            {
                _userService.GetByName("Jodle Birge")
            };

            _userService.Update("Ib Grønbech", followuser2);


            var followuser3 = _userService.GetByName("Finn Nørbygaard");
            followuser3.FollowedUsers = new List<User>()
            {
                _userService.GetByName("Jodle Birge")
            };

            _userService.Update("Finn Nørbygaard", followuser3);

            var followuser4 = _userService.GetByName("Marianne-Birgitte");
            followuser1.FollowedUsers = new List<User>()
            {
                _userService.GetByName("GrauballeManden")
            };

            _userService.Update("Marianne-Birgitte", followuser4);
        }

        //-----------------------BlockedUser seeding------------------------//

        public void BlockedUserSeed()
        {
            var blockuser1 = _userService.GetByName("Jodle Birge");
            blockuser1.BlockedUsers = new List<User>()
            {
                _userService.GetByName("´Toke")
            };

            _userService.Update("Jodle Birge", blockuser1);

            var blockuser2 = _userService.GetByName("GrauballeManden");
            blockuser2.BlockedUsers = new List<User>()
            {
                _userService.GetByName("Finn Nørbygaard")
            };

            _userService.Update("GrauballeManden", blockuser2);
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
                    ShownCircles = new List<Circle> {_circles[0]},
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
                    ShownCircles = new List<Circle> {_circles[0]},
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
                    PostFeeling = Feeling.Jævnt_Utilfreds,
                    ShownCircles = new List<Circle> {_circles[3]},
                    Created = DateTime.Now,
                },

                new Post()
                {
                    Author = _users[3],
                    IsPublic = false,
                    PostType = PostType.Text,
                    PostText = "Vi på vejen igen",
                    ShownCircles = new List<Circle> {_circles[2]},
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
                    ShownCircles = new List<Circle> {_circles[3]},
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
                    ShownCircles = new List<Circle> {_circles[1]},
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
                _postService.Create(post_);
            }
        }
    }
}