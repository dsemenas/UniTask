using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniTask_backend.DTO;
using UniTask_backend.Entities;
using UniTask_backend.Interfaces;
using UniTask_backend.Services;

namespace UniTask_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("create-group")]
        public async Task<IActionResult> AddGroup([FromBody] AddGroupDTO groupInfo)
        {
            try
            {
                var (success, errorMessage, groupId) = await _groupService.CreateGroup(groupInfo.Name, groupInfo.OwnerName);

                if (!success)
                    return BadRequest(new ApiResponse<string>
                    { Success = false, Errors = new List<string> { errorMessage } });

                return Created("", new ApiResponse<Guid>
                { Success = true, Data = groupId.Value }); // Returning newly created user id
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

        [HttpPost("add-members")]
        public async Task<IActionResult> AddMembers([FromBody] AddMembersToGroupRequest request)
        {
            var (success, error) =  await _groupService.AddMemberToGroup(request.Username, request.GroupId);

            if (!success)
                return BadRequest(new ApiResponse<string>
                { Success = false, Errors = new List<string> { error } });

            return Created("", new ApiResponse<Guid>
            { Success = true});
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetGroupsByUserId(Guid userId)
        {
            var (success, errorMessage, groups) = await _groupService.GetGroupsByUserId(userId);

            if (!success)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { errorMessage ?? "Unknown error" }
                });
            }

            return Ok(new ApiResponse<List<GroupDTO>>
            {
                Success = true,
                Data = groups
            });
        }

        [HttpGet("get-members/{groupId}")]
        public async Task<IActionResult> GetMembers(Guid groupId)
        {
            var (success, errorMessage, users) = await _groupService.GetMembers(groupId);

            if (!success)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { errorMessage ?? "Nepavyko gauti grupės narių." }
                });
            }
            
            return Ok(new ApiResponse<List<GetUsersDTO>>
            {
                Success = true,
                Data = users
            });
        }

    }
}
