using CRM.Domain.Entities;
using Kernal.Contracts;


namespace CRM.Application.Commands;

public class DeleteAddressCommand : ICommand
{
    public Address Address { get; set; }
}

public class DeleteAddressCommandHandler : ICommandHandler<DeleteAddressCommand>
{
    private readonly IRepository<Address> _address;
    public DeleteAddressCommandHandler(IRepository<Address> address)
    {
        _address = address;
    }

    public async Task Handle(DeleteAddressCommand command, CancellationToken cancellationToken = default)
    {
        await _address.DeleteAsync(command.Address);
    }
}

