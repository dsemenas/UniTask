namespace UniTask_backend.Interfaces
{
    public interface ITaskService
    {
        (bool Success, string? ErrorMessage, Guid? TaskId) CreateTask(string description, Guid groupId, string username, TaskStatus status);

        (bool Success, string? ErrorMessage, List<Entities.Task>? tasks) getTasks(Guid groupId);

        (bool Success, string? ErrorMessage) AssignMemberToTask(Guid userId, Guid taskId);

        (bool Success, string? ErrorMessage) DeleteTask(Guid taskId);

        (bool Success, string? ErrorMessage) UpdateTask(
        Guid taskId,
        string? newDescription = null,
        Guid? newUserId = null,
        TaskStatus? newStatus = null);
    }
}
