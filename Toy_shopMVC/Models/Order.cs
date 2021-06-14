using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "* Поле обязательно")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Поле обязательно")]
        public string Phone { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }

        public virtual ProductInBasket CodeProductInBasketNavigation { get; set; }
        public virtual Status CodeStatusNavigation { get; set; }
    }
}
