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
        public string Color { get; set; }
        public DateTime AddedDate { get; set; }
        public virtual Provider Provider { get; set; }
    }
}