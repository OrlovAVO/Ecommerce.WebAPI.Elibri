using Elibri.DTOs.DTOS;
using Elibri.Services.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CategoryDTO categoryDTO);
        Task UpdateAsync(CategoryDTO categoryDTO);
        Task DeleteAsync(int id);
    }
    /*public interface ICategoryService : IGenericService<CategoryDTO> { }*/
}
