using Kernal.Contracts;
using CRM.Domain.Entities;

namespace CRM.Application.Commands;
public class AddAddressCommand : ICommand
{
    public Address Address { get; set; }
}

public class AddAddressCommandHandler : ICommandHandler<AddAddressCommand>
{
    private readonly IRepository<Address> _address;
    public AddAddressCommandHandler(IRepository<Address> address)
    {
        _address = address;
    }

    public async Task Handle(AddAddressCommand command, CancellationToken cancellationToken = default)
    {
        await _address.AddAsync(command.Address);
    }
}
