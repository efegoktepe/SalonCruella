using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalonCruella.Data;

var builder = WebApplication.CreateBuilder(args);

// MVC ve View desteği
builder.Services.AddControllersWithViews();

// DbContext yapılandırması
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity yapılandırması
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = true; // En az bir rakam
    options.Password.RequiredLength = 8; // Minimum 8 karakter
    options.Password.RequireNonAlphanumeric = false; // Özel karakter gereksinimi yok
    options.Password.RequireUppercase = true; // En az bir büyük harf
    options.Password.RequireLowercase = true; // En az bir küçük harf
})
.AddRoles<IdentityRole>() // Rol desteği
.AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Hata ayıklama ve HSTS (sadece production için)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware yapılandırması
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Varsayılan route yapılandırması
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Veritabanı Migration ve Seed işlemleri
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Migration'ları uygula
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); // Migration'ları uygular

        // Seed işlemini başlat
        await SeedData.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata: {ex.Message}");
        Console.WriteLine(ex.StackTrace);
    }
}

app.Run();
