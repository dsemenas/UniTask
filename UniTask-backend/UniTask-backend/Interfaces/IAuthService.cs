using UniTask_backend.Entities;

namespace UniTask_backend.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string? ErrorMessage, Guid? UserId)> RegisterUser(string username, string password);
    Task<User?> AuthenticateUser(string username, string password);
}