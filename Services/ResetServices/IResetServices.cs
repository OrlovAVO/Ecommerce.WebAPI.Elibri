using System.Threading.Tasks;

namespace Elibri.Services.ResetServices
{
    public interface IResetService
    {
        Task<(bool Success, string ErrorMessage)> ResetPassword(string email);
    }

}
