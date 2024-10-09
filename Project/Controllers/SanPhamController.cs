using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
	[Area("Admin")]
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
		public IActionResult Upsert(int id)
		{
			SanPham sanpham = new SanPham();
			IEnumerable<SelectListItem> dstheloai = _db.TheLoai.Select(
				item=> new SelectListItem
				{
					Value=item.ToString(),
					Text=item.Name,
				}
				);
			ViewBag.DsTheLoai = dstheloai;
			if(id == 0)
			{
				//create / Insert
				return View(sanpham);
			}
			else
			{
				// Edit / Update
				sanpham = _db.Sanpham.Include("TheLoai").FirstOrDefault(sp=>sp.Id==id);
				return View(sanpham);
			}
		}
		[HttpPost]
		public IActionResult Upsert(SanPham sanpham)
		{
			if(ModelState.IsValid) 
			{
			if (sanpham.Id == 0) 
			{
				// thêm thông tin vào bảng theloai
				_db.Sanpham.Add(sanpham);
			}
			else 
			{
				_db.Sanpham.Update(sanpham);
			}
			// lưu
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
			return View();
        }
    }
}
