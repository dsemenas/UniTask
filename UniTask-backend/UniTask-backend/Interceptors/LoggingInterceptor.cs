using Microsoft.EntityFrameworkCore.Diagnostics;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace UniTask_backend.Interceptors
{
    public class LoggingInterceptor : Castle.DynamicProxy.IInterceptor
    {
        private readonly ILogger<LoggingInterceptor> _logger;
        private readonly IUserContext _userContext; // turi pateikti vartotojo info, pvz. vardą, teises

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger, IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        public void Intercept(IInvocation invocation)
        {
            var userName = _userContext.UserName ?? "anonymous";

            var methodName = $"{invocation.Method.DeclaringType?.Name}.{invocation.Method.Name}";
            var time = DateTime.UtcNow;

            _logger.LogInformation("User {User} with roles {Roles} calls {Method} at {Time}",
                userName, methodName, time);

            // vykdomas originalus metodas
            invocation.Proceed();

            // Jei reikia, galima užfiksuoti ir rezultatą ar išimtį
        }
    }
}
