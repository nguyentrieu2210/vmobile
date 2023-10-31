using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace vphone.Models
{
	public class Login
	{
        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression("^[a-z0-9]+(?!.*(?:\\+{2,}|\\-{2,}|\\.{2,}))(?:[\\.+\\-]{0,1}[a-z0-9])*@gmail\\.com$", ErrorMessage = "Bạn phải nhập đúng định dạng email")]
        public string Email { get; set; }
        //[RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
        //ErrorMessage = "Mật khẩu phải có chữ viết hoa, chữ thường và kí tự đặc biệt")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 kí")]
        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }
    }
}
