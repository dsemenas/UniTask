using System.Xml.Linq;
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

        public (bool Success, string? ErrorMessage, Guid? TaskId) CreateTask( string description,Guid groupId, string username, TaskStatus status)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(description))
                    return (false, "task name cannot be empty.", null);


                var user = _context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    return (false, "User not found", null);
                }
                var newTask = new Entities.Task(description, groupId, user.Id, status);


                _context.Tasks.Add(newTask);
                _context.SaveChanges();

               
                return (true, null, newTask.Id);
               

            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return (false, "Unable to create task: " + innerMessage, null);
            }


        }


        public (bool Success, string? ErrorMessage, List<Entities.Task>? tasks) getTasks(Guid groupId)
        {
            try
            {
                

                var tasks = _context.Tasks
                    .Where(t => t.GroupId == groupId)
                    .ToList();

                return (true, null, tasks);
            }
            catch (Exception ex)
            {
                // Log the error if needed
                return (false, "An error occurred while retrieving tasks.", null);
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
        string? newDescription = null,
        Guid? newUserId = null,
        TaskStatus? newStatus = null)
        {

            try
            {
                var task = _context.Tasks.Find(taskId);
                if (task == null)
                    return (false, "Task not found.");

                //_context.Entry(task).Property("Version").OriginalValue = rowVersion;


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
