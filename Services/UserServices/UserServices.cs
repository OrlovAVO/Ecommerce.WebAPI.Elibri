using System.Threading.Tasks;
using Elibri.DTOs.DTOS;
using Elibri.Models;
using Elibri.Repositories.UserRepo;
using Microsoft.AspNetCore.Identity;
using Elibri.Services.TokenServices;

namespace Elibri.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, UserManager<User> userManager, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            }).ToList();
        }

        public async Task<UserDTO> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<UserDTO> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task UpdateAsync(UserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(userDTO.Id);
            if (user != null)
            {
                user.UserName = userDTO.UserName;
                user.Email = userDTO.Email;
                await _userRepository.UpdateAsync(user);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<UserDTO> CreateAsync(UserDTO userDTO)
        {
            var user = new User
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email
            };
            await _userRepository.CreateAsync(user);

            var createdUserDTO = new UserDTO
            {
                UserName = user.UserName,
                Email = user.Email
            };

            return createdUserDTO;
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

    }
}
