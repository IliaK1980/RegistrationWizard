using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Server.Dtos;
using RegistrationWizard.Server.Services;

namespace RegistrationWizard.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterUserDto user)
    {
        await userService.RegisterUser(user);
        return Ok();
    }
}