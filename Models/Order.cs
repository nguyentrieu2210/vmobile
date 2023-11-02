using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace vphone.Models
{
    public partial class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? CusId { get; set; }   
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool State { get; set; }
        public decimal PriceTotal { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
