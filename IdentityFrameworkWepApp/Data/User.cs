using Microsoft.AspNetCore.Identity;

namespace IdentityFrameworkWepApp.Data
{
    public class User:IdentityUser
    {
        public int Age { get; set; }
    }
}
