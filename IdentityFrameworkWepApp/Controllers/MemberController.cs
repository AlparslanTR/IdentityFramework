using IdentityFrameworkWepApp.Data;
using IdentityFrameworkWepApp.Dtos;
using IdentityFrameworkWepApp.Enums;
using IdentityFrameworkWepApp.Extenisons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace IdentityFrameworkWepApp.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IFileProvider _fileProvider;

        public MemberController(SignInManager<User> signInManager, UserManager<User> userManager, IFileProvider fileProvider)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _fileProvider = fileProvider;
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var user =
                new UserDto
                {
                    Email = currentUser.Email,
                    PhoneNumber = currentUser.PhoneNumber,
                    UserName = currentUser.UserName,
                    PictureUrl=currentUser.Picture
                };
            return View(user);
        }

        public IActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var checkOldPass = await _userManager.CheckPasswordAsync(currentUser, request.oldPassword);
            if (!checkOldPass)
            {
                ModelState.AddModelError(string.Empty, "Eski Şifreniz Yanlıştır.!");
                return View();
            }

            var result = await _userManager.ChangePasswordAsync(currentUser, request.oldPassword, request.newPassword);
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
            }

            await _userManager.UpdateSecurityStampAsync(currentUser); // kullanıcının SecurityStamp özelliği güncellenir.
            await _signInManager.SignOutAsync(); // mevcut kullanıcının oturumu kapatılır. Bu işlem, güncellenen güvenlik damgasının yürürlüğe girmesi için gereklidir.
            await _signInManager.PasswordSignInAsync(currentUser, request.newPassword, true, false); // Kullanıcının yeni şifresiyle oturum açılır. Son parametre true olursa son şifre değişikliliğinden sonra sistemden atılır yeni şifreyle sisteme girilmesi istenilir. 
            TempData["SuccessMessage"] = "Şifreniz Güncellenmiştir. ";

            return View();
        }
        public async Task <IActionResult> UserEdit()
        {
            var genderList = new SelectList(
                 Enum.GetValues(typeof(Gender))
                     .Cast<Gender>()
                     .Select(g => new SelectListItem
        {
            Text = g.ToString(),
            Value = ((int)g).ToString()
        })
         , "Value", "Text");

            ViewBag.gender = genderList;
            ViewBag.cityList = new SelectList(Enum.GetNames(typeof(CityList)));

            var currentUser= await _userManager.FindByNameAsync(User.Identity.Name);
            var getUserInfo = new UserEditDto()
            {
                UserName = currentUser.UserName,
                Age = currentUser.Age,
                BirthDate = currentUser.BirthDate,
                City = currentUser.City,
                Gender = currentUser.Gender,
                Mail = currentUser.Email,
                Phone = currentUser.PhoneNumber,
            };
            return View(getUserInfo);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentUser= await _userManager.FindByNameAsync(User.Identity.Name);
            currentUser.UserName= request.UserName;
            currentUser.Age= request.Age;
            currentUser.BirthDate= request.BirthDate;
            currentUser.City= request.City;
            currentUser.Gender= request.Gender;
            currentUser.Email = request.Mail;
            currentUser.PhoneNumber = request.Phone;

            

            if (request.Picture!=null && request.Picture.Length>0)
            {
                var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot"); // değişkene wwroot dizinini ata.
                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(request.Picture.FileName)}"; // Oluşturulan resimler adların çakışmaması için guid olarak tutulacak.
                var newPicturePath = Path.Combine(wwwrootFolder.First(x => x.Name == "Images").PhysicalPath, randomFileName); //yüklenen bir dosyanın wwwroot/Images dizinine kaydedilecek yolu belirler. Dosyanın yolu, PhysicalPath özelliği ve oluşturulan rastgele dosya adı olan randomFileName'in birleştirilmesi ile elde edilir.
                using var stream = new FileStream(newPicturePath, FileMode.Create);
                await request.Picture.CopyToAsync(stream);
                currentUser.Picture = randomFileName;
                stream.Close();
                await _userManager.UpdateAsync(currentUser);
            }

            var updateToUserResult= await _userManager.UpdateAsync(currentUser);
            if (!updateToUserResult.Succeeded)
            {
                 ModelState.AddModelErrorList(updateToUserResult.Errors.Select(x=>x.Description).ToList());
                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, true);

            TempData["SuccessMessage"] = "Bilgileriniz Güncellendi";

            var getUserInfo = new UserEditDto()
            {
                UserName = currentUser.UserName,
                Age = currentUser.Age,
                BirthDate = currentUser.BirthDate,
                City = currentUser.City,
                Gender = currentUser.Gender,
                Mail = currentUser.Email,
                Phone = currentUser.PhoneNumber,
            };
            return RedirectToAction("UserEdit", "Member");   
        }
    }
}
