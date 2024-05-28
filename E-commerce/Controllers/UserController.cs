using API.Web;
using Elibri.DTOs.DTOS;
using Elibri.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Elibri.API.Controllers
{

    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <remarks>
        /// Для получение всех пользователей нужно ввести UserId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetAllUserRoute)]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            return await _userService.GetAllAsync();
        }

        /// <summary>
        /// Получение пользователя по Username
        /// </summary>
        /// <remarks>
        /// Для получения пользователя по Username нужно ввести Username
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetUserByUsernameRoute)]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userService.GetByUsernameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Получение пользователя по UserId
        /// </summary>
        /// <remarks>
        /// Для олучения пользователя нужно иметь права адмнистратора и ввести UserId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetUserByIdRoute)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            return await _userService.GetByIdAsync(id);
        }

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <remarks>
        /// Для обновления пользователя нужны права администратора и ввести UserId
        /// </remarks>
        [HttpPut]
        [Route(Routes.UpdateuserRoute)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, UserDTO userDTO)
        {
            await _userService.UpdateAsync(userDTO);
            return Ok("Updated Successfully");
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <remarks>
        /// Для удаления пользователя нужны права администратора и ввести UserId
        /// </remarks>
        [HttpDelete]
        [Route(Routes.DeleteUserRoute)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok("Deleted Successfully");
        }
    }
}

