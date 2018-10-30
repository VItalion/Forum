using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Forum.DTO;
using Forum.Models;

namespace Forum.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        public string Header { get; set; }

        [Display(Name = "Описанеие")]
        public string Description { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime TimeCreate { get; set; }

        public UserViewModel User { get; set; }
        public List<CommentViewModel> Comments { get; set; }

        public PostViewModel()
        {
            Comments = new List<CommentViewModel>();
            TimeCreate = DateTime.UtcNow;
        }

        public PostViewModel(PostDto model)
        {
            if (model == null)
            {
                Comments = new List<CommentViewModel>();
                TimeCreate = DateTime.UtcNow;
                return;
            }

            Id = model.Id;
            Header = model.Header;
            Description = model.Description;
            User = new UserViewModel { UserName = model?.User.UserName };
            Comments = model.Comments.Select(c => new CommentViewModel(c)).ToList();
        }

        public PostDto ToDto()
        {
            return new PostDto
            {
                Id = Id,
                Header = Header,
                Description = Description,
                TimeCreate = TimeCreate,
                User = User.ToDto(),
                Comments = Comments.Select(c => c.ToDto()).ToList()
            };
        }
    }
}