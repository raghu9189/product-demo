using System;

namespace ProductDemo.Models.Entities;

public class Product
{
    public int Id {get; set;}
    public required string Title {get; set;}
    public string Description {get; set;}
    public double Price {get; set;}

}
