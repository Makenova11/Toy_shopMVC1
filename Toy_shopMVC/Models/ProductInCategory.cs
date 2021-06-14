using System;
using System.Collections.Generic;

#nullable disable

namespace Toy_shopMVC.Models
{
    public partial class ProductInCategory
    {
        public long CodeOfProduct { get; set; }
        public int CodeOfCategory { get; set; }

        public virtual Category CodeOfCategoryNavigation { get; set; }
        public virtual Product CodeOfProductNavigation { get; set; }
    }
}
