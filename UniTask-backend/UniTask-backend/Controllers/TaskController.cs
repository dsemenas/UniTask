using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniTask_backend.DTO;
using UniTask_backend.Interfaces;
using UniTask_backend.Services;

namespace UniTask_backend.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("create-task")]
        public async Task<IActionResult> AddTask([FromBody] CreateTaskDTO info)
        {
            try
            {
                var (success, errorMessage, taskId) = await _taskService.CreateTask(
                    info.Description, info.GroupId, info.Username, info.Status);

                if (!success)
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { errorMessage }
                    });

                return Created("", new ApiResponse<Guid>
                {
                    Success = true,
                    Data = taskId.Value
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

        [HttpGet("all-tasks/{groupId}")]
        public async Task<IActionResult> GetTasks(Guid groupId)
        {
            try
            {
                var result = await _taskService.GetTasks(groupId);

                if (!result.Success)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { result.ErrorMessage ?? "Unknown error." }
                    });
                }

                return Ok(new ApiResponse<List<GetAllTasksDTO>>
                {
                    Success = true,
                    Data = result.tasks
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

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            try
            {
                var result = await _taskService.DeleteTask(taskId);

                if (!result.Success)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { result.ErrorMessage ?? "Unknown error." }
                    });
                }

                return Ok(new ApiResponse<string>
                {
                    Success = true
                });
            }
            catch
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Errors = new List<string> { "Internal server error occurred." }
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditTask([FromBody] EditTaskDTO info)
        {
            try
            {
                var result = await _taskService.UpdateTask(
                    info.TaskId, info.NewDescription, info.NewUserId, info.NewStatus);

                if (!result.Success)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Success = false,
                        Errors = new List<string> { result.ErrorMessage ?? "Unknown error." }
                    });
                }

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Data = "Task updated successfully"
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
}
