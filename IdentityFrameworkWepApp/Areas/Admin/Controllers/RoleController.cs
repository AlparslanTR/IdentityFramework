using IdentityFrameworkWepApp.Areas.Admin.Dtos;
using IdentityFrameworkWepApp.Areas.Admin.Models;
using IdentityFrameworkWepApp.Data;
using IdentityFrameworkWepApp.Extenisons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityFrameworkWepApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task <IActionResult> RoleList()
        {
            var roles= await _roleManager.Roles.Select(x => new RoleDtoArea()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return View();
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> AddRole(RoleArea request)
        {
            var result= await _roleManager.CreateAsync(new Role() {Name=request.Name });
            if (result is not {Succeeded:true }) // veya sadece !result.succeeded olarak da kullanılabilir.
            {
                ModelState.AddModelErrorList(result.Errors.Select(x=>x.Description).ToList());
                return View();
            }
            return RedirectToAction(nameof(RoleController.RoleList));
        }

        public async Task<IActionResult> AssignRoleToUser(string userId)
        {
            var currentUser= await _userManager.FindByIdAsync(userId); // Güncel Kullanıcıyı Al.
            ViewBag.userId = userId;
            var roles = await _roleManager.Roles.ToListAsync(); // Bütün Rolleri Al.
            var roleViewModel = new List<AssignRoleToUserDtoArea>(); // Bu dto dan değişkene bir liste ata
            var userRoles= await _userManager.GetRolesAsync(currentUser); // Kullanıcının Rolünü al.
            foreach (var role in roles)
            {
                var assignRoleToUserDtoArea= new AssignRoleToUserDtoArea() {Id=role.Id, Name=role.Name, };
                if (userRoles.Contains(role.Name))
                {
                    assignRoleToUserDtoArea.Exist = true; // Kullanıcının rolu mevcut ise true.
                }
                roleViewModel.Add(assignRoleToUserDtoArea);
            }
            return View(roleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(List<AssignRoleToUserDtoArea> request, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var role in request)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(user, role.Name); // True dönen existi ilgili kullanıcıya rol olarak ekle:
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name); // False dönenleri kullanıcıdan kaldır.
                }
            }
            return RedirectToAction(nameof(HomeController.UserList),"Home");
        }
    }
}
