using Microsoft.AspNetCore.Mvc;
using ProductDemo.Models.Entities;
using ProductDemo.Services.Interfaces;

namespace ProductDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    // GET: /api/user
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    // GET: api/user/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        if(user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    //POST: api/user
    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        var createdUser = await _userService.AddUser(user);
        return Ok(createdUser);
    }
}