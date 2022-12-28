using Entities.DTOs;

namespace HumanResourceAPI.Infrastructure
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserAuthenticationDto userAuth);

        Task<string> CreateToken();
    }
}
