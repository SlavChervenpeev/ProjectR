using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System_for_notebook_management.Models;
using System_for_notebook_management.Data;

var builder = WebApplication.CreateBuilder(args);

// Добавяне на DbContext с връзка към базата данни
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Добавяне на Identity със задаване на ролите и конфигурация за потвърждение на акаунта
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Добавяне на роли
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Добавяне на контролери и изгледи
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Инициализиране на ролите и потребителите при стартиране
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData.Initialize(services, userManager, roleManager);
}

// Конфигуриране на HTTP заявките
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Конфигурация на маршрутите
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
