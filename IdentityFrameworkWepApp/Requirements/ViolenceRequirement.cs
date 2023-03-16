using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IdentityFrameworkWepApp.Requirements
{
    public class ViolenceRequirement:IAuthorizationRequirement
    {
        public int staticAge { get; set; }
    }
    public class ViolenceRequirementHandler : AuthorizationHandler<ViolenceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViolenceRequirement requirement)
        {
            if (!context.User.HasClaim(x=>x.Type=="birthDate")) // Kullanıcının doğum tarihinin var olup olmadığını kontrol eder.
            {
                context.Fail();
                return Task.CompletedTask;
            }
            Claim violence = context.User.FindFirst("birthDate"); // Kullanıcının doğum tarihini al.

            var today = DateTime.Now; // Şimdiki tarih.
            var birthdate = Convert.ToDateTime(violence.Value); // kullanıcının doğum tarihi.
            var age = today.Year -birthdate.Year; // Kullanıcının yaşı.

            if (birthdate> today.AddYears(-age)) // Eğer doğum tarihi büyük ise şuanki yaşından, yaşdan 1 azalt.
            {
                age--;
            }

            if (requirement.staticAge> age ) // Statik 18 yaş büyük ise kullanıcının yaşından, hata döndür.
            {
                context.Fail();
                return Task.CompletedTask;
            }
            context.Succeed(requirement); // Küçük ise çalıştır.
            return Task.CompletedTask;
        }
    }
}
