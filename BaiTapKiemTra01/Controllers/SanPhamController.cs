using BaiTapKiemTra01.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapKiemTra01.Controllers
{
    public class SanPhamController : Controller
    {
        private const string V = "30000000";
        private const string V1 = " ip16";

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BaiTap2()
        {
            var sanpham = new SanPhamViewModel()
            {
                TenSP = V1,
                Gia = V,
                Anh = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fphone-16.com%2Fiphone-16-pro.html&psig=AOvVaw0ZJGyoXo3UBegmwi9aXGF2&ust=1726386440089000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCMjtv9L6wYgDFQAAAAAdAAAAABAE"
            };
            return View(sanpham);
        }
        
    }
}
