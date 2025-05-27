using System.Security.Claims;

namespace UniTask_backend.Interceptors
{
    public interface IUserContext
    {
        string? UserName { get; }
    }

    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name;

    }
}
