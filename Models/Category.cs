using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vphone.Models
{
    public partial class Category:BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
		[Required(ErrorMessage = "Phải có tên danh mục")]
		[BindProperty]
		public string Title { get; set; }
        public string Slug { get; set; }
		[Required(ErrorMessage = "Phải có mô tả")]
		[BindProperty]
		public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
