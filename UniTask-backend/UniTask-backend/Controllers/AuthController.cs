using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UniTask_backend.DTO;
using UniTask_backend.Entities;
using UniTask_backend.Interfaces;
using UniTask_backend.Services;
using UniTask_backend.Utils;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthService> _logger;
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;

    public AuthController(IAuthService authService, IConfiguration config, ILogger<AuthService> logger)
    {
        _authService = authService;
        _config = config;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO userInfo)
    {
        try
        {
            var (success, errorMessage, userId) = await _authService.RegisterUser(userInfo.Username, userInfo.Password);

            if (!success)
                return BadRequest(new ApiResponse<string> 
                    { Success = false, Errors = new List<string> { errorMessage } });
            
            return Created("", new ApiResponse<Guid> 
                    { Success = true, Data = userId.Value }); // Returning newly created user id
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Errors = new List<string> { "Internal server error occurred." }
            });
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDto)
    {
        try
        {
            var user = await _authService.AuthenticateUser(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                return Unauthorized(new ApiResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Neteisingi prisijungimo duomenys. Bandykite dar kartą..." }
                });
            }

            var token = JwtTokenHelper.GenerateToken(user, _config);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Data = token
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new ApiResponse<string>
            {
                Success = false,
                Errors = new List<string> { "Internal server error occurred." }
            });
        }
    }

    
}