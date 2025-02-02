using Kernal.Contracts;
using CRM.Domain.Entities;

namespace CRM.Application.Commands;
public class DeleteCustomerCommand : ICommand
{
    public Customer Customer { get; set; }
}
public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly IRepository<Customer> _customer;
    public DeleteCustomerCommandHandler(IRepository<Customer> customer)
    {
        _customer = customer;
    }

    public async Task Handle(DeleteCustomerCommand command, CancellationToken cancellationToken = default)
    {
        await _customer.SoftDelete(command.Customer);
    }
}
