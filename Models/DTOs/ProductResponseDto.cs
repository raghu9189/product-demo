namespace ProductDemo.Models.DTOs;

public class ProductResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public double Price { get; set; }
}
