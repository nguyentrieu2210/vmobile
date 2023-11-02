using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using vphone.Models;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Seo;
using vphone.Helper;
using vphone.Models.Authentication;

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
        [Authentication]

        public IActionResult Index(int? page)
		{
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var products = db.Products.OrderByDescending(p => p.CreatedAt).Where(p => p.DeletedAt == false).Include(m => m.Cat).ToList();
			return View("~/Views/Admin/Product/listproduct.cshtml", products.ToPagedList(pageNumber, pageSize));
		}

        [Route("/admin/product/add")]
        [Authentication]
        public IActionResult Add()
        {
            ViewBag.categories = db.Categories.ToList();
            return View("~/Views/Admin/Product/addproduct.cshtml");
        }

        [HttpPost]
        [Route("/admin/product/add")]
        [Authentication]
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
                var email = HttpContext.Session.Get<string>("Email");
                product.UserId = db.Users.Where(u => u.Email == email).FirstOrDefault().Id;
                Slug slugGenerator = new Slug();
                product.Slug = slugGenerator.GetSlug(product.Name);
                Console.WriteLine(product.Slug);
                db.Products.Add(product);
                db.SaveChanges();
                ViewBag.alert = "Đã thêm sản phẩm thành công!";
                int pageSize = 5;
                int pageNumber = 1;
                var products = db.Products.OrderByDescending(p => p.CreatedAt).Where(p => p.DeletedAt == false).Include(m => m.Cat).ToList();
                return View("~/Views/Admin/Product/listproduct.cshtml", products.ToPagedList(pageNumber, pageSize));
        }

        [Route("/admin/product/edit/{slug}/{id}")]
        [Authentication]
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
        [Authentication]
        public IActionResult Update(Product product, IFormFile Image, int id)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
                try
                {
                    if (Image != null)
                    {
                        string fileName = Image.FileName;
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            Image.CopyToAsync(stream);
                        }    
                        product.Image = fileName;
                    }
                    else
                    {
                        var existingProduct = db.Products.Find(id);
                        if(existingProduct != null)
                        {
                            product.Image = existingProduct.Image;
                        }
                        // Ngừng theo dõi thực thể hiện tại (nếu có)
                        db.Entry(existingProduct).State = EntityState.Detached;

                        // Attach thực thể mới từ cơ sở dữ liệu
                        db.Attach(product);
                        db.Entry(product).State = EntityState.Modified;
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
            int pageSize = 5;
            int pageNumber = 1;
            var products = db.Products.OrderByDescending(p => p.CreatedAt).Where(p => p.DeletedAt == false).Include(m => m.Cat).ToList();
            return View("~/Views/Admin/Product/listproduct.cshtml", products.ToPagedList(pageNumber, pageSize));
        }

        [Route("/admin/product/delete/{id}")]
        [Authentication]
        public IActionResult Delete(int id)
        {
            var item = db.Products.FirstOrDefault(o => o.Id == id);
            item.DeletedAt = true;
            db.SaveChanges();
            ViewBag.alert = "Sản phẩm đã được xóa!";
            int pageSize = 5;
            int pageNumber = 1;
            var products = db.Products.OrderByDescending(p => p.CreatedAt).Where(p => p.DeletedAt == false).Include(m => m.Cat).ToList();
            return View("~/Views/Admin/Product/listproduct.cshtml", products.ToPagedList(pageNumber, pageSize));
        }
    }
}
