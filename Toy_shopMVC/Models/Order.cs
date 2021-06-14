using System;
using System.Collections.Generic;

#nullable disable

namespace Toy_shopMVC.Models
{
    public partial class Order
    {
        public long CodeOrder { get; set; }
        public long? CodeProductInBasket { get; set; }
        public decimal? Sum { get; set; }
        public DateTime DateOfCreation { get; set; }
        public long CodeStatus { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }

        public virtual ProductInBasket CodeProductInBasketNavigation { get; set; }
        public virtual Status CodeStatusNavigation { get; set; }
    }
}
