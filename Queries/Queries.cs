using System;
using System.Collections.Generic;
using System.Linq;
using Dab_SocialNetwork.Models;
using Dab_SocialNetwork.Services;

namespace Dab_SocialNetwork
{
    class Queries
    {
        private readonly UserService _userService = new UserService();
        private readonly PostService _postService = new PostService();
        private readonly CircleService _circleService = new CircleService();

        public void ShowFeedForUser(User Subject)
        {
            List<Post> postsInSubjectFeed = new List<Post>();
            List<User> usersFollowedBySubject  = new List<User>();
            List<User> usersBlockedBySubject  = new List<User>();
            
            //***MAKE LISTS***
            //make list of users followed by subject
            foreach (var userId in Subject.FollowedUsers)
            {
                User userToAdd = _userService.GetUserById(userId);
                usersFollowedBySubject.Add(userToAdd);
            }
            
            //make list of users blocked by subject
            foreach (var userId in Subject.BlockedUsers)
            {
                User userToAdd = _userService.GetUserById(userId);
                usersBlockedBySubject.Add(userToAdd);
            }
            
            //***PULL POSTS***
            //pull posts from circles followed by subject
            foreach (var circleId in Subject.Circles)
            {
                List<Post> postsInCircle = _postService.GetPostsByCircleId(circleId);
                postsInSubjectFeed.AddRange(postsInCircle);
            }
            
            //pull posts from users followed by subject
            foreach (var x in usersFollowedBySubject)
            {
                List<Post> postsFromFollowedUser = _postService.GetPostByAuthor(x);
                postsInSubjectFeed.AddRange(postsFromFollowedUser);
            }
            
            //remove posts from blocked users
            foreach (var x in usersBlockedBySubject)
            {
                List<Post> postsFromBlockedUser = _postService.GetPostByAuthor(x);
                var result = postsInSubjectFeed.Where(x => postsFromBlockedUser.All(y => x.Id != y.Id));
                postsInSubjectFeed = result.ToList(); 
            }
            
            //sort posts
            postsInSubjectFeed = postsInSubjectFeed.OrderBy(p=>p.Created).ToList();

            //create wall in console
            Console.WriteLine($"-------------{Subject.Name}'s Feed-------------");
            for (var x = 0; x < postsInSubjectFeed.Count; x++)
            {
                //separate post from others
                Console.WriteLine("****");
                
                //output post number and post type
                Console.WriteLine($"Post #{x + 1}, content type: {postsInSubjectFeed[x].PostType}");
                
                //output post content
                if (postsInSubjectFeed[x].PostType == PostType.Text)
                {
                    Console.WriteLine($"'{postsInSubjectFeed[x].PostText}'");
                }
                else if (postsInSubjectFeed[x].PostType == PostType.Feeling)
                {
                    Console.WriteLine($"'{postsInSubjectFeed[x].PostFeeling}'");
                }
                
                //output post details
                Console.WriteLine($"Author: {postsInSubjectFeed[x].Author.Name}");
                Console.WriteLine($"Date of post: {postsInSubjectFeed[x].Created}");
                
                //output comments if any
                if (postsInSubjectFeed[x].Comments == null) continue;
                Console.WriteLine($"Comments:");
                foreach (var y in postsInSubjectFeed[x].Comments)
                {
                    Console.WriteLine($"'{y.Content}'");
                    Console.WriteLine($"Author: {y.Author.Name} ");
                    Console.WriteLine($"Date of comment: {y.DateAndTime} ");
                    Console.WriteLine("*");
                }
            }
        }

