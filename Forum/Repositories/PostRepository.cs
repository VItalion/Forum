using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Forum.Interfaces;
using Forum.Models;
using Microsoft.AspNet.Identity;

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
            return context.Posts.Include(p => p.User).Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> Get(Func<Post, bool> predicate)
        {
            return context.Posts.Include(p => p.User).Include(p => p.Comments).AsParallel().Where(predicate).OrderBy(p => p.TimeCreate).AsEnumerable();
        }

        public IEnumerable<Post> GetAll()
        {
            var posts = context.Posts.Include(p=>p.User).Include(p=>p.Comments);
            //foreach (var post in posts)
            //{
            //    context.Entry(post).Property<IUser>(p => p.User);
            //    context.Entry(post).Collection<Comment>(c => c.Comments).Load();
            //    foreach (var comment in post.Comments)
            //    {
            //        context.Entry(comment).Reference<IUser>(c => c.User).Load();
            //    }
            //}
            return posts.AsEnumerable();
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