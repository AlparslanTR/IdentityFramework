using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityFrameworkWepApp.Dtos
{
    public class SignInDto
    {
        public SignInDto()
        {

        }
        public SignInDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [EmailAddress(ErrorMessage = "Mail Formatı Yanlıştır.!")]
        [Display(Name = "*Mail Adresi :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Şifre :")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
