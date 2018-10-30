using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.Interfaces;
using Forum.Models;
using Forum.Services;
using Ninject.Modules;

namespace Forum.Tools
{
    public class NinjectRegistrations : NinjectModule
    {
        private string connection;

        public NinjectRegistrations(string connection)
        {
            this.connection = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connection);
            Bind<IPostService>().To<PostService>();
            Bind<ICommentService>().To<CommentService>();
        }
    }
}