using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using vphone.Models;
using X.PagedList;
using static System.Reflection.Metadata.BlobBuilder;

namespace vphone.Controllers.Site
{
    public class CategoryController : Controller
    {
        QLQuanDTContext db;

        public CategoryController(QLQuanDTContext context)
        {
            this.db = context;
        }
        [Route("/category/{slug}/{page?}")]
        public IActionResult Index(string slug, int? page)
        {
            //if (page == null) page = 1;
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var products = db.Products.Where(l => l.Cat.Slug == slug).Where(p => p.DeletedAt == false).Include(l => l.Cat).ToList();
            return View("~/Views/Site/Category/Index.cshtml", products.ToPagedList(pageNumber, pageSize));
        }
    }
}
