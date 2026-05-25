using ProductDemo.Models;
using ProductDemo.Models.DTOs;
using ProductDemo.Models.Entities;
using ProductDemo.Repositories.Interfaces;
using ProductDemo.Services.Interfaces;

namespace ProductDemo.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto> AddUser(User user)
    {   
        var createdUser = await _userRepository.AddUser(user);
        var dto = new UserResponseDto
        {
            Id = createdUser.Id,
            Name = createdUser.Name
        };
        return dto;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
        var dtos = users.Select(user => new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
            });
        return dtos;
    }

    public async Task<UserResponseDto?> GetUserById(int id)
    {
        var user = await _userRepository.GetUserById(id);
        var dto = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name
        };
        return dto;
    }
}