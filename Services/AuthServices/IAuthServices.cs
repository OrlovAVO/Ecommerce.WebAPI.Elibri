using System.Threading.Tasks;
using Elibri.DTOs.DTOS;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Elibri.Services.AuthServices
{
    public interface IAuthService
    {
        Task<IActionResult> RegisterAdmin(RegisterDto model);
        Task<IActionResult> RegisterUser(RegisterDto model);
        Task<IActionResult> Login(LoginDto model);

    }
}
