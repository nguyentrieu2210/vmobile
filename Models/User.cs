using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace vphone.Models
{
    public partial class User : BaseEntity
    {
        public User ()
        {
            Products = new HashSet<Product>();
            Orders = new HashSet<Order>();
            Categories = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }    
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool DeletedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }  
        public virtual ICollection<Category> Categories { get; set; }
    }
}
