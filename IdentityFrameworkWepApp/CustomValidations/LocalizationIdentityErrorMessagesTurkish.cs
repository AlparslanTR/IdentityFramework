using Microsoft.AspNetCore.Identity;

namespace IdentityFrameworkWepApp.CustomValidations
{
    public class LocalizationIdentityErrorMessagesTurkish:IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code="DuplicatedUsername",Description="Bu Kullanıcı Adı Sistemde Zaten Kayıtlı Lütfen Başka Bir Ad Giriniz.!"};
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicatedMail", Description = "Bu Mail Sistemde Zaten Kayıtlı Lütfen Başka Bir Mail Giriniz.!" };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = "Şifreniz En Az 12 Karakter İçermelidir.!" };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new() { Code = "PasswordRequiresNonAlphanumeric", Description = "Şifrenizde Özel Karakter Bulundurmak Zorundasınız.!" };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new() { Code = "PasswordRequiresUpper", Description = "Şifrenizin En Az Bir Büyük Harfi ('A'-'Z') Olmalıdır." };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new() { Code = "PasswordRequiresLower", Description = "Şifrenizin En Az Bir Küçük Harfe ('a'-'z') Sahip Olmalıdır." };
        }
    }
}
