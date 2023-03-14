using IdentityFrameworkWepApp.Areas.Admin.Models;
using IdentityFrameworkWepApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityFrameworkWepApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserList()
        {
            var users = await _userManager.Users.ToListAsync();
            var userAreas = new List<UserArea>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userArea = new UserArea
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault()
                };
                userAreas.Add(userArea);
            }
            return View(userAreas);
        }
    }
}
