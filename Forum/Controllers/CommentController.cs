using Forum.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Forum.Constants;
using Forum.Interfaces;

namespace Forum.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService service;

        public CommentController(ICommentService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> CreateComment(int postId, string text)
        {
            var commentVm = new CommentViewModel();
            commentVm.Text = text;
            commentVm.User = new UserViewModel { UserName = System.Web.HttpContext.Current.User.Identity.Name };

            await service.AddCommentAsync(postId, commentVm.ToDto());

            return RedirectToAction(Constant.View.GetPost, Constant.View.Post, new { postId = postId });
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> DeleteComment(CommentViewModel comment)
        {
            await service.RemoveCommentAsync(comment.Id);
            return RedirectToAction(Constant.View.GetPost, Constant.View.Post, new { postId = comment.Post.Id });
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public PartialViewResult BeginChangeComment(int commentId)
        {
            var comment = service.GetComment(commentId);
            return PartialView(Constant.View.ChangeComment, new CommentViewModel(comment));
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ActionResult> ChangeComment(CommentViewModel comment)
        {
            await service.ChangeCommentAsync(comment.Id, comment.ToDto());
            return RedirectToAction(Constant.View.GetPost, Constant.View.Post, new { postId = comment.Post.Id });
        }
    }
}