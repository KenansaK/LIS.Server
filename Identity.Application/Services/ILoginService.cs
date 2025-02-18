using Identity.Domain.Dtos;
using System.Threading.Tasks;
using Kernal.Helpers;
using SharedKernel;
using static Identity.Application.Services.LoginService;

namespace Identity.Application.Services;
public interface ILoginService
{
    Task<LoginResponse> LoginAsync(LoginDTO dto);
    Task<Result<RefreshTokenDto>>LoginUsingRefreshTokenAsync(string refreshToken);
}
