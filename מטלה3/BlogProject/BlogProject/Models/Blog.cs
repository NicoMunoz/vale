using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorWebSite { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorWebSite { get; set; }
        public string content { get; set; }
        public virtual Post Post { get; set; }
    }

    public class BlogDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}