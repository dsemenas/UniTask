using UniTask_backend.Entities;

namespace UniTask_backend.Interfaces;

public interface IAuthService
{
    (bool Success, string? ErrorMessage, Guid? UserId) RegisterUser(string username, string password);
    User? AuthenticateUser(string username, string password);
}