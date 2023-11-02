using Microsoft.AspNetCore.Mvc;
using vphone.Models;

namespace vphone.Controllers.Site
{
    public class HomeController : Controller
    {
        private QLQuanDTContext db;
        public HomeController(QLQuanDTContext db)
        {
            this.db = db;
        }
        //[Route("site")]
        public IActionResult Index()
        {
             ViewBag.products_featured = db.Products
                                            .OrderByDescending(p => p.CreatedAt)
                                            .Where(p => p.IsFeatured == true && p.IsStock == true && p.DeletedAt == false)
                                            .Take(6).
                                            ToList();
            ViewBag.products = db.Products.OrderByDescending(p => p.CreatedAt)
                                          .Where(p => p.IsStock == true && p.DeletedAt == false)
                                          .Take(6)
                                          .ToList();

            return View("~/Views/Site/Home/Index.cshtml", ViewBag);
        }
    }
}
