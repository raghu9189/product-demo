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

    public async Task<UserResponseDto> AddUser(CreateUserDto user)
    {   
        var userEntity = new User()
        {
            Name = user.Name
        };
        var createdUser = await _userRepository.AddUser(userEntity);
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
        if (user is null)
        {
            return null;
        }
        var dto = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name
        };
        return dto;
    }

    public async Task<UserResponseDto?> UpdateUser(int id, UpdateUserDto userDto)
    {
        var user = await _userRepository.GetUserById(id);
        if (user is null)
        {
            return null;
        }

        if (userDto.Name != null)
        {
            user.Name = userDto.Name;
        }

        await _userRepository.UpdateUser(user);

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name
        };
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user is null)
        {
            return false;
        }

        await _userRepository.DeleteUser(user);
        return true;
    }
}