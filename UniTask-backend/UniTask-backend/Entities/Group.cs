namespace UniTask_backend.Entities
{
    public class Group
    {
        public Group(string name, Guid ownerId)
        {
            Name = name;
            OwnerId = ownerId;
            Members = new List<GroupUser>();
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public List<GroupUser> Members { get; set; } = new();




    }
}
