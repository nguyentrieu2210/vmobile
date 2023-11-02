using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace vphone.Models
{
    public partial class OrderDetail : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int PrdId { get; set; }
        public int OrderId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public virtual Order Order { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
