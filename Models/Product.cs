using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vphone.Models
{
    public partial class Product : BaseEntity
    {
        public Product ()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsStock { get; set; }
        public int CatId { get; set; }
        public int UserId { get; set; }

        public string? Image { get; set; }

        public string Description { get; set; }
        public string Slug { get; set; }

        public bool IsFeatured { get; set; }

        public string ScreenTech { get; set; }

        public string Resolution { get; set; }

        public string ScreenSize { get; set; }

        public string OperatingSystem { get; set; }

        public string Processor { get; set; }

        public string InternalMemory { get; set; }

        public string Ram { get; set; }

        public string BatteryCapacity { get; set; }
        public bool DeletedAt { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Category Cat { get; set; } = null!;

        public virtual User User { get; set; } = null!;

    }
}
