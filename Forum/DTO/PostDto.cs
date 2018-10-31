using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.Models;

namespace Forum.DTO
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime TimeCreate { get; set; }

        public UserDto User { get; set; }
        public List<CommentDto> Comments { get; set; }

        public PostDto() { }

        public PostDto(Post model)
        {
            if (model == null) return;
            Id = model.Id;
            Header = model.Header;
            Description = model.Description;
            TimeCreate = model.TimeCreate;
            User = new UserDto { UserName = model.User.UserName };
            Comments = model.Comments.Select(c => new CommentDto(c) { Post = this }).ToList();
        }

        public void FillModel(ref Post model)
        {
            if (model == null)
                model = new Post()
                {
                    Id = Id,
                    Header = Header,
                    Description = Description,
                    TimeCreate = TimeCreate,
                };

            model.Header = Header;
            model.Description = Description;
            model.TimeCreate = TimeCreate;
            if (model.User == null)
                model.User = new ApplicationUser { UserName = User?.UserName };
        }
    }
}