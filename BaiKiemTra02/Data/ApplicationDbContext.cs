using BaiKiemTra02.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaiKiemTra02.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> // Sử dụng IdentityDbContext nếu bạn dùng Identity
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<LopHoc> LopHocs { get; set; } // Định nghĩa DbSet cho LopHoc
    }
}
