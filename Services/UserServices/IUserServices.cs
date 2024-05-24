using Elibri.DTOs.DTOS;
using Elibri.Services.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Services.UserServices
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(string id);
        Task<UserDTO> GetByUsernameAsync(string username);
        Task<UserDTO> CreateAsync(UserDTO dto);
        Task UpdateAsync(UserDTO dto);
        Task DeleteAsync(int id);
        Task<UserDTO> GetUserByEmail(string email);


    }
}