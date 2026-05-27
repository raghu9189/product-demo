using ProductDemo.Models.Entities;

namespace ProductDemo.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product?> GetProductById(int id);
    Task<Product> AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Product product);
}