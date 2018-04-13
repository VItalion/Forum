using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.Models;
using System.Threading.Tasks;
using PagedList;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            using (var context = Models.ApplicationDbContext.Create())
            {
                var posts = context.Posts.ToList();
                posts.Reverse();
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(posts.ToPagedList(pageNumber, pageSize));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Search(string request, int? page)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var posts = context.Posts.Where(p => p.Header.Contains(request)).ToList();
                                
                return View("SearchResult", posts);
            }
        }

        [HttpGet]
        public ActionResult GetPost(int postId)
        {
            Post post;
            using (var context = ApplicationDbContext.Create())
            {
                post = context.Posts.First(p => p.Id == postId);
                context.Entry(post).Reference(x => x.User).Load();
                context.Entry(post).Collection(x => x.Comments).Load();

                foreach(var comm in post.Comments)
                {
                    context.Entry(comm).Reference(x => x.User).Load();
                }
            }
            return View(post);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult CreatePost()
        {
            return View(new Post());
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult> CreatePost(Post newPost)
        {
            using (var context = Models.ApplicationDbContext.Create())
            {
                var currentUser = context.Users.First(user => user.UserName == User.Identity.Name);
                newPost.User = currentUser;

                context.Posts.Add(newPost);
                await context.SaveChangesAsync();
            }
            return this.RedirectToAction("Index", "Post");
        }

        [HttpGet]
        public ActionResult ChangePost(int postId)
        {
            Post post;
            using (var context = ApplicationDbContext.Create())
            {
                post = context.Posts.First(p => p.Id == postId);
            }

            if (post == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(post);
            }
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult> ChangePost(Post post)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var oldPost = context.Posts.First(p => p.Id == post.Id);
                oldPost.Header = post.Header;
                oldPost.Description = post.Description;

                await context.SaveChangesAsync();
            }

            return RedirectToAction("GetPost", new { postId = post.Id });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeletePost(int postId)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var post = context.Posts.First(p => p.Id == postId);
                if (post == null)
                    return HttpNotFound();

                context.Posts.Remove(post);

                await context.SaveChangesAsync();
            }
            return this.RedirectToAction("Index", "Post");
        }
    }
}