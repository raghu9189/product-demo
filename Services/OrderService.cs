using System.Text.Json;
using ProductDemo.Models.DTOs;
using ProductDemo.Models.Entities;
using ProductDemo.Repositories.Interfaces;
using ProductDemo.Services.Interfaces;

namespace ProductDemo.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IUserRepository userRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderResponseDto?> AddOrder(CreateOrderDto orderDto)
    {
        // Validate User existence
        var user = await _userRepository.GetUserById(orderDto.UserId);
        if (user is null)
        {
            return null;
        }

        // Validate Product existence
        var product = await _productRepository.GetProductById(orderDto.ProductId);
        if (product is null)
        {
            return null;
        }

        var order = new Order
        {
            UserId = orderDto.UserId,
            ProductId = orderDto.ProductId,
            Quantity = orderDto.Quantity
        };

        var createdOrder = await _orderRepository.AddOrder(order);

        return new OrderResponseDto
        {
            Id = createdOrder.Id,
            UserId = createdOrder.UserId,
            ProductId = createdOrder.ProductId,
            Quantity = createdOrder.Quantity,
            User = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name
            },
            Product = new ProductResponseDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price
            }
        };
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrders();
        return orders.Select(order => new OrderResponseDto
        {
            Id = order.Id,
            UserId = order.UserId,
            ProductId = order.ProductId,
            Quantity = order.Quantity
        });
    }

    public async Task<OrderResponseDto?> GetOrderById(int id)
    {
        var order = await _orderRepository.GetOrderById(id);
        if (order is null)
        {
            return null;
        }

        return new OrderResponseDto
        {
            Id = order.Id,
            UserId = order.UserId,
            ProductId = order.ProductId,
            Quantity = order.Quantity,
            User = order.User != null ? new UserResponseDto
            {
                Id = order.User.Id,
                Name = order.User.Name
            } : null,
            Product = order.Product != null ? new ProductResponseDto
            {
                Id = order.Product.Id,
                Title = order.Product.Title,
                Description = order.Product.Description,
                Price = order.Product.Price
            } : null
        };
    }

    public async Task<OrderResponseDto?> UpdateOrder(int id, UpdateOrderDto orderDto)
    {
        var order = await _orderRepository.GetOrderById(id);
        if (order is null)
        {
            return null;
        }

        if (orderDto.UserId.HasValue)
        {
            // Validate User existence
            var user = await _userRepository.GetUserById(orderDto.UserId.Value);
            if (user is null)
            {
                return null;
            }
            order.UserId = orderDto.UserId.Value;
            order.User = user;
        }

        if (orderDto.ProductId.HasValue)
        {
            // Validate Product existence
            var product = await _productRepository.GetProductById(orderDto.ProductId.Value);
            if (product is null)
            {
                return null;
            }
            order.ProductId = orderDto.ProductId.Value;
            order.Product = product;
        }

        if (orderDto.Quantity.HasValue)
        {
            order.Quantity = orderDto.Quantity.Value;
        }

        await _orderRepository.UpdateOrder(order);

        return new OrderResponseDto
        {
            Id = order.Id,
            UserId = order.UserId,
            ProductId = order.ProductId,
            Quantity = order.Quantity,
            User = order.User != null ? new UserResponseDto
            {
                Id = order.User.Id,
                Name = order.User.Name
            } : null,
            Product = order.Product != null ? new ProductResponseDto
            {
                Id = order.Product.Id,
                Title = order.Product.Title,
                Description = order.Product.Description,
                Price = order.Product.Price
            } : null
        };
    }

    public async Task<bool> DeleteOrder(int id)
    {
        var order = await _orderRepository.GetOrderById(id);
        if (order is null)
        {
            return false;
        }

        await _orderRepository.DeleteOrder(order);
        return true;
    }
}
