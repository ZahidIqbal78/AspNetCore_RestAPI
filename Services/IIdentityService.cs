using AspNetCore_RestAPI.DTOs.V1;

namespace AspNetCore_RestAPI.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResponse> RegisterUserAsync(string email, string password);
    }
}