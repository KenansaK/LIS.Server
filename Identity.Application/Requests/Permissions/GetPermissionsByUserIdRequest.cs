using Identity.Application.Queries;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Requests.Permissions
{

    public class GetPermissionsByUserIdRequest : IRequest<List<string>>
    {
        public long UserId { get; set; }
    }

    public class GetPermissionsByUserIdRequestHandler(Dispatcher _dispatcher) : IRequestHandler<GetPermissionsByUserIdRequest, List<string>>
    {
        public async Task<List<string>> Handle(GetPermissionsByUserIdRequest request, CancellationToken cancellationToken)
        {
            var permissions = await _dispatcher.DispatchAsync(new GetPermissionsByUserIdQuery { UserId = request.UserId }, cancellationToken);

            if (permissions.Any())
            {
                return permissions;
            }
            return null;
        }
    }
}
