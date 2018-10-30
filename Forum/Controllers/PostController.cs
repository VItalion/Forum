using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forum.ViewModels;
using System.Threading.Tasks;
using Forum.Constants;
using Forum.Interfaces;
using PagedList;

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private IPostService service;
        public PostController(IPostService service)
        {
            this.service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(int? page)
        {
            var posts = service.GetAllPosts()
                .OrderBy(p => p.TimeCreate)
                .Select(p => new PostViewModel(p))
                .ToList();

            int pageNumber = (page ?? 1);
            return View(posts.ToPagedList(pageNumber, Constant.PageSize));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Search(string request, int? page)
        {
            var posts = service.FindPosts(request).Select(p => new PostViewModel(p)).ToList();
            return View(Constant.View.SearchResult, posts);
        }

        [HttpGet]
        public ActionResult GetPost(int postId)
        {
            //Post post;
            //using (var context = Repositories.ApplicationDbContext.Create())
            //{
            //    post = context.Posts.First(p => p.Id == postId);
            //    context.Entry(post).Reference(x => x.User).Load();
            //    context.Entry(post).Collection(x => x.Comments).Load();

            //    foreach (var comm in post.Comments)
            //    {
            //        context.Entry(comm).Reference(x => x.User).Load();
            //    }
            //}
            //return View(post);

            var post = service.GetPost(postId);
            var postVm = new PostViewModel(post);
            return View(postVm);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult CreatePost()
        {
            return View(new PostViewModel());
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult> CreatePost(PostViewModel newPost)
        {
            var currentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            var userVm = new UserViewModel { UserName = currentUserName };
            newPost.User = userVm;
            service.CreatePost(newPost.ToDto());

            return RedirectToAction(Constant.View.Index, Constant.View.Post);
        }

        [HttpGet]
        public ActionResult ChangePost(int postId)
        {
            var postDto = service.GetPost(postId);

            if (postDto == null)
                return HttpNotFound();
            else
                return View(new PostViewModel(postDto));
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ActionResult> ChangePost(PostViewModel post)
        {
            service.ModifyPost(post.Id, post.ToDto());

            return RedirectToAction(Constant.View.GetPost, new { postId = post.Id });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeletePost(int postId)
        {
            try
            {
                service.RemovePost(postId);
            }
            catch (Exception ex)
            {
                return HttpNotFound("This post does not exist");
            }

            return RedirectToAction(Constant.View.Index, Constant.View.Post);
        }
    }
}