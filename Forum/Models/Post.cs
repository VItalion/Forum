using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Заголовок")]
        public string Header { get; set; }
        [Display(Name = "Описанеие")]
        public string Description { get; set; }
        public ApplicationUser User { get; set; }
        public List<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}