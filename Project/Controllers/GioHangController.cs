using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Security.Claims;

namespace Project.Controllers
{
    [Area("Customer")]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GioHangController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Lấy thông tin đăng nhập
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                // Nếu không có thông tin claim, có thể chuyển hướng người dùng đến trang đăng nhập
                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách sản phẩm trong giỏ hàng của user
            GioHangViewModel giohang = new GioHangViewModel
            {
                DsGioHang = _db.GioHang.Include("SanPham")
                    .Where(gh => gh.ApplicationUserId == claim.Value)
                    .ToList(),
                HoaDon = new HoaDon()
            };

            foreach (var item in giohang.DsGioHang)
            {
                // Tính tiền theo số lượng sản phẩm
                item.ProductPrice = item.Quantity * item.SanPham.price;
                // Tính tổng số tiền trong giỏ hàng
                giohang.HoaDon.Total += item.ProductPrice;
            }

            return View(giohang);
        }

        [Authorize]
        public IActionResult ThanhToan()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            GioHangViewModel giohang = new GioHangViewModel
            {
                DsGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList(),
                HoaDon = new HoaDon()
            };

            giohang.HoaDon.ApplicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == claim.Value);
            giohang.HoaDon.Name = giohang.HoaDon.ApplicationUser?.Name ?? "";
            giohang.HoaDon.Address = giohang.HoaDon.ApplicationUser?.Address ?? "";
            giohang.HoaDon.PhoneNumber = giohang.HoaDon.ApplicationUser?.PhoneNumber ?? "";

            foreach (var item in giohang.DsGioHang)
            {
                double prodcutprice = item.Quantity * item.SanPham.price;
                giohang.HoaDon.Total += prodcutprice;
            }

            return View(giohang);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult ThanhToan(GioHangViewModel giohang)
        {
            // Lấy thông tin tài khoản
            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized(); // Trả về Unauthorized nếu identity là null
            }

            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null || string.IsNullOrEmpty(claim.Value))
            {
                return Unauthorized(); // Trả về Unauthorized nếu claim là null hoặc không có giá trị
            }

            // Tìm danh sách sản phẩm trong giỏ hàng của người dùng
            giohang.DsGioHang = _db.GioHang.Include("SanPham")
                .Where(gh => gh.ApplicationUserId == claim.Value)
                .ToList();

            giohang.HoaDon.ApplicationUserId = claim.Value;
            giohang.HoaDon.OrderDate = DateTime.Now;
            giohang.HoaDon.OrderStatus = "Đang xác nhận";

            foreach (var item in giohang.DsGioHang)
            {
                // Tính tiền sản phẩm theo số lượng
                double productPrice = item.Quantity * item.SanPham?.price ?? 0; // Sử dụng toán tử ?? để xử lý null cho SanPham hoặc price
                giohang.HoaDon.Total += productPrice;
            }

            // Thêm hóa đơn vào cơ sở dữ liệu và lưu thay đổi
            _db.HoaDon.Add(giohang.HoaDon);
            _db.SaveChanges();

            // Thêm thông tin chi tiết hóa đơn
            foreach (var item in giohang.DsGioHang)
            {
                ChiTietHoaDon chitiethoadon = new ChiTietHoaDon()
                {
                    SanPhamId = item.SanPhamId,
                    HoaDonId = giohang.HoaDon.Id,
                    ProductPrice = item.SanPham?.price * item.Quantity ?? 0, // Xử lý null cho SanPham hoặc price
                    Quantity = item.Quantity
                };
                _db.ChiTietHoaDon.Add(chitiethoadon);
            }
            _db.SaveChanges();

            // Xóa thông tin trong giỏ hàng
            _db.GioHang.RemoveRange(giohang.DsGioHang);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Xoa(int giohangId)
        {
            var gioHang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);

            _db.GioHang.Remove(gioHang);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Tang(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);

            if (giohang == null)
            {
                // Xử lý nếu không tìm thấy giỏ hàng, có thể trả về NotFound hoặc thông báo lỗi
                return NotFound();
            }

            giohang.Quantity++;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Giam(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);

            giohang.Quantity--;

            if (giohang.Quantity == 0)
            {
                { _db.GioHang.Remove(giohang); }
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
