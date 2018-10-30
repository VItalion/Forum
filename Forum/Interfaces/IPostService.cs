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
        void CreatePost(PostDto dto);
        void ModifyPost(int id, PostDto dto);
        void RemovePost(int id);
        PostDto GetPost(int id);
        IEnumerable<PostDto> FindPosts(string request);
        IEnumerable<PostDto> GetAllPosts();
    }
}
