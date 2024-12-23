using Microsoft.AspNetCore.Identity;

namespace SalonCruella.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Rolleri oluştur
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Admin kullanıcıyı oluştur
            var adminUser = await userManager.FindByEmailAsync("OgrenciNumarasi@sakarya.edu.tr");
            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "OgrenciNumarasi@sakarya.edu.tr",
                    Email = "OgrenciNumarasi@sakarya.edu.tr",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "sau"); // Varsayılan şifre
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
