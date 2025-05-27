using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniTask_backend.DTO;
using UniTask_backend.Entities;
using UniTask_backend.Persistence;
using UniTask_backend.Interfaces;

namespace UniTask_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            return Unauthorized(new ApiResponse<string>
            {
                Success = false,
                Errors = new List<string> { "User not authenticated" }
            });
        }

        if (!Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized(new ApiResponse<string>
            {
                Success = false,
                Errors = new List<string> { "Invalid user ID" }
            });
        }

        var user = await _userService.GetCurrentUser(userId);

        if (user == null)
        {
            return NotFound(new ApiResponse<string>
            {
                Success = false,
                Errors = new List<string> { "User not found" }
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            Data = new
            {
                id = user.Id,
                username = user.Username
            }
        });
    }
}