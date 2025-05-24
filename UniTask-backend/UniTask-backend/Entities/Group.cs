namespace UniTask_backend.Entities
{
    public class Group
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public User Owner { get; set; }
        public string GroupName { get; set; }
    }
}
