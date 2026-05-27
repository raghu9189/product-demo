namespace ProductDemo.Models.Entities;

public class Order
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    // Foreign Key
    public int UserId { get; set; }

    // Navigation Property
    public User User { get; set; } = null!;

    // Foreign Key
    public int ProductId { get; set; }

    // Navigation Property
    public Product Product { get; set; } = null!;
}