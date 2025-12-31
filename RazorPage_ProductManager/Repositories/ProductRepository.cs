using Microsoft.EntityFrameworkCore;
using RazorPage_ProductManager.Core.Interfaces;
using RazorPage_ProductManager.Core.Models;
using RazorPage_ProductManager.Data;

namespace RazorPage_ProductManager.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Product product)
        {
            _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public Task<Product?> GetByIdAsync(int id)
        {
            return _context.Products.FirstOrDefaultAsync(p=>p.Id == id);
        }
        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Code == code);
        }
        public async Task UpdateAsync(Product Product)
        {
            _context.Products.Update(Product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw; 
            }
        }

        public async Task<List<Product>> GetByKeywordsAsync(string keyword)
        {
            var products = _context.Products.Where(p => p.Name.Contains(keyword)).ToListAsync();
            return await products;
        }
    }
}
