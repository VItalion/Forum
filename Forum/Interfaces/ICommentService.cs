using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.DTO;

namespace Forum.Interfaces
{
    public interface ICommentService
    {
        CommentDto GetComment(int commentId);
        IEnumerable<CommentDto> GetComments(int postId);
        void AddComment(int postId, CommentDto dto);
        void ChangeComment(int commentId, CommentDto dto);
        void RemoveComment(int commentId);
    }
}
