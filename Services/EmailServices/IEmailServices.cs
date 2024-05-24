using System.Threading.Tasks;

namespace Elibri.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendNewPasswordEmailAsync(string email, string newPassword);
    }
}
