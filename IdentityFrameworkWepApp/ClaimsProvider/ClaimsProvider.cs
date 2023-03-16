using IdentityFrameworkWepApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityFrameworkWepApp.ClaimsProvider
{
    public class ClaimsProvider : IClaimsTransformation
    {
        private readonly UserManager<User> _userManager;

        public ClaimsProvider(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;
            var currentUser= await _userManager.FindByNameAsync(identity.Name);

            if (currentUser == null)
            {
                return principal;
            }

            if (currentUser.City!=null) 
            {
                if (principal.HasClaim(x=>x.Type!="City"))
                {
                    Claim cityClaim = new Claim("City", currentUser.City);
                    identity.AddClaim(cityClaim);
                }
            }
            return principal;
        }
    }
}
