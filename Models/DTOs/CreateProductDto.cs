using System.ComponentModel.DataAnnotations;

namespace ProductDemo.Models.DTOs;

public class CreateProductDto
{
    [Required]
    [MinLength(2)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public double Price { get; set; }
}
