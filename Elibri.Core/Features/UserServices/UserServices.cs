using Elibri.Core.Repository.UserRepo;
using Elibri.EF.DTOS;
using Elibri.EF.Models;
using Microsoft.AspNetCore.Identity;

namespace Elibri.Core.Features.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        // Получает всех пользователей асинхронно.
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

        // Получает пользователя по идентификатору асинхронно.
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

        // Получает пользователя по имени пользователя асинхронно.
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

        // Обновляет информацию о пользователе асинхронно.
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

        // Удаляет пользователя по идентификатору асинхронно.
        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        // Создает нового пользователя асинхронно.
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

        // Получает пользователя по адресу электронной почты асинхронно.
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
