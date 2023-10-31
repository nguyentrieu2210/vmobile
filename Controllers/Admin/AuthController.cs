using Microsoft.AspNetCore.Mvc;
using vphone.Models;
using Microsoft.AspNetCore.Http;
using System.Text;
using vphone.Helper;

namespace vphone.Controllers.Admin
{
	public class AuthController : Controller
	{
        private QLQuanDTContext db;
        public AuthController(QLQuanDTContext db)
        {
            this.db = db;
        }

        [Route("admin/login")]
		public IActionResult Index()
		{
			return View("~/Views/Admin/Auth/login.cshtml");
		}
        [HttpPost]
        [Route("/admin/login")]
       //    [ValidateAntiForgeryToken]
        public IActionResult Login( Login user)
        {
            if (ModelState.IsValid)
            {
                var existUser = db.Users.SingleOrDefault(u => u.Email == user.Email);
                if(existUser != null && existUser.Password == user.Password)
                {
                    var email = user.Email;
                    HttpContext.Session.Set<String>("Email", email);
                    return Redirect("/admin/dashboard");
                }
                else
                {
                    ViewBag.Msg = "Email hoặc mật khẩu không chính xác!";
                }
            }
            return View("~/Views/Admin/Auth/login.cshtml", user);
        }

        [Route("/admin/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            return Redirect("/admin/login");
        }

    }
}
