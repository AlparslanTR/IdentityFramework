using System.ComponentModel.DataAnnotations;

namespace IdentityFrameworkWepApp.Dtos
{
    public class UserEditDto
    {
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Kullanıcı Adı :")]
        public string UserName { get; set; }
        //
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Mail :")]
        public string Mail { get; set; }
        //
        [Display(Name = "Yaş :")]
        public int Age { get; set; }
        //
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Telefon :")]
        public string Phone { get; set; }
        //
        [Display(Name = "Şehir :")]
        public string City { get; set; }
        //
        [Display(Name = "Profil Resmi :")]
        public IFormFile Picture { get; set; }
        //
        [Display(Name = "Doğum Tarihi :")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        //
        [Display(Name = "Cinsiyet :")]
        public byte Gender { get; set; }
    }
}
