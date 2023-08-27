using CleanArchMVC.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMVC.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int? id);
        Task AddCategoryAsync(CategoryDTO categoryDTO);
        Task UpdateCategoryAsync(CategoryDTO categoryDTO);
        Task RemoveCategoryAsync(int? id);
    }
}
