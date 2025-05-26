using Microsoft.AspNetCore.Mvc;
using UniTask_backend.DTO;
using UniTask_backend.Interfaces;
using UniTask_backend.Services;

namespace UniTask_backend.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("create-task")]
        public IActionResult AddTask([FromBody] CreateTaskDTO info)
        {

            try
            {
                var (success, errorMessage, taskId) = _taskService.CreateTask(info.Name, info.Description, info.UserId, info.Status);

                if (!success)
                    return BadRequest(new ApiResponse<string>
                    { Success = false, Errors = new List<string> { errorMessage } });

                return Created("", new ApiResponse<Guid>
                { Success = true, Data = taskId.Value });
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
    }
}
