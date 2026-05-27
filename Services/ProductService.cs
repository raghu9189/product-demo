using ProductDemo.Models.DTOs;
using ProductDemo.Models.Entities;
using ProductDemo.Repositories.Interfaces;
using ProductDemo.Services.Interfaces;

namespace ProductDemo.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponseDto> AddProduct(CreateProductDto productDto)
    {
        var product = new Product
        {
            Title = productDto.Title,
            Description = productDto.Description ?? string.Empty,
            Price = productDto.Price
        };

        var createdProduct = await _productRepository.AddProduct(product);

        return new ProductResponseDto
        {
            Id = createdProduct.Id,
            Title = createdProduct.Title,
            Description = createdProduct.Description,
            Price = createdProduct.Price
        };
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProducts();
        return products.Select(product => new ProductResponseDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price
        });
    }

    public async Task<ProductResponseDto?> GetProductById(int id)
    {
        var product = await _productRepository.GetProductById(id);
        if (product is null)
        {
            return null;
        }

        return new ProductResponseDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price
        };
    }

    public async Task<ProductResponseDto?> UpdateProduct(int id, UpdateProductDto productDto)
    {
        var product = await _productRepository.GetProductById(id);
        if (product is null)
        {
            return null;
        }

        if (productDto.Title != null)
        {
            product.Title = productDto.Title;
        }

        if (productDto.Description != null)
        {
            product.Description = productDto.Description;
        }

        if (productDto.Price.HasValue)
        {
            product.Price = productDto.Price.Value;
        }

        await _productRepository.UpdateProduct(product);

        return new ProductResponseDto
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price
        };
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var product = await _productRepository.GetProductById(id);
        if (product is null)
        {
            return false;
        }

        await _productRepository.DeleteProduct(product);
        return true;
    }
}
