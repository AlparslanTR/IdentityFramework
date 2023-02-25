using IdentityFrameworkWepApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityFrameworkWepApp.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public MemberController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task <IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
