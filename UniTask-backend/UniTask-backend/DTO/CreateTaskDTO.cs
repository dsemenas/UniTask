namespace UniTask_backend.DTO
{
    public class CreateTaskDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        public TaskStatus Status { get; set; }
    }
}
