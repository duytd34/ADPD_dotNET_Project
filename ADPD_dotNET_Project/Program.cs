using ADPD_dotNET_Project.Data;
using Microsoft.EntityFrameworkCore;
using ADPD_dotNET_Project.Facade;
using ADPD_dotNET_Project.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

// Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Dịch vụ để truy cập HttpContext (dùng cho session)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Scoped DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserFacade, UserFacade>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseFacade, CourseFacade>();

// Cấu hình xác thực bằng cookie (nếu cần về sau)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login"; // trang login nếu chưa đăng nhập
        options.AccessDeniedPath = "/User/AccessDenied"; // trang bị từ chối nếu không đủ quyền
    });

var app = builder.Build();

// Bắt đầu session
app.UseSession();

// Middleware xử lý lỗi khi không ở môi trường dev
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // <- Đăng nhập
app.UseAuthorization();  // <- Kiểm tra quyền

// Định tuyến mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
