using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClothStore.Models
{
    public class Cloth
    {
        public int ID { get; set; }
        public int ProviderID { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Price { get; set; } = 100;
        [Required]
        public int Quantity { get; set; }
        public string Color { get; set; }
        public DateTime AddedDate { get; set; }
        public virtual Provider Provider { get; set; }
    }

    public class Provider
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Cloth> Clothes { get; set; }
    }

    public class StoreDbContext : DbContext
    {
        public DbSet<Cloth> Clothes { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}