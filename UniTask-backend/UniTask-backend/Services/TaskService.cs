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

        public (bool Success, string? ErrorMessage, Guid? TaskId) CreateTask(string name, string description, Guid userId, TaskStatus status)
        {
            _logger.LogInformation("New Task is being created: {TaskName}", name);
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return (false, "task name cannot be empty.", null);

                var newTask = new Entities.Task(name, description, userId, status);


                _context.Tasks.Add(newTask);
                _context.SaveChanges();

               
                return (true, null, newTask.Id);
               

            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError(ex, "Klaida kuriant užduotį: {Message}", innerMessage);
                return (false, "Unable to create task: " + innerMessage, null);
            }
        }

        public (bool Success, string? ErrorMessage) AssignMemberToTask(Guid userId, Guid taskId)
        {
            try
            {
 
                var task = _context.Tasks.Find(taskId);
                if (task == null)
                    return (false, "Task not found.");

                var user = _context.Users.Find(userId);
                if (user == null)
                    return (false, "User not found.");

                if (task.UserId == userId)
                    return (false, "User is already assigned");

                task.UserId = userId;

                // Išsaugome narystės įrašą
                _context.SaveChanges();

                return (true, null);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to assign user " + innerMessage);
            }
        }

        public (bool Success, string? ErrorMessage) DeleteTask(Guid taskId)
        {
            try
            {
                var task = _context.Tasks.Find(taskId);
                if (task == null)
                    return (false, "Task not found.");

                _context.Tasks.Remove(task);
                _context.SaveChanges();

                return (true, null);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to delete task: " + innerMessage);
            }
        }

        public (bool Success, string? ErrorMessage) UpdateTask(
        Guid taskId,
        string? newName = null,
        string? newDescription = null,
        Guid? newUserId = null,
        TaskStatus? newStatus = null)
        {

            try
            {
                var task = _context.Tasks.Find(taskId);
                if (task == null)
                    return (false, "Task not found.");

                if (newName != null)
                    task.Name = newName;

                if (newDescription != null)
                    task.Description = newDescription;

                if (newStatus != null)
                    task.Status = newStatus.Value;

                if (newUserId.HasValue)
                {
                    var user = _context.Users.Find(newUserId.Value);
                    if (user == null)
                        return (false, "User not found.");

                    task.UserId = newUserId.Value;
                }


                _context.SaveChanges();

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
