using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalonCruella.Models;

namespace SalonCruella.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            // Veritabanını oluştur ve en son migrationları uygula
            context.Database.EnsureCreated();

            // Rolleri oluştur
            string adminRole = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Admin kullanıcıyı oluştur
            string adminEmail = "B211210052@sakarya.edu.tr";
            string adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, adminPassword);
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }

            // Salonları ekle
            if (!context.Salonlar.Any())
            {
                context.Salonlar.AddRange(
                    new Salon
                    {
                        Id =1,
                        Adi = "Salon Cruella Atatürk Şubesi",
                        Adres = "Atatürk Mahallesi, Gül Caddesi No: 28, Kat: 2, Daire: 10, Beylikdüzü, İstanbul"
                    },
                    new Salon
                    {
                        Id =2,
                        Adi = "Salon Cruella Bahçelievler",
                        Adres = "Bahçelievler Mahallesi, Papatya Sokak No: 16, Kat: 4, Daire: 12, Kadıköy, İstanbul"
                    },
                    new Salon
                    {
                        Id =3,
                        Adi = "Salon Cruella Cekmeköy",
                        Adres = "Çekmeköy Mahallesi, Defne Caddesi No: 5, Kat: 1, Daire: 3, Çekmeköy, İstanbul"
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
