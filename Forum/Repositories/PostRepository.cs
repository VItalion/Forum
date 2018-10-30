using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Forum.Interfaces;
using Forum.Models;

namespace Forum.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private readonly ApplicationDbContext context;
        public PostRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Post Find(int id)
        {
            return context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> Get(Func<Post, bool> predicate)
        {
            return context.Posts.AsParallel().Where(predicate).OrderBy(p=>p.TimeCreate).AsEnumerable();
        }

        public IEnumerable<Post> GetAll()
        {
            return context.Posts.AsEnumerable();
        }

        public void Add(Post entity)
        {
            context.Posts.Add(entity);
        }

        public void Update(Post entity)
        {
            context.Posts.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var post = Find(id);
            if (post == null) return;

            context.Posts.Remove(post);
        }
    }
}