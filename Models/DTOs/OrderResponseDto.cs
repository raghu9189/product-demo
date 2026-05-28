using ProductDemo.Models.Entities;

namespace ProductDemo.Models.DTOs;

public class OrderResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public UserResponseDto? User { get; set; }
    public ProductResponseDto? Product { get; set; }
}
