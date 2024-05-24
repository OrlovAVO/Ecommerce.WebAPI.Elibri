using Elibri.Models;
using Elibri.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<User> GetByUsernameAsync(string username);

        Task<User> CreateAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(int id);
        Task<User> GetByEmailAsync(string email);

    }
}
