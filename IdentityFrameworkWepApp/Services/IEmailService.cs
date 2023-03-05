namespace IdentityFrameworkWepApp.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string resetEmailLink, string toEmail);
        Task SendMailWelcomeMessage(string toEmail);
    }
}
