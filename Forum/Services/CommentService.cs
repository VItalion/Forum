using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Forum.DTO;
using Forum.Interfaces;
using Forum.Models;

namespace Forum.Services
{
    public class CommentService : ICommentService, IDisposable
    {
        private IUnitOfWork Database { get; set; }

        public CommentService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public CommentDto GetComment(int commentId)
        {
            var model = Database.Comments.Find(commentId);
            return model == null ? null : new CommentDto(model);
        }

        public IEnumerable<CommentDto> GetComments(int postId)
        {
            return Database.Comments.Get(c => c.Post.Id == postId).Select(c => new CommentDto(c));
        }

        public async Task AddCommentAsync(int postId, CommentDto dto)
        {
            if (dto == null) return;

            var post = Database.Posts.Find(postId);
            if (post == null) return;

            var user = Database.Users.Get(dto.User.UserName);
            if (user == null) return;

            var comment = new Comment();
            dto.FillModel(ref comment);
            comment.Post = post;
            comment.User = user;

            Database.Comments.Add(comment);
            await Database.SaveAsync();
        }

        public async Task ChangeCommentAsync(int commentId, CommentDto dto)
        {
            var comment = Database.Comments.Find(commentId);
            if (comment == null) return;

            dto.FillModel(ref comment);
            Database.Comments.Update(comment);
            await Database.SaveAsync();
        }

        public async Task RemoveCommentAsync(int commentId)
        {
            var comment = Database.Comments.Find(commentId);
            if (comment == null) return;

            Database.Comments.Delete(commentId);
            await Database.SaveAsync();
        }

        public void Dispose()
        {
            Database?.Dispose();
        }
    }
}