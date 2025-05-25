namespace UniTask_backend.Entities
{
    public class GroupUser
    {
        public GroupUser(Guid userId, Guid groupId)
        {
            UserId = userId;
            GroupId = groupId;
        }
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
