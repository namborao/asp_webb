using BaiTapKiemTra01.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace BaiTapKiemTra01.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TaiKhoan()
        {

            return View();
        }
        public IActionResult DangKy(TaiKhoanViewModel model)
        {
            if (model != null && model.Password != null && model.Password.Length > 0)
            {
                model.Password = model.Password + "_da_ma_hoa";
            }
            if(model.Username != null)
            {
                return Content($"Tên tài khoản: {model.Username}\nHọ tên: {model.Name}\nTuổi: {model.Age}");

            }
            
            return View();
        }
        public IActionResult Done()
        {

            return View();
        }
    }
}
