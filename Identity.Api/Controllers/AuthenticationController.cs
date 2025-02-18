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
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;

    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Authentication(LoginDTO dto)
    {
        var response = await _mediator.Send(new LoginRequest { LoginDTO = dto });

        if (response.IsSuccessful) // assuming there's an IsSuccess property
        {
            return Ok(response); // returns 200 OK with the response
        }

        return BadRequest(response); // returns 400 BadRequest if the login fails
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> RefreshToken(string refreshToken)
    {
        var response = await _mediator.Send(new RefreshTokenRequest { RefreshToken = refreshToken });
        if (response.Data.IsSuccessful) // assuming there's an IsSuccess property
        {
            return Ok(response.Data); // returns 200 OK with the response
        }
        return BadRequest(response.Data);
    }
    
    
}