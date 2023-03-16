using Microsoft.AspNetCore.Identity;

namespace IdentityFrameworkWepApp.Data
{
    public class User:IdentityUser
    {
        public int Age { get; set; }
        public string City { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte? Gender { get; set; }
        public string Picture { get; set; }
    }
}
