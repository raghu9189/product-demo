using System.ComponentModel.DataAnnotations;

namespace ProductDemo.Models.DTOs;

public class UpdateUserDto
{
    [MinLength(3)]
    public string? Name { get; set; }
}
