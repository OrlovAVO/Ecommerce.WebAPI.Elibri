using System.Threading.Tasks;
using Elibri.EF.Models;

namespace Elibri.Authorization.Services.ResetServices
{
    public interface IResetService
    {
        Task<(bool Success, string ErrorMessage)> ResetPassword(string email);
    }

}
