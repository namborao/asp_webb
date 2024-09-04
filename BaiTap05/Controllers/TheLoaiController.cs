using BaiTap05.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTap05.Controllers
{
    public class TheLoaiController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Ngay = "Ngày 1";
            ViewBag.Thang = "thang 1";
            ViewData["Nam"] = "Nam 2020";
            return View();
        }
        public IActionResult Index2()
        {
            var theloai = new TheLoaiViewModel
            {
                Id = 1,
                Name = "Trinh tham"
            };

            return View(theloai);
        }
    }
}
