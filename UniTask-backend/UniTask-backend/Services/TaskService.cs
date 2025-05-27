using Microsoft.EntityFrameworkCore;
using UniTask_backend.Entities;
using UniTask_backend.Interfaces;
using UniTask_backend.Persistence;

namespace UniTask_backend.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TaskService> _logger;

        public TaskService(AppDbContext context, ILogger<TaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(bool Success, string? ErrorMessage, Guid? TaskId)> CreateTask(
            string description, Guid groupId, string username, TaskStatus status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(description))
                    return (false, "Task name cannot be empty.", null);

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                    return (false, "User not found.", null);

                var newTask = new Entities.Task(description, groupId, user.Id, status);

                await _context.Tasks.AddAsync(newTask);
                await _context.SaveChangesAsync();

                return (true, null, newTask.Id);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to create task: " + innerMessage, null);
            }
        }

        public async Task<(bool Success, string? ErrorMessage, List<Entities.Task>? tasks)> GetTasks(Guid groupId)
        {
            try
            {
                var tasks = await _context.Tasks
                    .Where(t => t.GroupId == groupId)
                    .ToListAsync();

                return (true, null, tasks);
            }
            catch (Exception)
            {
                return (false, "An error occurred while retrieving tasks.", null);
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> AssignMemberToTask(Guid userId, Guid taskId)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(taskId);
                if (task == null)
                    return (false, "Task not found.");

                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return (false, "User not found.");

                if (task.UserId == userId)
                    return (false, "User is already assigned.");

                task.UserId = userId;
                await _context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to assign user: " + innerMessage);
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> DeleteTask(Guid taskId)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(taskId);
                if (task == null)
                    return (false, "Task not found.");

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to delete task: " + innerMessage);
            }
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateTask(
            Guid taskId,
            string? newDescription = null,
            Guid? newUserId = null,
            TaskStatus? newStatus = null)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(taskId);
                if (task == null)
                    return (false, "Task not found.");

                if (newDescription != null)
                    task.Description = newDescription;

                if (newStatus != null)
                    task.Status = newStatus.Value;

                if (newUserId.HasValue)
                {
                    var user = await _context.Users.FindAsync(newUserId.Value);
                    if (user == null)
                        return (false, "User not found.");

                    task.UserId = newUserId.Value;
                }

                await _context.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to update task: " + innerMessage);
            }
        }
    }
}
