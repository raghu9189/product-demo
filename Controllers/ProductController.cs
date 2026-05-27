using Microsoft.AspNetCore.Mvc;
using ProductDemo.Models.DTOs;
using ProductDemo.Services.Interfaces;

namespace ProductDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: /api/product
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAllProducts();
        return Ok(products);
    }

    // GET: api/product/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product is null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    // POST: api/product
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdProduct = await _productService.AddProduct(product);
        return Ok(createdProduct);
    }

    // PATCH: api/product/1
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updatedProduct = await _productService.UpdateProduct(id, productDto);
        if (updatedProduct is null)
        {
            return NotFound();
        }
        return Ok(updatedProduct);
    }

    // DELETE: api/product/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _productService.DeleteProduct(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
