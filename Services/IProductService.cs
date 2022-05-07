using dotnet_webapi_example.Models;

namespace dotnet_webapi_example.Services
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(Product product);
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<Product> GetProductByNameAsync(string productName);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(Guid productId);
    }
}
