using Microsoft.IdentityModel.Tokens;
using RazorPage_ProductManager.Core.Interfaces;
using RazorPage_ProductManager.Core.Models;

namespace RazorPage_ProductManager.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }
        public async Task AddProductAsync(Product product)
        {
            var existingProduct = await _repo.GetByCodeAsync(product.Code);
            if (existingProduct != null) throw new Exception("Mã sản phẩm đã tồn tại");
            await _repo.AddAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product.Quantity > 0) throw new Exception("Không thể xóa sản phẩm vì vẫn còn hàng trong kho");
            await _repo.DeleteAsync(id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<List<Product>> GetByKeywordsAsync(string keyword)
        {
            if (keyword.IsNullOrEmpty())
            {
                return await _repo.GetAllAsync();
            }
            else
            {
                return await _repo.GetByKeywordsAsync(keyword);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) throw new Exception("Không tìm thấy sản phẩm nào");
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _repo.GetByCodeAsync(product.Code);
            if (existingProduct != null && product.Id != existingProduct.Id) throw new Exception("Đã có sản phẩm có mã như vậy rồi nên không cấp nhật được");

            await _repo.UpdateAsync(product);
        }
    }
}
