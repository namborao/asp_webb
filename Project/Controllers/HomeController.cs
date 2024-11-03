using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Project.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _db;
		
		public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
		{
			_logger = logger;
			_db = db;
		}

		public IActionResult Index()
		{
			IEnumerable<SanPham> sanpham = _db.Sanpham.Include("TheLoai").ToList();
			return View(sanpham);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
        [HttpGet]
        public IActionResult Details(int sanphamid)
        {
            GioHang giohang = new GioHang()
            {
                SanPhamId = sanphamid,
                SanPham = _db.Sanpham.Include("TheLoai").FirstOrDefault(sp => sp.Id == sanphamid),
                Quantity = 1
            };

            return View(giohang);
        }

        [HttpPost]
        [Authorize] // Yêu cầu đăng nhập
        public IActionResult Details(GioHang giohang)
        {
            // Lấy thông tin người dùng hiện tại
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            giohang.ApplicationUserId = claim.Value; // Gán UserId của người dùng cho đối tượng giohang

            // Kiểm tra xem sản phẩm có trong giỏ hàng của người dùng hay chưa
            var giohangdb = _db.GioHang.FirstOrDefault(sp => sp.SanPhamId == giohang.SanPhamId
                && sp.ApplicationUserId == giohang.ApplicationUserId);

            if (giohangdb == null)
            {
                // Nếu chưa có, thêm sản phẩm mới vào giỏ hàng
                _db.GioHang.Add(giohang);
            }
            else
            {
                // Nếu đã có, cập nhật số lượng sản phẩm trong giỏ hàng
                giohangdb.Quantity += giohang.Quantity;
            }

            // Lưu các thay đổi vào cơ sở dữ liệu
            _db.SaveChanges();

            // Chuyển hướng về trang Index sau khi xử lý xong
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult FilterByTheLoai(int id)
        {
            IEnumerable<SanPham> sanpham = _db.Sanpham.Include("TheLoai").Where(sp => sp.TheLoai.Id == id).ToList();
            return View("Index", sanpham);
        }
    }
}
