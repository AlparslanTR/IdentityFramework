using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityFrameworkWepApp.Data
{
    public class AppDbContext:IdentityDbContext<User,Role,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base (options)
        {
            
        }
    }
}
