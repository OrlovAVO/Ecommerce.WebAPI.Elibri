using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Elibri.Authorization.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtKey;
        private readonly string _issuer;
        private readonly string _audience;

        // Конструктор получает параметры из конфигурации.
        public TokenService(IConfiguration configuration)
        {
            _jwtKey = configuration["jwt:Key"];
            _issuer = configuration["jwt:issuer"];
            _audience = configuration["jwt:audience"];
        }

        // Метод для генерации JWT токена на основе email.
        public string GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email)  // Добавляем email в качестве claim.
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Устанавливаем срок действия токена.
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Указываем алгоритм шифрования.
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Асинхронный метод для проверки валидности JWT токена.
        public async Task<bool> IsValidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            try
            {
                // Проверка токена с использованием параметров проверки.
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    ClockSkew = TimeSpan.Zero  // Устанавливаем допустимое отклонение времени.
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Метод для извлечения email из JWT токена.
        public string GetEmailFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                ClockSkew = TimeSpan.Zero
            };

            // Валидация токена и извлечение данных.
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            var emailClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (emailClaim == null)
                throw new InvalidOperationException("Токен не содержит требуемого утверждения (claim) с электронной почтой.");

            return emailClaim.Value;
        }
    }
}
