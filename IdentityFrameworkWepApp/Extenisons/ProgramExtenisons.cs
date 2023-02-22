using IdentityFrameworkWepApp.CustomValidations;
using IdentityFrameworkWepApp.Data;
using Microsoft.AspNetCore.Identity;

namespace IdentityFrameworkWepApp.Extenisons
{
    public static class ProgramExtenisons
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(opts =>
            {
                opts.User.RequireUniqueEmail = true; // Mail Uniq olsun.
                opts.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöprsştuüvyz1234567890ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ"; // Username de kullanılabilir harfleri belirtiyorum.

                opts.Password.RequiredLength = 12; // Minimum Karakter Uzunluğu şifre
                opts.Password.RequireNonAlphanumeric = true; // en az bir non-alphanumeric karakter içermelidir şifre.
                opts.Password.RequireLowercase = true; // kullanıcı şifre belirlerken en az bir küçük harf içeren bir şifre belirlemek zorundadır.
                opts.Password.RequireUppercase = true; // kullanıcı şifre belirlerken en az bir büyük harf içeren bir şifre belirlemek zorundadır.
                opts.Password.RequireDigit = true; // kullanıcı şifre belirlerken en az bir rakam içeren bir şifre belirlemek zorundadır.

            }).AddPasswordValidator<PasswordValid>().AddUserValidator<UserValid>().AddErrorDescriber<LocalizationIdentityErrorMessagesTurkish>().AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
