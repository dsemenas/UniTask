using UniTask_backend.Entities;
using UniTask_backend.Interfaces;
using UniTask_backend.Persistence;
using Microsoft.EntityFrameworkCore;


namespace UniTask_backend.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetCurrentUser(Guid userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }
}