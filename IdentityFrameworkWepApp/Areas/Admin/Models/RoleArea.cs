using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace IdentityFrameworkWepApp.Areas.Admin.Models
{
    public class RoleArea
    {
        [Required(ErrorMessage = "* Bu Alanın Doldurulması Zorunludur")]
        [Display(Name = "*Rol :")]
        public string Name { get; set; }
    }
}
