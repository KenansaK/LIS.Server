using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Commands;
public class UpdateAddressCommand : ICommand
{
    public Address Address { get; set; }
}

public class UpdateAddressCommandHandler : ICommandHandler<UpdateAddressCommand>
{
    private readonly IRepository<Address> _address;
    public UpdateAddressCommandHandler(IRepository<Address> address)
    {
        _address = address;
    }

    public async Task Handle(UpdateAddressCommand command, CancellationToken cancellationToken = default)
    {
        await _address.UpdateAsync(command.Address);
    }
}
