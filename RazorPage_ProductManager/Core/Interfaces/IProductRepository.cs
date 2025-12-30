using RazorPage_ProductManager.Core.Models;

namespace RazorPage_ProductManager.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> GetByCodeAsync(string code);
        Task AddAsync(Product product);
        Task UpdateAsync(Product Product);
        Task DeleteAsync(int id);
    }
}
