using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClothStore.Models
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
    }
}