using Project.Data;
using Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Project.Controllers
{
    [Area("Admin")]
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TheLoaiController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var theloai = _db.TheLoai.ToList();
            ViewBag.theloai = theloai;
            return View();
        }
        [HttpGet]
		public IActionResult Create()
		{
			
			return View();
		}
		[HttpPost]
		public IActionResult Create(TheLoai theloai)
		{
            if (ModelState.IsValid)
            {
                // thêm thông tin vào bảng theloai
                _db.TheLoai.Add(theloai);
                // lưu
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
			return View();
		}
		[HttpGet]
		public IActionResult Edit(int Id)
		{
            if(Id == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(Id);
			return View(theloai);
		}
		[HttpPost]
		public IActionResult Edit(TheLoai theloai)
		{
			if (ModelState.IsValid)
			{
				// thêm thông tin vào bảng theloai
				_db.TheLoai.Update(theloai);
				// lưu
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View();
		}
		[HttpGet]
		public IActionResult Delete(int Id)
		{
			if (Id == 0)
			{
				return NotFound();
			}
			var theloai = _db.TheLoai.Find(Id);
			return View(theloai);
		}
		[HttpPost]
		public IActionResult DeleteConfirm(TheLoai id)
		{
			var theloai = _db.TheLoai.Find(id);
			if(theloai == null)
			{
				return NotFound();
			}
			_db.TheLoai.Remove(theloai);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult Detail(int Id)
		{
			if (Id == 0)
			{
				return NotFound();
			}
			var theloai = _db.TheLoai.Find(Id);
			return View(theloai);
		}
		[HttpGet]
		public IActionResult Search(String searchString)
		{
			if (!string.IsNullOrEmpty(searchString))
			{
				// sử dụng LINQ để tìm kiếm
				var theloai = _db.TheLoai.
					Where(tl => tl.Name.Contains(searchString)).ToList();

				ViewBag.SearchString = searchString;
				ViewBag.TheLoai = theloai;
			}
			else
			{
				var theloai = _db.TheLoai.ToList();
				ViewBag.TheLoai = theloai;
			}

			return View("Index"); // sử dụng lại View Index
		}
	}
}
