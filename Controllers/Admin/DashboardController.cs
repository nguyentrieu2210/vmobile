using Microsoft.AspNetCore.Mvc;

namespace vphone.Controllers.Admin
{
	public class DashboardController : Controller
	{
		[Route("/admin/dashboard")]
		public IActionResult Index()
		{
			return View("~/Views/Admin/Dashboard/Index.cshtml");
		}
	}
}
