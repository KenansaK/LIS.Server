using Identity.Application.Services;
using Identity.Domain.Dtos;
using MediatR;
using SharedKernel;
namespace Identity.Application.Requests.Auth;

public class LoginRequest : IRequest<LoginResponse>
{
    public LoginDTO LoginDTO { get; set; }
}
public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly ILoginService _loginService;

    public LoginRequestHandler(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var loginResult = await _loginService.LoginAsync(request.LoginDTO);


        return loginResult;
    }
}