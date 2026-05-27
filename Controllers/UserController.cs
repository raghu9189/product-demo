using Microsoft.AspNetCore.Mvc;
using ProductDemo.Models.DTOs;
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
    public async Task<IActionResult> CreateUser(CreateUserDto user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdUser = await _userService.AddUser(user);
        return Ok(createdUser);
    }

    // PATCH: api/user/1
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updatedUser = await _userService.UpdateUser(id, userDto);
        if (updatedUser is null)
        {
            return NotFound();
        }
        return Ok(updatedUser);
    }

    // DELETE: api/user/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userService.DeleteUser(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}