using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Forum.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime TimeCreate { get; set; }
        public IUser User { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}