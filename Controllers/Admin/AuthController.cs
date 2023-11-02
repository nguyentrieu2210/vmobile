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
            if (HttpContext.Session.Get<string>("Email") != null)
            {
                return Redirect("/admin/dashboard");
            }
            return View("~/Views/Admin/Auth/login.cshtml");
		}
        [HttpPost]
        [Route("/admin/login")]
        public IActionResult Login( Login user)
        {
            if (ModelState.IsValid && HttpContext.Session.Get<string>("Email") == null)
            {
                var existUser = db.Users.SingleOrDefault(u => u.Email == user.Email);
                if(existUser != null && existUser.Password == user.Password)
                {
                    var email = user.Email;
                    HttpContext.Session.Set<String>("Email", email.ToString());
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
