using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forum.DTO;

namespace Forum.Interfaces
{
    public interface IPostService
    {
        Task CreatePostAsync(PostDto dto);
        Task ModifyPostAsync(int id, PostDto dto);
        Task RemovePostAsync(int id);
        PostDto GetPost(int id);
        IEnumerable<PostDto> FindPosts(string request);
        IEnumerable<PostDto> GetAllPosts();
    }
}
