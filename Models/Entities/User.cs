using System;

namespace ProductDemo.Models.Entities;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    // One User -> Many Orders
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}