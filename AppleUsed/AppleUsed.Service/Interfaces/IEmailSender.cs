using System.Threading.Tasks;

namespace AppleUsed.Service.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}