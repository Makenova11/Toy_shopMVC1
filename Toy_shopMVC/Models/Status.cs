using System;
using System.Collections.Generic;

#nullable disable

namespace Toy_shopMVC.Models
{
    public partial class Status
    {
        public Status()
        {
            Orders = new HashSet<Order>();
        }

        public long CodeOfStatus { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
