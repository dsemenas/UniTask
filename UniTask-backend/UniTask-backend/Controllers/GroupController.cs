using Microsoft.AspNetCore.Mvc;
using UniTask_backend.DTO;
using UniTask_backend.Entities;
using UniTask_backend.Interfaces;
using UniTask_backend.Services;

namespace UniTask_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] GroupDTO group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            await _groupService.CreateGroup(group);
            return Ok();

        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] GroupUser groupUser)
        {
            if (groupUser == null)
            {
                return BadRequest();
            }

            await _groupService.AddUserToGroup(groupUser);
            return Ok();

        }

    }
}