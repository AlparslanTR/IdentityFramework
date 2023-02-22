using IdentityFrameworkWepApp.Data;
using Microsoft.AspNetCore.Identity;

namespace IdentityFrameworkWepApp.CustomValidations
{
    public class PasswordValid : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            var errors = new List<IdentityError>();
            if (password.ToLower().Contains(user.UserName.ToLower())) // Kullanıcı adına girilen kelime şifre ile aynı olamaz.!
            {
                errors.Add(new() { Code = "PasswordNotContainUserName", Description = "Şifre Alanı Kullanıcı Adı İçeremez." });
            }

            if (password.StartsWith("123") || password.EndsWith("123")) // Şifrelerin başı veya sonu 123 ile başlayamaz veya bitemez.
            {
                errors.Add(new() { Code = "PasswordNotContain123456789", Description = "Şifre Alanı Basit Şifrelerle Başlayamaz veya Bitemez.!" });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
