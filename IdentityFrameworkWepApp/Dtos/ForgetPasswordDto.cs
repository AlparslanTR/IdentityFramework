using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityFrameworkWepApp.Dtos
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [EmailAddress(ErrorMessage = "Mail Formatı Yanlıştır.!")]
        [Display(Name = "*Mail Adresi :")]
        public string Email { get; set; }
    }
}
