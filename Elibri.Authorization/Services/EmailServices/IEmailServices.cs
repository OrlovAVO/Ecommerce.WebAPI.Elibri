using System.Threading.Tasks;

namespace Elibri.Authorization.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendNewPasswordEmailAsync(string email, string newPassword);
    }
}
