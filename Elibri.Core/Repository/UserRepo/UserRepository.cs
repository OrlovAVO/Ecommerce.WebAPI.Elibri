using Elibri.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Elibri.Core.Repository.UserRepo
{
    // Репозиторий пользователей, реализующий интерфейс IUserRepository
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly DbSet<User> _dbSet;

        // Конструктор класса UserRepository
        public UserRepository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        // Получить всех пользователей
        public async Task<List<User>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Получить пользователя по имени пользователя
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.UserName == username);
        }

        // Получить пользователя по идентификатору
        public async Task<User> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Создать нового пользователя
        public async Task<User> CreateAsync(User entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Обновить информацию о пользователе
        public async Task UpdateAsync(User entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Удалить пользователя по идентификатору
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Получить пользователя по адресу электронной почты
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
