using Elibri.EF.DTOS;
using Elibri.Core.Features.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Features.CategoryServices
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
