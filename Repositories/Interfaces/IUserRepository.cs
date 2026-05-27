using ProductDemo.Models.Entities;

namespace ProductDemo.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task<User> AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(User user);
}