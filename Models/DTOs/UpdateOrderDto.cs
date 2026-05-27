using System.ComponentModel.DataAnnotations;

namespace ProductDemo.Models.DTOs;

public class UpdateOrderDto
{
    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int? Quantity { get; set; }
}
