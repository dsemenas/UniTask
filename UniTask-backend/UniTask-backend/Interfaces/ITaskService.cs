namespace UniTask_backend.Interfaces
{
    public interface ITaskService
    {
        (bool Success, string? ErrorMessage, Guid? TaskId) CreateTask(string name, string description, Guid userId, TaskStatus status);

        (bool Success, string? ErrorMessage) AssignMemberToTask(Guid userId, Guid taskId);

        (bool Success, string? ErrorMessage) DeleteTask(Guid taskId);

        public (bool Success, string? ErrorMessage) UpdateTask(
        Guid taskId,
        string? newName = null,
        string? newDescription = null,
        Guid? newUserId = null,
        TaskStatus? newStatus = null);
    }
}
