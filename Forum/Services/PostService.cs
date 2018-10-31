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
    public class PostService : IPostService, IDisposable
    {
        private IUnitOfWork Database { get; set; }

        public PostService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task CreatePostAsync(PostDto dto)
        {
            Post post = null;
            dto.FillModel(ref post);
            var user = Database.Users.Get(dto.User.UserName);
            if (user is ApplicationUser)
                post.User = user as ApplicationUser;
            Database.Posts.Add(post);
            await Database.SaveAsync();
        }

        public async Task ModifyPostAsync(int id, PostDto dto)
        {
            var post = Database.Posts.Find(id);
            if (post == null) return;

            dto.FillModel(ref post);

            Database.Posts.Update(post);
            await Database.SaveAsync();
        }

        public async Task RemovePostAsync(int id)
        {
            Database.Posts.Delete(id);
            await Database.SaveAsync();
        }

        public PostDto GetPost(int id)
        {
            var model = Database.Posts.Find(id);
            if (model == null)
                return null;

            var dto = new PostDto(model);
            return dto;
        }

        public IEnumerable<PostDto> FindPosts(string request)
        {
            var models = Database.Posts.Get(p => p.Header.ToUpper().Contains(request.ToUpper()))?.ToList();
            if (models == null || !models.Any())
                return null;

            return models.AsParallel().Select(m => new PostDto(m)).OrderBy(d => d.TimeCreate);
        }

        public IEnumerable<PostDto> GetAllPosts()
        {
            return Database.Posts.GetAll().Select(p => new PostDto(p));
        }
        public void Dispose()
        {
            Database?.Dispose();
        }
    }
}