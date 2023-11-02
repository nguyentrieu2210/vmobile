using Microsoft.AspNetCore.Mvc;
using vphone.Models;
using vphone.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nop.Core.Domain.Catalog;
using X.PagedList;
using vphone.Models.Authentication;
//using System.Web.Mvc;

namespace vphone.Controllers.Admin
{
	public class UserController : Controller
	{
		private QLQuanDTContext db;

		public UserController(QLQuanDTContext db)
		{
			this.db = db;
		}
		[Route("/admin/user/{page?}")]
		[Authentication]
		public IActionResult Index(int? page)
		{
			if (page == null) page = 1;
			int pageSize = 5;
			int pageNumber = (page ?? 1);
			var users = db.Users
				.Where(e => e.DeletedAt == false)
				.ToList();

			ViewBag.Msg = TempData["msg"];
			ViewBag.Err = TempData["err"];
			return View("~/Views/Admin/User/listuser.cshtml", users.ToPagedList(pageNumber, pageSize));
		}
		[Route("/admin/user/add")]
		[Authentication]
		public IActionResult Add()
		{
			var user = new List<SelectListItem>();
			foreach (var item in db.Users)
			{
				user.Add(new SelectListItem
				{
					Text = item.Name,
					Value = item.Id.ToString()
				});
			}
			ViewBag.Id = user;
			ViewBag.Id = new SelectList(db.Users, "Id", "Name");
			return View("~/views/admin/user/adduser.cshtml");
		}
		[HttpPost("/admin/user/add")]
		[ValidateAntiForgeryToken]
		public IActionResult Add([Bind("Email,Password,Name,Address,Phone")] User user)
		{
			try
			{
				if (ModelState.IsValid)
				{
					db.Users.Add(user);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				ViewBag.Msg = "Email đã tồn tại";
			}
			ViewBag.Id = new SelectList(db.Users, "Id", "Name");
			return View("~/Views/Admin/User/adduser.cshtml", user);
		}
		[Route("/admin/user/edit/{id}")]
		[Authentication]

		public IActionResult Edit(int id)
		{
			if (id == null || db.Users == null)
			{
				return NotFound();
			}
			var user = db.Users.Find(id);
			if (user == null)
			{
				return NotFound();
			}
			ViewBag.Id = new SelectList(db.Users, "Id", "Name", user.Id);
			return View("~/Views/Admin/User/edituser.cshtml", user);
		}

		[HttpPost("/admin/user/edit/{id}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,Email,Password,Name,Address,Phone")] User user)
		{
			try
			{
				if (id != user.Id)
				{
					return NotFound();
				}
				if (ModelState.IsValid)
				{
					try
					{
						db.Update(user);
						db.SaveChanges();
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!UserExists(user.Id))
						{
							return NotFound();
						}
						else
						{
							throw;
						}
					}
					return RedirectToAction(nameof(Index));
				}
			}
			catch (Exception ex)
			{
				ViewBag.Msg = "Email đã tồn tại";
			}
			ViewBag.Id = new SelectList(db.Users, "Id", "Name", user.Id);
			return View("~/Views/Admin/User/edituser.cshtml", user);
		}
		private bool UserExists(int id)
		{
			return (db.Users?.Any(e => e.Id == id)).GetValueOrDefault();
		}
		public async Task<IActionResult> Delete(int id)
		{
			var user = await db.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			user.DeletedAt = true;
			db.Update(user);
			await db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}