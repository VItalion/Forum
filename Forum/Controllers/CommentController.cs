using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class CommentController : Controller
    {
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> CreateComment(int postId, string text)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var comment = new Comment();
                comment.Text = text;
                var posts = context.Posts.ToList();
                var post = posts.First(p => p.Id == postId);
                context.Entry(post).Collection(x => x.Comments).Load();
                var currentUser = context.Users.First(u => u.UserName == User.Identity.Name);
                comment.User = currentUser;
                comment.Post = post;
                post.Comments.Add(comment);
                await context.SaveChangesAsync();
                //return RedirectToAction("GetPost", new { postId = postId });
                return PartialView("Comments", context.Comments.ToList());
            }
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> DeleteComment(Comment comment)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var commentToDelete = context.Comments.First(c => c.Id == comment.Id);
                if (commentToDelete == null)
                    return HttpNotFound();

                context.Entry(commentToDelete).Reference(x => x.Post).Load();
                var postId = commentToDelete.Post.Id;
                context.Comments.Remove(commentToDelete);
                await context.SaveChangesAsync();

                return RedirectToAction("GetPost", "Post", new { postId = postId });
            }
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public PartialViewResult BeginChangeComment(int commentId)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var comment = context.Comments.First(c => c.Id == commentId);
                return PartialView("ChangeComment", comment);
            }
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> ChangeComment(Comment comment)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var oldComment = context.Comments.First(c => c.Id == comment.Id);
                oldComment.Text = comment.Text;
                await context.SaveChangesAsync();
                context.Entry(oldComment).Reference(c => c.Post).Load();
                return RedirectToAction("GetPost", "Post", new { postId = oldComment.Post.Id });
            }

        }
    }
}