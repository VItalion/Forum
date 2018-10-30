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
        Task AddCommentAsync(int postId, CommentDto dto);
        Task ChangeCommentAsync(int commentId, CommentDto dto);
        Task RemoveCommentAsync(int commentId);
    }
}
