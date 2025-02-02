using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Commands;
public class UpdateCustomerCommand : ICommand
{
    public Customer Customer { get; set; }
}
public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
{
    private readonly IRepository<Customer> _customer;
    public UpdateCustomerCommandHandler(IRepository<Customer> customer)
    {
        _customer = customer;
    }

    public async Task Handle(UpdateCustomerCommand command, CancellationToken cancellationToken = default)
    {
        await _customer.UpdateAsync(command.Customer);
    }
}
