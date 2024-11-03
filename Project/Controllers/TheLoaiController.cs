using Project.Data;
using Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        public IActionResult Edit(int theLoaiId) // Đổi tên tham số thành theLoaiId để tránh trùng với thuộc tính Id
        {
            if (theLoaiId == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(theLoaiId);
            return View(theloai);
        }

        [HttpPost]
        public IActionResult Edit(TheLoai theloai)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật thông tin trong bảng theloai
                _db.TheLoai.Update(theloai);
                // lưu
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theloai);
        }

        [HttpGet]
        public IActionResult Delete(int theLoaiId) // Đổi tên tham số thành theLoaiId để tránh trùng với thuộc tính Id
        {
            if (theLoaiId == 0)
            {
                return NotFound();
            }
            var theloai = _db.TheLoai.Find(theLoaiId);
            return View(theloai);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int theLoaiId) // Sửa tham số thành int theLoaiId thay vì TheLoai
        {
            var theloai = _db.TheLoai.Find(theLoaiId);
            if (theloai == null)
            {
                return NotFound();
            }
            _db.TheLoai.Remove(theloai);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var theloai = _db.TheLoai.Find(id);
            return View(theloai);
        }

        [HttpGet]
        public IActionResult Search(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                // Sử dụng LINQ để tìm kiếm
                var theloai = _db.TheLoai
                    .Where(tl => tl.Name.Contains(searchString))
                    .ToList();

                ViewBag.SearchString = searchString;
                ViewBag.TheLoai = theloai;
            }
            else
            {
                var theloai = _db.TheLoai.ToList();
                ViewBag.TheLoai = theloai;
            }

            return View("Index"); // Sử dụng lại View Index
        }

    }
}
