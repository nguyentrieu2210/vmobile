using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using vphone.Helper;
using Nop.Services.Catalog;
using vphone.Models;
using System.Web.Helpers;

namespace vphone.Controllers.Site
{
    public class CartController : Controller
    {
        private QLQuanDTContext db;
        public CartController(QLQuanDTContext db)
        {
            this.db = db;
        }

        [Route("/cart")]
        public IActionResult Index()
        {
            ViewBag.cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            return View("~/Views/Site/Cart/Index.cshtml", ViewBag);
        }

        public void AddItem(int productId)
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            // Tìm sản phẩm theo productId và thêm vào giỏ hàng
            var product = db.Products.FirstOrDefault(p => p.Id == productId);   
            if (product != null)
            {
                var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Qty++;
                }
                else
                {
                    cart.Items.Add(new CartItem
                    {
                        ProductId = productId,
                        Name = product.Name,
                        Image = product.Image,
                        Price = product.Price,
                        Qty = 1
                    });
                }
                HttpContext.Session.Set("Cart", cart);
            }
            //return RedirectToAction("Index");
        }

        [Route("/add-to-cart/{id}")]
        public IActionResult AddToCart(int id)
        {
            AddItem(id);
            ViewBag.cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            return Redirect("/");
        }

        [Route("/buy-now/{id}")]
        public IActionResult BuyNow(int id)
        {
            AddItem(id);
            return Redirect("/cart");
        }

        [Route("/delete-item-cart/{id}")]
        public IActionResult DeleteItemCart(int id)
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            var item = cart.Items.FirstOrDefault(p => p.ProductId == id);
            if (item != null)
            {
                cart.Items.Remove(item);
                HttpContext.Session.Set("Cart", cart);
            }
            return Redirect("/cart");
        }
        public void UpdateCart(int id, int qty)
        {
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == id);
            if (existingItem != null)
            {
                existingItem.Qty = qty;
            }
            HttpContext.Session.Set("Cart", cart);
        }
        [Route("/update-item-cart")]
        [HttpPost]
        public IActionResult UpdateItemCart(int id, int qty)
        {
            UpdateCart(id, qty);
            ViewBag.cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            return PartialView("~/Views/Site/Cart/CartItem.cshtml", ViewBag);
        }
        [Route("/update-total-cart")]
        [HttpPost]
        public IActionResult UpdateTotalCart(int id, int qty)
        {
            UpdateCart(id, qty);
            var cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            ViewBag.totalItem = cart.Items.Sum(item => item.Qty);
            return PartialView("~/Views/Site/Shared/Header.cshtml", ViewBag);
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
