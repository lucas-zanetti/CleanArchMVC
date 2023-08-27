using CleanArchMVC.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMVC.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int? id);
        Task<ProductDTO> GetProductCategoryAsync(int? id);
        Task AddProductAsync(ProductDTO productDTO);
        Task UpdateProductAsync(ProductDTO productDTO);
        Task RemoveProductAsync(int? id);
    }
}
