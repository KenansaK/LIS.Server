using Identity.Domain.Dtos;
using System.Threading.Tasks;
using static Identity.Application.Services.LoginService;

namespace Identity.Application.Services;
public interface ILoginService
{
    Task<LoginResponse> LoginAsync(LoginDTO dto);

}
