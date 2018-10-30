using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.Models;

namespace Forum.DTO
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeCreate { get; set; }
        public UserDto User { get; set; }
        public PostDto Post { get; set; }

        public CommentDto() { }

        public CommentDto(Comment model)
        {
            if (model == null)
            {
                User = new UserDto();
                Post = new PostDto();
                return;
            }

            Id = model.Id;
            Text = model.Text;
            TimeCreate = model.TimeCreate;
            User = new UserDto { UserName = model.User.UserName };
            //Post = new PostDto(model.Post);
        }

        public void FillModel(ref Comment model)
        {
            if (model == null)
                model = new Comment { Id = Id };

            model.Text = Text;
            model.TimeCreate = TimeCreate;
        }
    }
}