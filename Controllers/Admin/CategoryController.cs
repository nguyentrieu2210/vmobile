using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using vphone.Models;
using X.PagedList;

namespace vphone.Controllers.Admin
{
	public class CategoryController : Controller
	{
		private QLQuanDTContext db;

		public CategoryController(QLQuanDTContext context)
		{
			this.db = context;
		}

		[Route("/admin/category/{page?}")]
		public IActionResult Index(int? page)
		{
			if (page == null) page = 1;
			int pageSize = 5;
			int pageNumber = (page ?? 1);
			var categories = db.Categories.ToList();
			ViewBag.Msg = TempData["msg"];
			ViewBag.Err = TempData["err"];
			return View("~/Views/Admin/Category/category.cshtml", categories.ToPagedList(pageNumber, pageSize));
		}
		[Route("/admin/category/add")]
		public IActionResult Add()
		{
			return View("~/Views/Admin/Category/addcategory.cshtml");
		}
		[HttpPost("/admin/category/add")]
		[ValidateAntiForgeryToken]
		public IActionResult Add([Bind("Title", "Description")] Category category)
		{

			if (CategoryExist(category.Title) == false)
			{
				if (ModelState.GetFieldValidationState("Title") == ModelValidationState.Valid && ModelState.GetFieldValidationState("Description") == ModelValidationState.Valid)
				{
					TempData["msg"] = "Đã thêm " + category.Title + " thành công!";
					category.UserId = 1;
					category.Slug = category.Title.Trim().Replace(" ", "-");
					db.Categories.Add(category);
					db.SaveChanges();
					return RedirectToAction(nameof(Index));
				}
			}
			else
			{
				ViewBag.Msg = "Danh mục đã tồn tại";
			}
			return View("~/Views/Admin/Category/addcategory.cshtml", category);
		}

		[Route("/admin/category/edit/{id}")]
		public IActionResult Edit(int id)
		{
			if (db.Categories == null)
			{
				return NotFound();
			}
			var category = db.Categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}

			return View("~/Views/Admin/Category/editcategory.cshtml", category);
		}
		[HttpPost("/admin/category/edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id", "Title", "Description", "UserId")] Category category)
		{
			if (id != category.Id)
			{
				return NotFound();
			}
			var CheckE = (db.Categories.Except(db.Categories.Where(l => l.Id == category.Id))).Any(l => l.Title == category.Title);
			if (CheckE == false)
			{
				if (ModelState.GetFieldValidationState("Title") == ModelValidationState.Valid && ModelState.GetFieldValidationState("Description") == ModelValidationState.Valid)
				{
					category.UserId = 1;
					category.Slug = category.Title.Trim().Replace(" ", "-");
					db.Update(category);
					db.SaveChanges();
					TempData["msg"] = "Đã cập nhật thành công";
					return RedirectToAction(nameof(Index));
				}
			}
			else
			{
				ViewBag.Msg = "Danh mục " + category.Title + " đã tồn tại!";
			}
			return View("~/Views/Admin/Category/editcategory.cshtml", category);
		}
		[Route("/admin/category/delete/{id}")]
		public IActionResult Delete(int id)
		{
			if (db.Categories == null)
			{
				return NotFound();
			}
			var category = db.Categories.Include(m => m.Products).FirstOrDefault(m => m.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			if (category.Products.Count > 0)
			{
				TempData["err"] = "Danh mục này có chứa sản phẩm. Không thể xóa!";
				return RedirectToAction(nameof(Index));
			}
			return View("~/Views/Admin/Category/deletecategory.cshtml", category);
		}
		[HttpPost("/admin/category/delete/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteComfirmed(int id)
		{
			if (db.Categories == null)
			{
				return Problem("Danh mục trống");
			}
			var category = db.Categories.Find(id);
			if (category != null)
			{
				db.Categories.Remove(category);
				TempData["msg"] = "Đã xóa thành công!";
			}
			db.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public bool CategoryExist(string Title)
		{
			return (db.Categories.Any(l => l.Title == Title));
		}
	}
}