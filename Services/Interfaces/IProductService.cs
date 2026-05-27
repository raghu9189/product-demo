using ProductDemo.Models.DTOs;

namespace ProductDemo.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllProducts();
    Task<ProductResponseDto?> GetProductById(int id);
    Task<ProductResponseDto> AddProduct(CreateProductDto product);
    Task<ProductResponseDto?> UpdateProduct(int id, UpdateProductDto productDto);
    Task<bool> DeleteProduct(int id);
}
