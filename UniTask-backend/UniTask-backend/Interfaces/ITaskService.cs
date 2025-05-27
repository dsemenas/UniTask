using UniTask_backend.DTO;

namespace UniTask_backend.Interfaces
{
    public interface ITaskService
    {
        Task<(bool Success, string? ErrorMessage, Guid? TaskId)> CreateTask(string description, Guid groupId, string username, TaskStatus status);

        Task<(bool Success, string? ErrorMessage, List<GetAllTasksDTO>? tasks)> GetTasks(Guid groupId);

        Task<(bool Success, string? ErrorMessage)> AssignMemberToTask(Guid userId, Guid taskId);

        Task<(bool Success, string? ErrorMessage)> DeleteTask(Guid taskId);

        Task<(bool Success, string? ErrorMessage)> UpdateTask(
        Guid taskId,
        string? newDescription = null,
        Guid? newUserId = null,
        TaskStatus? newStatus = null);
    }
}
