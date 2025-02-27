using CRM.Application.Requests;
using Identity.Application.Requests;
using Identity.Domain.Models;
using Kernal.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/users
    [HttpGet]
    [AuthorizePermission("ViewUser")]
    public async Task<ActionResult> GetAllUsers()
    {
        return await _mediator.Send(new GetUsersRequest());
    }

    [HttpPost("GetPaginatedUsers")]
    [AuthorizePermission("ViewUser")]
    public async Task<ActionResult> GetPaginatedUsers([FromBody] GetPaginatedUsersRequest request)
    {
        if (request.PageIndex < 1 || request.PageSize < 1)
        {
            return BadRequest("PageIndex and PageSize must be greater than 0.");
        }
        return await _mediator.Send(request);
    }

    // POST: api/users
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateUser([FromBody] UserModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid user data.");
        }
        return await _mediator.Send(new CreateUserRequest { Model = model });
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    [AuthorizePermission("EditUser")]
    public async Task<ActionResult> UpdateUser(long id, [FromBody] UserModel model)
    {
        return await _mediator.Send(new UpdateUserRequest { Id = id, Model = model });
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    [AuthorizePermission("ViewUser")]
    public async Task<ActionResult> GetUserById(long id)
    {
        var result = await _mediator.Send(new GetUserRequest { Id = id });
        return result;
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    [AuthorizePermission("DeleteUser")]
    public async Task<ActionResult> DeleteUser(long id)
    {
        return await _mediator.Send(new DeleteUserRequest { Id = id });
    }
}
