using Elibri.DTOs.DTOS;
using Elibri.Models;
using Elibri.Services.EmailServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Elibri.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;


        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;

        }

        public async Task<IActionResult> RegisterAdmin(RegisterDto model)
        {
            var existingUser = await _userManager.FindByNameAsync(model.UserName); //Проверка username в бд
            if (existingUser != null)
            {
                return new BadRequestObjectResult("Администратор уже существует.");
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            _userManager.Options.Password.RequireNonAlphanumeric = false;
            _userManager.Options.Password.RequireDigit = false;
            _userManager.Options.Password.RequireUppercase = false;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "User");

            return new OkObjectResult("Администратор успешно зарегистрирован.");
        }

        public async Task<IActionResult> RegisterUser(RegisterDto model)
        {
            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                return new BadRequestObjectResult("Пользователь уже существует.");
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            _userManager.Options.Password.RequireNonAlphanumeric = false;
            _userManager.Options.Password.RequireDigit = false;
            _userManager.Options.Password.RequireUppercase = false;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(user, "User");

            return new OkObjectResult("Пользователь успешно зарегистрирован.");
        }

        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new BadRequestObjectResult("Неверные данные для входа");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(24)
            };

            _httpContextAccessor.HttpContext.Response.Cookies.Append("jwt-cookies", tokenString, cookiesOptions);

            return new OkObjectResult(new { token = tokenString });
        }

     
    }
}
