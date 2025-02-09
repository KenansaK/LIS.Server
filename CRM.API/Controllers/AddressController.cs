using CRM.Application.Requests;
using CRM.Domain.Models;
using Kernal.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Permission("ViewAddress")]

        public async Task<ActionResult> GetAllAddresses()
        {
            return await _mediator.Send(new GetAddressesRequest() { });
        }

        [HttpPost]
        [Permission("CreateAddress")]
        public async Task<ActionResult> CreateAddress([FromBody] AddressModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid Address data.");
            }
            return await _mediator.Send(new CreateAddressRequest { Model = model });
        }

        [HttpPut("{id}")]
        [Permission("EditAddress")]
        public async Task<ActionResult> UpdateAddress(int id, [FromBody] AddressModel model)
        {
            return await _mediator.Send(new UpdateAddressRequest { Id = id, Model = model });
        }

        [HttpGet("{id}")]
        [Permission("ViewAddress")]
        public async Task<ActionResult> GetAddressById(int id)
        {
            var result = await _mediator.Send(new GetAddressRequest() { Id = id });
            return result;
        }


        [HttpDelete("{id}")]
        [Permission("DeleteAddress")]
        public async Task<ActionResult> DeleteAddress(int id)
        {
            return await _mediator.Send(new DeleteAddressRequest { Id = id });
        }
    }

}
