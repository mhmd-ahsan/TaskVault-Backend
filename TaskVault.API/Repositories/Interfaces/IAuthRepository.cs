using TaskVault.API.Dtos.AuthDtos;
using TaskVault.API.Helpers;

namespace TaskVault.API.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<HelperResponse> RegisterAsync(RegisterDto dto);
        Task<HelperResponse> LoginAsync(LoginDto dto);
    }
}
