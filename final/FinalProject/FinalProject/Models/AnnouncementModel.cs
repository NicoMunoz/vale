using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Announcement
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class AnnouncementsContext : DbContext
    {
        public DbSet<Announcement> Announcements { get; set; }
    }
}