using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreatePost(PostDto dto)
        {
            Post post = null;
            dto.FillModel(ref post);
            post.User = Database.Users.Get(dto.User.UserName);
            Database.Posts.Add(post);
        }

        public void ModifyPost(int id, PostDto dto)
        {
            var post = Database.Posts.Find(id);
            if (post == null) return;

            dto.FillModel(ref post);
            Database.Posts.Update(post);
            Database.Save();
        }

        public void RemovePost(int id)
        {
            Database.Posts.Delete(id);
            Database.Save();
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
            var models = Database.Posts.Get(p => p.Header.Contains(request))?.ToList();
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