using Microsoft.AspNetCore.Mvc;
using vphone.Models.Authentication;

namespace vphone.Controllers.Admin
{
	public class DashboardController : Controller
	{

		[Route("/admin/dashboard")]
		[Authentication]
		public IActionResult Index()
		{
			return View("~/Views/Admin/Dashboard/Index.cshtml");
		}
	}
}
