using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BlogProject.Models
{
    public class BlogFan
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int Seniority { get; set; }
    }

    public class BlogFanDbContext: DbContext
    {
        public DbSet<BlogFan> FansClub { get; set; }
    }
}