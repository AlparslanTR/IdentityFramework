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
        public async Task <IActionResult> UserList()
        {
            var list= await _userManager.Users.ToListAsync();
            var userlist = list.Select(x => new UserArea()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
            }).ToList();
            return View(userlist);
        }
    }
}
