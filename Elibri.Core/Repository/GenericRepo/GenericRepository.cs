using Elibri.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Elibri.Core.Repository.GenericRepo
{
    // Обобщенный репозиторий GenericRepository<T> реализует интерфейс IGenericRepository<T>.
    // Этот класс обеспечивает общую реализацию базовых операций CRUD для сущностей типа T.
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;

        // Конструктор класса, принимающий контекст базы данных и инициализирующий набор сущностей типа T.
        public GenericRepository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Метод для получения всех сущностей типа T из базы данных асинхронно.
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Метод для получения сущности типа T по заданному идентификатору асинхронно.
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Метод для создания новой сущности типа T в базе данных асинхронно.
        public async Task<T> CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Метод для обновления существующей сущности типа T в базе данных асинхронно.
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Метод для удаления сущности типа T из базы данных по заданному идентификатору асинхронно.
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
