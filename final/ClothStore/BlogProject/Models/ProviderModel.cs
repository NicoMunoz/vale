using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClothStore.Models
{
    public class Provider
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Cloth> Clothes { get; set; }
    }
}