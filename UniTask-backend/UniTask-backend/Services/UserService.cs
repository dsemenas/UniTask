using UniTask_backend.Entities;
using UniTask_backend.Interfaces;
using UniTask_backend.Persistence;

namespace UniTask_backend.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User? GetCurrentUser(Guid userId)
    {
        return _context.Users.FirstOrDefault(u => u.Id == userId);
    }
}