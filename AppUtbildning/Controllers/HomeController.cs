using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppUtbildning.Models;

namespace AppUtbildning.Controllers;

public class HomeController : Controller
{
    private readonly RegisterDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(RegisterDbContext db, UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    public IActionResult Index() => View("hemsida");

    public IActionResult microneedling() => View();

    public IActionResult fillers() => View();

    public IActionResult botox() => View();

    public IActionResult omoss() => View();

    // Admin får en översikt, studenter och utloggade får den vanliga profilsidan
    public async Task<IActionResult> profil()
    {
        if (!User.IsInRole(SeedData.AdminRole))
            return View();

        var overview = new AdminOverview
        {
            TotalRegistrations = _db.Registers.Count(),
            StudentAccounts = (await _userManager.GetUsersInRoleAsync(SeedData.StudentRole)).Count,
            RegistrationsPerCourse = Register.Courses.ToDictionary(
                c => c,
                c => _db.Registers.Count(r => r.Course == c)),
            Latest = _db.Registers.OrderByDescending(r => r.Id).Take(3).ToList()
        };

        return View(overview);
    }

}

//Föreläsning,handledning,kompisars ramverk
