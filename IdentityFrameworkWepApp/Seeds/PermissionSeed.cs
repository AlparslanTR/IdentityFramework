using IdentityFrameworkWepApp.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityFrameworkWepApp.Seeds
{
    public class PermissionSeed
    {
        public static async Task Seed(RoleManager<Role> roleManager)
        {
            var basicRole = await roleManager.RoleExistsAsync("BasicRole");
            var advanceRole = await roleManager.RoleExistsAsync("AdvancedRole");
            var ultimateRole = await roleManager.RoleExistsAsync("UltimateRole");
            if (!basicRole)
            {
                await roleManager.CreateAsync(new Role() { Name = "BasicRole" });
                var findRole = await roleManager.FindByNameAsync("BasicRole");
                await roleManager.AddClaimAsync(findRole, new Claim("Permission",Permissions.Permission.Stock.Read));
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Order.Read));
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Catalog.Read));
            }

            if (!advanceRole)
            {
                await roleManager.CreateAsync(new Role() { Name = "AdvancedRole" });
                var findRole = await roleManager.FindByNameAsync("AdvancedRole");
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Order.Read));
                await roleManager.AddClaimAsync(findRole,new Claim("Permission",Permissions.Permission.Order.Update));
                await roleManager.AddClaimAsync(findRole,new Claim("Permission",Permissions.Permission.Order.Create));
                await roleManager.AddClaimAsync(findRole,new Claim("Permission",Permissions.Permission.Order.Delete));
            }

            if (!ultimateRole)
            {
                await roleManager.CreateAsync(new Role() { Name = "UltimateRole" });
                var findRole = await roleManager.FindByNameAsync("UltimateRole");
                await roleManager.AddClaimAsync(findRole,new Claim("Permission",Permissions.Permission.Catalog.Update));
                await roleManager.AddClaimAsync(findRole,new Claim("Permission",Permissions.Permission.Catalog.Delete));
                await roleManager.AddClaimAsync(findRole,new Claim("Permission",Permissions.Permission.Catalog.Create));
                await roleManager.AddClaimAsync(findRole,new Claim("Permission",Permissions.Permission.Catalog.Read));
                //
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Order.Update));
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Order.Delete));
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Order.Create));
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Order.Read));
                //
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Stock.Update));
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Stock.Delete));
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Stock.Create));
                await roleManager.AddClaimAsync(findRole, new Claim("Permission", Permissions.Permission.Stock.Read));
            }
        }

       
    }
}
