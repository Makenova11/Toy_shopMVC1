using System;
using System.Collections.Generic;

#nullable disable

namespace Toy_shopMVC.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductInCategories = new HashSet<ProductInCategory>();
        }

        public int CodeOfCategory { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductInCategory> ProductInCategories { get; set; }
    }
}
