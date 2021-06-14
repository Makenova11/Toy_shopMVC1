using System;
using System.Collections.Generic;

#nullable disable

namespace Toy_shopMVC.Models
{
    public partial class ProductInBasket
    {
        public ProductInBasket()
        {
            Orders = new HashSet<Order>();
        }

        public long CodeProductInBasket { get; set; }
        public long CodeOfProduct { get; set; }
        public int Quantity { get; set; }

        public virtual Product CodeOfProductNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
