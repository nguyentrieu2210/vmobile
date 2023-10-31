using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using vphone.Models;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Seo;
using vphone.Helper;

namespace vphone.Controllers.Admin
{
	public class ProductController : Controller
	{
        private QLQuanDTContext db;
        public ProductController(QLQuanDTContext db)
        {
            this.db = db;   
        }
		[Route("/admin/product/{page?}")]
		public IActionResult Index(int? page)
		{
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var products = db.Products.Where(p => p.DeletedAt == false).Include(m => m.Cat).ToList();
			return View("~/Views/Admin/Product/listproduct.cshtml", products.ToPagedList(pageNumber, pageSize));
		}

        [Route("/admin/product/add")]
        public IActionResult Add()
        {
            ViewBag.categories = db.Categories.ToList();
            return View("~/Views/Admin/Product/addproduct.cshtml");
        }

        [HttpPost]
        [Route("/admin/product/add")]
        public IActionResult Store(Product product, IFormFile Image)
        {   
                if (Image != null)
                {
                    string fileName = Image.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);
                    var stream = new FileStream(path, FileMode.Create);
                    Image.CopyToAsync(stream);
                    product.Image = fileName;
                }
                product.Id = 4;
                var email = HttpContext.Session.Get<string>("Email");
                product.UserId = db.Users.Where(u => u.Email == email).FirstOrDefault().Id;
                Slug slugGenerator = new Slug();
                product.Slug = slugGenerator.GetSlug(product.Name);
                Console.WriteLine(product.Slug);
                db.Products.Add(product);
                db.SaveChanges();
                ViewBag.alert = "Đã thêm sản phẩm thành công!";
                return Redirect("/admin/product/{pages)");
        }

        [Route("/admin/product/edit/{slug}/{id}")]
        public IActionResult Edit(int id)
        {
            
             ViewBag.product = db.Products.FirstOrDefault(p => p.Id == id);
            ViewBag.categories = db.Categories.ToList();
            return View("~/Views/Admin/Product/editproduct.cshtml");
        }

        private bool ProductExists(int id)
        {
            return (db.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        [Route("/admin/product/update/{slug}/{id}")]
        public IActionResult Update(Product product, IFormFile Image, int id)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
                try
                {
                Console.WriteLine(product.Image);
                if (Image != null)
                {
                    string fileName = Image.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);
                    var stream = new FileStream(path, FileMode.Create);
                    Image.CopyToAsync(stream);
                    product.Image = fileName;
                }
                product.Id = id;
                var email = HttpContext.Session.Get<string>("Email");
                product.UserId = db.Users.Where(u => u.Email == email).FirstOrDefault().Id;
                Slug slugGenerator = new Slug();
                product.Slug = slugGenerator.GetSlug(product.Name);
                db.Update(product);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            ViewBag.alert = "Đã cập nhật sản phẩm thành công!";
            return Redirect("/admin/product/{pages)");
        }

        [Route("/admin/product/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = db.Products.FirstOrDefault(o => o.Id == id);
            item.DeletedAt = true;
            db.SaveChanges();
            ViewBag.alert = "Sản phẩm đã được xóa!";
            int pageSize = 5;
            int pageNumber = 1;
            var products = db.Products.Where(p => p.DeletedAt == false).Include(m => m.Cat).ToList();
            return View("~/Views/Admin/Product/listproduct.cshtml", products.ToPagedList(pageNumber, pageSize));
        }
    }
}
