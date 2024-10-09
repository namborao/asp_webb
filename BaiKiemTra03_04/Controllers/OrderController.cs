using BaiKiemTra03_04.Data;
using Microsoft.AspNetCore.Mvc;
using BaiKiemTra03_04.Models;
using Microsoft.EntityFrameworkCore;


namespace BaiKiemTra03_04.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách đơn hàng
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(o => o.Supplier).ToListAsync();
            return View(orders);
        }

        // Tạo mới đơn hàng
        public IActionResult Create()
        {
            ViewBag.Suppliers = _context.Suppliers.ToList(); // Lấy danh sách nhà cung cấp
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Suppliers = _context.Suppliers.ToList(); // Nếu có lỗi, tải lại danh sách nhà cung cấp
            return View(order);
        }

        // Chỉnh sửa đơn hàng
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Suppliers = _context.Suppliers.ToList(); // Lấy danh sách nhà cung cấp
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Suppliers = _context.Suppliers.ToList(); // Nếu có lỗi, tải lại danh sách nhà cung cấp
            return View(order);
        }

        // Xóa đơn hàng
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}