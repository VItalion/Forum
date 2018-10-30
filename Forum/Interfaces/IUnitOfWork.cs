using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Post> Posts { get; }
        IRepository<Comment> Comments { get; }
        IUserRepository Users { get; }
        Task SaveAsync();
    }
}
