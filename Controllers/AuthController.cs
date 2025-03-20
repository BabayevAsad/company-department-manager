using Company_Expense_Tracker.Dtos.UserDtos;
using Company_Expense_Tracker.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace Company_Expense_Tracker.Controllers;

[ApiController]
[Route("api/[controller]")]
    
public class AuthController : Controller
{
    private readonly IUserService _service;

    public AuthController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDto userDto)
    {
        var token = await _service.RegisterUser(userDto);
        return Ok(token);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserDto userDto)
    {
        var token = await _service.LoginUser(userDto);
        return Ok(token);
    }
}