using AppUtbildning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Databas
builder.Services.AddDbContext<RegisterDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inloggning med ASP.NET Identity (användare och roller sparas i samma databas)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<RegisterDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Skicka till profilsidan när man måste logga in
    options.LoginPath = "/Home/Profil";
    options.AccessDeniedPath = "/Home/Profil";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RegisterDbContext>();
    db.Database.EnsureCreated();

    await SeedData.SeedAsync(scope.ServiceProvider, app.Configuration);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Några delar är från föreläsningar och andras ramverk, men när jag inte kunde starta programmet tog jag hjälp av AI för att hitta felet
