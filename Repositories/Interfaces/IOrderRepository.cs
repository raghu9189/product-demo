using ProductDemo.Models.Entities;

namespace ProductDemo.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<Order?> GetOrderById(int id);
    Task<Order> AddOrder(Order order);
    Task UpdateOrder(Order order);
    Task DeleteOrder(Order order);
}
