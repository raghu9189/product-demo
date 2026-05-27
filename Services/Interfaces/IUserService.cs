using ProductDemo.Models;
using ProductDemo.Models.DTOs;
using ProductDemo.Models.Entities;

namespace ProductDemo.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllUsers();
    Task<UserResponseDto?> GetUserById(int id);
    Task<UserResponseDto> AddUser(CreateUserDto user);
    Task<UserResponseDto?> UpdateUser(int id, UpdateUserDto userDto);
    Task<bool> DeleteUser(int id);
}