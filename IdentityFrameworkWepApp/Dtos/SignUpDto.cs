using System.ComponentModel.DataAnnotations;

namespace IdentityFrameworkWepApp.Dtos
{
    public class SignUpDto
    {
        public SignUpDto()
        {

        }
        public SignUpDto(string userName, string email, string phone, string password)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
        }
        [Required(ErrorMessage ="* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Kullanıcı Adı :")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [EmailAddress(ErrorMessage ="Mail Formatı Yanlıştır.!")]
        [Display(Name = "*Mail :")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Telefon :")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Şifre :")]
        public string Password { get; set; }

        [Compare(nameof(Password),ErrorMessage ="Şifreler Uyuşmuyor.!")]
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Şifre Tekrar :")]
        public string PasswordConfirm { get; set; }
    }
}
