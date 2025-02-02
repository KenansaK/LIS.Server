using Kernal.Contracts;
using CRM.Domain.Entities;

namespace CRM.Application.Commands;
public class AddCustomerCommand : ICommand
{
    public Customer Customer { get; set; }
}
public class AddCustomerCommandHandler : ICommandHandler<AddCustomerCommand>
{
    private readonly IRepository<Customer> _customer;
    public AddCustomerCommandHandler(IRepository<Customer> customer)
    {
        _customer = customer;
    }

    public async Task Handle(AddCustomerCommand command, CancellationToken cancellationToken = default)
    {
        await _customer.AddAsync(command.Customer);
    }
}
