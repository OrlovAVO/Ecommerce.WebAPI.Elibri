using Elibri.Authorization.DTOS;
using Elibri.Authorization.Services.EmailServices;
using Elibri.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Elibri.Authorization.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;

        // Конструктор класса AuthService, который инициализирует необходимые сервисы и менеджеры.
        public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }

        // Метод для регистрации администратора.
        // Он проверяет, существует ли пользователь с данным именем, создает нового пользователя и назначает ему роли "Admin" и "User".
        public async Task<IActionResult> RegisterAdmin(RegisterDto model)
        {
            // Проверка, существует ли пользователь с таким именем.
            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                return new BadRequestObjectResult("Администратор уже существует.");
            }

            // Создание нового пользователя.
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            // Установка параметров пароля для нового пользователя.
            _userManager.Options.Password.RequireNonAlphanumeric = false;
            _userManager.Options.Password.RequireDigit = false;
            _userManager.Options.Password.RequireUppercase = false;

            // Создание пользователя в системе.
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            // Проверка наличия роли "Admin" и "User" в системе и их создание, если они отсутствуют.
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Назначение ролей "Admin" и "User" новому пользователю.
            await _userManager.AddToRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "User");

            return new OkObjectResult("Администратор успешно зарегистрирован.");
        }

        // Метод для регистрации обычного пользователя.
        // Он проверяет, существует ли пользователь с данным именем, создает нового пользователя и назначает ему роль "User".
        public async Task<IActionResult> RegisterUser(RegisterDto model)
        {
            // Проверка, существует ли пользователь с таким именем.
            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                return new BadRequestObjectResult("Пользователь уже существует.");
            }

            // Создание нового пользователя.
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            // Установка параметров пароля для нового пользователя.
            _userManager.Options.Password.RequireNonAlphanumeric = false;
            _userManager.Options.Password.RequireDigit = false;
            _userManager.Options.Password.RequireUppercase = false;

            // Создание пользователя в системе.
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            // Проверка наличия роли "User" в системе и её создание, если она отсутствует.
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Назначение роли "User" новому пользователю.
            await _userManager.AddToRoleAsync(user, "User");

            return new OkObjectResult("Пользователь успешно зарегистрирован.");
        }

        // Метод для авторизации пользователя.
        // Он проверяет существование пользователя и правильность пароля, а затем генерирует JWT-токен для аутентификации.
        public async Task<IActionResult> Login(LoginDto model)
        {
            // Поиск пользователя по имени.
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return new BadRequestObjectResult("Пользователь с таким именем не существует.");
            }

            // Проверка правильности пароля.
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new BadRequestObjectResult("Неверные данные для входа");
            }

            // Получение ролей пользователя.
            var roles = await _userManager.GetRolesAsync(user);

            // Формирование списка claims (утверждений) для включения в JWT.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            // Добавление ролей пользователя в claims.
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Создание ключа безопасности из конфигурации.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Создание JWT-токена.
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            // Преобразование токена в строку.
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Настройка опций куки для хранения JWT.
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(24)
            };

            // Добавление JWT в куки ответа.
            _httpContextAccessor.HttpContext.Response.Cookies.Append("jwt-cookies", tokenString, cookiesOptions);

            // Возвращение JWT в ответе.
            return new OkObjectResult(new { token = tokenString });
        }
    }
}
