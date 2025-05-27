namespace UniTask_backend.DTO
{
    public class EditTaskDTO
    {
        public Guid TaskId { get; set; }

        public string? NewDescription { get; set; }

        public Guid? NewUserId { get; set; }

        public TaskStatus? NewStatus { get; set; }
    }
}