using Microsoft.AspNetCore.Mvc;
using vphone.Models;
using vphone.Helper;

namespace vphone.Controllers.Site
{
    public class OrderController : Controller
    {
        private QLQuanDTContext db;
        public OrderController(QLQuanDTContext db)
        {
            this.db = db;
        }
        [Route("/success")]
        public IActionResult Index()
        {
            return View("~/Views/Site/Success/Index.cshtml");
        }
        [HttpPost]
        public IActionResult AddOrder(decimal totalPrice, [Bind("Name", "Phone", "Email", "Address")] Order order)
        {
            order.UserId = 1;
            order.State = false;
            order.PriceTotal = totalPrice;
            db.Orders.Add(order);
            db.SaveChanges();
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            ViewBag.Ordersc = order;
            ViewBag.cartsc = cart;
            foreach (var c in cart.Items)
            {
                OrderDetail ord = new OrderDetail();
                ord.OrderId = order.Id;
                ord.PrdId = c.ProductId;
                ord.Price = c.Price;
                ord.Image = c.Image;
                ord.Name = c.Name;
                ord.Qty = c.Qty;
                db.OrderDetails.Add(ord);
                db.SaveChanges();
            }
            HttpContext.Session.Clear();
            return View("~/Views/Site/Success/Index.cshtml", ViewBag);
        }
    }
}
