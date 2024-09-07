using Microsoft.AspNetCore.Mvc;

namespace BaiTapVeNha02.Controllers
{
    public class Tuan02Controller : Controller
    {
        public ActionResult Index()
        {
            ViewBag.HoTen = "Tran Hoai Nam";
            ViewBag.MSSV = "1822040472";
            ViewBag.Nam = 2024;

            return View();
        }
    }
}
