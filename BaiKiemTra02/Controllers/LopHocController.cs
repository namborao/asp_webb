using BaiKiemTra02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BaiKiemTra02.Controllers
{
    public class LopHocController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Sử dụng Dependency Injection để inject DbContext
        public LopHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LopHoc
        public async Task<IActionResult> Index()
        {
            var lopHocs = await _context.LopHocs.ToListAsync();
            return View(lopHocs);
        }

        // GET: LopHoc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();  // Trả về BadRequest nếu id là null
            }

            var lopHoc = await _context.LopHocs.FindAsync(id);
            if (lopHoc == null)
            {
                return NotFound();  // Trả về NotFound nếu không tìm thấy
            }

            return View(lopHoc);
        }

        // GET: LopHoc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LopHoc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenLopHoc,NamNhapHoc,NamRaTruong,SoLuongSinhVien")] LopHoc lopHoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lopHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(lopHoc);
        }

        // GET: LopHoc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var lopHoc = await _context.LopHocs.FindAsync(id);
            if (lopHoc == null)
            {
                return NotFound();
            }

            return View(lopHoc);
        }

        // POST: LopHoc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenLopHoc,NamNhapHoc,NamRaTruong,SoLuongSinhVien")] LopHoc lopHoc)
        {
            if (id != lopHoc.Id)
            {
                return NotFound();  // Kiểm tra id có khớp không
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lopHoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LopHocExists(lopHoc.Id))
                    {
                        return NotFound();  // Kiểm tra tồn tại của LopHoc
                    }
                    throw;  // Ném lại lỗi nếu không tìm thấy
                }

                return RedirectToAction(nameof(Index));
            }

            return View(lopHoc);  // Trả về view nếu không hợp lệ
        }

        // GET: LopHoc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var lopHoc = await _context.LopHocs.FindAsync(id);
            if (lopHoc == null)
            {
                return NotFound();
            }

            return View(lopHoc);
        }

        // POST: LopHoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lopHoc = await _context.LopHocs.FindAsync(id);
            if (lopHoc == null)
            {
                return NotFound();  // Kiểm tra nếu lopHoc không tồn tại
            }

            _context.LopHocs.Remove(lopHoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LopHocExists(int id)
        {
            return _context.LopHocs.Any(e => e.Id == id);  // Kiểm tra tồn tại của LopHoc
        }
    }
}
