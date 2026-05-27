using Microsoft.AspNetCore.Mvc;
using ProductDemo.Models.DTOs;
using ProductDemo.Services.Interfaces;

namespace ProductDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET: /api/order
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }

    // GET: /api/order/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order is null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    // POST: /api/order
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdOrder = await _orderService.AddOrder(orderDto);
        if (createdOrder is null)
        {
            return BadRequest("Invalid UserId or ProductId. Referenced user or product does not exist.");
        }

        return Ok(createdOrder);
    }

    // PATCH: /api/order/1
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedOrder = await _orderService.UpdateOrder(id, orderDto);
        if (updatedOrder is null)
        {
            return NotFound("Order not found, or referenced UserId/ProductId does not exist.");
        }

        return Ok(updatedOrder);
    }

    // DELETE: /api/order/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var success = await _orderService.DeleteOrder(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}
