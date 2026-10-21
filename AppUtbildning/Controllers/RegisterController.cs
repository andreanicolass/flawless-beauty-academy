using AppUtbildning.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUtbildning.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegisterDbContext _db;

        public RegisterController(RegisterDbContext db)
        {
            _db = db;
        }

        // Här listas anmälningarna. Endast inloggade med rollen Admin kommer åt sidan.
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var persons = _db.Registers
                .OrderByDescending(r => r.Id)
                .ToList();

            return View(persons);
        }

        // Här visas formuläret för anmälan
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Register());
        }

        // Här sparas anmälan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Register model)
        {
            if (model.Course != null && !Register.Courses.Contains(model.Course))
                ModelState.AddModelError(nameof(model.Course), "Välj en utbildning i listan.");

            if (!ModelState.IsValid)
            {
                // Kom anmälan från en kurssida? Visa den sidan igen med felen, så hamnar man inte på en annan sida.
                if (model.Course != null && Register.Courses.Contains(model.Course))
                    return View($"~/Views/Home/{model.Course.ToLowerInvariant()}.cshtml", model);

                return View(model);
            }

            model.CreatedAt = DateTime.UtcNow;
            _db.Registers.Add(model);
            _db.SaveChanges();

            TempData["Course"] = model.Course; // visas på bekräftelsesidan
            return RedirectToAction(nameof(Konfirmation));
        }

        // Det här är bekräftelsen man får efter man skickat in sin anmälan
        public IActionResult Konfirmation()
        {
            return View("Konfirmation");
        }
    }
}

// Lite av koden var med hjälp av föreläsningar, handledning och hjölp från klasskamrater,
// däremot så stötte jag på vissa problem som jag tog hjälp av AI.
