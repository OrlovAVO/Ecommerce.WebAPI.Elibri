using AutoMapper;
using Elibri.Core.Repository.GenericRepo;

namespace Elibri.Core.Features.GenericServices
{
    // Обобщенный сервис для работы с сущностями.
    public class GenericService<TModel, TDTO> : IGenericService<TDTO>
        where TModel : class
        where TDTO : class
    {
        protected readonly IGenericRepository<TModel> _repository;
        protected readonly IMapper _mapper;

        // Конструктор класса GenericService.
        public GenericService(IGenericRepository<TModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Получает все сущности асинхронно.
        public async Task<List<TDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<TDTO>>(entities);
        }

        // Получает сущность по идентификатору асинхронно.
        public async Task<TDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDTO>(entity);
        }

        // Создает новую сущность асинхронно.
        public async Task<TDTO> CreateAsync(TDTO dto)
        {
            var entity = _mapper.Map<TModel>(dto);
            entity = await _repository.CreateAsync(entity);
            return _mapper.Map<TDTO>(entity);
        }

        // Обновляет информацию о сущности асинхронно.
        public async Task UpdateAsync(TDTO dto)
        {
            var entity = _mapper.Map<TModel>(dto);
            await _repository.UpdateAsync(entity);
        }

        // Удаляет сущность по идентификатору асинхронно.
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
