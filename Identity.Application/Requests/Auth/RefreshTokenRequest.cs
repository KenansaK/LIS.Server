using Identity.Application.Queries;
using Identity.Application.Services;
using Identity.Domain.Dtos;
using Identity.Domain.Entities;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace Identity.Application.Requests.Auth;

public class RefreshTokenRequest : IRequest<Result<RefreshTokenDto>>
{
    public string RefreshToken { get; set; }
}

public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest,Result<RefreshTokenDto>>
{
    private readonly ILoginService _loginService;

    public RefreshTokenRequestHandler(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public async Task<Result<RefreshTokenDto>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
      
        var loginResult = await _loginService.LoginUsingRefreshTokenAsync(request.RefreshToken);

        return loginResult;

    }
    
}

