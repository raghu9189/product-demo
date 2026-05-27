using System.ComponentModel.DataAnnotations;

namespace ProductDemo.Models.DTOs;

public class CreateUserDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; }
}