using IdentityFrameworkWepApp.Data;
using IdentityFrameworkWepApp.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityFrameworkWepApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;

        public LoginController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> SignUp(SignUpDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var identityResult= await _userManager.CreateAsync(new() { UserName = request.UserName, Email = request.Email, PhoneNumber = request.Phone, NormalizedEmail = request.Email }, request.PasswordConfirm);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Kayıt Başarılı. Otomatik Yönlendirme Aktif";
                return RedirectToAction(nameof(LoginController.SignUp));
            }

            foreach (IdentityError item in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return View();
        }
    }
}
