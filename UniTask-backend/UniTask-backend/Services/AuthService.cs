using Microsoft.AspNetCore.Identity;
using UniTask_backend.Entities;
using UniTask_backend.Interfaces;
using UniTask_backend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace UniTask_backend.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(AppDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<(bool Success, string? ErrorMessage, Guid? UserId)> RegisterUser(string username, string password)
    {
        // Username validation: atleast 4 symbols
        if (string.IsNullOrWhiteSpace(username) || username.Length < 4)
            return (false, "Username must be at least 4 characters long.", null);

        // Password validation: atleast 8 symbols
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            return (false, "Password must be at least 8 characters long.", null);
        
        // Password validation: should contain letters and digits
        if (!password.Any(char.IsLetter) || !password.Any(char.IsDigit))
            return (false, "Password must contain at least one letter and one digit.", null);

        var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        if (existingUser != null)
            return (false, "User already exists.", null);

        var hashedPassword = _passwordHasher.HashPassword(null, password);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = hashedPassword
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return (true, null, user.Id);
    }

    public async Task<User?> AuthenticateUser(string username, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        if (user == null)
            return null;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success ? user : null;
    }
}