using UniTask_backend.Entities;

namespace UniTask_backend.Interfaces;

public interface IUserService
{
    Task<User?> GetCurrentUser(Guid userId);
}