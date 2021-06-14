using System;
using System.Collections.Generic;

#nullable disable

namespace Toy_shopMVC.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductInBaskets = new HashSet<ProductInBasket>();
            ProductInCategories = new HashSet<ProductInCategory>();
        }

        public long CodeOfProduct { get; set; }
        public long IdManufacturer { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int InStock { get; set; }

        public virtual Manufacturer IdManufacturerNavigation { get; set; }
        public virtual ICollection<ProductInBasket> ProductInBaskets { get; set; }
        public virtual ICollection<ProductInCategory> ProductInCategories { get; set; }
    }
}
