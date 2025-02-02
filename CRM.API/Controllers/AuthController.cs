using CRM.Application.Commands;
using CRM.Application.Requests.Access;
using CRM.Domain.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
    {
        var command = new SignUpRequest { SignUpDTO = signUpDTO };
        var user = await _mediator.Send(command);
        return Ok(user);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var command = new LoginRequest { LoginDTO = loginDTO };
        var token = await _mediator.Send(command);
        return Ok(new { token });
    }
}

