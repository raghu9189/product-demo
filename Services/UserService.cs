using ProductDemo.Models;
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

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _userRepository.GetUserById(id);
    }

}