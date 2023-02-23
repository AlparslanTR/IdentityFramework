using IdentityFrameworkWepApp.Data;
using IdentityFrameworkWepApp.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using IdentityFrameworkWepApp.Extenisons;

namespace IdentityFrameworkWepApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                
            ModelState.AddModelErrorList(identityResult.Errors.Select(x=>x.Description).ToList());   
            return View();
        }
       
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> SignIn(SignInDto request, string returnUrl=null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home"); //  Eğer değişkenin değeri null ise, kod sağ tarafındaki Url.Action("Index", "Home") metodunu çalıştırarak "returnUrl" değişkenine varsayılan bir değer atar.

            var isUser=await _userManager.FindByEmailAsync(request.Email);
            if (isUser == null)
            {
                ModelState.AddModelError(string.Empty, "Mail Adresiniz veya Şifreniz Yanlış.!");
                return View();
            }

            var result =await _signInManager.PasswordSignInAsync(isUser, request.Password, request.RememberMe,true);

            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "Çok Fazla Deneme Girişinde Bulundunuz 3 Dakika Sonra Tekrar Deneyin" });
                return View();
            }


            ModelState.AddModelErrorList(new List<string>() {$"Mail Adresiniz veya Şifreniz Yanlış(Başarısız Giriş Sayısı : {await _userManager.GetAccessFailedCountAsync(isUser)} , 3 Hakkınız Bulunmaktadır. )" });
            return View();
        }
    }
}
