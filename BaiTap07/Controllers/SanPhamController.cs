using BaiTap07.Data;
using Microsoft.AspNetCore.Mvc;

namespace BaiTap07.Controllers
{
    [Area("admin")]
    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SanPhamController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {

            return View();
        }
    }
}
