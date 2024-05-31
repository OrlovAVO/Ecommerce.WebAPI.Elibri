using Elibri.EF.DTOS;
using Elibri.Core.Features.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Features.UserServices
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