namespace UniTask_backend.DTO
{
    public class CreateTaskDTO
    {

        public string Description { get; set; }

        public Guid GroupId { get; set; }

        public string Username { get; set; }

        public TaskStatus Status { get; set; }
    }
}
