using ProductDemo.Models.DTOs;

namespace ProductDemo.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseDto>> GetAllOrders();
    Task<OrderResponseDto?> GetOrderById(int id);
    Task<OrderResponseDto?> AddOrder(CreateOrderDto orderDto);
    Task<OrderResponseDto?> UpdateOrder(int id, UpdateOrderDto orderDto);
    Task<bool> DeleteOrder(int id);
}