        public void ShowWallForFriend(User wallOwner, User viewer)
        {
            List<Post> postsOnWall = new List<Post>();
            List<Post> postsThatViewerHasAccessTo = new List<Post>();
            
            //add all wall owners posts to list of posts on wall
            postsOnWall.AddRange(_postService.GetPostByAuthor(wallOwner));
            
            //check if viewing friend has access to each post
            foreach (var post in postsOnWall)
            {
                var viewingFriendHasAccess = false;
                
                //break if list is empty
                if (post.ShownCircles == null) continue;
                
                //make list of circles that post is shown in
                List<Circle> postCircles = new List<Circle>();
                foreach (var circleId in post.ShownCircles)
                {
                    var circleToAdd = _circleService.GetCircleById(circleId);
                    postCircles.Add(circleToAdd);
                }

                //check if subject is member of circles
                foreach (var circle in postCircles)
                {
                    if (circle.Members == null) continue;
                    if (circle.Members.Contains(viewer)
                    {
                        viewingFriendHasAccess = true;
                    }
                }

                if (post.IsPublic == true)
                {
                    viewingFriendHasAccess = true;
                }

                if (viewingFriendHasAccess == true)
                {
                    postsThatViewerHasAccessTo.Add(post);
                }
            }

            Console.WriteLine($"-------------{wallOwner.Name}'s Wall-------------");
            for (var x = 0; x < postsThatViewerHasAccessTo.Count; x++)
            {
                Console.WriteLine("****");
                Console.WriteLine($"Post #{x + 1}, content type: {postsThatViewerHasAccessTo[x].PostType}");
                if (postsThatViewerHasAccessTo[x].PostType == PostType.Text)
                {
                    Console.WriteLine($"'{postsThatViewerHasAccessTo[x].PostText}'");
                }
                else if (postsThatViewerHasAccessTo[x].PostType == PostType.Feeling)
                {
                    Console.WriteLine($"'{postsThatViewerHasAccessTo[x].PostFeeling}'");
                }

                Console.WriteLine($"Author: {postsThatViewerHasAccessTo[x].Author.Name}");
                Console.WriteLine($"Date of post: {postsThatViewerHasAccessTo[x].Created}");

                if (postsThatViewerHasAccessTo[x].Comments == null) continue;
                Console.WriteLine($"Comments:");
                foreach (var y in postsThatViewerHasAccessTo[x].Comments)
                {
                    Console.WriteLine($"'{y.Content}'");
                    Console.WriteLine($"Author: {y.Author.Name} ");
                    Console.WriteLine($"Date of comment: {y.DateAndTime} ");
                    Console.WriteLine("*");
                }
            }
        }

        public void ShowOwnWall(User wallOwner)
        {
            List<Post> postsOnWall = new List<Post>();

            postsOnWall = _postService.GetPostByAuthor(wallOwner);
            
            Console.WriteLine($"-------------{wallOwner.Name}'s Wall-------------");
            for (var x = 0; x < postsOnWall.Count; x++)
            {
                Console.WriteLine("****");
                Console.WriteLine($"Post #{x + 1}, content type: {postsOnWall[x].PostType}");
                if (postsOnWall[x].PostType == PostType.Text)
                {
                    Console.WriteLine($"'{postsOnWall[x].PostText}'");
                }
                else if (postsOnWall[x].PostType == PostType.Feeling)
                {
                    Console.WriteLine($"'{postsOnWall[x].PostFeeling}'");
                }

                Console.WriteLine($"Author: {postsOnWall[x].Author.Name}");
                Console.WriteLine($"Date of post: {postsOnWall[x].Created}");
                
                if (postsOnWall[x].Comments == null) continue;
                Console.WriteLine($"Comments:");
                foreach (var y in postsOnWall[x].Comments)
                {
                    Console.WriteLine($"'{y.Content}'");
                    Console.WriteLine($"Author: {y.Author.Name} ");
                    Console.WriteLine($"Date of comment: {y.DateAndTime} ");
                    Console.WriteLine("*");
                }
            }

        }

        //////////////////////////////////////CREATIONS/////////////////////////////////////////////////////
        public void CreatePost(User author, string postText, bool isPublic)
        {
            var post = new Post
            {
                Author = author,
                PostText = postText,
                Created = DateTime.Now,
                IsPublic = isPublic,
                Comments = new List<Comment>()
            };

            _postService.CreatePost(post);
        }

        public void CreateComment(User author, Post post)
        {

            Console.Write("Enter Content of Comment ");
            string content = Console.ReadLine();

        var comment = new Comment
            {
                Author = author,
                Content = content,
                DateAndTime = DateTime.Now
            };

            post.Comments.Add(comment);
            _postService.UpdatePost(post.Id, post);
        }
    }
}

