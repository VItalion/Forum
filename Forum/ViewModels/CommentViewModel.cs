using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.DTO;

namespace Forum.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreate { get; set; }
        public UserViewModel User { get; set; }
        public PostViewModel Post { get; set; }

        public CommentViewModel()
        {
            TimeCreate = DateTime.UtcNow;
        }

        public CommentViewModel(CommentDto model)
        {
            if (model == null)
            {
                TimeCreate = DateTime.UtcNow;
                return;
            }

            Id = model.Id;
            Text = model.Text;
            TimeCreate = model.TimeCreate;
            User = new UserViewModel { UserName = model.User?.UserName };
            Post = new PostViewModel();
        }

        public CommentDto ToDto()
        {
            return new CommentDto
            {
                Id = Id,
                Text = Text,
                TimeCreate = TimeCreate,
                User = User?.ToDto(),
                Post = Post?.ToDto()
            };
        }
    }
}