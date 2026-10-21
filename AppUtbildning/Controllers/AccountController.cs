using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppUtbildning.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // Inloggningen kontrolleras på servern, så lösenordet syns aldrig i webbläsarens kod
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe, string? returnUrl)
        {
            var result = await _signInManager.PasswordSignInAsync(
                email ?? "", password ?? "", rememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                TempData["LoginError"] = "Fel e-post eller lösenord.";
                return RedirectToAction("Profil", "Home", new { returnUrl });
            }

            // Skicka tillbaka till sidan man försökte nå (t.ex. anmälningslistan)
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Profil", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Profil", "Home");
        }
    }
}
