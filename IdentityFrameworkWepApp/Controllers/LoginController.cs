using IdentityFrameworkWepApp.Data;
using IdentityFrameworkWepApp.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using IdentityFrameworkWepApp.Extenisons;
using IdentityFrameworkWepApp.Services;

namespace IdentityFrameworkWepApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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
                var user = await _userManager.FindByEmailAsync(request.Email);
                await _emailService.SendMailWelcomeMessage(user.Email);
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
        
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> ForgetPassword(ForgetPasswordDto request)
        {
            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Bu Mail Adresine Kayıtlı Kullanıcı Bulunamamıştır");
                return View();
            }
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            var passwordResetLink = Url.Action("ResetPassword", "Login", new { userId = hasUser.Id, Token = passwordResetToken },HttpContext.Request.Scheme,"localhost:7246");

            await _emailService.SendResetPasswordEmail(passwordResetLink, hasUser.Email);

            TempData["SuccessMessage"] = "Şifre Sıfırlama Linki Adresinize Gönderilmiştir";
            return RedirectToAction(nameof(ForgetPassword));
        }

        public IActionResult ResetPassword(string userId,string token)
        {
            TempData["userId"]=userId;
            TempData["token"]=token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
        {
            var userId = TempData["userId"].ToString();
            var token = TempData["token"].ToString();

            if (userId == null || token==null)
            {
                throw new Exception("Bir Hata Meydana Geldi");
            }

            var hasUser = await _userManager.FindByIdAsync(userId);

            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Kullanıcı Bulunamamıştır.!");
                return View();
            }

            var result= await _userManager.ResetPasswordAsync(hasUser,token,request.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Şifreniz Yenilenmiştir.";
            }

            else
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
            }
            return View();

        }
    }
}
