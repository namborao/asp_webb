using BaiKiemTra03_04.Data;  
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Razor Pages với Runtime Compilation để tự động biên dịch lại khi có thay đổi
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Cấu hình chuỗi kết nối từ tệp appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Đăng ký ApplicationDbContext và sử dụng SQL Server cho cơ sở dữ liệu
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Thêm bộ lọc trang lỗi dành cho môi trường phát triển (Developer Page Exception Filter)
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Cấu hình dịch vụ Identity, yêu cầu tài khoản phải xác nhận email trước khi đăng nhập
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Thêm dịch vụ cho Controllers với Views (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình pipeline xử lý yêu cầu HTTP
if (app.Environment.IsDevelopment())
{
    // Trong môi trường phát triển, sử dụng trang Migration
    app.UseMigrationsEndPoint();
}
else
{
    // Trong môi trường sản xuất, sử dụng Exception Handler và kích hoạt HSTS
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Cấu hình để sử dụng HTTPS và phục vụ các tệp tĩnh
app.UseHttpsRedirection();
app.UseStaticFiles();

// Cấu hình Routing
app.UseRouting();

// Sử dụng xác thực (Authorization)
app.UseAuthorization();

// Cấu hình định tuyến cho Controllers và Views (MVC)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Thêm route tùy chỉnh cho Supplier
app.MapControllerRoute(
    name: "suppliers",
    pattern: "suppliers/{action=Index}/{id?}",
    defaults: new { controller = "Supplier" });

// Cấu hình cho Razor Pages
app.MapRazorPages();

// Chạy ứng dụng
app.Run();
