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
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [Permission("ViewCustomer")]
    public async Task<ActionResult> GetAllCustomers()
    {
        return await _mediator.Send(new GetCustomersRequest() { });
    }


    [HttpPost("GetPaginatedCustomers")]
    [Permission("ViewCustomer")]
    public async Task<ActionResult> GetPaginatedCustomers([FromBody] GetPaginatedCustomersRequest request)
    {
        if (request.PageIndex < 1 || request.PageSize < 1)
        {
            return BadRequest("PageIndex and PageSize must be greater than 0.");
        }
        return await _mediator.Send(request);
    }


    [HttpPost]
    [Permission("CreateCustomer")]
    public async Task<ActionResult> CreateCustomer([FromBody] CustomerModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid customer data.");
        }
        return await _mediator.Send(new CreateCustomerRequest { Model = model });

    }

    [HttpPut("{id}")]
    [Permission("EditCustomer")]
    public async Task<ActionResult> UpdateCustomer(long id, [FromBody] CustomerModel model)
    {
        return await _mediator.Send(new UpdateCustomerRequest { Id = id, Model = model });
    }

    [HttpGet("{id}")]
    [Permission("ViewCustomer")]
    public async Task<ActionResult> GetCustomerById(long id)
    {
        var result = await _mediator.Send(new GetCustomerRequest() { Id = id });
        return result;
    }

    //[HttpGet("GetStatusById/{id}")]
    //public async Task<ActionResult> GetStatusById(long id)
    //{
    //    var result = await _mediator.Send(new GetCustomerRequest() { Id = id });
    //    return result == null ? Result.NotFound() : Result.Success(result.Data.Status.ToString());
    //}

    [HttpDelete("{id}")]
    [Permission("DeleteCustomer")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        return await _mediator.Send(new DeleteCustomerRequest { Id = id });
    }

    //[HttpPost("ChangeStatusOrders")]
    //public async Task<ActionResult> ChangeStatusForCustomer([FromBody] ChangeStatusCustomerModel model)
    //{
    //    return await _mediator.Send(new ChangeStatusCustomerRequest { Model = model });
    //}

    // Get branches by Customer ID
    [HttpGet("{id}/branches")]  // Descriptive route
    [Permission("ViewBranch")]
    public async Task<ActionResult> GetBranchesByCustomerId(long id)
    {
        var result = await _mediator.Send(new GetBranchByCustomerIdRequest() { Id = id });
        return result;
    }
}

