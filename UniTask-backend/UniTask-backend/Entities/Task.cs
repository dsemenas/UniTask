namespace UniTask_backend.Entities
{
    public class Task
    {
        public Task(string name, string description, Guid userId, TaskStatus status)
        {
            Name = name;
            Description = description;
            UserId = userId;
            Status = status;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid UserId{ get; set; }

        public User? User { get; set; }

        public int Version { get; set; }

        public TaskStatus Status { get; set; }
    }
}
