using Microsoft.EntityFrameworkCore;
using ProductDemo.Models.Entities;
using ProductDemo.Repositories.Interfaces;

namespace ProductDemo.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> AddProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
