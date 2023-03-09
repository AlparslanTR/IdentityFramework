using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityFrameworkWepApp.Dtos
{
    public class UserPasswordChangeDto
    {
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name ="*Eski Şifre :")]
        public string oldPassword { get; set; }
        


        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Yeni Şifre :")]
        public string newPassword { get; set; }



        [Compare(nameof(newPassword), ErrorMessage = "Şifreler Uyuşmuyor.!")]
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Yeni Şifre Tekrar :")]
        public string confirmPassword { get; set; }
    }
}
