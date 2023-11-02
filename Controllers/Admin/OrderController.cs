using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using vphone.Models;
using vphone.Helper;

namespace vphone.Controllers.Admin
{
	public class OrderController : Controller
	{
		private QLQuanDTContext db;

		public OrderController(QLQuanDTContext db)
		{
			this.db = db;
		}
		[Route("/admin/order")]
		public IActionResult Order()
		{
			ViewBag.alert = null;
			ViewBag.orders = db.Orders.Where(o => o.State == false).ToList();
			return View("~/Views/Admin/Order/order.cshtml", ViewBag);
		}

		[Route("/admin/order/detail/{id}")]
		public IActionResult OrderDetail (int id)
		{
			ViewBag.order = db.Orders.FirstOrDefault(o => o.Id == id);
			ViewBag.orderDetails = db.OrderDetails.Where(o => o.OrderId == id).ToList();
            return View("~/Views/Admin/Order/detailorder.cshtml", ViewBag);
		}


		[Route("/admin/order/processed")]
		public IActionResult Processed ()
		{
			ViewBag.orders = db.Orders.Where(o => o.State == true).ToList();
			return View("~/Views/Admin/Order/processed.cshtml", ViewBag);
		}

		[Route("/admin/order/update/{id}")]
		public IActionResult Update (int id)
		{
			var item = db.Orders.FirstOrDefault(o => o.Id == id);
			item.State = true;
            var email = HttpContext.Session.Get<string>("Email");
            item.UserId = db.Users.Where(u => u.Email == email).FirstOrDefault().Id;
			db.SaveChanges();
			ViewBag.alert = "Đơn hàng đã được xử lý!";
            ViewBag.orders = db.Orders.Where(o => o.State == true).ToList();
            return View("~/Views/Admin/Order/processed.cshtml", ViewBag);
		}
		
	}
}
