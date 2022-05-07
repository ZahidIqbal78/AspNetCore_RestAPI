using AspNetCore_RestAPI.Data;
using dotnet_webapi_example.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi_example.Services
{
    public class ProductService : IProductService
    {
        private readonly TestDbContext _dbContext;
        public ProductService(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            var created = await _dbContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteProductAsync(Guid productId)
        {
            var product = await GetProductByIdAsync(productId);
            //if (product is null) return false;
            _dbContext.Products.Remove(product);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(x => x.Name == productName);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}
