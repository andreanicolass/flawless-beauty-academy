using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace AppUtbildning.Models;

// Fyller databasen med ett admin-konto och några studentkonton så att det finns något att visa.
// Uppgifterna läses från appsettings.Development.json ("Admin" och "Students").
public static class SeedData
{
    public const string AdminRole = "Admin";
    public const string StudentRole = "Student";

    // Egna claims som sparas på användaren och följer med vid inloggning
    public const string FullNameClaim = "FullName";
    public const string CourseClaim = "Course";

    public static async Task SeedAsync(IServiceProvider services, IConfiguration config)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var db = services.GetRequiredService<RegisterDbContext>();

        foreach (var role in new[] { AdminRole, StudentRole })
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Admin
        var adminEmail = config["Admin:Email"];
        var adminPassword = config["Admin:Password"];
        if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
            await CreateUserAsync(userManager, adminEmail, adminPassword, AdminRole, "Admin", null);

        // Studenter
        var students = config.GetSection("Students").Get<List<StudentSeed>>() ?? new();
        foreach (var s in students)
        {
            await CreateUserAsync(userManager, s.Email, s.Password, StudentRole, s.Name, s.Course);

            // Lägg även in en anmälan för studenten så att admin ser den i listan
            if (!db.Registers.Any(r => r.Email == s.Email))
            {
                var nameParts = s.Name.Split(' ', 2);
                db.Registers.Add(new Register
                {
                    FirstName = nameParts[0],
                    LastName = nameParts.Length > 1 ? nameParts[1] : "",
                    Address = s.Address,
                    Email = s.Email,
                    Phone = s.Phone,
                    Course = s.Course
                });
            }
        }
        await db.SaveChangesAsync();
    }

    private static async Task CreateUserAsync(UserManager<IdentityUser> userManager,
        string email, string password, string role, string fullName, string? course)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return;

            await userManager.AddClaimAsync(user, new Claim(FullNameClaim, fullName));
            if (course != null)
                await userManager.AddClaimAsync(user, new Claim(CourseClaim, course));
        }

        if (!await userManager.IsInRoleAsync(user, role))
            await userManager.AddToRoleAsync(user, role);
    }

    public class StudentSeed
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Course { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}
