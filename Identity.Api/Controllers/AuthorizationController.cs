using Identity.Application.Requests;
using Identity.Application.Requests.Auth;
using Identity.Application.Requests.Permissions;
using Identity.Domain.Dtos;
using Identity.Domain.Models;
using Identity.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthorizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;

    }
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login(LoginDTO dto)
    {
        var response = await _mediator.Send(new LoginRequest { LoginDTO = dto });

        if (response.IsSuccessful) // assuming there's an IsSuccess property
        {
            return Ok(response); // returns 200 OK with the response
        }

        return BadRequest(response); // returns 400 BadRequest if the login fails
    }
}