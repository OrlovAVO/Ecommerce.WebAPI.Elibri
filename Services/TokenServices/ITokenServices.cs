using Elibri.DTOs.DTOS;

namespace Elibri.Services.TokenServices
{
    public interface ITokenService
    {
        string GenerateToken(string email);
        Task<bool> IsValidToken(string token);
        string GetEmailFromToken(string token);

    }
}
