using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityFrameworkWepApp.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "Yeni Şifre :")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifreler Uyuşmuyor.!")]
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "Yeni Şifre Tekrar :")]
        public string PasswordConfirm { get; set; }
    }
}
