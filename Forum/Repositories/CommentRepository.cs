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
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationDbContext context;
        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        public Comment Find(int id)
        {
            return context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Comment> Get(Func<Comment, bool> predicate)
        {
            return context.Comments.AsParallel().Where(predicate).OrderBy(c => c.TimeCreate);
        }

        public IEnumerable<Comment> GetAll()
        {
            return context.Comments.AsEnumerable();
        }

        public void Add(Comment entity)
        {
            context.Comments.Add(entity);
        }

        public void Update(Comment entity)
        {
            var comment = Find(entity.Id);
            if (comment == null) return;

            comment.Text = entity.Text;
        }

        public void Delete(int id)
        {
            var comment = Find(id);
            if (comment == null) return;

            context.Comments.Remove(comment);
        }
    }
}