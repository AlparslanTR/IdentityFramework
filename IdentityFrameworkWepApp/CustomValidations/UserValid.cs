using IdentityFrameworkWepApp.Data;
using Microsoft.AspNetCore.Identity;

namespace IdentityFrameworkWepApp.CustomValidations
{
    public class UserValid : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            var errors= new List<IdentityError>();
            var isNumeric = int.TryParse(user.UserName[0].ToString(), out _); // Eğer Kullanıcı Adı rakam ile başlarsa hata aldır.
            if (isNumeric)
            {
                errors.Add(new() { Code = "UsernameContainFirstLetterDigit",Description="Kullanıcı Adı Rakam İle Başlayamaz.!" });
            }
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
