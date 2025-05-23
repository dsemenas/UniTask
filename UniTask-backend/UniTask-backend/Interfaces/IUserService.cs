using UniTask_backend.Entities;

namespace UniTask_backend.Interfaces;

public interface IUserService
{
    User? GetCurrentUser(Guid userId);
}