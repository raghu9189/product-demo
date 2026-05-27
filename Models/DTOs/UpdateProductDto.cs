using System.ComponentModel.DataAnnotations;

namespace ProductDemo.Models.DTOs;

public class UpdateProductDto
{
    [MinLength(2)]
    public string? Title { get; set; }

    public string? Description { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public double? Price { get; set; }
}
