using ProductDemo.Models;
using ProductDemo.Models.Entities;

namespace ProductDemo.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task<User> AddUser(User user);
}