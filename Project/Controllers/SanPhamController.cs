using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
	public class SanPhamController : Controller
	{
		private readonly ApplicationDbContext _db;
		public SanPhamController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			//lấy thông tin trong bảng sản phẩm và bao gồm thêm thông tin bảng thể loại
			IEnumerable<SanPham> sanpham = _db.Sanpham.Include("TheLoai").ToList();
			return View(sanpham);
		}
		[HttpGet]
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            SanPham sanpham = new SanPham();
            // Thiết lập danh sách thể loại cho dropdown
            IEnumerable<SelectListItem> dstheloai = _db.TheLoai.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(), // Sửa lỗi này
                    Text = item.Name,
                }
            );
            ViewBag.DsTheLoai = dstheloai;

            if (id == 0)
            {
                // Tạo mới sản phẩm
                return View(sanpham);
            }
            else
            {
                // Chỉnh sửa sản phẩm
                sanpham = _db.Sanpham.Include("TheLoai").FirstOrDefault(sp => sp.Id == id);
                if (sanpham == null)
                {
                    return NotFound(); // Nếu sản phẩm không tồn tại
                }
                return View(sanpham);
            }
        }

        [HttpPost]
        public IActionResult Upsert(SanPham sanpham)
        {
            if (ModelState.IsValid)
            {
                if (sanpham.Id == 0)
                {
                    // Thêm sản phẩm mới
                    _db.Sanpham.Add(sanpham);
                }
                else
                {
                    // Cập nhật sản phẩm
                    _db.Sanpham.Update(sanpham);
                }
                // Lưu thay đổi
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Nếu ModelState không hợp lệ, thiết lập lại ViewBag.DsTheLoai
            IEnumerable<SelectListItem> dstheloai = _db.TheLoai.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                }
            );
            ViewBag.DsTheLoai = dstheloai;

            // Trả về view cùng với thông tin sản phẩm đã nhập
            return View(sanpham);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var sanpham = _db.Sanpham.FirstOrDefault(sp => sp.Id == id);
            if (sanpham == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm." });
            }

            _db.Sanpham.Remove(sanpham);
            _db.SaveChanges();

            return Json(new { success = true, message = "Xóa thành công." });
        }


    }
}
