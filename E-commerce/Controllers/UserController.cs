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

        [HttpGet]
        [Route(Routes.GetAllUserRoute)]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            return await _userService.GetAllAsync();
        }

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

        [HttpGet]
        [Route(Routes.GetUserByIdRoute)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDTO>> GetUserById(string id)
        {
            return await _userService.GetByIdAsync(id);
        }

        [HttpPut]
        [Route(Routes.UpdateuserRoute)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id, UserDTO userDTO)
        {
            await _userService.UpdateAsync(userDTO);
            return Ok("Updated Successfully");
        }

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

