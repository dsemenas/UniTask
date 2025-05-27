namespace UniTask_backend.Entities
{
    public class Task
    {
        public Task(string description, Guid groupId, Guid userId, TaskStatus status)
        {
            Description = description;
            GroupId = groupId;
            UserId = userId;
            Status = status;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; }

        public Guid GroupId { get; set; }

        public Group? Group { get; set; }

        public Guid UserId{ get; set; }

        public User? User { get; set; }

        public int Version { get; set; }

        public TaskStatus Status { get; set; }
    }
}
