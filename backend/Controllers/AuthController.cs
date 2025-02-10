using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Models;
using backend.Services;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly UserRepository _userRepository;

    public AuthController(AuthService authService, UserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        var token = _authService.Register(user);
        if (token == null) return BadRequest(new { message = "Email já cadastrado." });

        return Ok(new { token });
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var token = _authService.Login(loginRequest.Email, loginRequest.Password);
        if (token == null)
            return Unauthorized(new { message = "Invalid email or password." });

        return Ok(new { token });
    }
    
    [HttpGet("user")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public IActionResult GetUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized(new { message = "Invalid token." });

        var user = _userRepository.GetById(int.Parse(userId));
        if (user == null)
            return NotFound(new { message = "User not found." });

        return Ok(new
        {
            id = user.Id,
            name = user.Name,
            email = user.Email
        });
    }
}