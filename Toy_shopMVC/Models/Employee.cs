using System;
using System.Collections.Generic;

#nullable disable

namespace Toy_shopMVC.Models
{
    public partial class Employee
    {
        public Guid IdEmployee { get; set; }
        public int CodeOfRole { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Salt { get; set; }

        public virtual Role CodeOfRoleNavigation { get; set; }
    }
}
