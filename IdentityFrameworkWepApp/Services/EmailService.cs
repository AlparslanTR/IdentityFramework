using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;

namespace IdentityFrameworkWepApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }
        public async Task SendResetPasswordEmail(string resetEmailLink, string toEmail)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = _settings.Host; // SMTP sunucusunun adresini belirlemek için kullanılır.
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; // smtpClient.DeliveryMethod özelliği, SmtpDeliveryMethod.Network olarak ayarlanır. Bu, SMTP sunucusuyla ağ üzerinden iletişim kurmak için kullanılacağını belirtir.
            smtpClient.UseDefaultCredentials = false; // Bu, varsayılan kimlik doğrulama bilgilerinin kullanılmayacağını belirtir.
            smtpClient.Port = 587; // SMTP sunucusuna gönderilecek e-postalar için kullanılacak portu belirtir. Bu örnekte, 587 portu kullanılır.
            smtpClient.Credentials = new NetworkCredential(_settings.Email, _settings.Password); // e-posta göndermek için kullanılacak kimlik bilgilerini belirtir. Bu örnekte, _settings.Email ve _settings.Password özellikleri kullanılır. Bu, e-posta gönderme işlemi için kullanılacak hesap bilgilerini belirtir.
            smtpClient.EnableSsl = true; // SSL/TLS bağlantısının kullanılacağını belirtir.
            smtpClient.Timeout = 10000;  // SMTP sunucusuna bağlantı kurarken veya veri gönderirken bekleyeceği maksimum süreyi belirler.

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_settings.Email);// e - postanın gönderici adresini belirtir.
            mailMessage.To.Add(toEmail); // e-postanın alıcısını belirtir.
            mailMessage.Subject = "Alparslan Blogger Şifre Sıfırlama"; // e postanın konusunu belirtir.
            mailMessage.Body = $"<h4>Şifrenizi Yenilemek için alttaki linke tıklayınız.</h4><p><a href='{resetEmailLink}'>Şifre Yenileme Link</a></p>"; // E postanın açıklamasını belirtir yani içeriğini.
            mailMessage.IsBodyHtml = true; // e - postanın HTML biçiminde olacağını belirtir.

            await smtpClient.SendMailAsync(mailMessage); // Bu, belirtilen e-postayı SMTP sunucusuna gönderir.


        }

        public async Task SendMailWelcomeMessage(string toEmail)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Host = _settings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_settings.Email, _settings.Password);
            smtpClient.EnableSsl = true; 
            smtpClient.Timeout = 10000; 

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_settings.Email);
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "Alparslan Blogger Hesabınız Oluşturuldu";
            mailMessage.Body = $"<h4>Blogger Ailesine Katıldığınız İçin Teşekkür Ederiz. Lütfen İçeriklere Göz Atmayı İhmal Etmeyin.</h4><p>Alparslan Akbaş Blogger Kurucusu</p>";
            mailMessage.IsBodyHtml=true;
            await smtpClient.SendMailAsync(mailMessage);
        }

    }
}