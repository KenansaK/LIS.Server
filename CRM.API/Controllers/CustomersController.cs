using CRM.Application.Queries.Customers;
using CRM.Application.Requests;
using CRM.Application.Requests.Branches;
using CRM.Application.Requests.Customers;
using CRM.Domain.Models;
using Kernal.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Permissions;

namespace CRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]

public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    //[Permission("every")]
    public async Task<ActionResult> GetAllCustomers()
    {
        return await _mediator.Send(new GetCustomersRequest() { });
    }
    [HttpPost("GetPaginatedCustomers")]
    public async Task<IActionResult> GetPaginatedCustomers([FromBody] GetPaginatedCustomersQuery query)
    {
        if (query.PageIndex < 1 || query.PageSize < 1)
        {
            return BadRequest("PageIndex and PageSize must be greater than 0.");
        }

        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [HttpPost]
    [Permission("every")]
    public async Task<ActionResult> CreateCustomer([FromBody] CustomerModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid customer data.");
        }
        return await _mediator.Send(new CreateCustomerRequest { Model = model });

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer(long id, [FromBody] CustomerModel model)
    {
        return await _mediator.Send(new UpdateCustomerRequest { Id = id, Model = model });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCustomerById(long id)
    {
        var result = await _mediator.Send(new GetCustomerRequest() { Id = id });
        return result;
    }

    [HttpGet("GetStatusById/{id}")]
    public async Task<ActionResult> GetStatusById(long id)
    {
        var result = await _mediator.Send(new GetCustomerRequest() { Id = id });
        return result == null ? Result.NotFound() : Result.Success(result.Data.Status.ToString());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        return await _mediator.Send(new DeleteCustomerRequest { Id = id });
    }

    [HttpPost("ChangeStatusOrders")]
    public async Task<ActionResult> ChangeStatusForCustomer([FromBody] ChangeStatusCustomerModel model)
    {
        return await _mediator.Send(new ChangeStatusCustomerRequest { Model = model });
    }

    // Get branches by Customer ID
    [HttpGet("{id}/branches")]  // Descriptive route
    public async Task<ActionResult> GetBranchesByCustomerId(long id)
    {
        var result = await _mediator.Send(new GetBranchByCustomerIdRequest() { Id = id });
        return result;
    }
}

