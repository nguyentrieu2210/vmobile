using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vphone.Models
{
    public partial class User : BaseEntity
    {
        public User()
        {
            Products = new HashSet<Product>();
            Orders = new HashSet<Order>();
            Categories = new HashSet<Category>();
        }
<<<<<<< HEAD
=======

>>>>>>> adcategory
        public int Id { get; set; }
        [RegularExpression("^[a-zA-Z]+([\\s][a-zA-Z]+)*$", ErrorMessage = "Tên không được chứa số và kí tự đặc biệt")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Tên phải có ít nhất 4 kí tự và dưới 100 kí tự")]
        [Required(ErrorMessage = "Bạn phải nhập họ tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression("^[a-z0-9]+(?!.*(?:\\+{2,}|\\-{2,}|\\.{2,}))(?:[\\.+\\-]{0,1}[a-z0-9])*@gmail\\.com$", ErrorMessage = "Bạn phải nhập đúng định dạng email")]
        public string Email { get; set; }
        //[RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            //ErrorMessage = "Mật khẩu phải có chữ viết hoa, chữ thường và kí tự đặc biệt")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 kí")]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập số điện thoại")]
        //[RegularExpression("/(84|0[3|5|7|8|9])+([0-9]{8})\b/g;",ErrorMessage ="Bạn phải nhập đúng định dạng số điện thoại ở Việt Nam")]
        [StringLength(11, MinimumLength = 0, ErrorMessage = "SDT phải dưới 11 kí tự")]
        public string Phone { get; set; }
        public bool DeletedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
