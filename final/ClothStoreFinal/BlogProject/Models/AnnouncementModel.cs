using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClothStore.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        [Required]
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class AnnouncementDbContext : DbContext
    {
        public DbSet<Announcement> Announcements { get; set; }
    }
}