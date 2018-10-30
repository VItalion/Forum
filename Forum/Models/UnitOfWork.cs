using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.Interfaces;
using Forum.Repositories;

namespace Forum.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context;
        private PostRepository posts;
        private CommentRepository comments;
        private UserRepository users;

        public UnitOfWork(string connection)
        {
            context = new ApplicationDbContext(connection);
        }

        public IRepository<Post> Posts => posts ?? (posts = new PostRepository(context));
        public IRepository<Comment> Comments => comments ?? (comments = new CommentRepository(context));
        public IUserRepository Users => users ?? (users = new UserRepository(context));

        public async void Save()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}